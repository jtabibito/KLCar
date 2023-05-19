using UnityEngine;
using System.Collections;

/// <summary>
/// 在路点上创建各种对象.
/// </summary>
public class ActionCreateRoadObj :ActionBase
{

	public GameObject[] objPrefab = {};
	
	/// <summary>
	/// 创建的路点位置.
	/// </summary>
	public int wayPoint;
	/// <summary>
	/// T指定的路点是全局路点还是相对于的路点.
	/// </summary>
	public bool wayPointRelativeToUser = true;
	/// <summary>
	/// 道路中间的偏移.
	/// </summary>
	public float xOffset;
	public float maxXOffset;
	/// <summary>
	/// 是不是相对于玩家的车.
	/// </summary>
	public bool xOffsetRelativeToUser = false;
	/// <summary>
	/// 是不是创建的车距离玩家一定的路点后,自动删除.
	/// </summary>
	public bool autoRemove = true;
	internal static GameObject nextCreateObj;
 

	void Awake ()
	{

	}

	internal override void onCopyTo (ActionBase cloneTo)
	{

	}

	protected override void onStart ()
	{
		GameObject name = objPrefab [MathUtils.getIntBetween (0, objPrefab.Length - 1)];
		if (name == null)
		{
			return;
		}
		CarEngine car = RaceManager.Instance.PlayerCar.transform.FindChild ("Engine").GetComponent <CarEngine> ();
		int index = wayPoint;
		float offset = maxXOffset == 0 ? xOffset : Random.Range (xOffset, maxXOffset);
		if (wayPointRelativeToUser)
		{
			index += car.currentWaypoint;
		}
		if (xOffsetRelativeToUser)
		{
			offset = offset + car.xOffsetByWayPoint;
		}
		GameObject en = RaceManager.Instance.CreateObjInRoad (index, offset, name);
		if (autoRemove)
		{
			AutoRemoveObject ot=en.AddComponent<AutoRemoveObject> ();
			ot.index=index;
		}
		nextCreateObj = en;
	}
}
