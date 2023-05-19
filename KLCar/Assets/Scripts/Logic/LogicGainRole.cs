using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicGainRole :LogicBase {

	/// <summary>
	/// Acts the logic.
	/// 参数属性
	/// roleId--提供角色的ID
	/// </summary>
	/// <param name="logicPar">Logic par.</param>
	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		string roleId=logicPar["roleId"].ToString();
		foreach(RoleData rd in MainState.Instance.playerInfo.roleDatas)
		{
			if(rd.id==roleId)
			{
				Debug.Log("already own the car");
				this.FinishLogic(null);
				return;
			}
		}
		RoleConfigData rcd=RoleConfigData.GetConfigData<RoleConfigData>(roleId);
		switch(rcd.costTypeOfGain)
		{
		case 1://金币
			if(rcd.costValueOfGain>MainState.Instance.playerInfo.gold)
			{
				Debug.Log("not enough gold");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
				return;
			}
			MainState.Instance.playerInfo.ChangeGold(-rcd.costValueOfGain);
			break;
		case 2://钻石
			if(rcd.costValueOfGain>MainState.Instance.playerInfo.diamond)
			{
				Debug.Log("not enough diamond");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHDIAMOND);
				return;
			}
			MainState.Instance.playerInfo.ChangeDiamond(-rcd.costValueOfGain);
			break;
		case 3://积分
			if(rcd.costValueOfGain>MainState.Instance.playerInfo.score)
			{
				Debug.Log("not enough score");
				this.ReturnAndFinish(LogicReturn.LR_NOTENOUGHSCORE);
				return;
			}
			MainState.Instance.playerInfo.ChangeScore(-rcd.costValueOfGain);
			break;
		}
		RoleData addRole=new RoleData();
		addRole.id=roleId;
		addRole.lv=0;
		MainState.Instance.playerInfo.roleDatas.Add(addRole);
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
