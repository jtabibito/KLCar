using UnityEngine;
using System.Collections;

/// <summary>
/// Logic base.
/// 游戏逻辑基类
/// </summary>
public abstract class LogicBase
{

		public string logicTeamId;
		public bool isMain = false;
		public LogicManager.OnLogicOver onLogicOver;

		public abstract void ActLogic (Hashtable logicPar);

		/// <summary>
		/// Destroy this instance.
		/// 销毁逻辑
		/// </summary>
		public virtual void Destroy ()
		{
				this.onLogicOver = null;
		}

		/// <summary>
		/// Raises the logic over event.
		/// 完成逻辑
		/// </summary>
		/// <param name="logicPar">Logic par.</param>
		public void FinishLogic (Hashtable logicPar)
		{
				if (this.onLogicOver != null) {
						this.onLogicOver (logicPar);
				}
				if (this.isMain) {
						LogicManager.Instance.DestroyLogicTeam (this.logicTeamId);
				}
		}


		/// <summary>
		/// Adds the logic.
		/// 增加一个额外的逻辑
		/// </summary>
		/// <param name="logicPar">Logic par.</param>
		/// <param name="onLogicOver">On logic over.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddLogic<T> (Hashtable logicPar, LogicManager.OnLogicOver onLogicOver) where T:LogicBase
		{
				LogicManager.Instance.AddLogic<T> (logicPar, this.logicTeamId, onLogicOver);
		}
}
