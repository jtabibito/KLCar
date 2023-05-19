using UnityEngine;
using System.Collections;

/// <summary>
/// 可以触发的游戏对象.需要添加很多的触发条件.然后再添加需要自行的Action.
/// </summary>
/// 
[RequireComponent(typeof(Collider))]
public class TriggerGameObject : TriggerObjectBase
{
	void OnTriggerEnter (Collider c)
	{
		onTrigger (c.gameObject);
	}
}
