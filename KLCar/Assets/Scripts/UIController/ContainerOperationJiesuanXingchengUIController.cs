using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 极限赛--结算界面, 界面显示行程数字
/// create by  maojudong
/// 2015-4-16 16:17:39
/// </summary>
public partial class ContainerOperationJiesuanXingchengUIController : UIControllerBase
{
		// Use this for initialization
		void Start ()
		{
				this.ButtonFenxiang.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFenxiang));
				this.ButtonChongxinkaishi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChongxinkaishi));
				this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));
				
				//从数据库或一个全局变量中得到 
				this.LabelXingchengwenzi.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance.raceDistance.ToString ();
				this.LabelLishichengji.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.historyMaxDistance.ToString();
				
				if(RaceManager.Instance.RaceCounterInstance.raceDistance>MainState.Instance.playerInfo.historyMaxDistance)
				{
					MainState.Instance.playerInfo.historyMaxDistance = RaceManager.Instance.RaceCounterInstance.raceDistance;
				}

				//从数据库或一个全局变量中得到
				//				this.LabelShouji.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounter.gainGoldNum.ToString ();
				//				this.LabelGuoguan.GetComponent<UILabel> ().text = "999";
				
				int collectNum = 0;
				int rewardsNum = 0;
				if(RaceManager.Instance.RaceCounterInstance!=null)
					collectNum = RaceManager.Instance.RaceCounterInstance.gainGoldNum;
				else
					collectNum = 0;
				
				
				DOVirtual.Float(0,collectNum,2,delegate(float value) {
					this.LabelShouji.GetComponent<UILabel> ().text = ((int)value).ToString();
				});
				
				rewardsNum = 1000;
				DOVirtual.Float(0,rewardsNum,2,delegate(float value) {
					this.LabelGuoguan.GetComponent<UILabel> ().text = ((int)value).ToString();
				});

				MainState.Instance.playerInfo.ChangeGold(collectNum);				
				MainState.Instance.playerInfo.ChangeGold(rewardsNum);

				this.transform.localScale = Vector3.zero;
				this.transform.DOScale (Vector3.one, 0.35f).SetEase (Ease.OutBack);

				MainState.Instance.SavePlayerData();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// 分享
		/// </summary>
		void OnClickButtonFenxiang ()
		{
				//还没有开发完成提示框
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
		}

		/// <summary>
		/// 重新开始
		/// </summary>
		void OnClickButtonChongxinkaishi ()
		{
		LogicManager.Instance.ActNewLogic<LogicRestart>(null,null);
//				LogicManager.Instance.ActNewLogic<LogicLeaveRace> (null, null);
		}

		/// <summary>
		/// 返回
		/// </summary>
		void OnClickButtonFanhui ()
		{
				LogicManager.Instance.ActNewLogic<LogicLeaveRace> (null, null);
		}
}
