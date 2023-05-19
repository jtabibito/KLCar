using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 任务容器UI控制代码
/// 2015年5月28日10:45:54
/// </summary>
public partial class ContainerRenwuUIController : UIControllerBase
{
	private List<GameObject>  everyDayTaskItemList = new List<GameObject> ();
	private List<GameObject>  lifeTaskItemList = new List<GameObject> ();
	private GameObject everyDayTaskContainer = null;
	private GameObject lifeTaskContainer = null;
	private GameObject taskObjItem = null;

	private int  taskCheckTime = 6;

	// Use this for initialization
	void Start ()
	{
		this.ButtonGuanbi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonGuanbi));
		InitUIAnim ();
		InitTaskItem ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateLeftButtonTips ();
	}

	void InitUIAnim ()
	{
		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.25f).SetEase (Ease.OutBack);
	}

	void InitTaskItem ()
	{
		//0. Init Task Item Container
		this.everyDayTaskContainer = this.Container1Richangrenwu.transform.FindChild ("ScrollView/Grid").gameObject;
		this.lifeTaskContainer = this.Container2Shengyarenwu.transform.FindChild ("ScrollView/Grid").gameObject;

		//1. Clear List
		this.everyDayTaskItemList.Clear ();
		this.lifeTaskItemList.Clear ();

//		Debug.Log(MainState.Instance.playerInfo.missionOfRichang.Count);
//		Debug.Log(MainState.Instance.playerInfo.missionOfChengjiu.Count)

		int dateTaskCount = MainState.Instance.playerInfo != null ? MainState.Instance.playerInfo.missionOfRichang.Count : 0;
		int lifeTaskCount = MainState.Instance.playerInfo != null ? MainState.Instance.playerInfo.missionOfChengjiu.Count : 0;

		//2. 暂时确定是5个日常任务
		int i = 0;
		for (i=0; i<dateTaskCount; i++) {
			string taskId = MainState.Instance.playerInfo.missionOfRichang [i].id;
			int taskState = MainState.Instance.playerInfo.missionOfRichang [i].state;
			int taskProgress = MainState.Instance.playerInfo.missionOfRichang [i].savePar;

			MissionConfigData data = MissionConfigData.GetConfigData<MissionConfigData> (taskId);
			if (data != null) {
				GameObject item = NGUITools.AddChild (this.everyDayTaskContainer, GameResourcesManager.GetUIPrefab ("SpriteRichangrenwutiao"));
				SpriteRichangrenwutiaoUIController itemController = item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ();

				itemController.SetTaskId (taskId);
				itemController.SetTaskType (SpriteRichangrenwutiaoUIController.UITaskType.EveryDayTask);
				//任务状态--三种状态
				if (taskState == 0) {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskInProgress);
				} else if (taskState == 1) {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskRewards);
				} else {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskComplete);
				}

				//item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ().SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskRewards);

				//任务名称
				itemController.SetTaskName (data.missionName);
				//任务描述
				itemController.SetTaskDescriptionName (data.missionDescription);
				//任务奖励

				//任务奖励多种类型,需要新加判断
				itemController.SetTaskRewardCoin (data.rewardValue.ToString());

				//任务进度
				itemController.SetTaskProgress (taskProgress, data.showPar);
				this.everyDayTaskItemList.Add (item);

				//为 “点击领取” 添加 OnClick事件
				UIEventListener.Get (item).onPress = this.OnPressButtonTask;
			}
		}

		//3. 成就任务--生涯任务
		for (i=0; i<lifeTaskCount; i++) {
			string taskId = MainState.Instance.playerInfo.missionOfChengjiu [i].id;
			int taskState = MainState.Instance.playerInfo.missionOfChengjiu [i].state;
			int taskProgress = MainState.Instance.playerInfo.missionOfChengjiu [i].savePar;
			MissionConfigData data = MissionConfigData.GetConfigData<MissionConfigData> (taskId);

			if (data != null) {
				GameObject item = NGUITools.AddChild (this.lifeTaskContainer, GameResourcesManager.GetUIPrefab ("SpriteRichangrenwutiao"));
				SpriteRichangrenwutiaoUIController itemController = item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ();

				itemController.SetTaskId (taskId);
				itemController.SetTaskType (SpriteRichangrenwutiaoUIController.UITaskType.LifeTask);
				//任务状态--三种状态
				if (taskState == 0) {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskInProgress);
				} else if (taskState == 1) {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskRewards);
				} else {
					itemController.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskComplete);
				}
							
				//任务名称
				itemController.SetTaskName (data.missionName);
				//任务描述
				itemController.SetTaskDescriptionName (data.missionDescription);

				//任务奖励多种类型,需要新加判断
				itemController.SetTaskRewardCoin (data.rewardValue.ToString());

				//任务进度
				itemController.SetTaskProgress (taskProgress, data.showPar);
				this.lifeTaskItemList.Add (item);
				//为 “点击领取” 添加 OnClick事件
				UIEventListener.Get (item).onPress = this.OnPressButtonTask;
			}
		}
	}

	/// <summary>
	/// 动态检测任务界面上的小红色Tips图片，是否显示
	/// </summary>
	void UpdateLeftButtonTips ()
	{
		if (MainState.Instance.playerInfo != null) {
			if (taskCheckTime > 5) {
				int dateTaskCount = MainState.Instance.playerInfo != null ? MainState.Instance.playerInfo.missionOfRichang.Count : 0;
				int juqingTaskCount = MainState.Instance.playerInfo != null ? MainState.Instance.playerInfo.missionOfChengjiu.Count : 0;
				bool findFlag = false;		
					
				//a
				int i = 0;
				for (i=0; i<dateTaskCount; i++) {
					int taskState = MainState.Instance.playerInfo.missionOfRichang [i].state;
					if (taskState == 1) {
						findFlag = true;
						break;
					}
				}
				NGUITools.SetActive (this.SpriteTishidiandian1, findFlag);

				//b
				findFlag = false;
				for (i=0; i<juqingTaskCount; i++) {
					int taskState = MainState.Instance.playerInfo.missionOfChengjiu [i].state;
					if (taskState == 1) {
						findFlag = true;
						break;
					}
				}
				NGUITools.SetActive (this.SpriteTishidiandian2, findFlag);
				//c
				taskCheckTime = 0;
			} else {
				taskCheckTime++;
			}
		}
	}

	void OnPressButtonTask (GameObject obj, bool state)
	{
		if (state == false && obj != null) {
			SpriteRichangrenwutiaoUIController itemComp = obj.GetComponent<SpriteRichangrenwutiaoUIController> ();
			if (itemComp != null) {
				if (itemComp.taskStatus == SpriteRichangrenwutiaoUIController.UITaskStatus.TaskRewards) {
					//奖励任务--日常任务
					if (itemComp.taskType == SpriteRichangrenwutiaoUIController.UITaskType.EveryDayTask) {
						//LogicManager.Instance.AddLogic<LogicFinishMission>(null,null,this.onRewardsFinish);
						Debug.Log ("Start rewards.............");
						taskObjItem = obj;
						Hashtable newLogicPar = new Hashtable ();
						newLogicPar.Add ("missionId", itemComp.GetTaskId ());
						LogicManager.Instance.ActNewLogic<LogicFinishMission> (newLogicPar, this.onRewardsFinish);
					} else {
						//.............
					}

				}

			}
		}
	}

	/// <summary>
	/// 完成奖励任务的回调函数-------修改状态为"已经完成"
	/// </summary>
	void onRewardsFinish (Hashtable logicPar)
	{
		if (taskObjItem != null) {
			SpriteRichangrenwutiaoUIController itemComp = taskObjItem.GetComponent<SpriteRichangrenwutiaoUIController> ();
			itemComp.SetTaskStatus (SpriteRichangrenwutiaoUIController.UITaskStatus.TaskComplete);
			//但是不可以设置Button为Disable状态，否则导致ScrollView不能滑动
			taskObjItem = null;
			Debug.Log ("End rewards.............");
		}
	}
	
	/// <summary>
	/// 关闭
	/// </summary>
	void OnClickButtonGuanbi ()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack);
	}
}

