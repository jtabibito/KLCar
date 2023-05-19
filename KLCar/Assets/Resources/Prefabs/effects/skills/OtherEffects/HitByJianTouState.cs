using UnityEngine;
using System.Collections;

public class HitByJianTouState : SkillBase
{
	/// <summary>
	/// 被攻击到时,获得的制动力系数.
	/// </summary>
	public float BrakeFactor = 0;
	public float hitRotation = 720f;
	private bool isStart = false;

	void Start ()
	{
	
	}
	 
	protected override void onPlay ()
	{
		base.onPlay ();
		if (carEngine.getCarState (CarState.HuDun))
		{
			//如果我有护盾,那么我就没事.
			return;
		} else
		{
			if (!carEngine.getCarState (CarState.WuDi))
			{
				isStart = true;
				carEngine.setCarState (CarState.CantContol, true);
				carEngine.addBrakeFactor (BrakeFactor);
				carEngine.setCarState (CarState.YingShen, true);
				carEngine.setCarState (CarState.IgnoreChangeRotation, true);
				iTween.RotateAdd (carEngine.carBody.gameObject, iTween.Hash ("time", duration + 0.5f, "y", hitRotation, "islocal", true, "easetype", "easeOutQuint"));
			}
			carEngine.doSnakeScreen ();
		}
	}

	protected override void onStop ()
	{
		if (!isStart)
		{
			return;
		}
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
 
}
