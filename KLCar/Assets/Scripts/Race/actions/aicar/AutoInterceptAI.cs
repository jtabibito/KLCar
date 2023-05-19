using UnityEngine;
using System.Collections;

public class AutoInterceptAI : MonoBehaviour {
	/// <summary>
	/// 响应的路点数量.在这个范围内,车可能会去拦截.
	/// </summary>
	public int maxWayPointOffset=15;
	public int minWayPointOffset=7;
	private int activeWayPointOffset;
	/// <summary>
	/// 回去碰撞的概率.
	/// </summary>

	public float probability=1.0f;
	public int maxMoveOffset=6;
	private float minDelayTime=3;

	private CarEngine car;
	private CarEngine user;
	private float cdTime;
	private bool isInTrgger;
	private int turn=0;
	private float lastOffset=0;
	void Start () {
		Transform t= transform.root.FindChild ("Engine");
		if (t != null)
		{
			car=t.GetComponent<CarEngine>();
			if(car==null)
			{

			}
		}
		user = RaceManager.Instance.userCar;
		int carIndex=car.currentWaypoint;
		int userIndex = user.currentWaypoint;
		int offset = carIndex - userIndex;
	}
	
	void Update () {
		if (car == null)
		{
			return;
		}
		int carIndex=car.currentWaypoint;
		int userIndex = user.currentWaypoint;
		int offset = carIndex - userIndex;
		int num = RaceManager.Instance.wayPointNumber / 2;
		if (offset < -num)
		{
			offset += num;
		} else if (offset > num)
		{
			offset-=num;
		}
		if (isInTrgger)
		{
			if(offset<=minWayPointOffset)
			{
				isInTrgger=false;
				return;
			}else{
				if(turn!=0)
				{//靠近.
					if(Mathf.Abs(car.xOffsetByWayPoint-lastOffset)<=maxMoveOffset)
					{
						if(RaceManager.Instance.isInSafeRoadOffset(car.xOffsetByWayPoint,3))
						{
							if(offset<activeWayPointOffset)
							{
								car.doTurnCar(turn);
							}
							return;
						}
					}
				}
				car.doTurnCar(0);
			}
		} else
		{
			if(offset<=maxWayPointOffset)
			{
				isInTrgger=true;
				activeWayPointOffset=MathUtils.getIntBetween(minWayPointOffset,maxWayPointOffset);
				if(MathUtils.isInProbability(probability))
				{
					float xoff=user.xOffsetByWayPoint-car.xOffsetByWayPoint;
					turn= (int)Mathf.Sign(xoff);
					lastOffset=car.xOffsetByWayPoint;
				}else{
					turn=0;
					car.doTurnCar(0);
				}
			}
		}
	}
}
