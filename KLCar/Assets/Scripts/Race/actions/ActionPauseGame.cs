using UnityEngine;
using System.Collections;
/// <summary>
/// 暂停或继续游戏.
/// </summary>
public class ActionPauseGame : ActionBase {

	public enum Type
	{
		toggle,pause,resume
	}
	public Type type;
	/// <summary>
	/// 此属性只是起提示作用.不能修改.
	/// </summary>
	public bool pause;

	protected override void onStart ()
	{
		switch (type)
		{
		case Type.resume:
			Time.timeScale=1;
			pause=false;
			break;
		case Type.pause:
			Time.timeScale = 0;
			pause=true;
			break;
		case Type.toggle:
			if(Time.timeScale==0)
			{
				Time.timeScale=1;
				pause=false;
			}else
			{
				Time.timeScale = 0;
				pause=true;
			}
			break;
		}
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
}
