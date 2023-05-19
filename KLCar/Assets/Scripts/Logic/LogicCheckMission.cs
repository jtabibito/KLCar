using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyGameProto;

public class LogicCheckMission : LogicBase {
	Hashtable logicPar;
	bool needSave;
	public override void ActLogic (Hashtable logicPar)
	{
		this.logicPar=logicPar;
		string determinePoint=logicPar["determinePoint"].ToString();

		foreach(MissionData md in MainState.Instance.playerInfo.missionOfRichang)
		{
			if(md.state!=0)
			{
				//任务已经完成
				continue;
			}

			MissionConfigData mcd=md.ConfigData;
			if(mcd.determinePoint!=determinePoint)
			{
				//该任务不在该判定点判定
				continue;
			}


			if(CheckOnCondition(mcd.condition1,mcd.condition1par1,mcd.condition1par2,mcd.condition1par3,md) && 
			   CheckOnCondition(mcd.condition2,mcd.condition2par1,mcd.condition2par2,mcd.condition2par3,md) && 
			   CheckOnCondition(mcd.condition3,mcd.condition3par1,mcd.condition3par2,mcd.condition3par3,md) && 
			   CheckOnCondition(mcd.condition4,mcd.condition4par1,mcd.condition4par2,mcd.condition4par3,md) && 
			   CheckOnCondition(mcd.condition5,mcd.condition5par1,mcd.condition5par2,mcd.condition5par3,md))
			{
				md.state=1;
				this.needSave=true;
				Hashtable sendPar=new Hashtable();
				sendPar.Add("missionId",md.id);
				LogicManager.Instance.SendLogicCommand(LogicCommand.LC_MissionOfRichangIsOver,sendPar,null);
			}
		}

		foreach(MissionData md in MainState.Instance.playerInfo.missionOfChengjiu)
		{
			if(md.state!=0)
			{
				//任务已经完成
				continue;
			}
			
			MissionConfigData mcd=md.ConfigData;
			if(mcd.determinePoint!=determinePoint)
			{
				//该任务不在该判定点判定
				continue;
			}
			
			
			if(CheckOnCondition(mcd.condition1,mcd.condition1par1,mcd.condition1par2,mcd.condition1par3,md) && 
			   CheckOnCondition(mcd.condition2,mcd.condition2par1,mcd.condition2par2,mcd.condition2par3,md) && 
			   CheckOnCondition(mcd.condition3,mcd.condition3par1,mcd.condition3par2,mcd.condition3par3,md) && 
			   CheckOnCondition(mcd.condition4,mcd.condition4par1,mcd.condition4par2,mcd.condition4par3,md) && 
			   CheckOnCondition(mcd.condition5,mcd.condition5par1,mcd.condition5par2,mcd.condition5par3,md))
			{
				md.state=1;
				this.needSave=true;
				Hashtable sendPar=new Hashtable();
				sendPar.Add("missionId",md.id);
				LogicManager.Instance.SendLogicCommand(LogicCommand.LC_MissionOfChengjiuIsOver,sendPar,null);
			}
		}

		if(needSave)
		{
			MainState.Instance.SavePlayerData();
		}
			
		this.FinishLogic(null);
	}

	bool CheckOnCondition(string condition,string par1,string par2,string par3,MissionData md)
	{
		if(condition=="0")
		{
			return true;
		}
		if(condition=="1")
		{
			//判定器
			int getNum=GetConditionNum(par1,md);
			return equalValue(par2,getNum,par3);
		}
		else if(condition=="2")
		{
			//数值累加器
			int getNum=GetConditionNum(par1,md);
			md.savePar+=getNum;
			this.needSave=true;
			return true;
		}
		else if(condition=="3")
		{
			//次数累加器
			md.savePar+=1;
			this.needSave=true;
			return true;
		}
		else if(condition=="4")
		{
			//列表数量获取器
			List<string> getList=GetConditionList(par1);
			if(getList!=null)
			{
				int containerNum=0;
				foreach(string getStr in getList)
				{
					if(getStr==par3)
					{
						containerNum+=1;
					}
				}
				md.tempPar=containerNum;
			}
			return true;
		}
		return false;
	}

	List<string> GetConditionList(string conditionType)
	{
		RaceCounter rc=null;
		if(logicPar["determinePoint"].ToString()=="1")
		{
			rc=RaceManager.Instance.RaceCounterInstance;
		}

		switch(conditionType)
		{
		case "10"://比赛中使用宠物技能的表(List)
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "19"://收集车辆(List)
			break;
		case "20"://收集宠物(List)
			break;
		case "21"://收集人物(List)
			break;
		}
		return null;
	}

	int GetConditionNum(string conditionType,MissionData md)
	{
		RaceCounter rc=null;
		if(logicPar["determinePoint"].ToString()=="1")
		{
			rc=RaceManager.Instance.RaceCounterInstance;
		}

		int getNum=0;
		switch(conditionType)
		{
		case "0"://不获取数值
			break;
		case "1"://当前存储数据
			getNum=md.savePar;
			break;
		case "2"://当前临时数据
			getNum=md.tempPar;
			break;
		case "3"://比赛是否完成
			if(rc==null)
			{
				throw new UnityException("no race");
			}
		    getNum=rc.overRace==true?1:0;
			break;
		case "4"://比赛类型
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			getNum=(int)rc.CurMode;
			break;
		case "5"://比赛形式公里数
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "6"://比赛消耗时间
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "7"://比赛中使用的道具的表(List)
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "8"://比赛中拾取金币数量
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			getNum=rc.gainGoldNum;
			break;
		case "9"://比赛中拾取钻石数量
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "11"://比赛最高时速
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "12"://比赛最高加速度
			if(rc==null)
			{
				throw new UnityException("no race");
			}
			break;
		case "13"://金币数量
			getNum=(int)MainState.Instance.playerInfo.gold;
			break;
		case "14"://钻石数量
			getNum=(int)MainState.Instance.playerInfo.diamond;
			break;
		case "15"://消耗金币数
			int changeGold=int.Parse(logicPar["changeGold"].ToString());
			if(changeGold<0)
			{
				getNum=-changeGold;
			}
			break;
		case "16"://消耗钻石数
			int changeDiamond=int.Parse(logicPar["changeDiamond"].ToString());
			if(changeDiamond<0)
			{
				getNum=-changeDiamond;
			}
			break;
		case "17"://消耗爱心数
			int changePower=int.Parse(logicPar["changePower"].ToString());
			if(changePower<0)
			{
				getNum=-changePower;
			}
			break;
		case "18"://通过剧情模式地图ID
			break;
		}
		return getNum;
	}

	bool equalValue(string CheckType,int value,string parValue)
	{
		switch(CheckType)
		{
		case "<":
			return value<int.Parse(parValue);
		case ">":
			return value>int.Parse(parValue);
		case "=":
			string[] getValues1=parValue.Split('#');
			foreach(string getValue1 in getValues1)
			{
				if(value==int.Parse(getValue1))
				{
					return true;
				}
			}
			return false;
		case "!=":
			string[] getValues2=parValue.Split('#');
			foreach(string getValue2 in getValues2)
			{
				if(value==int.Parse(getValue2))
				{
					return false;
				}
			}
			return true;
		case ">=":
			return value>=int.Parse(parValue);
		case "<=":
			return value<=int.Parse(parValue);
		}
		return false;
	}


	public override void Destroy ()
	{
		logicPar=null;
		base.Destroy ();
	}
}
