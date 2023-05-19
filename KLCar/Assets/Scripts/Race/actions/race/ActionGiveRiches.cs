using UnityEngine;
using System.Collections;
/// <summary>
///给予车辆指定数值的财富.
/// 比如:金币.破坏分数.油.等.如果runTarget为null,表示玩家车辆.
/// </summary>
public class ActionGiveRiches : ActionBase {
	/// <summary>
	/// 给予金币.
	/// </summary>
	public int gold;
	/// <summary>
	/// 给予汽油.
	/// </summary>
	public float oil;
	/// <summary>
	/// 给予破坏积分.
	/// </summary>
	public int breakScore;
	internal override void onCopyTo (ActionBase cloneTo)
	{

	}
	protected override void onStart ()
	{
		CarEngine car;
		if (runTarget == null)
		{
			car = RaceManager.Instance.userCar;
		} else
		{
			car=gameObject.GetComponent <CarEngine>();
		}
		if (car != null) {
			car.addGlod(gold);
			car.addOil(oil);
			car.addBreakScore(breakScore);
		}
	}
}
