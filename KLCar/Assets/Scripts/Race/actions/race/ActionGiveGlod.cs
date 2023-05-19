using UnityEngine;
using System.Collections;
/// <summary>
///给予碰撞车辆一定金币的效果.
/// </summary>
public class ActionGiveGlod : ActionBase {
	/// <summary>
	/// 给予的数量
	/// </summary>
	public int giveNum;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
	protected override void onStart ()
	{
		CarEngine car=gameObject.GetComponent <CarEngine>();
		if (car != null) {
			car.addGlod(giveNum);
		}
	}
}
