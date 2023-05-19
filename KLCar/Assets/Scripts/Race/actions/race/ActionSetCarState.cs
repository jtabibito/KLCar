using UnityEngine;
using System.Collections;
/// <summary>
///设置车的状态,只是一种状态,车辆的属性.不会有显示效果的变化.
/// </summary>
public class ActionSetCarState : ActionBase {
	///车的状态取值在CarState类中.
	///
	public enum CarStateName{
		/// <summary>
		/// 隐身状态,不参与碰撞.但是玩家可以看到.
		/// </summary>
		yingsheng=2,
		wudi=0
	}
	/// <summary>
	/// 要调整的状态名称.
	/// </summary>
	public CarStateName stateName;
	/// <summary>
	/// 是增加这个状态,还是消除这个状态.
	/// </summary>
	public bool isAdd=true;
	/// <summary>
	/// 是否在时间到后,还原到之前的状态.
	/// </summary>
	public bool revert=false;
	/// <summary>
	/// 给予的数量
	/// </summary>
	 internal override void onCopyTo (ActionBase cloneTo)
	{

	}
	protected override void onStart ()
	{
		base.onStart ();
		CarEngine car=gameObject.GetComponent <CarEngine>();
		if (car != null)
		{
			car.setCarState((int)stateName,isAdd);
		}
	}
	protected override void onOver ()
	{
		base.onOver ();
		if (revert)
		{
			CarEngine car=gameObject.GetComponent <CarEngine>();
			if (car != null)
			{
				car.setCarState((int)stateName,!isAdd);
			}
		}
	}
}
