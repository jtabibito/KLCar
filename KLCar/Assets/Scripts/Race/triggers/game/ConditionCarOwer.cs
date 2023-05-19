using UnityEngine;
using System.Collections;

/// <summary>
/// 跟车的玩家归属有关的判断.
/// </summary>
public class ConditionCarOwer : ConditionBase
{
	public enum CarOnwerType
	{
		/// <summary>
		/// T任何车.
		/// </summary>
		everyCar,
		/// <summary>
		/// T用户车
		/// </summary>
		userCar,
		/// <summary>
		/// 电脑控制的玩家车.
		/// </summary>
		aiCar,
		/// <summary>
		/// 非比赛车.
		/// </summary>
		otherCar,
		///除玩家的车之外的所有车.
		notUserCar
				
	}
	/// <summary>
	/// 车归属的类型.
	/// </summary>
	public CarOnwerType carType;
		
	public override bool isMatch (GameObject col)
	{
		CarEngine e = col.GetComponent <CarEngine> ();
		if (e == null)
		{
			return false;
		} else
		{
			switch (carType)
			{
			case CarOnwerType.otherCar:
				return e.carType==CarEngine.CarType.OtherCar;
			case CarOnwerType.userCar:
				return e.carType==CarEngine.CarType.UserCar;
			case CarOnwerType.aiCar:
				return e.carType==CarEngine.CarType.AICar;
			case CarOnwerType.everyCar:
				return true;
			case CarOnwerType.notUserCar:
				return e.carType!=CarEngine.CarType.UserCar;
			default:
				return false;
			}
		}
	}
}
