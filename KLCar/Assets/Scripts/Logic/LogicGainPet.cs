using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicGainPet :LogicBase {

	/// <summary>
	/// Acts the logic.
	/// petId--提供宠物的ID
	/// </summary>
	/// <param name="logicPar">Logic par.</param>
	public override void ActLogic (Hashtable logicPar)
	{
		string petId=logicPar["petId"].ToString();
		foreach(PetData pd in MainState.Instance.playerInfo.petDatas)
		{
			if(pd.id==petId)
			{
				Debug.Log("already own the car");
				this.FinishLogic(null);
				return;
			}
		}
		PetConfigData pcd=PetConfigData.GetConfigData<PetConfigData>(petId);
		switch(pcd.costTypeOfGain)
		{
		case 1://金币
			if(pcd.costValueOfGain>MainState.Instance.playerInfo.gold)
			{
				Debug.Log("not enough gold");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
				return;
			}
			MainState.Instance.playerInfo.ChangeGold(-pcd.costValueOfGain);
			break;
		case 2://钻石
			if(pcd.costValueOfGain>MainState.Instance.playerInfo.diamond)
			{
				Debug.Log("not enough diamond");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHDIAMOND);
				return;
			}
			MainState.Instance.playerInfo.ChangeDiamond(-pcd.costValueOfGain);
			break;
		case 3://积分
			if(pcd.costValueOfGain>MainState.Instance.playerInfo.score)
			{
				Debug.Log("not enough score");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHSCORE);
				return;
			}
			MainState.Instance.playerInfo.ChangeScore(-pcd.costValueOfGain);
			break;
		}
		PetData addPet=new PetData();
		addPet.id=petId;
		MainState.Instance.playerInfo.petDatas.Add(addPet);
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
