using UnityEngine;
using System.Collections;

public class LogicEndRace :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
		//throw new System.NotImplementedException ();
//		PanelMainUIController.Instance.OverRace ();
		if(RaceManager.Instance.RaceData.storyId!=null)
		{
			//剧情模式
			if(RaceManager.Instance.RaceCounterInstance.curResult==RaceCounter.RaceResult.RR_Victory)
			{
				//当前剧情配置数据
				StoryConfigData nowScd=null;
				if(MainState.Instance.playerInfo.missionOfJuqing!="")
				{
					nowScd=StoryConfigData.GetConfigData<StoryConfigData>(MainState.Instance.playerInfo.missionOfJuqing);
				} 
				//比赛剧情配置数据
				StoryConfigData scd=StoryConfigData.GetConfigData<StoryConfigData>(RaceManager.Instance.RaceData.storyId);
				if(nowScd==null || scd.weight>nowScd.weight)
				{
					//比赛剧情权重高于当前剧情,完成剧情获得奖励并进行剧情推进
					MainState.Instance.playerInfo.missionOfJuqing=RaceManager.Instance.RaceData.storyId;
					MainState.Instance.playerInfo.ChangeGold(scd.rewardValue);
				}
			}
		}
		else
		{
			//休闲模式,直接奖励1000金币
			MainState.Instance.playerInfo.ChangeGold(1000);
		}

		RaceManager.Instance.RaceCounterInstance.overRace=true;
		Hashtable newLogicPar = new Hashtable ();
		newLogicPar.Add ("determinePoint", "1");
		LogicManager.Instance.ActNewLogic<LogicCheckMission> (newLogicPar, null);

		this.FinishLogic(null);
	}
}
