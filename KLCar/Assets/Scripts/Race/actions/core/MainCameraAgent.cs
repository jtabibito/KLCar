using UnityEngine;
using System.Collections;
/// <summary>
/// 代表游戏中的主要摄像机.
/// </summary>
public class MainCameraAgent : GameObjectAgent {

	public override GameObject getAgent (object requster)
	{
		return RaceManager.Instance.CarCamera;
	}
}
