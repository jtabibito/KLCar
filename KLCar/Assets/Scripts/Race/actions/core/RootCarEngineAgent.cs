using UnityEngine;
using System.Collections;

/// <summary>
/// 从车体内任何一个子节点获取CarEngine节点.
/// </summary>
public class RootCarEngineAgent : GameObjectAgent
{
	public override GameObject getAgent (object requster)
	{
		GameObject obj = (GameObject)requster;
		Transform t= obj.transform.root.FindChild ("Engine");
		if (t != null)
		{
			return t.gameObject;
		} else
		{
			return null;
		}
	}
}
