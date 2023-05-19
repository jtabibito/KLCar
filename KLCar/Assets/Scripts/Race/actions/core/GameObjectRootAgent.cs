using UnityEngine;
using System.Collections;
/// <summary>
/// 代表车体的物理组成部分.(Engine子节点.)
/// </summary>
public class GameObjectRootAgent : GameObjectAgent {

	public override GameObject getAgent (object requster)
	{
		GameObject r=(GameObject)requster;
		return r.transform.root==null?null:r.transform.root.gameObject;
	}
}
