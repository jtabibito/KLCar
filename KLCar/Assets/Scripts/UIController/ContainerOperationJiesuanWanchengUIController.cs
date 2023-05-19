using UnityEngine;
using System.Collections;
using DG.Tweening;


/// <summary>
/// 剧情模式-任务完成
/// create by  maojudong
/// 2015-4-16 16:11:01
/// </summary>
public partial class ContainerOperationJiesuanWanchengUIController : UIControllerBase
{
		// Use this for initialization
		void Start ()
		{
				this.ButtonFenxiang.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFenxiang));
				this.ButtonChongxinkaishi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChongxinkaishi));
				this.ButtonXiayiguan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonXiayiguan));
				
				//从数据库或一个全局变量中得到
//				this.LabelShouji.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounter.gainGoldNum.ToString ();
//				this.LabelGuoguan.GetComponent<UILabel> ().text = "999";
	
				int collectNum = 0;
				int rewardsNum = 0;
				if(RaceManager.Instance.RaceCounterInstance!=null)
					collectNum = RaceManager.Instance.RaceCounterInstance.gainGoldNum;
				else
					collectNum = 999;


				DOVirtual.Float(0,collectNum,2,delegate(float value) {
					this.LabelShouji.GetComponent<UILabel> ().text = ((int)value).ToString();
				});

				rewardsNum = 999;
				DOVirtual.Float(0,rewardsNum,2,delegate(float value) {
					this.LabelGuoguan.GetComponent<UILabel> ().text = ((int)value).ToString();
				});

				SetTaskData();
	
				this.transform.localScale = Vector3.zero;
				this.transform.DOScale (Vector3.one, 0.35f).SetEase (Ease.OutBack);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void SetTaskData()
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
		/// 下一关
		/// </summary>
		void OnClickButtonXiayiguan ()
		{
			if(MainState.Instance.playerInfo!=null)
			{
				MainState.Instance.playerInfo.missionOfJuqing = RaceManager.Instance.RaceData.raceId;
				MainState.Instance.playerInfo.missionOfPreviousJuqing = "-1";
				MainState.Instance.SavePlayerData();
			}

			LogicManager.Instance.ActNewLogic<LogicNextStory> (null, null);

			//LogicManager.Instance.ActNewLogic<LogicLeaveRace> (null, null);
		}
}
