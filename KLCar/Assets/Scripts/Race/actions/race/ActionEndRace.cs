using UnityEngine;
using System.Collections;

public class ActionEndRace  : ActionBase {
	
	internal override void onCopyTo (ActionBase cloneTo)
	{
		
	}
	protected override void onStart ()
	{
		PanelMainUIController.Instance.OverRace ();
	}  
}