using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 游戏大厅主界面UI 控制代码
/// 2015年4月21日19:42:51
/// </summary>
public partial class ContainerMainzhujiemianUIController : UIControllerBase
{
	private Vector3  UIButtonOutPosition = new Vector3 (0, Screen.height, 0) ;
	private GameObject uiShow2D = null;
	private string 	   currentCarPrefebName;
	private string 	   currentRolePrefebName;
	private bool  isDraging = false;
	private float dragSum = 0.0f;
	private List<CarConfigData>  carConfigDataList = null;
	private List<RoleConfigData>  roleConfigDataList = null;

	//日常任务 + 系统任务
	private int  taskCheckTime = 6;

	// Use this for initialization 
	void Start ()
	{
		InitButtonEvent ();
		UpdateTopStatusBar ();
		InitBottomUIButtonAnim ();
		InitCarAndStartAnimation ();
		CheckDailyLoginRewardsUI ();
	}


	// Update is called once per frame
	void Update ()
	{
		UpdateTopStatusBar ();
	}

	void InitButtonEvent ()
	{
		this.ButtonCheku.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonCheku));					//车
		this.ButtonJuese.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJuese));					//角色
		this.ButtonChongwu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChongwu));				//宠物
			
		this.ButtonTiaozhansai.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonTiaozhansai));		//挑战赛
		this.ButtonXiuxianmoshi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonXiuxianmoshi));		//休闲模式
		this.ButtonGushimoshi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonGushimoshi));			//故事模式
			
		this.ButtonHaoyou.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonHaoyou));					//好友
		this.ButtonJingcaigonggao.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJingcaigonggao));	//精彩公告
		this.ButtonChoujiang.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChoujiang));			//抽奖
		this.ButtonYoujian.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYoujian));				//邮件
		this.ButtonRenwu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonRenwu));					//任务
			
		this.ButtonShezhi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShezhi));					//设置
		this.ButtonJiahaobi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaobi));				//加金币
		this.ButtonJiahaozuan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaozuan));			//加钻石
		this.ButtonJiahaoxin.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaoxin));			//好心？？
		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));		//更好头像

		UIEventListener.Get (this.ContainerHuadong).onPress = this.OnPressButtonDrag;											//记录按下和抬起
		UIEventListener.Get (this.ContainerHuadong).onDrag = OnDragUI;															/// 初始化滚动事件，车辆，宠物，角色等信息进行旋转

	}
		
	/// <summary>
	/// 头像,名称，好心，金币，钻石个数，邮件个数，任务个数------从数据层获得
	/// </summary>
	void UpdateTopStatusBar ()
	{
		//1.头像---后期用表格保存图片与ID的对应关系
		if (MainState.Instance.playerInfo != null) {
			string xx = MainState.Instance.playerInfo.userRoleImgID < 10 ? ("0" + MainState.Instance.playerInfo.userRoleImgID) : MainState.Instance.playerInfo.userRoleImgID.ToString ();
			this.SpriteTouxiang.GetComponent<UISprite> ().spriteName = "ui_role_" + xx.ToString ();
		}

		//2.昵称
		if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nickname.Length > 0) {
			this.LabelMingzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.nickname;
		}
			
		//3.好心
		if (MainState.Instance.playerInfo != null) {
			this.LabelXinshuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.power.ToString ();
		}
		//4. 金币
		if (MainState.Instance.playerInfo != null) {
			this.LabelBishuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.gold.ToString ();
		}
		//5. 钻石
		if (MainState.Instance.playerInfo != null) {
			this.LabelZuanshuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.diamond.ToString ();
		}

		//6. 邮件个数
		if (MainState.Instance.playerInfo != null) {
			//int count = MainState.Instance.playerInfo.mi
		}

		//7. 任务个数---每5帧检测一次
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

				//b
				if (findFlag == false) {
					for (i=0; i<juqingTaskCount; i++) {
						int taskState = MainState.Instance.playerInfo.missionOfChengjiu [i].state;
						if (taskState == 1) {
							findFlag = true;
							break;
						}
					}
				}
					
				//c
				NGUITools.SetActive (this.SpriteTishixiaodiandian2, findFlag);
				//d
				taskCheckTime = 0;
			}
			else
			{
				taskCheckTime++;
			}
		}
	}

	void  InitBottomUIButtonAnim ()
	{
//		this.ButtonCheku.transform.transform.localPosition = UIButtonOutPosition;
//		this.ButtonJuese.transform.transform.localPosition = UIButtonOutPosition;
//		this.ButtonChongwu.transform.transform.localPosition = UIButtonOutPosition;
//		this.ButtonTiaozhansai.transform.transform.localPosition = UIButtonOutPosition;
//		this.ButtonXiuxianmoshi.transform.transform.localPosition = UIButtonOutPosition;
//		this.ButtonGushimoshi.transform.transform.localPosition = UIButtonOutPosition;
//			
//		this.ButtonCheku.transform.DOLocalMove (this.UIOriginalPositionButtonCheku, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);
//		this.ButtonJuese.transform.DOLocalMove (this.UIOriginalPositionButtonJuese, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);
//		this.ButtonChongwu.transform.DOLocalMove (this.UIOriginalPositionButtonChongwu, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);
//		this.ButtonTiaozhansai.transform.DOLocalMove (this.UIOriginalPositionButtonTiaozhansai, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);
//		this.ButtonXiuxianmoshi.transform.DOLocalMove (this.UIOriginalPositionButtonXiuxianmoshi, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);
//		this.ButtonGushimoshi.transform.DOLocalMove (this.UIOriginalPositionButtonGushimoshi, 0.8f, true).SetEase (Ease.OutBounce).SetUpdate(true);

		//bottom to up
		this.ButtonCheku.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonCheku.x, UIOriginalPositionButtonCheku.y+60, UIOriginalPositionButtonCheku.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonCheku.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonCheku.x, UIOriginalPositionButtonCheku.y, UIOriginalPositionButtonCheku.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonJuese.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJuese.x, UIOriginalPositionButtonJuese.y+60, UIOriginalPositionButtonJuese.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonJuese.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJuese.x, UIOriginalPositionButtonJuese.y, UIOriginalPositionButtonJuese.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonChongwu.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonChongwu.x, UIOriginalPositionButtonChongwu.y+60, UIOriginalPositionButtonChongwu.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonChongwu.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonChongwu.x, UIOriginalPositionButtonChongwu.y, UIOriginalPositionButtonChongwu.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonTiaozhansai.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonTiaozhansai.x, UIOriginalPositionButtonTiaozhansai.y+60, UIOriginalPositionButtonTiaozhansai.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonTiaozhansai.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonTiaozhansai.x, UIOriginalPositionButtonTiaozhansai.y, UIOriginalPositionButtonTiaozhansai.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonXiuxianmoshi.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXiuxianmoshi.x, UIOriginalPositionButtonXiuxianmoshi.y+60, UIOriginalPositionButtonXiuxianmoshi.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonXiuxianmoshi.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXiuxianmoshi.x, UIOriginalPositionButtonXiuxianmoshi.y, UIOriginalPositionButtonXiuxianmoshi.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonGushimoshi.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonGushimoshi.x, UIOriginalPositionButtonGushimoshi.y+60, UIOriginalPositionButtonGushimoshi.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonGushimoshi.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonGushimoshi.x, UIOriginalPositionButtonGushimoshi.y, UIOriginalPositionButtonGushimoshi.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		//left to right
		this.ButtonYoujian.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonYoujian.x+60, UIOriginalPositionButtonYoujian.y, UIOriginalPositionButtonYoujian.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonYoujian.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonYoujian.x, UIOriginalPositionButtonYoujian.y, UIOriginalPositionButtonYoujian.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.SpriteTishixiaodiandian1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteTishixiaodiandian1.x+60, UIOriginalPositionSpriteTishixiaodiandian1.y, UIOriginalPositionSpriteTishixiaodiandian1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteTishixiaodiandian1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteTishixiaodiandian1.x, UIOriginalPositionSpriteTishixiaodiandian1.y, UIOriginalPositionSpriteTishixiaodiandian1.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonRenwu.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonRenwu.x+60, UIOriginalPositionButtonRenwu.y, UIOriginalPositionButtonRenwu.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonRenwu.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonRenwu.x, UIOriginalPositionButtonRenwu.y, UIOriginalPositionButtonRenwu.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.SpriteTishixiaodiandian2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteTishixiaodiandian2.x+60, UIOriginalPositionSpriteTishixiaodiandian2.y, UIOriginalPositionSpriteTishixiaodiandian2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteTishixiaodiandian2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteTishixiaodiandian2.x, UIOriginalPositionSpriteTishixiaodiandian2.y, UIOriginalPositionSpriteTishixiaodiandian2.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		//right to left
		this.ButtonChoujiang.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonChoujiang.x-60, UIOriginalPositionButtonChoujiang.y, UIOriginalPositionButtonChoujiang.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonChoujiang.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonChoujiang.x, UIOriginalPositionButtonChoujiang.y, UIOriginalPositionButtonChoujiang.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonJingcaigonggao.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJingcaigonggao.x-60, UIOriginalPositionButtonJingcaigonggao.y, UIOriginalPositionButtonJingcaigonggao.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonJingcaigonggao.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJingcaigonggao.x, UIOriginalPositionButtonJingcaigonggao.y, UIOriginalPositionButtonJingcaigonggao.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});

		this.ButtonHaoyou.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonHaoyou.x-60, UIOriginalPositionButtonHaoyou.y, UIOriginalPositionButtonHaoyou.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonHaoyou.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonHaoyou.x, UIOriginalPositionButtonHaoyou.y, UIOriginalPositionButtonHaoyou.z), 0.55f, true)
					.SetEase (Ease.OutBounce);
			});
	}
	
	void InitCarAndStartAnimation ()
	{
		this.carConfigDataList = CarConfigData.GetConfigDatas<CarConfigData> ();
		this.roleConfigDataList = RoleConfigData.GetConfigDatas<RoleConfigData> ();

		//从数据层得到参数，暂时手动修改
		if (MainState.Instance.playerInfo != null) {
			//nowCarId下标是从1开始的，而carConfigDataList是从0开始的
			currentCarPrefebName = this.carConfigDataList [int.Parse (MainState.Instance.playerInfo.nowCarId) - 1].carAvt;
			currentRolePrefebName = this.roleConfigDataList [int.Parse (MainState.Instance.playerInfo.nowRoleId) - 1].roleAvt;
		} else {
			currentCarPrefebName = "CarAvt6";
			currentRolePrefebName = "RoleAvt7";
		}

		this.uiShow2D = NGUITools.AddChild (null, GameResourcesManager.GetUIPrefab (UIControllerConst.UIPrefebUIShow2D));
		this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CreateCarRole (currentCarPrefebName, currentRolePrefebName);
		//this.uiShow2D.GetComponent<ui_show_3DUIController> ().CreateCarRole (currentCarPrefebName, currentRolePrefebName);
		this.uiShow2D.GetComponent<ui_show_2DUIController> ().StartCarAnim ();
	}

	void  Clean2DCarRole ()
	{
		if (this.uiShow2D != null) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CleanAllAvt ();
			this.uiShow2D = null;
		}
	}

	private List<DailyLogInConfigData> dataList = null;
	private DailyLogInConfigData data;
	private List<int> daysTypeList = new List<int> ();
	private List<int> daysValueList = new List<int> ();

	/// <summary>
	/// 检测每日登陆UI是否显示
	/// </summary>
	void CheckDailyLoginRewardsUI ()
	{
		if (MainState.Instance.playerInfo != null) {
			this.dataList = DailyLogInConfigData.GetConfigDatas<DailyLogInConfigData> ();
			if (this.dataList.Count > 0)
				this.data = this.dataList [0];

			this.daysTypeList.Clear ();
			this.daysTypeList.Add (this.data.day1type);
			this.daysTypeList.Add (this.data.day2type);
			this.daysTypeList.Add (this.data.day3type);
			this.daysTypeList.Add (this.data.day4type);
			this.daysTypeList.Add (this.data.day5type);
			this.daysTypeList.Add (this.data.day6type);
			this.daysTypeList.Add (this.data.day7type);
					
			this.daysValueList.Clear ();
			this.daysValueList.Add (this.data.day1value);
			this.daysValueList.Add (this.data.day2value);
			this.daysValueList.Add (this.data.day3value);
			this.daysValueList.Add (this.data.day4value);
			this.daysValueList.Add (this.data.day5value);
			this.daysValueList.Add (this.data.day6value);
			this.daysValueList.Add (this.data.day7value);

			//得到之前已经连续登陆的天数
			int count = MainState.Instance.playerInfo.dailyLoginRewardsCount;

			//				用户首次登陆或者清空了缓存-------后续一直累加，用%取余数
			//				if(count==0)
			//				{
			//					SetDailyLoginRewardsData(0);
			//				}else 
			if (count >= 1 && count <= 6) {				//1~6
				//判断是否为‘连续登陆，或已经领取过 用tick判断--------分三种情况;或者直接用TimeSpan的TotalDay进行判断
				System.DateTime oldDT = new System.DateTime (MainState.Instance.playerInfo.dailyLoginRewardsTicks);
				System.DateTime oldDTMax = new System.DateTime (oldDT.Year, oldDT.Month, oldDT.Day, 23, 59, 59, 999, System.DateTimeKind.Local);

				long delta = System.DateTime.Now.Ticks - oldDTMax.Ticks;
				System.TimeSpan deltaTP = new System.TimeSpan (delta);
				double deltaDay = deltaTP.TotalDays;

				if (deltaDay > 0 && deltaDay <= 1) {					//连续登陆了，继续下一个
					SetDailyLoginRewardsData (count);
				} else  if (deltaDay > 1) {                  			//未连续登陆----只显示第1天的登陆，重新开始
					SetDailyLoginRewardsData (0);
				}
				//				else if(deltaDay<=0)					//同一天已经领取过了--不做任何处理
				//				{}
					
			} else { 		//	>=7 ，已经连续登陆7天，重新从第1天开始 / 首次登陆count=0
				SetDailyLoginRewardsData (0);
			}
		}
	}
		
	/// <summary>
	/// 当前是第几天
	/// </summary>
	/// <param name="index">玩家已经连续登陆的天数，有效范围0~7</param>
	void SetDailyLoginRewardsData (int count)
	{
		if (count < 0 || count > 7) {
			Debug.LogWarning ("Out of range ,validge is [0,7],Please check this code.");
			return;
		}

		//1. 给予奖励,增加金币或者宝石
		if (this.daysTypeList [count] == 1)
			MainState.Instance.playerInfo.gold += this.daysValueList [count];
		else
			MainState.Instance.playerInfo.diamond += this.daysValueList [count];
			
		//2. 连续领取次数更新
		MainState.Instance.playerInfo.dailyLoginRewardsCount = count + 1;					
		MainState.Instance.playerInfo.dailyLoginRewardsTicks = System.DateTime.Now.Ticks;
		//3. 保存数据
		MainState.Instance.SavePlayerData ();
		//4. 弹出界面 并设置 当前是第几天
		GameObject obj = PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebMeiridenglu);
		obj.AddMissingComponent<ContainerMeiridengluUIController> ().SetDailyRewardsDays (MainState.Instance.playerInfo.dailyLoginRewardsCount);
	}

	/// <summary> 
	/// 车库
	/// </summary>
	void OnClickButtonCheku ()
	{
		this.Clean2DCarRole ();
		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebCheliang);
	}

	/// <summary>
	/// 角色
	/// </summary>
	void OnClickButtonJuese ()
	{
		this.Clean2DCarRole ();
		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebJuese);
	}

	/// <summary>
	/// 宠物
	/// </summary>
	void OnClickButtonChongwu ()
	{
		this.Clean2DCarRole ();
		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebChongwu);
	}

	/// <summary>
	/// 挑战赛
	/// </summary>
	void OnClickButtonTiaozhansai ()
	{
		//this.Clean2DCarRole ();
		//还没有开发完成提示框
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
	}

	/// <summary>
	/// 休闲模式  ContainerMoshixuanze
	/// </summary>
	void OnClickButtonXiuxianmoshi ()
	{
		this.Clean2DCarRole ();
		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebMoShixuanze);
	}

	/// <summary>
	/// 故事模式
	/// </summary>
	void OnClickButtonGushimoshi ()
	{
		this.Clean2DCarRole ();
		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebStory);
	}

	/// <summary>
	/// 好友
	/// </summary>
	void OnClickButtonHaoyou ()
	{
		//还没有开发完成提示框
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
	}
		
	/// <summary>
	/// 精彩公告
	/// </summary>
	void OnClickButtonJingcaigonggao ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebGonggao);
	}
		
	/// <summary>
	/// 抽奖
	/// </summary>
	void OnClickButtonChoujiang ()
	{
		if(MainState.Instance.playerInfo!=null)
		{
			MainState.Instance.playerInfo.ChangeGold (9999);
			MainState.Instance.playerInfo.ChangeDiamond (9999);
			MainState.Instance.playerInfo.ChangePower(9999);
		}
		//LogicManager.Instance.ActNewLogic<LogicLeaveRace> (null, null);
		//还没有开发完成提示框
//				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
	}
		
	/// <summary>
	/// 邮件
	/// </summary>
	void OnClickButtonYoujian ()
	{
		//还没有开发完成提示框
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebYoujian);
	}
		
	/// <summary>
	/// 任务
	/// </summary>
	void OnClickButtonRenwu ()
	{
		//还没有开发完成提示框
		//PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTask);
	}
		
	/// <summary>
	/// 设置
	/// </summary>
	void OnClickButtonShezhi ()
	{
//				还没有开发完成提示框
		//PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTankuang);
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebShezhi);
	}
		
	/// <summary>
	/// 加金币
	/// </summary>
	void OnClickButtonJiahaobi ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}

	/// <summary>
	/// 加钻石
	/// </summary>
	void OnClickButtonJiahaozuan ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}

	/// <summary>
	/// 加好心
	/// </summary>
	void OnClickButtonJiahaoxin ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopPower;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}

	/// <summary>
	/// 更换头像
	/// </summary>
	void OnClickSpriteTouxiang ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTouxiang);
	}

	void OnDragUI (GameObject obj, Vector2 delta)
	{
		if (isDraging == true) {
			//由于delta参数的单位 是 屏幕坐标像素，但是采用FixedSize NGUI缩放策略，图片的大小是不缩放的大小，所以需要自己进行处理
			float afterW = Screen.width * this.ContainerHuadong.GetComponent<UIWidget> ().width / 1280.0f;
			//string debugfelix = "滑动 x = " + delta.x + "  宽度 Width" + afterW + "  Total" +UICamera.currentTouch.totalDelta + "dragSum " + dragSum;
						
			//GameObject obj11 = this.transform.FindChild ("LabelDebug").gameObject;
			//obj11.GetComponent<UILabel>().text = debugfelix;

			//Debug.Log (debugfelix);
			//处理快速滑动导致的无滑动效果,||(Mathf.Abs (dragSum) < afterW && (Mathf.Abs (dragSum+delta.x)>= afterW)) 
			if (Mathf.Abs (delta.x) >= afterW) {
				//Debug.Log("进入特效");
				if (delta.x > 0)
					this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (Random.Range (15 * afterW / 360.0f, 65 * afterW / 360.0f), afterW, false);
				else
					this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (Random.Range (-15 * afterW / 360.0f, -65 * afterW / 360.0f), afterW, false);
								
				//obj11.GetComponent<UILabel>().text = debugfelix + "进入特殊处理" ;
			} else {
				dragSum += delta.x;
				//this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (delta.x, afterW);
				//只旋转360度
				if (Mathf.Abs (dragSum) <= afterW) {
					//已经旋转了flag
					this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (delta.x, afterW, false);
					//obj11.GetComponent<UILabel>().text = debugfelix + "进入旋转处理" ;
				} else {
					//只旋转360度，并不在接受事件了
					//obj11.GetComponent<UILabel>().text = debugfelix + "越界了不旋转" ;
				}
			}
		}
	}
	
//		void OnDragUI (GameObject obj, Vector2 delta)
//		{
//				//由于delta参数的单位 是 屏幕坐标像素，但是采用FixedSize NGUI缩放策略，图片的大小是不缩放的大小，所以需要自己进行处理
//				float afterW = Screen.width * this.ContainerHuadong.GetComponent<UIWidget> ().width / 1280.0f;
//
//				Debug.Log ("滑动 x = " + delta.x + "  宽度 Width" + afterW + "  Total" +UICamera.currentTouch.totalDelta);
//				if (isDraging == true) {
//				
//						dragSum += delta.x;
//						//只旋转360度
//						if (Mathf.Abs (dragSum) <= afterW)
//							this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (delta.x, afterW);
//				}
//		}
		
	void OnPressButtonDrag (GameObject go, bool state)
	{
		isDraging = state;
		dragSum = 0.0f;

		if (state == true) {
			//Debug.Log ("按下" + state);
		} else {
			//Debug.Log ("抬起\n\n\n\n\n\n  " + state);
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCarRole (0, 0, true);
		}
	}

}
