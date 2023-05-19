using UnityEngine;
using System.Collections;

/// <summary>
/// 首次进入游戏逻辑
/// </summary>
public class LogicFirstEnterGame :LogicBase {

	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		MainState.Instance.playerInfo = new MyGameProto.MyPlayerInfo ();
		MainState.Instance.playerInfo.userID = 0;
		MainState.Instance.playerInfo.userName = "测试玩家";
		LocalDataByProto.SaveData<MyGameProto.MyPlayerInfo> ("playerInfo", MainState.Instance.playerInfo);
		this.FinishLogic (null);
	}

}
