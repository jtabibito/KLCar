using UnityEngine;
using System.Collections;

public class LogicEnterGame :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
		this.AddLogic<LogicLoadResources> (null, this.OnResourcesLoadOver);
		//throw new System.NotImplementedException ();
	}

	void OnResourcesLoadOver(Hashtable logicPar)
	{
		PanelMainUIController.Instance.EnterGame();
		this.FinishLogic(null);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
