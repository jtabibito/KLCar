using UnityEngine;
using System.Collections;
using MyGameProto;

public class MainState
{
		static MainState instance;

		public static MainState Instance {
				get {
						if (instance == null) {
								instance = new MainState ();
						}
						return instance;
				}
		}
	
		public bool isRun = false;//游戏开始标记
		public MyPlayerInfo playerInfo;//玩家数据


		private bool _netSupport = false;			//是否有网络支持
		/// <summary>
		/// 是否有网络支持
		/// </summary>
		/// <value><c>true</c> if net support; otherwise, <c>false</c>.</value>
		public bool netSupport {
				get {
						if (Application.internetReachability == NetworkReachability.NotReachable)
								_netSupport = false;
						else 
								_netSupport = true;

						return _netSupport;
				}
		}

		/// <summary>
		/// Saves the player data.
		/// 存储玩家数据
		/// </summary>
		public void SavePlayerData ()
		{
				if (this.playerInfo != null) {
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);
				}
		}
}
