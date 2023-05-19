using UnityEngine;
using System.Collections;
/// <summary>
/// 代表游戏中汽车车体的对象.CarBody子节点.
/// </summary>
public class CarBodyAgent : GameObjectAgent {

	public override GameObject getAgent (object obj)
	{
		return RaceManager.Instance.PlayerCar.gameObject.transform.FindChild("CarBody").transform.gameObject;
	}
}
