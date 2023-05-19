using UnityEngine;
using System.Collections;

public class TweenLite :MonoBehaviour  {
	public delegate void TweenLiteFunc (object target,float value,TweenLite tween);
	private float duration;
	public float progress;
	private object target;
	private TweenLiteFunc interval;
	private static GameObject baseObj;
	private TweenUtils.EasingFunction ease;
	public static TweenLite doTween(object target,float duration,TweenUtils.EasingFunction ease,TweenLiteFunc interval)
	{
		return newTweenLite (target,duration,ease,interval);
	}
	public static TweenLite  newTweenLite(object target,float duration,TweenUtils.EasingFunction ease,TweenLiteFunc interval)
	{
		if (baseObj == null)
		{
			baseObj=new GameObject("tweenLiteBase");
			DontDestroyOnLoad(baseObj);
		}
		TweenLite tl;
		if (target is GameObject)
		{
			tl=((GameObject)target).AddComponent<TweenLite>();
		} else
		{
			tl=baseObj.AddComponent <TweenLite >();
		}
		tl.target = target;
		tl.duration = duration;
		tl.interval = interval;
		tl.ease = ease;
		return tl;
	}

	void Update()
	{
		bool over = false;
		if (duration <= 0)
		{
			progress=1;
		} else
		{
			progress += Time.deltaTime / duration;
		}
		if (progress >= 1)
		{
			progress=1;
			over=true;
		} 
		float f=ease (0,1,progress);
		interval (target,f,this);
		if (over)
		{
			DestroyObject(this);
		}
	}
}
