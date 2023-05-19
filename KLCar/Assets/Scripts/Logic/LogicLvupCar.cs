using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicLvupCar :LogicBase {
	
	public enum AtrType
	{
		Acc,//加速度属性
		Speed,//速度属性
		Handler,//操作性属性
	}

	/// <summary>
	/// Acts the logic.
	/// 参数属性
	/// carId--提供车辆的ID
	/// atrType--提供升级属性的类型
	/// </summary>
	/// <param name="logicPar">Logic par.</param>
	public override void ActLogic (Hashtable logicPar)
	{
		string carId=logicPar["carId"].ToString();
		AtrType atrType=(AtrType)logicPar["atrType"];
		bool own=false;
		foreach(CarData cd in MainState.Instance.playerInfo.carDatas)
		{
			if(cd.id==carId)
			{
				own=true;
				CarConfigData ccd=CarConfigData.GetConfigData<CarConfigData>(carId);
				int nextLv=0;
				switch(atrType)
				{
				case AtrType.Acc:
					nextLv=cd.accLv+1;
					if(nextLv<ccd.accMaxLv)
					{
						string[] lvupCostGolds=ccd.accLvupCostGold.Split('#');
						string[] lvupValues=ccd.accLvupValue.Split('#');
						if(lvupCostGolds.Length<nextLv || lvupValues.Length<nextLv)
						{
							throw new UnityException("config error");
						}
						int lvupCostGold=int.Parse(lvupCostGolds[nextLv-1]);
						int lvupValue=int.Parse(lvupValues[nextLv-1]);
						if(lvupCostGold<=MainState.Instance.playerInfo.gold)
						{
							MainState.Instance.playerInfo.ChangeGold(-lvupCostGold);
							cd.accLv+=1;
						}
						else
						{
							Debug.Log("not enough money");
							this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
							return;
						}
					}
					else
					{
						Debug.Log("has reached max lv");
						this.ReturnAndFinish(LogicReturn.LR_REACHEDMAXLV);
						return;
					}
					break;
				case AtrType.Speed:
					nextLv=cd.speedLv+1;
					if(nextLv<ccd.speedMaxLv)
					{
						string[] lvupCostGolds=ccd.speedLvupCostGold.Split('#');
						string[] lvupValues=ccd.speedLvupValue.Split('#');
						if(lvupCostGolds.Length<nextLv || lvupValues.Length<nextLv)
						{
							throw new UnityException("config error");
						}
						int lvupCostGold=int.Parse(lvupCostGolds[nextLv-1]);
						int lvupValue=int.Parse(lvupValues[nextLv-1]);
						if(lvupCostGold<=MainState.Instance.playerInfo.gold)
						{
							MainState.Instance.playerInfo.ChangeGold(-lvupCostGold);
							cd.speedLv+=1;
						}
						else
						{
							Debug.Log("not enough money");
							this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
							return;
						}
					}
					else
					{
						Debug.Log("has reached max lv");
						this.ReturnAndFinish(LogicReturn.LR_REACHEDMAXLV);
						return;
					}
					break;
				case AtrType.Handler:
					nextLv=cd.handlerLv+1;
					if(nextLv<ccd.handlerMaxLv)
					{
						string[] lvupCostGolds=ccd.handlerLvupCostGold.Split('#');
						string[] lvupValues=ccd.handlerLvupValue.Split('#');
						if(lvupCostGolds.Length<nextLv || lvupValues.Length<nextLv)
						{
							throw new UnityException("config error");
						}
						int lvupCostGold=int.Parse(lvupCostGolds[nextLv-1]);
						int lvupValue=int.Parse(lvupValues[nextLv-1]);
						if(lvupCostGold<=MainState.Instance.playerInfo.gold)
						{
							MainState.Instance.playerInfo.ChangeGold(-lvupCostGold);
							cd.handlerLv+=1;
						}
						else
						{
							Debug.Log("not enough money");
							this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
							return;
						}
					}
					else
					{
						Debug.Log("has reached max lv");
						this.ReturnAndFinish(LogicReturn.LR_REACHEDMAXLV);
						return;
					}
					break;
				default:
					Debug.Log("error atrType");
					this.FinishLogic(null);
					return;
					break;
				}
				break;
			}
		}
		if(own==false)
		{
			Debug.Log("not own the car");
			this.FinishLogic(null);
			return;
		}
		LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);

		Hashtable determineLogicPar=new Hashtable();
		determineLogicPar.Add ("determinePoint", "7");
		LogicManager.Instance.ActNewLogic<LogicCheckMission>(determineLogicPar,null);
		this.ReturnAndFinish(LogicReturn.LR_SUCCESS);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	void ReturnAndFinish(LogicReturn lr)
	{
		Hashtable logicPar=new Hashtable();
		logicPar.Add("logicReturn",lr);
		this.FinishLogic(logicPar);
	}
}
