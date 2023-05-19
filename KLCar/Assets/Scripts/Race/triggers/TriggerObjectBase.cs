using UnityEngine;
using System.Collections;

/// <summary>
/// 可以触发的游戏对象.需要添加很多的触发条件.然后再添加需要自行的Action.需要执行调用onTrigger()函数.
/// </summary>
/// 
public class TriggerObjectBase : MonoBehaviour
{
	/// <summary>
	/// 触发成功的类型.
	/// </summary>
	public TriggerMatchType matchType;
	/// <summary>
	/// 要执行的动作名次.
	/// </summary>
	public string actionName;
	/// <summary>
	/// 上一次进入触发器的对象.
	/// </summary>
	private GameObject _lastTrggerObj;
	public enum TriggerMatchType
	{
		all,
		some//,one,two,three,four,five
	}
	/// <summary>
	/// 是不是触发后,不再触发.除非被其它代码打开.
	/// </summary>
	public bool disableOnTrggier;
	private ConditionBase[] all;

	void OnEnable ()
	{
		if (actionName == "")
		{
			actionName=null;
		}
		all = GetComponents <ConditionBase> ();
//		if (collider == null)
//		{
//			Debug.LogError ("触发器必须添加碰撞体.否则无法触发事件." + GameObjectUtils.getGameObjectPath (gameObject));
//		}
	}

	public GameObject lastTrggerObject ()
	{
		 
		return _lastTrggerObj;

	}

	protected bool onTrigger (GameObject obj)
	{
		if (!enabled)
			return false;
		int num = 0;
		foreach (ConditionBase b in all)
		{
			if (b.ignore)
			{
				continue;
			}
			if (b.isMatch (obj))
			{
				if (b.conditioType == ConditionBase.ConditionType.triggerNow)
				{
					num = 10000;
					break;
				} else
				{
					num++;
				}
			} else
			{
				if (matchType == TriggerMatchType.all || b.conditioType == ConditionBase.ConditionType.triggerLost)
				{
					return false;
				}
			}
		}
		doTrigger (obj);
		return true;
	}

	public void doTrigger (GameObject trigger)
	{
		_lastTrggerObj = trigger;
		ActionUtils.runAction (gameObject,actionName);
//		ActionBase b = GetComponent<ActionBase> ();
//		if (b != null)
//		{
//			b.enabled = true;		
//		}
		if (disableOnTrggier)
		{
			enabled = false;
		}
	}
}
