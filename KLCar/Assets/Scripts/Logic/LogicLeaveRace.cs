using UnityEngine;
using System.Collections;

public class LogicLeaveRace :LogicBase {
	public override void ActLogic (Hashtable logicPar)
	{
		SceneLoader.BeginLoadScene("login",this.OnHallSceneLoadOver);
	}

	void OnHallSceneLoadOver()
	{
		this.FinishLogic(null);
		RaceManager.Instance.ResetRace();
		PanelMainUIController.Instance.EnterHall();
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
