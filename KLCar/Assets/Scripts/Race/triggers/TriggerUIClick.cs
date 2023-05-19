using UnityEngine;
using System.Collections;

/// <summary>
/// 可以触发的游戏对象.需要添加很多的触发条件.然后再添加需要自行的Action.
/// </summary>
/// 
[RequireComponent(typeof(UIButton))]
public class TriggerUIClick : TriggerObjectBase
{
	public string sound;
	void Start()
	{
		UIButton ui= gameObject.GetComponent<UIButton> ();
		if (ui == null)
		{
			Debug.LogError("点击触发器必须添加在UIButtion上"+gameObject.name);
		} else
		{
			ui.onClick.Insert (0,new EventDelegate (this.onClick));
		}
	}
	void onClick()
	{
		if (sound == null || sound == "")
		{
			onTrigger (gameObject);
		} else
		{
			SoundManager.effect.play(sound);
		}
	}
}
