using UnityEngine;
using System.Collections;

public class LogicEnterRace : LogicBase {

	RaceData rd;
	RaceConfigData rc;
	/// <summary>
	/// Acts the logic.
	/// 如果是剧情模式,传参storyId
	/// 如果是非剧情模式,传参raceId
	/// </summary>
	/// <param name="logicPar">Logic par.</param>
	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();

		rd = new RaceData ();
		rd.roleId=MainState.Instance.playerInfo.nowRoleId;
		rd.carId=MainState.Instance.playerInfo.nowCarId;
		rd.petId=MainState.Instance.playerInfo.nowPetId;

		string storyId=(string)logicPar["storyId"];
		string raceId=(string)logicPar["raceId"];
		if(storyId!=null)
		{
			StoryConfigData scd=StoryConfigData.GetConfigData<StoryConfigData>(storyId);
			rd.storyId=storyId;
			rd.raceId=scd.raceId;
		}
		else if(raceId!=null)
		{
			rd.raceId=logicPar["raceId"].ToString();
		}
		else
		{
			rd.raceId="1";
		}


//		string raceId=logicPar["raceId"].ToString();
//		if (raceId == "")
//		{
//			rd.raceId = "1";
//		} 

		if(this.rd.raceId.Length<=3)
			MainState.Instance.playerInfo.missionOfPreviousRelaxation = this.rd.raceId;	 //add by maojudong,2015年6月23日18:04:16
		else 
			MainState.Instance.playerInfo.missionOfPreviousJuqing = this.rd.raceId;		 //add by maojudong,2015年6月23日18:04:16

		rc=RaceConfigData.GetConfigData<RaceConfigData>(this.rd.raceId);
		SceneCreator.Instance.CreatScene(this.rc.sceneName,this.OnRaceSceneLoadOver);
//		SceneLoader.BeginLoadScene (this.rc.sceneName, this.OnRaceSceneLoadOver);
	}

	void OnRaceSceneLoadOver()
	{
		RaceManager.Instance.InitRace(this.rd,this.rc);
		this.FinishLogic(null);
	}

	public override void Destroy ()
	{
		this.rd=null;
		this.rc=null;
		base.Destroy ();
	}
}
