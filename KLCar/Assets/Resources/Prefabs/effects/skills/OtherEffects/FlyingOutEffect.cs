using UnityEngine;
using System.Collections;

public class FlyingOutEffect : SkillBase {
	/// <summary>
	/// 被攻击到时,获得的制动力系数.
	/// </summary>
	public float BrakeFactor=0;
	public float hitRotation=720f;
	void Start () {
	
	}
	
	 
	protected override void onPlay ()
	{
		base.onPlay ();
		if (carEngine.getCarState (CarState.HuDun)) {
			//如果我有护盾,那么我就没事.
						return;
				} else {
					carEngine.setCarState (CarState.CantContol, true);
					carEngine.setCarState (CarState.IgnoreChangeRotation, true);
					carEngine.addBrakeFactor (BrakeFactor);
					carEngine.setCarState (CarState.YingShen, true);
					carEngine.doSnakeScreen ();
					iTween.RotateAdd(carEngine.carBody.gameObject, iTween.Hash ("time",duration+0.5f, "y", hitRotation, "islocal", true,"easetype","easeOutQuint"));
				}
	}
	protected override void onStop ()
	{
		carEngine.setCarState (CarState.CantContol, false);
		carEngine.addBrakeFactor (-BrakeFactor);
		carEngine.setCarState (CarState.YingShen, false);
		carEngine.setCarState (CarState.IgnoreChangeRotation, false);
		base.onStop ();
	}
	protected override void onPlayOver ()
	{

		base.onPlayOver ();
	}
	void LateUpdate ()
	{
		Debug.Log ("update");
	}
	 
	void OnDestroy()
	{
		Debug.Log ("story");
	}
}
