using UnityEngine;
using System.Collections;
/// <summary>
///给予碰撞车辆一定金币的效果.
/// </summary>
public class ActionPlaySkill : ActionBase {
	/// <summary>
	/// 给予的数量
	/// </summary>
	public GameObject skill;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionPlaySkill sk = (ActionPlaySkill)cloneTo;
		sk.skill = skill;
	}
	protected override void onStart ()
	{
		CarEngine car=gameObject.GetComponent <CarEngine>();
		car.doUseSkill (skill);
	}
}
