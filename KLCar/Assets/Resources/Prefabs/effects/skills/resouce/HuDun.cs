using UnityEngine;
using System.Collections;

public class HuDun: SkillBase {
	
	protected override void onPlay()
	{
		base.onPlay ();
		CarEngine car =transform.parent.GetComponent <CarEngine>();
		if (car != null) {
			car.setCarState (CarState.HuDun,true);
		}
	}
	protected override void onStop ()
	{
		base.onStop ();
		carEngine.setCarState (CarState.HuDun,false);
	}
}
