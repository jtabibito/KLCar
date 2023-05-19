using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 自动驾驶的车的AI.
/// </summary>
public class CarAutoDriver : MonoBehaviour
{
	/// <summary>
	/// AI车辆速度作弊的距离.
	/// </summary>
	public int jiashuZuoBiLength=15;
	public int jianshuZuoBiLength=10;
	public float zuoBiValue=0.2f;
	/// <summary>
	/// 两个作弊距离的长度随机范围.
	/// </summary>
	public float zuoBiLengthPro = 0.3f;
	private CarEngine car;
	private List<CarEngine> cars;
	private float duration;
	public float IQDelay = 1f;
	private float isActive=0;
	private bool isOut=false;
	public float Wayport_Offset = 10;
	public float Horz_Offset = 3.5f;
	private CarEngine userCar;
	private float defaultMaxValue=0;
	private int maxWaypintsNumber=0;
	private bool isUseZuobi=false;
	/// <summary>
	/// 与玩家的位置差.大于0表示玩家在前面.自己在后面.
	/// </summary>
	private float userPosOffset;
	/// <summary>
	/// 上一次改变跟玩家之间位置关系的时间.
	/// </summary>
	private float lastChangePosOffsetTime;
	/// <summary>
	/// 释放技能的间隔时间.0.5~1倍之间.
	/// </summary>
	public float playSkillDelay=30;//30;
	void Awake ()
	{
		car = transform.root.FindChild ("Engine").GetComponent<CarEngine> ();
		cars = RaceManager.Instance.getAllCarList ();
		userCar = RaceManager.Instance.userCar;
		maxWaypintsNumber = RaceManager.Instance.wayPointNumber;
		jiashuZuoBiLength = (int)(jiashuZuoBiLength*MathUtils.getFloatBetween (1 - zuoBiLengthPro, 1 + zuoBiLengthPro));
		jianshuZuoBiLength= (int)(jianshuZuoBiLength*MathUtils.getFloatBetween (1 - zuoBiLengthPro, 1 + zuoBiLengthPro));
	}
	public bool isInUserBack 
	{
		get
		{
			return userPosOffset > 0;
		}
	}
	void Start ()
	{
		defaultMaxValue = car.maxSpeed;
		lastValidatePlaySkillTime= MathUtils.getFloatBetween(playSkillDelay*0.2f,playSkillDelay*1.2f);
	}
	
	void Update ()
	{
		if (RaceManager.Instance.isRaceOver||!car.IsRun)
		{
			return;
		}
		if (isActive!=0)
		{
			doValidate ();
		} else
		{
			duration += Time.deltaTime;
//			if (duration >= IQDelay)
//			{
				doValidate ();
				duration = 0;
//			}
		}
		//后面开始验证作弊.
		int a=car.currentWaypoint;
		int b = userCar.currentWaypoint;
		float off= MathUtils.getRoundDiff (a, b, maxWaypintsNumber);
		if (Mathf.Sign(lastChangePosOffsetTime)!=Mathf.Sign(off))
		{
			lastChangePosOffsetTime=Time.time;
		}
		userPosOffset = off;
		if (off > 0) 
		{//玩家在我前面
			if(off>=jiashuZuoBiLength&&!isUseZuobi)
			{
				isUseZuobi=true;
				car.maxSpeed =defaultMaxValue+zuoBiValue*defaultMaxValue;
			}else if(isUseZuobi&&off<jiashuZuoBiLength)
			{
				isUseZuobi=false;
				car.maxSpeed=defaultMaxValue;
			}
		} else
		{//玩家在我后面.
			if(Mathf.Abs(off)>=jianshuZuoBiLength&&!isUseZuobi)
			{
				isUseZuobi=true;
				car.maxSpeed =defaultMaxValue-zuoBiValue*defaultMaxValue;
			}else if(isUseZuobi&&Mathf.Abs(off)<jianshuZuoBiLength)
			{
				isUseZuobi=false;
				car.maxSpeed=defaultMaxValue;
			}
		}
		validatePlaySkill ();
	}
	/// <summary>
	/// 下一次需要开始检查释放技能的时间.
	/// </summary>
	private float lastValidatePlaySkillTime;
	/// <summary>
	/// 准备释放技能.
	/// </summary>
	void validatePlaySkill()
	{
		lastValidatePlaySkillTime -= Time.deltaTime;
		if (lastValidatePlaySkillTime <=0)//每秒钟进行一次判定.
		{
			lastValidatePlaySkillTime =MathUtils.getFloatBetween( 0.5f*playSkillDelay,playSkillDelay);
		} else
		{
			return;
		}
 		if (isInUserBack)
// 		if (isInUserBack && isLastChangePosTimeOver (10))
		{
			SkillManager.instance.playJiaSu (car);
		} else if (!isInUserBack)
		{
			findAttackTarget();
		}
	}
	/// <summary>
	/// 查找攻击目标.
	/// </summary>
	void findAttackTarget()
	{

		List<CarEngine> list = RaceManager.Instance.allRaceCars;
		int n = list.Count;
		int my = car.currentWaypoint;
		int all = RaceManager.Instance.wayPointNumber;
		float pro = 0;
		CarEngine target = null;
		for (int i=0; i<n; i++)
		{
			CarEngine e= list[i];
			if(e==car||e==userCar)
			{//忽略玩家
				continue;
			}
			int o=e.currentWaypoint;
			float off=MathUtils.getRoundDiff(my,o,all);
			if(off>3&&off<50)//3到50个路点之间的车才攻击.
			{
				if(pro<off)
				{
					pro=off;
					target=e;
				}
			}
		}
		if (target != null)
		{
			lockAttackTarget(target);
		}
	}
	private CarEngine attackTarget;
	private float lockTargetTime;
	/// <summary>
	/// 锁定攻击目标.
	/// </summary>
	/// <param name="car">Car.</param>
	void lockAttackTarget(CarEngine car)
	{
		stopRunAway ();
		attackTarget = car;
		lockTargetTime = Time.time;
//		SkillManager.instance.playFeiDan (this.car);
//		Debug.LogError ("攻击目标:>>>>"+car);
	}
	public bool isLastChangePosTimeOver(float duration)
	{
		return Time.time - lastChangePosOffsetTime >= duration;
	}
	void doValidate ()
	{
		if (!car.IsRun)
			return;
		if (attackTarget == null)
		{
			int wp = car.fowardWaypointIndex;
			float max = 0f;
			int total = RaceManager.Instance.wayPointNumber;
			CarEngine minCar = null;
			int offset = 0;
			for (int i=0; i<cars.Count; i++)
			{
				CarEngine o = cars [i];
				if (o == car)
				{
					continue;
				} else
				{
					float w = getPRI (wp, o.fowardWaypointIndex, total, o);
					if (w <= 0)
					{
						continue;
					} else
					{
						if (minCar == null || w > max)
						{
							minCar = o;
							max = w;
						} 
					}
				}
			}
			if (minCar != null)//有攻击目标,优先攻击.
			{
				turnOff (minCar);
			} else
			{
				isActive = 0;
				isOut = false;
				car.doTurnCar (0);
			}
		} else
		{
			stopRunAway();
			gotoAttack();
		}
	}
	void stopRunAway()
	{
		isActive=0;
		isOut=false;
	}
	void gotoAttack()
	{
		float my = car.horzOffsetFromWayPoint;
		float myI = car.currentWaypoint;
		float offset=attackTarget.horzOffsetFromWayPoint;
		float offsetI = attackTarget.currentWaypoint;
		if (Mathf.Abs (offset - my) <=0.3)
		{
			SkillManager.instance.playFeiDan (car);
			attackTarget = null;
		} else if (lockTargetTime + 4 < Time.time)
		{//4秒内没有移动到目标,则放弃攻击.
			attackTarget=null;
		}else
		{
			if(my>=offset)
			{//目标在左边.
				car.doTurnCar (-1);
			}else
			{
				car.doTurnCar (1);
			}
		}
	}
	/// <summary>
	/// 躲避某车.
	/// </summary>
	/// <param name="t">T.</param>
	void turnOff (CarEngine t)
	{
		if (!RaceManager.Instance.isInSafeRoadOffset (car.horzOffsetFromWayPoint,1))
		{
			car.doTurnCar (0);
			return;
		}
		if (isOut)
		{
			car.doTurnCar (isActive);
		} else
		{
			if (RaceManager.Instance.isInSafeRoadOffset (car.horzOffsetFromWayPoint, Horz_Offset*1.3f))
			{
				float r=(t.isFoward?t.horzOffsetFromWayPoint:-t.horzOffsetFromWayPoint);
//				float offset = car.horzOffsetFromWayPoint - ;
//				isActive=-Mathf.Sign (offset);
				if(r>car.horzOffsetFromWayPoint)
				{
					isActive=-1;
				}else{
					isActive=1;
				}
			} else
			{
				isActive=Mathf.Sign(-car.horzOffsetFromWayPoint);
			}
			isOut=true;
			car.doTurnCar (isActive);
		}
	}
	/// <summary>
	/// 获得优先级.
	/// </summary>
	/// <returns>The PR.</returns>
	/// <param name="s">S.</param>
	/// <param name="e">E.</param>
	/// <param name="total">Total.</param>
	/// <param name="t">T.</param>
	float getPRI (int s, int e, int total, CarEngine t)
	{
		float w = MathUtils.getRoundDiff (s, e, total);
		if (w < 1 || w > 15)
		{
			return 0;
		} else
		{
			float offset = Mathf.Abs (car.horzOffsetFromWayPoint  - (t.isFoward?t.horzOffsetFromWayPoint:-t.horzOffsetFromWayPoint));
			if (offset > Horz_Offset)
			{
				return 0;
			} else
			{
				float vPRI = 1 - w / Wayport_Offset;
				float hPRI = 1 - offset / Horz_Offset;
				return vPRI + hPRI;
			}
		}
	}
}