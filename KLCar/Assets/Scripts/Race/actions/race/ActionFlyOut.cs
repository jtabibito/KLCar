using UnityEngine;
using System.Collections;

/// <summary>
///给予碰撞车辆一定金币的效果.
/// </summary>
public class ActionFlyOut : ActionBase
{
	public int rotation = 360;
	private Vector3 lastPos;
	private Vector3 targetPos;
	public float xOffset = 30;
	public float yOffset = 30;
	public float zOffset = 10;
	public Easetype rEaseType;
	public Easetype xEaseType;
	public Easetype yEaseType;
	public Easetype zEaseType;
	private int direction;
	private GameObject target;
	internal override void onCopyTo (ActionBase cloneTo)
	{
//		ActionPlaySkill sk = (ActionPlaySkill)cloneTo;
//		sk.skill = skill;
	}

	protected override void onStart ()
	{
		CarEngine car = gameObject.GetComponent <CarEngine> ();
		if (car != null)
		{
			direction = car.hitFlyDirection;
			target=car.carBody.FindChild("car").gameObject;
			car.setCarState (CarState.CantContol, true);
			car.setCarState (CarState.YingShen, true);
			car.setCarState (CarState.IgnoreChangeRotation, true);
			car.addBrakeFactor (10000);
		} else
		{
			direction=1;
			target=gameObject;
		}
//		car.doUseSkill (skill);
//		gameObject.rigidbody.isKinematic = true;

		iTween.RotateAdd (target, iTween.Hash ("z", rotation, "space", Space.Self, "time", time, "easetype", rEaseType.ToString()));
		TweenLite.doTween (target, time, TweenUtils.GetEasingFunction (xEaseType), new TweenLite.TweenLiteFunc (tweenUpdate));
		lastPos = target.transform.localPosition;
//		targetPos = new Vector3 (lastPos.x+30,lastPos.y+30,lastPos.z+10);

	}

	void Update ()
	{

	}

	void tweenUpdate (object target, float value, TweenLite tween)
	{
		Vector3 pos = new Vector3 ();
		pos.x =  direction *xOffset * value + lastPos.x;
		pos.y = yOffset * TweenUtils.GetEasingFunction (yEaseType) (0, 1, tween.progress) + lastPos.y;
		pos.z = zOffset * TweenUtils.GetEasingFunction (zEaseType) (0, 1, tween.progress) + lastPos.z;
		this.target.transform.localPosition = pos;
	}
}
