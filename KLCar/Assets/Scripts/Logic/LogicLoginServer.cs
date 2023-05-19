using UnityEngine;
using System.Collections;
using MyGameProto;
using System;

/// <summary>
/// 登陆服务器逻辑
/// </summary>
using System.Collections.Generic;


public class LogicLoginServer : LogicBase
{

		public static string loginAdress = "http://192.168.0.220:8080/KLSaicheServer/playInfoAction!getPlayInfo.do";
		MyGameProto.MyPlayerInfo localInfo;

		public override void ActLogic (Hashtable logicPar)
		{
				this.LoginNoNet ();
//				localInfo = LocalDataByProto.LoadData<MyPlayerInfo> ("playerInfo");
//				RequestLogin rl = new RequestLogin ();
//				rl.userID = (localInfo == null) ? 0 : localInfo.userID;
//				HttpRequestByProto.RequestByProto<RequestLogin,MyPlayerInfo> (this.ReponseFromServer, rl, loginAdress);
		}

		void ReponseFromServer (System.Object responsObj)
		{
				if (responsObj != null) {
						MyPlayerInfo serverInfo = (MyPlayerInfo)responsObj;
						if (localInfo == null || serverInfo.updateTime >= localInfo.updateTime) {
								MainState.Instance.playerInfo = serverInfo;
						} else {
								MainState.Instance.playerInfo = localInfo;
						}
				} else {
						if (localInfo == null) {
//								Debug.Log ("net error,can not first login");
						} else {
								MainState.Instance.playerInfo = localInfo;
						}
				}
				this.AddLogic<LogicStoreData> (null, null);
				this.FinishLogic (null);
		}

		void LoginNoNet ()
		{
				localInfo = LocalDataByProto.LoadData<MyPlayerInfo> ("playerInfo");
				if (localInfo == null) {
						localInfo = new MyPlayerInfo ();
						localInfo.userID = 1;
						localInfo.userName = "单机测试";
						localInfo.updateTime = 0;
						localInfo.gold = 100;
						localInfo.diamond = 100;
						localInfo.score = 100;
						localInfo.nowCarId = "1";
						localInfo.nowPetId = "1";
						localInfo.nowRoleId = "1";
						localInfo.Feidanitem1Num = 5;
						localInfo.Hudunitem2Num = 5;
						localInfo.Yinshenitem3Num = 5;
						localInfo.Jiasuitem4Num = 5;

						CarData carData = new CarData ();
						carData.id = "1";
						carData.accLv = 0;
						carData.speedLv = 0;
						carData.handlerLv = 0;
						localInfo.carDatas.Add (carData);
						localInfo.nowCarId = carData.id;

						RoleData roleData = new RoleData ();
						roleData.id = "1";
						roleData.lv = 0;
						localInfo.roleDatas.Add (roleData);
						localInfo.nowRoleId = roleData.id;
						
						//add by maojudong，2015年6月24日16:38:28
						PetData petData = new PetData ();
						petData.id = "1";
						localInfo.petDatas.Add (petData);
						localInfo.nowPetId = petData.id;

						MainState.Instance.playerInfo = localInfo;
						MainState.Instance.SavePlayerData ();
						this.AddChengjiuMissions ();
				} else {
						MainState.Instance.playerInfo = localInfo;
				}
				this.AddLogic<LogicUpdataMissionRichang> (null, this.FinishLogic);
		}

		/// <summary>
		/// Adds the chengjiu missions.
		/// 增加成就任务
		/// </summary>
		void AddChengjiuMissions ()
		{
				List<MissionConfigData> configs = MissionConfigData.GetConfigDatas<MissionConfigData> ();
				for (int i=0; i<configs.Count; i++) {
						if (configs [i].missionType == 2) {
								Hashtable logicPar = new Hashtable ();
								logicPar.Add ("missionId", configs [i].id);
								this.AddLogic<LogicAddMission> (logicPar, null);
						}
				}
		}

		public override void Destroy ()
		{
				localInfo = null;
				base.Destroy ();
		}
}
