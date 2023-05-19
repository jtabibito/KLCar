using UnityEngine;
using System.Collections;
/// <summary>
///控制车辆驾驶的动作.0表示开启这个功能.不会再恢复.否则时间到了之后会恢复运来的状态.
/// </summary>
public class ActionCarController : ActionBase {
	public enum SteerType
	{
		None=2,Left=-1,Right=1,Center=0,OtherSide=3
	}
	public enum ControllModel
	{
		None,Disable,Enable
	}
	/// <summary>
	/// 增加刹车系数.0表示没有踩刹车.1表示把刹车踩死.0.05慢慢刹车.
	/// </summary>
	public float addBrakeFactor;
	/// <summary>
	/// 控制车辆的转向. 
	/// </summary>
	public SteerType setSteerDirection=SteerType.None;
	/// <summary>
	/// 当前是否让玩家处于无法控制的状态.
	/// </summary>
	public ControllModel userControllModel;
	/// <summary>
	/// 是否使用漂移特效.
	/// </summary>
	public bool useKidmark;
	/// <summary>
	/// 刹车时,车身的方向.-1表示根据车的方向.
	/// </summary>
	public int brakeDirection=-1;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionCarController sk = (ActionCarController)cloneTo;
	}
	protected override void onStart ()
	{
		CarEngine car=gameObject.GetComponent <CarEngine>();
		if (car != null)
		{
			int d=0;
			if(addBrakeFactor!=0)
			{
				car.addCurBrakeFactor(addBrakeFactor);
			}
			if(setSteerDirection==SteerType.OtherSide)
			{
				d=MathUtils.sign0(-car.carInRoadSide);
				car.doTurnCar(d);
			}else if(setSteerDirection!=SteerType.None)
			{
				d=(int)setSteerDirection;
				car.doTurnCar(d);
			}
			if(userControllModel!=ControllModel.None)
			{
				Debug.LogError("还没有实现的方法!");
			}
			if(useKidmark)
			{
				car.autoEntryCurve=false;
				car.doShowBrakeEffect(brakeDirection<0?d:brakeDirection);
			}
		}

	}
	protected override void onOver ()
	{
		base.onOver ();
		if (time == 0)
		{
			return;
		}
		CarEngine car=gameObject.GetComponent <CarEngine>();
		if (car != null)
		{
			if(addBrakeFactor!=0)
			{
				car.addCurBrakeFactor(-addBrakeFactor);
			}
			if(setSteerDirection!=SteerType.None)
			{
				car.doTurnCar(0);
			}
			if(userControllModel!=ControllModel.None)
			{
				
			}
			if(useKidmark)
			{
				car.stopShowBrakeEffect(setSteerDirection==SteerType.None?0:(int)setSteerDirection);
				car.autoEntryCurve=true;
			}
		}
	}
}
