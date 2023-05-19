using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///创建一枚飞弹攻击指定目标.如果没有指定目标,则表示玩家.
/// </summary>
public class ActionFeiDanAttack : ActionBase {
	private CarEngine target;
	private GameObject zhunxin;
	public GameObject zhunXinPrefab;
	public GameObject feidanPrefab;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionFeiDanAttack sk = (ActionFeiDanAttack)cloneTo;
	}
	protected override void onStart ()
	{
		if (RaceManager.Instance.isRaceOver||zhunXinPrefab==null)
		{
			return;
		}
		if (runTarget == null)
		{
			target = RaceManager.Instance.userCar;
		} else
		{
			target=gameObject.GetComponent<CarEngine>();
		}
		if (target ==null||!isCarInBack ())
		{
			target=null;
			return;
		} else
		{
			zhunxin=(GameObject)GameObject.Instantiate(zhunXinPrefab);
			Vector3 last=zhunxin.transform.localPosition;
			Quaternion lastR=zhunxin.transform.localRotation;
			zhunxin.transform.parent=target.carBody;
			zhunxin.transform.localPosition=last;
			zhunxin.transform.localRotation=lastR;
		}
	}
	/// <summary>
	/// 判断是否有车载后面.
	/// </summary>
	/// <returns><c>true</c>, if car in back was ised, <c>false</c> otherwise.</returns>
	bool isCarInBack()
	{
		int total = RaceManager.Instance.wayPointNumber;
		int my = target.currentWaypoint;
		List<CarEngine> list= RaceManager.Instance.allRaceCars;
		foreach (CarEngine en in list)
		{
			if(en!=target)
			{
				float f=MathUtils.getRoundDiff(my,en.currentWaypoint,total);
				if(MathUtils.isInRange(f,-5,-50))
				{
					return true;
				}
			}
		}
		return false;
	}
	protected override void onOver ()
	{
		if (target == null)
		{
			return;
		}
		GameObject.DestroyObject (zhunxin);
		if (RaceManager.Instance.isRaceOver)
		{
			return;
		}
		if (feidanPrefab != null)
		{
			GameObject obj=(GameObject)GameObject.Instantiate(feidanPrefab);
			obj.transform.position=target.transform.position;
			obj.transform.rotation=target.transform.rotation;
			JianTou j=obj.GetComponent<JianTou>();
			if(j!=null)
			{
//				j.parent=carEngine.gameObject;
				j.startSpeed=100;
			}
		} else
		{
			Debug.LogError("ActionFeiDanAttack没有指定箭头对象.");
		}
	}
}
