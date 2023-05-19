using UnityEngine;
using System.Collections;

/// <summary>
/// 快速登录游戏逻辑
/// </summary>
public class LogicQuicklyLogin :LogicBase{

	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		MainState.Instance.playerInfo=LocalDataByProto.LoadData<MyGameProto.MyPlayerInfo>("playerInfo");
		this.AddLogic<LogicFirstEnterGame>(null,this.OnFirstEnterGameOver);
		return;
//		if(MainState.Instance.playerInfo==null)
//		{
//			//进入首次进入游戏逻辑
//			this.AddLogic<LogicFirstEnterGame>(null,this.OnFirstEnterGameOver);
//			return;
//		}
//		else
//		{
//			if(MainState.Instance.netSupport)
//			{
//				//有网络支持,进入登陆服务器逻辑
//				this.AddLogic<LogicLoginServer>(null,this.OnLoginServerOver);
//				return;
//			}
//			else
//			{
//				//无网络支持,直接结束逻辑
//				this.onLogicOver(null);
//				return;
//			}
//		}
//		this.onLogicOver (null);
	}

	void OnFirstEnterGameOver(Hashtable logicPar)
	{
		this.FinishLogic (null);
	}

	void OnLoginServerOver(Hashtable logicPar)
	{
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
