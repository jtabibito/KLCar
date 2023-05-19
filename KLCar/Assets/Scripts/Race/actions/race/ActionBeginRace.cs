using UnityEngine;
using System.Collections;

public class ActionBeginRace : ActionBase {

	 internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
	protected override void onStart ()
	{
		if (RaceManager.Instance.RaceCounterInstance != null)
		{
			RaceManager.Instance.startRace ();
		} else
		{
			LogicManager.Instance.ActNewLogic<LogicEnterRace> (null,null);
		}
	}
}
