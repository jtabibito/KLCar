using UnityEngine;
using System.Collections;

namespace MyGameProto
{
		public partial class MyPlayerInfo
		{	
				public bool needUpdate = false;
				public delegate void OnPlayerInfoUpdate ();

				public event OnPlayerInfoUpdate onPlayerInfoUpdate;

				void Update ()
				{
						if (needUpdate && this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}
				}


				/// <summary>
				/// Changes the items.
				/// 改变玩家的道具数
				/// </summary>
				/// <param name="it">It.</param>
				/// <param name="value">Value.</param>
				public void ChangeItems (GameConsts.ItemType it, int value)
				{
						switch (it) {
						case GameConsts.ItemType.IT_Daodan:
								this.Feidanitem1Num += value;
								break;
						case GameConsts.ItemType.IT_Jiasu:
								this.Jiasuitem4Num += value;
								break;
						case GameConsts.ItemType.IT_Yinshen:
								this.Yinshenitem3Num += value;
								break;
						case GameConsts.ItemType.IT_Hudun:
								this.Hudunitem2Num += value;
								break;
						}
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);

				}

				/// <summary>
				/// Changes the power.
				/// 改变玩家的体力值(爱心)
				/// </summary>
				/// <param name="vlaue">Vlaue.</param>
				public void ChangePower (int value)
				{
						this.power += value;
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);


						Hashtable logicPar = new Hashtable ();
						logicPar.Add ("determinePoint", "5");
						logicPar.Add ("changePower", value);
						LogicManager.Instance.ActNewLogic<LogicCheckMission> (logicPar, null);

						if (this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}
				}

				/// <summary>
				/// Changes the gold.
				/// 改变玩家的金币数
				/// </summary>
				/// <param name="value">Value.</param>
				public void ChangeGold (int value)
				{
						this.gold += value;
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);

						Hashtable logicPar = new Hashtable ();
						logicPar.Add ("determinePoint", "2");
						logicPar.Add ("changeGold", value);
						LogicManager.Instance.ActNewLogic<LogicCheckMission> (logicPar, null);

						if (this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}
						
				}

				/// <summary>
				/// Changes the diamond.
				/// 改变玩家的钻石数
				/// </summary>
				/// <param name="value">Value.</param>
				public void ChangeDiamond (int value)
				{
						this.diamond += value;
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);
			
						Hashtable logicPar = new Hashtable ();
						logicPar.Add ("determinePoint", "3");
						logicPar.Add ("changeDiamond", value);
						LogicManager.Instance.ActNewLogic<LogicCheckMission> (logicPar, null);

						if (this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}
						
				}

				/// <summary>
				/// Changes the score.
				/// 改变玩家的积分数
				/// </summary>
				/// <param name="value">Value.</param>
				public void ChangeScore (int value)
				{
						this.score += value;
						LogicManager.Instance.ActNewLogic<LogicStoreData> (null, null);
			
						Hashtable logicPar = new Hashtable ();
						logicPar.Add ("determinePoint", "4");
						logicPar.Add ("changeScore", value);
						LogicManager.Instance.ActNewLogic<LogicCheckMission> (logicPar, null);

						if (this.onPlayerInfoUpdate != null) {
								this.onPlayerInfoUpdate ();
						}

				}
		}

}