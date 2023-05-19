using UnityEngine;
using System.Collections;
/// <summary>
///对话模式开关进入或退出对话模式.
/// </summary>
public class ActionChatModel : ActionBase {
 
	/// <summary>
	/// 是进入还是退出对话模式
	/// </summary>
	public bool isStart;
	internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
	protected override void onStart ()
	{
		if (isStart)
		{
			RaceManager.Instance.startChat ();
//			TweenUtils.CameraFadeTo(0.3f,1);
			Debug.Log("进入对话模式"+Time.time);
		} else
		{
			RaceManager.Instance.endChat();
			Debug.Log("退出对话模式"+Time.time);
//			TweenUtils.CameraFadeTo(0,1);
		}
	}
}
