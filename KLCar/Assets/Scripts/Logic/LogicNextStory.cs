using UnityEngine;
using System.Collections;

public class LogicNextStory :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		RaceData rd = RaceManager.Instance.RaceData;
		if(rd.storyId!=null)
		{
			StoryConfigData scd=StoryConfigData.GetConfigData<StoryConfigData>(rd.storyId);
			string nextStoryId=scd.nextId;

			Hashtable newLogicPar=new Hashtable();
			newLogicPar.Add("storyId",nextStoryId);
			LogicManager.Instance.ActNewLogic<LogicEnterRace>(newLogicPar,null);
		}
		else
		{
			throw new UnityException("this story not exist");
		}
		this.FinishLogic(null);
	}
}
