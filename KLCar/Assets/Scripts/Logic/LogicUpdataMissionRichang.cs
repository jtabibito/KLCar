using UnityEngine;
using System.Collections;
using System;
using MyGameProto;
using System.Collections.Generic;

public class LogicUpdataMissionRichang :LogicBase{

	public const int MissionRichangNum=5;

	public override void ActLogic (Hashtable logicPar)
	{
		//throw new System.NotImplementedException ();
		DateTime updateTime=DateTimeExtensions.DateTimeFromSeconds(MainState.Instance.playerInfo.timeOfRichang);
		int updateDay=updateTime.DayOfYear;
		int nowDay=DateTime.UtcNow.DayOfYear;
		if(nowDay!=updateDay)
		{
			//刷新时间不是当天,清除所有任务
			MainState.Instance.playerInfo.missionOfRichang.Clear();
			MainState.Instance.playerInfo.timeOfRichang=DateTimeExtensions.CurrentTimeSeconds();

			List<MissionConfigData> configs=MissionConfigData.GetConfigDatas<MissionConfigData>();
			int addNum=0;
			for(int i=0;i<configs.Count;i++)
			{
				if(configs[i].missionType!=1)
				{
					//去掉非日常任务
					configs.RemoveAt(i);
					i--;
				}
				else if(configs[i].missionTypePar1=="-1")
				{
					//生成必然生成的任务
					this.AddMission(configs[i].id);
					configs.RemoveAt(i);
					addNum+=1;
					i--;
				}
			}
			while(addNum<MissionRichangNum)
			{
				//根据权值增加日常任务
				if(configs.Count==0)
				{
					throw new UnityException("not enough mission configs");
				}
				int totalValue=0;
				foreach(MissionConfigData mcd in configs)
				{
					totalValue+=int.Parse(mcd.missionTypePar1);
				}
				int getValue=UnityEngine.Random.Range(0,totalValue);
				for(int i=0;i<configs.Count;i++)
				{
					if(getValue<int.Parse(configs[i].missionTypePar1))
					{
						this.AddMission(configs[i].id);
						configs.RemoveAt(i);
						addNum+=1;
						break;
					}
					else
					{
						getValue-=int.Parse(configs[i].missionTypePar1);
					}
				}
			}
			MainState.Instance.SavePlayerData();
		}
		this.FinishLogic(null);
	}

	void AddMission(string missionId)
	{
		Hashtable logicPar=new Hashtable();
		logicPar.Add("missionId",missionId);
		this.AddLogic<LogicAddMission>(logicPar,null);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
