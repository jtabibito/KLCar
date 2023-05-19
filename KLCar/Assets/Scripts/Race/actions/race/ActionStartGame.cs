using UnityEngine;
using System.Collections;

public class ActionStartGame  : ActionBase {

	public int raceId;
	internal override void onCopyTo (ActionBase cloneTo)
	{
		
	}
	protected override void onStart ()
	{
		RaceManager.Instance.startRaceGame (raceId.ToString());
	}  
}