using UnityEngine;
using System.Collections;
/// <summary>
/// 比赛中,赛车的特殊状态.
/// </summary>
public class CarState {
	/// <summary>
	/// 无敌状态.可以把别人的车撞飞.
	/// </summary>
	public static int WuDi=0;
	/// <summary>
	/// 护盾状态.任何导弹无法攻击.
	/// </summary>
	public static int HuDun=1;
	/// <summary>
	/// 隐身状态.可以直接穿透车辆和导弹.
	/// </summary>
	public static int YingShen=2;
	/// <summary>
	/// 车辆处于禁止控制状态.
	/// </summary>
	public static int CantContol=3;
 	/// <summary>
 	/// 瞬间失去动力的状态.
 	/// </summary>
	public static int NoMotivePower=4;
	/// <summary>
	/// 忽略自动转向车体的功能.这个时候,车体不会转向行动方向.
	/// </summary>
	public static int IgnoreChangeRotation=5;

	/// <summary>
	/// 加速中的状态.
	/// </summary>
	public static int ShowState_AddSpeed=10;
	/// <summary>
	/// 漂移时的状态.
	/// </summary>
	public static int ShowState_Drift=11;

}
