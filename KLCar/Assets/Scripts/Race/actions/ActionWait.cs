using UnityEngine;
using System.Collections;
/// <summary>
/// A等待指定长度的时间.什么都不做.如果指定了minTime和maxTime.则time自动取随机.
/// </summary>
public class ActionWait : ActionBase {

	public float minTime;
	public float maxTime;
	protected override void onStart ()
	{
		if (minTime == 0 && maxTime == 0)
		{
		} else
		{
//			time=Random.Range(minTime,maxTime);
			setWait(Random.Range(minTime,maxTime));
		}
		base.onStart ();
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
}
