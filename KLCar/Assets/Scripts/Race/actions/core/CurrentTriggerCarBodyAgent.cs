using UnityEngine;
using System.Collections;

/// <summary>
/// 代表当前进入触发器(或者说上一次进入触发器),触发特效功能的车体.
/// </summary>
public class CurrentTriggerCarBodyAgent : GameObjectAgent
{
	public override GameObject getAgent (object requster)
	{
		GameObject obj = (GameObject)requster;
		TriggerGameObject t = obj.GetComponent <TriggerGameObject> ();
		if (t != null)
		{
			GameObject g = t.lastTrggerObject ();
			if (g != null)
			{
				CarEngine c=g.GetComponent<CarEngine>();
				if(c!=null)
				{
					return c.carBody.gameObject;
				}
			}
			return null;
		} else
		{
			return null;
		}
	}
}
