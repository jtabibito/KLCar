using UnityEngine;
using System.Collections;
/// <summary>
/// 等待某个对象移动指定的额距离.但是可以指定最多等待多长时间.0表示无限等待.
/// A等待指定长度的时间.什么都不做.如果指定了minTime和maxTime.则time自动取随机.
/// </summary>
public class ActionWaitMove : ActionBase {
	/// <summary>
	/// 等待移动的直线距离.
	/// </summary>
	public float distance;
	private Vector3 lastPos;
	/// <summary>
	/// 是全局坐标还是局部坐标.
	/// </summary>
	public bool isWordPos;
	/// <summary>
	/// 是等待距离大于distance(false)还是等待距离小于distance(true)
	/// </summary>
	public bool isRunIn;
	/// <summary>
	/// 计算的距离是不是直线距离.
	/// </summary>
	public bool linearDistance;
	private float totalDistance;
	protected override void onStart ()
	{
		base.onStart ();
		if (time == 0)
		{
			this.duration=int.MinValue;
		}
		lastPos = isWordPos?gameObject.transform.position:gameObject.transform.localPosition;
		totalDistance = 0;
	}
	void Update()
	{
		Vector3 v = isWordPos?gameObject.transform.position:gameObject.transform.localPosition;
		if (isRunIn)
		{
			if(Vector3.Distance(v,lastPos)<distance)
			{
				duration=time;
			}
		} else
		{
			if (linearDistance)
			{
				if(Vector3.Distance(v,lastPos)>distance)
				{
					duration=time;
				}
			}else
			{
				totalDistance+=Vector3.Distance(lastPos,v);
				lastPos=v;
				if(totalDistance>=distance)
				{
					duration=time;
				}
			}
		}
	}
	internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
}
