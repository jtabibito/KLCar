using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicAddMission :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		string missionId=logicPar["missionId"].ToString();
		MissionConfigData mcd=MissionConfigData.GetConfigData<MissionConfigData>(missionId);
		if(mcd.missionType==1)
		{
			//日常
			MissionData md=new MissionData();
			md.id=missionId;
			md.state=0;
			md.savePar=0;
			MainState.Instance.playerInfo.missionOfRichang.Add(md);
		}
		else if(mcd.missionType==2)
		{
			//成就
			MissionData md=new MissionData();
			md.id=missionId;
			md.state=0;
			md.savePar=0;
			MainState.Instance.playerInfo.missionOfChengjiu.Add(md);
		}

		MainState.Instance.SavePlayerData();
		this.FinishLogic(null);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
