using UnityEngine;
using System.Collections;
/// <summary>
/// 代表车体的物理组成部分.(Engine子节点.)
/// </summary>
public class UserCarAgent : GameObjectAgent {

	public override GameObject getAgent (object requster)
	{
		return RaceManager.Instance.PlayerCar.gameObject.transform.FindChild("Engine").gameObject;
	}
}
