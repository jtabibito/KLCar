using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicGainCar :LogicBase{
	/// <summary>
	/// Acts the logic.
	/// 参数属性
	/// carId--提供车辆的ID
	/// </summary>
	/// <param name="logicPar">Logic par.</param>
	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		string carId=logicPar["carId"].ToString();
		foreach(CarData cd in MainState.Instance.playerInfo.carDatas)
		{
			if(cd.id==carId)
			{
				Debug.Log("already own the car");
				this.FinishLogic(null);
				return;
			}
		}
		CarConfigData ccd=CarConfigData.GetConfigData<CarConfigData>(carId);
		switch(ccd.costTypeOfGain)
		{
		case 1://金币
			if(ccd.costValueOfGain>MainState.Instance.playerInfo.gold)
			{
				Debug.Log("not enough gold");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
				return;
			}
			MainState.Instance.playerInfo.ChangeGold(-ccd.costValueOfGain);
			break;
		case 2://钻石
			if(ccd.costValueOfGain>MainState.Instance.playerInfo.diamond)
			{
				Debug.Log("not enough diamond");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHDIAMOND);
				return;
			}
			MainState.Instance.playerInfo.ChangeDiamond(-ccd.costValueOfGain);
			break;
		case 3://积分
			if(ccd.costValueOfGain>MainState.Instance.playerInfo.score)
			{
				Debug.Log("not enough score");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHSCORE);
				return;
			}
			MainState.Instance.playerInfo.ChangeScore(-ccd.costValueOfGain);
			break;
		}
		CarData addCar=new CarData();
		addCar.id=carId;
		addCar.accLv=0;
		addCar.speedLv=0;
		addCar.handlerLv=0;
		MainState.Instance.playerInfo.carDatas.Add(addCar);
		LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
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
