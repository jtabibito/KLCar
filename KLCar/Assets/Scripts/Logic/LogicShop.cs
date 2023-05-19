using UnityEngine;
using System.Collections;

public class LogicShop :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
		//throw new System.NotImplementedException ();
		string goodsId=logicPar["goodsId"].ToString();
		GoodsConfigData gcd=GoodsConfigData.GetConfigData<GoodsConfigData>(goodsId);
		switch((GameConsts.CostType)gcd.costType)
		{
		case GameConsts.CostType.CT_Gold:
			if(MainState.Instance.playerInfo.gold<gcd.costValue)
			{
				ReturnAndFinish(LogicReturn.LR_NOTENOUGHGOLD);
				return;
			}
			MainState.Instance.playerInfo.ChangeGold(-gcd.costValue);
			break;
		case GameConsts.CostType.CT_Diamond:
			if(MainState.Instance.playerInfo.diamond<gcd.costValue)
			{
				ReturnAndFinish(LogicReturn.LR_NOTENOUGHDIAMOND);
				return;
			}
			MainState.Instance.playerInfo.ChangeDiamond(-gcd.costValue);
			break;
		case GameConsts.CostType.CT_Score:
			if(MainState.Instance.playerInfo.score<gcd.costValue)
			{
				ReturnAndFinish(LogicReturn.LR_NOTENOUGHSCORE);
				return;
			}
			MainState.Instance.playerInfo.ChangeScore(-gcd.costValue);
			break;
		case GameConsts.CostType.CT_RMB:
			Debug.Log("需要人民币消耗接口");
			break;
		}
		switch((GameConsts.GoodsType)gcd.goodsType)
		{
		case GameConsts.GoodsType.GT_Gold:
			MainState.Instance.playerInfo.ChangeGold(gcd.goodsNum);
			break;
		case GameConsts.GoodsType.GT_Power:
			MainState.Instance.playerInfo.ChangePower(gcd.goodsNum);
			break;
		case GameConsts.GoodsType.GT_Daodan:
			MainState.Instance.playerInfo.ChangeItems(GameConsts.ItemType.IT_Daodan,gcd.goodsNum);
			break;
		case GameConsts.GoodsType.GT_Jiasu:
			MainState.Instance.playerInfo.ChangeItems(GameConsts.ItemType.IT_Jiasu,gcd.goodsNum);
			break;
		case GameConsts.GoodsType.GT_Yinshen:
			MainState.Instance.playerInfo.ChangeItems(GameConsts.ItemType.IT_Yinshen,gcd.goodsNum);
			break;
		case GameConsts.GoodsType.GT_Hudun:
			MainState.Instance.playerInfo.ChangeItems(GameConsts.ItemType.IT_Hudun,gcd.goodsNum);
			break;
		}

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
