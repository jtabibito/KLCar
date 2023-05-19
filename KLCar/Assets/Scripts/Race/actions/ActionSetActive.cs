using UnityEngine;
using System.Collections;
/// <summary>
/// 设置对象的激活状态. time=0表示永久.
/// 让自己被禁用时,后续的代码将不会执行.有可能会出现错误.这个功能慎用.
/// </summary>
public class ActionSetActive : ActionBase {
	public bool isActive;
 	
	protected override void onStart ()
	{
		base.onStart ();
		gameObject.SetActive(isActive);
	}
	protected override void onOver ()
	{
		base.onOver ();
		 if (time != 0) {
			gameObject.SetActive(!isActive);
				}
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionSetActive s = (ActionSetActive)cloneTo;
		s.isActive = isActive;
	}
}
