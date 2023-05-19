using UnityEngine;
using System.Collections;
/// <summary>
/// P代表游戏中的主角.可以用来播放动画.
/// </summary>
public class PlayerRoleAgent : GameObjectAgent {

	public override GameObject getAgent (object requster)
	{
		return RaceManager.Instance.PlayerCar.gameObject.transform.FindChild("CarBody/car/car_transform/car_role/role").transform.gameObject;
	}
}
