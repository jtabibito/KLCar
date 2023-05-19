using UnityEngine;
using System.Collections;

/// <summary>
/// 环绕某个对象或者位置旋转.对象会一直保持与目标的距离.然后做弧形环绕运动.
/// </summary>
public class ActionAround : ActionBase
{
	public enum LookType
	{
		Keep,LookTarget,KeepFirst
	}
	public enum AroundType
	{
		AroundAdd,
		//AroundTo,
		//AroundFrom,
		//AroundBy
	}
	/// <summary>
	/// 环绕的类型.目前只有增加多少角度.
	/// </summary>
	public AroundType aroundType;
	/// <summary>
	/// 旋转角度数量.
	/// </summary>
	public float angle;
	/// <summary>
	/// 缓动类型.
	/// </summary>
	public Easetype easetype;
	/// <summary>
	/// 环绕的目标点位置.通常使用下面的target 
	/// </summary>
	public Vector3 targetPos;
	/// <summary>
	/// 要注视的目标,环绕这个对象做圆周运动.支持GameObjectAgent.
	/// </summary>
	public GameObject target;
	/// <summary>
	/// 旋转的轴向.可以多个轴.比如,绕y轴旋转.
	/// </summary>
	public Vector3 axis = new Vector3 (0, 1, 0);
 
	public bool isWordAxis = true;
	/// <summary>
	/// 旋转时,是否保持世界朝向,默认保持自己原来的朝向.
	/// </summary>
	public bool keepWorldRotate;
	/// <summary>
	/// 在绕某个物体运动的过程中,是否需要注视着另一个对象.
	/// </summary>
	public bool lookAtTarget;
	private float startTime;
	private float lastAngle;
	private TweenUtils.EasingFunction func;
	void Start()
	{

	}
	void Update ()
	{
		if (!isStart)
		{
			return;
		}
		Vector3 axis;
		if (isWordAxis) {
			axis=this.axis;
		} else {
			axis=gameObject.transform.TransformDirection(this.axis);
		}
		Quaternion lastRotation = gameObject.transform.rotation;
		float current = startTime / time;
		float currentValue = func (0, angle, current);
		if (target == null)
		{
			transform.RotateAround (targetPos, axis, (currentValue - lastAngle));
			if (lookAtTarget)
			{
				transform.LookAt (targetPos);
			} else
			{
				if(keepWorldRotate)
				{
					gameObject.transform.rotation=lastRotation;
				}
			}
		} else
		{
			Vector3 pos = GameObjectAgent.getAgentTransform (gameObject, target).position;
			transform.RotateAround (pos, axis, (currentValue - lastAngle));
			if (lookAtTarget)
			{
				transform.LookAt (pos);
			}else{
				if(keepWorldRotate)
				{
					gameObject.transform.rotation=lastRotation;
				}
			}
		}
		lastAngle = currentValue;
		startTime += Time.deltaTime;
	}
			
	protected override void onStart ()
	{
		startTime = 0;	
		lastAngle = 0;
		func = TweenUtils.GetEasingFunction (easetype);
		//if (keepWorldRotate)
		//{
		//	lastRotation=gameObject.transform.rotation;
		//}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionAround a = (ActionAround)cloneTo;
		a.aroundType = aroundType;
		a.angle = angle;
		a.easetype = easetype;
		a.targetPos = targetPos;
		a.target = target;
		a.axis = axis;
		a.keepWorldRotate = keepWorldRotate;
		a.lookAtTarget = lookAtTarget;

	}
}
