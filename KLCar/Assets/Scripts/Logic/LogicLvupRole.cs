using UnityEngine;
using System.Collections;
using MyGameProto;

public class LogicLvupRole : LogicBase {

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
		bool own=false;
		foreach(RoleData rd in MainState.Instance.playerInfo.roleDatas)
		{
			if(rd.id==roleId)
			{
				own=true;
				RoleConfigData rcd=RoleConfigData.GetConfigData<RoleConfigData>(roleId);
				int nextLv=rd.lv+1;
				Debug.Log("Lvup:"+nextLv);
				if(nextLv<=rcd.maxLv)
				{
					string[] lvupCostGolds=rcd.costGoldOfLvup.Split('#');
					int lvupCostGold=int.Parse(lvupCostGolds[nextLv-1]);
					if(lvupCostGold<=MainState.Instance.playerInfo.gold)
					{
						MainState.Instance.playerInfo.ChangeGold(-lvupCostGold);
						rd.lv+=1;
						LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
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
			}
		}
		if(own==false)
		{
			Debug.Log("not own the role");
			this.FinishLogic(null);
			return;
		}

		Hashtable determineLogicPar=new Hashtable();
		determineLogicPar.Add ("determinePoint", "6");
		LogicManager.Instance.ActNewLogic<LogicCheckMission>(determineLogicPar,null);
		this.ReturnAndFinish(LogicReturn.LR_SUCCESS);
		return;
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
