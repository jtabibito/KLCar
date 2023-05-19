using UnityEngine;
using System.Collections;
/// <summary>
/// 将指定目标上的代码拷贝到自身来执行.
/// </summary>
public class ActionCopyRun : ActionBase {
	/// <summary>
	/// 提供脚本的目标.支持GameObjectAgent.
	/// </summary>
	public GameObject target;
	protected override void onStart ()
	{
		base.onStart ();
//		duration = -int.MaxValue;
		waitOver = false;
		openNext = false;
		ActionUtils.runAction (GameObjectAgent.GetAgentGameObject(gameObject,gameObject), target);
	}

	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionCopyRun r = (ActionCopyRun)cloneTo;
		r.target = target;
	}
}
