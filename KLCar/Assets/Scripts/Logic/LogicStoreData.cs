using UnityEngine;
using System.Collections;
using MyGameProto;
using System;

public class LogicStoreData : LogicBase {

	public static string storeAdress="http://192.168.0.220:8080/KLSaicheServer/playInfoAction!addPlayInfo.do";

	public override void ActLogic (Hashtable logicPar)
	{
		if(MainState.Instance.playerInfo!=null)
		{
			MainState.Instance.playerInfo.updateTime=DateTimeExtensions.CurrentTimeSeconds();
			LocalDataByProto.SaveData<MyPlayerInfo>("playerInfo",MainState.Instance.playerInfo);
//			HttpRequestByProto.RequestByProto<MyPlayerInfo,System.Object>(null,MainState.Instance.playerInfo,storeAdress);
		}
		FinishLogic(null);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
