using UnityEngine;
using System.Collections;

public class LogicRestart :LogicBase {
	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		Hashtable newLogicPar=new Hashtable();
		if(RaceManager.Instance.RaceData.storyId!=null)
		{
			newLogicPar.Add("storyId",RaceManager.Instance.RaceData.storyId);
		}
		else
		{
			newLogicPar.Add("raceId",RaceManager.Instance.RaceData.raceId);
		}


		RaceManager.Instance.ResetRace();

		LogicManager.Instance.ActNewLogic<LogicEnterRace>(newLogicPar,null);
		this.FinishLogic(null);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
