using UnityEngine;
using System.Collections;

/// <summary>
/// 代表当前进入触发器(或者说上一次进入触发器),触发特效功能的对象.
/// </summary>
public class CurrentTriggerAgent : GameObjectAgent
{
		public override GameObject getAgent (object requster)
		{
				GameObject obj = (GameObject)requster;
				TriggerGameObject t = obj.GetComponent <TriggerGameObject> ();
				if (t != null) {
						return t.lastTrggerObject();
				} else {
						return null;
				}
		}
}
