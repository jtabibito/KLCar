using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Logic manager.
/// 游戏逻辑管理器
/// </summary>
public class LogicManager
{
		static LogicManager instance;

		public static LogicManager Instance {
				get {
						if (instance == null) {
								instance = new LogicManager ();
						}
						return instance;
				}
		}

		int logicTeamIdCount = 0;

		public int LogicTeamIdCount {
				get {
						logicTeamIdCount += 1;
						return logicTeamIdCount;
				}
		}

		Dictionary<string,List<LogicBase>> logicDic = new Dictionary<string, List<LogicBase>> ();

		public delegate void OnLogicOver (Hashtable logicPar);

		public delegate void OnLogicContinue (Hashtable logicPar);

		public delegate void OnSendLogicCommand (LogicCommand lc,Hashtable logicPar,OnLogicContinue onLogicContinue);

		public event OnSendLogicCommand onSendLogicCommand;

		/// <summary>
		/// Acts the new logic.
		/// 执行一个新的主逻辑
		/// </summary>
		/// <param name="logicPar">Logic par.</param>
		/// <param name="onLogicOver">On logic over.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void ActNewLogic<T> (Hashtable logicPar, OnLogicOver onLogicOver) where T:LogicBase
		{
				T newLogic = Activator.CreateInstance<T> ();
				newLogic.logicTeamId = LogicTeamIdCount.ToString ();
				newLogic.isMain = true;
				newLogic.onLogicOver = onLogicOver;
				List<LogicBase> logics = new List<LogicBase> ();
				logics.Add (newLogic);
				this.logicDic.Add (newLogic.logicTeamId, logics);
				newLogic.ActLogic (logicPar);
		}

	
		/// <summary>
		/// Adds the logic.
		/// 在逻辑运作时增加一个额外的逻辑,仅由逻辑对象自身调用
		/// </summary>
		/// <param name="logicPar">Logic par.</param>
		/// <param name="logicTeamId">Logic team identifier.</param>
		/// <param name="onLogicOver">On logic over.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddLogic<T> (Hashtable logicPar, string logicTeamId, OnLogicOver onLogicOver) where T:LogicBase
		{
				T newLogic = Activator.CreateInstance<T> ();
				newLogic.logicTeamId = logicTeamId;
				newLogic.onLogicOver = onLogicOver;
				List<LogicBase> logics = this.logicDic [logicTeamId];
				logics.Add (newLogic);
				newLogic.ActLogic (logicPar);
		}

		/// <summary>
		/// Destroies the logic team.
		/// 在主逻辑结束时销毁一个逻辑组
		/// </summary>
		/// <param name="logicTeamId">Logic team identifier.</param>
		public void DestroyLogicTeam (string logicTeamId)
		{
				List<LogicBase> logics = this.logicDic [logicTeamId];
				foreach (LogicBase lb in logics) {
						lb.Destroy ();
				}
				this.logicDic.Remove (logicTeamId);
		}

		/// <summary>
		/// 发送逻辑命令
		/// </summary>
		/// <param name="lc">Lc.逻辑命令</param>
		/// <param name="logicPar">Logic par.逻辑参数</param>
		/// <param name="onLogicContinue">On logic continue.逻辑继续的代理</param>
		public void SendLogicCommand (LogicCommand lc, Hashtable logicPar, OnLogicContinue onLogicContinue)
		{
				if (this.onSendLogicCommand != null) {
						this.onSendLogicCommand (lc, logicPar, onLogicContinue);
				}
		}
}
