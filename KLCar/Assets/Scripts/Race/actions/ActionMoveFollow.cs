using UnityEngine;
using System.Collections;

/// <summary>
/// 让目标在指定的时间内移动到目标旁边.
/// </summary>
public class ActionMoveFollow : ActionBase
{
	public GameObject moveToTarget;
	/// <summary>
	/// 缓动函数.
	/// </summary>
	public Easetype easetype;
	/// <summary>
	/// T要注视的对象.
	/// </summary>
	public GameObject lookTarget;
	private Transform target;
	private TweenUtils.EasingFunction ease;
	protected float _lastFactor ;

	protected override void onStart ()
	{
		target = GameObjectAgent.getAgentTransform (gameObject, moveToTarget);
		ease = TweenUtils.GetEasingFunction (easetype);
		_lastFactor = 0;
	}

	void Update ()
	{
		if (!isStart)
		{
			return;
		}
		Vector3 end = target.transform.position;
		Vector3 start=gameObject.transform.position;
		float n=ease(0,1,progress);
		if (n != _lastFactor) {
			float ratio = (n == 1 || _lastFactor == 1) ? 0 : 1 - ((n - _lastFactor) / (1 - _lastFactor));
			start.x = end.x - ((end.x - start.x) * ratio);
			start.y = end.y - ((end.y - start.y) * ratio);
			start.z = end.z - ((end.z - start.z) * ratio);
			_lastFactor = n;
			gameObject.transform.position=start;
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{

	}
}
