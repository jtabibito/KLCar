using UnityEngine;
using System.Collections;
/// <summary>
/// 执行指定目标上的代码.执行此动作之后,目标上的脚本和当前动作后续的脚本会同步执行.
/// </summary>
public class ActionStopOtherAction : ActionBase {
	/// <summary>
	/// 如果是需要执行目标某个子节点上的代码.可以直接指定子节点的名词.
	/// </summary>
	public string childName;
	/// <summary>
	/// 如果不为null,表示执行指定info的action.
	/// </summary>
	public string actionInfo;
	/// <summary>
	/// 如果actionInfo为null,表示执行指定位置的action.从0开始.
	/// 如果是关闭功能.-1表示关闭所有脚本.否则表示从第几个脚本开始关闭.直到openNext=false
	/// </summary>
	public int index;
	protected override void onStart ()
	{
		base.onStart ();
//		if (time == 0)
//		{
//			doAction ();
//		} else
//		{
//			wantDo=true;
//		}
		doAction ();
	}
	 
	void doAction()
	{
		GameObject obj = gameObject;
		if (childName != null&&childName.Length!=0)
		{
			Transform t= obj.transform.FindChild(childName);
			if(t!=null)
			{
				obj=t.gameObject;
			}
		}
		if (actionInfo != null&&actionInfo.Length!=0)
		{
			ActionUtils.stopAction (gameObject, actionInfo);
		} else
		{
			ActionUtils.stopAction (gameObject, index);
		}
		 
	}
	protected override void onOver ()
	{
		base.onOver ();
	}
	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionStopOtherAction r = (ActionStopOtherAction)cloneTo;
		r.childName = childName;
		r.actionInfo = actionInfo;
		r.index = index;
	}
}
