using UnityEngine;
using System.Collections;

public class ActionCreatePlayerCar : ActionBase {

	 internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
	protected override void onStart ()
	{
		RaceManager.Instance.createCar ();
	}
}
