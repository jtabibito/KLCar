using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicFinishMission :LogicBase {
	
	public override void ActLogic (Hashtable logicPar)
	{
		bool sign=false;
		MissionData lastMission = null;
		bool overOther = true;

		//throw new System.NotImplementedException ();
		string missionId=logicPar["missionId"].ToString();
		foreach(MissionData md in MainState.Instance.playerInfo.missionOfRichang)
		{
			if(md.id==missionId)
			{
				md.state=2;
				this.ExcuteMissionReward(md);
				sign=true;
			}
			if(md.id=="15")
			{
				//最后一个任务
				if(md.state==0)
				{
					lastMission=md;
				}
			}
			else
			{
				if(md.state!=2)
				{
					overOther=false;
				}
			}
		}

		if(lastMission!=null && overOther)
		{
			lastMission.state=1;
		}

		if(!sign)
		{
			foreach(MissionData md in MainState.Instance.playerInfo.missionOfChengjiu)
			{
				if(md.id==missionId)
				{
					md.state=2;
					this.ExcuteMissionReward(md);
					
					MissionConfigData mcd=MissionConfigData.GetConfigData<MissionConfigData>(md.id);
					if(mcd.missionTypePar1!="0" && mcd.missionTypePar1!="")
					{
						Hashtable newLogicPar=new Hashtable();
						newLogicPar.Add("missionId",mcd.id);
						
						LogicManager.Instance.ActNewLogic<LogicAddMission>(newLogicPar,null);
					}
					break;
				}
			}
		}
		LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
		this.FinishLogic(null);
	}

	void ExcuteMissionReward(MissionData md)
	{
		MissionConfigData mcd=MissionConfigData.GetConfigData<MissionConfigData>(md.id);
		switch(mcd.rewardType)
		{
		case 1://金币
			MainState.Instance.playerInfo.ChangeGold(mcd.rewardValue);
			break;
		case 2://钻石
			MainState.Instance.playerInfo.ChangeDiamond(mcd.rewardValue);
			break;
		case 3://积分
			MainState.Instance.playerInfo.ChangeScore(mcd.rewardValue);
			break;
		case 4://抽奖机会（最多只有一次,不能累加）
			if(MainState.Instance.playerInfo.lotteryNum==0)
			{
				MainState.Instance.playerInfo.lotteryNum=1;
			}
			break;
		}
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
