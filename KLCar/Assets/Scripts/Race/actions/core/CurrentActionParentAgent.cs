using UnityEngine;
using System.Collections;

/// <summary>
/// 当前执行脚本的对象的代理.
/// </summary>
public class CurrentActionParentAgent : GameObjectAgent
{
	public override GameObject getAgent (object requster)
	{
		GameObject obj = (GameObject)requster;
		return obj;
	}
}
