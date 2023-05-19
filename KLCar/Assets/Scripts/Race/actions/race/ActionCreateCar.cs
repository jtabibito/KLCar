using UnityEngine;
using System.Collections;
/// <summary>
/// 创建车的脚本.
/// </summary>
public class ActionCreateCar :ActionBase {
	public static string default_role = "RoleAvt1";
	public string[] carPrefab={"CarAvt3"};
	/// <summary>
	/// 如果不填,则隐藏人物.
	/// </summary>
	public string rolePrefab="RoleAvt1";
	public bool isAICar;
	/// <summary>
	/// T车的起始速度.负数表示逆向行驶.0表示停止.
	/// </summary>
	public float startSpeed;
	/// <summary>
	/// 最大速度.
	/// </summary>
	public float maxSpeed=120;
	/// <summary>
	/// 加速度.
	/// </summary>
	public float speedAddtion=500;
	/// <summary>
	/// 转弯速度.
	/// </summary>
	public float manualHorzSpeed=10;
	/// <summary>
	/// 创建的路点位置.
	/// </summary>
	public int wayPoint;
	/// <summary>
	/// T指定的路点是全局路点还是相对于的路点.
	/// </summary>
	public bool wayPointRelativeToUser=true;
	/// <summary>
	/// 道路中间的偏移.
	/// </summary>
	public float xOffset;
	public float maxXOffset;
	/// <summary>
	/// 是不是相对于玩家的车.
	/// </summary>
	public bool xOffsetRelativeToUser=false;
	/// <summary>
	/// 是不是创建的车距离玩家一定的路点后,自动删除.
	/// </summary>
	public bool autoRemove=true;
	internal static CarEngine nextCreateCar;
	/// <summary>
	/// 车辆的碰撞级别.基本越高,越容易撞飞别人.
	/// </summary>
	public int hitLevel;
	void Awake()
	{

	}
	internal override void onCopyTo (ActionBase cloneTo)
	{

	}
	protected override void onStart ()
	{
		string name=carPrefab[MathUtils.getIntBetween(0,carPrefab.Length-1)];
		if (name == null || name.Length == 0)
		{
			return;
		} else
		{
			CarEngine car=RaceManager.Instance.PlayerCar.transform.FindChild ("Engine").GetComponent <CarEngine>();
			int index = wayPoint;
			float offset =maxXOffset==0? xOffset:Random.Range(xOffset,maxXOffset);
			if (wayPointRelativeToUser)
			{
				index+=car.currentWaypoint;
			}
			if (xOffsetRelativeToUser)
			{
				offset=offset+car.xOffsetByWayPoint;
			}
			CarEngine en= RaceManager.Instance.CreateObstacleCar (index,offset, isAICar, startSpeed,name,rolePrefab==""?default_role:rolePrefab);
			if(rolePrefab=="")
			{
				//MeshRenderer[] rs=en.roleAvtConteoller.gameObject.GetComponentsInChildren<MeshRenderer>();
				//foreach(MeshRenderer r in rs)
				//{
					//r.enabled=false;
				//}
				en.roleAvtConteoller.gameObject.SetActive(false);
			}
			if(en==null)
			{
				return;
			}
			en.maxSpeed=maxSpeed;
			en.baseTorque=speedAddtion;
			en.manualHorzSpeed=manualHorzSpeed;
			en.carType=isAICar?CarEngine.CarType.AICar:CarEngine.CarType.OtherCar;
			en.hitLevel=hitLevel;

			if(startSpeed!=0)
			{
				en.setCurrentSpeed(Mathf.Abs(startSpeed));
			}
			if (autoRemove)
			{
				en.gameObject.AddComponent<AutoRemoveAI>();
			}
		}
	}
}
