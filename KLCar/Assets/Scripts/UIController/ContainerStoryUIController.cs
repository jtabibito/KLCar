using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 故事模式UI控制代码
/// 2015年5月5日15:24:27
/// </summary>
public partial class ContainerStoryUIController : UIControllerBase
{
	//过关/new/锁 是不能同时显示的， SpriteGuoguan New SpriteSuo
	
	private	GameObject gridObj;
	private	UIScrollView scrollview = null;
	private	UIGrid 		grid;
		
	///
	private int mIntLevelButtonCheckBoxGroup = 0;
	private	int pageIndex = 0;										//左右箭头 用到的Index
	private	int pageCount = 0;										//篇章个数
	
	///已经通过关卡的计数器
	private int passLevelMax = 0;
	private string  taskID = "";

	private RaceConfigData  raceConfigData = null;
	private StoryConfigData storyConfigData = null;

	// 显示New 关卡图标的图标对象
	private UIToggle newToggle = null;								//no use
	private List<GameObject> activeButtonList = new List<GameObject> ();

	//List中的index值 从0开始
	private int  selectIndex = -1;
	private string oldID = "-1";
	private List<StoryConfigData> storyConfigDataList = null;

	//
	Tweener newTween = null;

	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitUIStartAnimation ();

		InitConfigData();

		InitScrollViewParam ();
		InitScrollViewButtonEvent ();
		InitPageIndex ();
		InitLevelStatus ();
		InitScrollViewOffset (this.pageIndex);
		UpdateTopStatusBar ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTopStatusBar ();
	}

	void InitButtonEvent ()
	{
		this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));						//返回
		this.ButtonJiahaobi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaobi));					//加金币
		this.ButtonJiahaoxin.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaoxin));				//加好心
		this.ButtonJiahaozuan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaozuan));				//加砖石
		this.ButtonJixu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJixu));							//继续---开始游戏

		this.ButtonYou.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYou));							//右箭头
		this.ButtonZuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZuo));							//左箭头
		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));			//更换头像

	}

	void InitUIStartAnimation ()
	{
		this.ButtonZuo.transform.DOLocalMoveX (UIOriginalPositionButtonZuo.x - 17, 1.5f).SetLoops (-1).SetEase (Ease.OutBounce);
		this.ButtonYou.transform.DOLocalMoveX (UIOriginalPositionButtonYou.x + 17, 1.5f).SetLoops (-1).SetEase (Ease.OutBounce);

		UIWidget rootWidget = GetComponent<UIWidget>();
		DOVirtual.Float(0.313f,1,1,delegate(float value) {
			rootWidget.alpha = value;
		}).SetEase(Ease.Linear).SetUpdate(true);
	}

	void InitConfigData()
	{
		this.storyConfigDataList = StoryConfigData.GetConfigDatas<StoryConfigData>();
	}

	/// <summary>
	/// 初始化一些ScrollView的变量
	/// </summary>
	void InitScrollViewParam ()
	{
		scrollview = this.ScrollView.GetComponent<UIScrollView> ();
		gridObj = this.ScrollView.transform.FindChild ("Grid").gameObject;		//FIX ME,暂时这么做
		grid = gridObj.GetComponent<UIGrid> ();
		pageCount = gridObj.transform.childCount;

		if (gridObj != null && this.gridObj.transform.FindChild ("Page1/Button1") != null) {
			this.mIntLevelButtonCheckBoxGroup = this.gridObj.transform.FindChild ("Page1/Button1").GetComponent<UIToggle> ().group;			//模式选择groupID
		} else {
			this.mIntLevelButtonCheckBoxGroup = 1;
		}
	}
	
	/// <summary>
	/// pageX下的每个button事件进行初始化
	/// 此函数必须在 InitScrollViewParam 函数之后调用
	/// </summary>
	void InitScrollViewButtonEvent ()
	{
		if (scrollview == null) {
			InitScrollViewParam ();
		}
		/////开始添加事件处理Event
		int i = 0;
		int j = 0;
		int childCount = 0;
		string pageName;

		this.activeButtonList.Clear ();

		for (i = 1; i<=pageCount; i++) {
			pageName = "Page" + i.ToString ();					//Page从1开始

			if (this.gridObj != null && this.gridObj.transform.FindChild (pageName) != null) {
				childCount = this.gridObj.transform.FindChild (pageName).childCount;
			} else {
				childCount = 10;
			}

			for (j=1; j<=childCount; j++) {
				GameObject button = this.gridObj.transform.FindChild (pageName + "/Button" + (j + (i - 1) * 10).ToString ()).gameObject;
				//button可能隐藏，不是10关
				if (button != null && button.activeInHierarchy == true) {
					button.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonLevel));
					this.activeButtonList.Add (button);

					button.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClick));
				}
			}
		}
	}

	/// <summary>
	/// 篇章索引
	/// </summary>
	void InitPageIndex ()
	{
		if(MainState.Instance.playerInfo!=null)
			taskID = MainState.Instance.playerInfo.missionOfJuqing;
		else
			taskID = "1001";

		int temp;
		if (taskID.Length <= 1)		//数字零或者空字符串
			temp = 0;
		else
			temp = int.Parse (taskID);

		int xx = temp % 1000;
		//pageIndex = ((xx % 10 == 0) ? (int)(xx / 10) - 1 : ((int)(xx / 10)));
				
		if (xx <= 0) {
			this.passLevelMax = 0;
		} else {
//			//遍历 XXXX
//			string id = "";
//			foreach(StoryConfigData configData in this.storyConfigDataList)
//			{
//				if(configData.raceId == taskID)
//				{
//					id = configData.id;
//				}
//			}
//	
//			if(id.Length==0)
//			{
//				Debug.LogWarning("Can't found valid story id");
//			}
//			else
//			{
//				this.passLevelMax = id;
//			}
//
//			////

			this.raceConfigData = RaceConfigData.GetConfigData<RaceConfigData> (taskID);
			//开始读表
			this.passLevelMax = this.raceConfigData.storyIndex;
			//SaveData
			MainState.Instance.playerInfo.storyPassLevelMax = this.passLevelMax;
			MainState.Instance.SavePlayerData ();
		}

//		///下列数据应该从data层得到
//		passLevelMax = MainState.Instance.playerInfo.storyPassLevelMax;
//
//		//开始求pageIndex  ---  passLevelMax正好是 new图片在List中的索引
		int buttonSuffix = int.Parse (this.activeButtonList [passLevelMax].name.Substring ("Button".Length));
//
//		//根据buttonSuffix 当前的pageIndex；pageIndex是从0开始的,所以需要减去1			
		pageIndex = ((buttonSuffix % 10 == 0) ? (int)(buttonSuffix / 10) - 1 : ((int)(buttonSuffix / 10)));

		CheckButtonArrowEnable (pageIndex);		//可能有只有1个子节点，所以左右两边button都隐藏
	}
	
	void  InitScrollViewOffset (int index)
	{
		index = NGUIMath.ClampIndex (index, pageCount);
		scrollview.MoveRelative (new Vector3 (-grid.cellWidth * index, 0, 0));		//prefeb初始化的起点必须是left
	}

	void InitLevelStatus ()
	{
		string buttonNumStr;
		string pageNumStr;
		int buttonSuffix;
		//1 已经过关-----index表示activeButtonList的索引
		for (int index=1; index<=passLevelMax; index++) {
						
			//buttonNumStr = "Button"+int.Parse(this.activeButtonList[index-1].name.Substring("Button".Length));
			//buttonNumStr = "Button" + index;
			buttonNumStr = this.activeButtonList [index - 1].name;
			buttonSuffix = int.Parse (this.activeButtonList [index - 1].name.Substring ("Button".Length));
			pageNumStr = "Page" + ((buttonSuffix % 10 == 0) ? (int)(buttonSuffix / 10) : ((int)(buttonSuffix / 10) + 1));
						
			Transform buttonTran = this.gridObj.transform.FindChild (pageNumStr + "/" + buttonNumStr);
			if (buttonTran != null) {
				//隐藏New图片
				Transform newSpriteTran = buttonTran.FindChild ("New");
				if (newSpriteTran != null) {
					NGUITools.SetActive (newSpriteTran.gameObject, false);
				}
					
				//隐藏锁图片
				Transform lockSpriteTran = buttonTran.FindChild ("SpriteSuo");
				if (lockSpriteTran != null) {
					NGUITools.SetActive (lockSpriteTran.gameObject, false);
				}

				//显示过关图片
				Transform passSpriteTran = buttonTran.FindChild ("SpriteGuoguan");
				if (passSpriteTran != null) {
					NGUITools.SetActive (passSpriteTran.gameObject, true);
				}

				//选择框的缩放特效
				Transform selectSpriteTran = buttonTran.FindChild ("SpriteXuanzhongzhuangtai");
				if (selectSpriteTran != null) {
					Tweener dt=selectSpriteTran.DOScale(1.08f,0.5f).SetUpdate(true).SetLoops(-1,LoopType.Yoyo);
				}
			}
		}
		
		//2 新挑战的关卡--------------封装为一个函数 
		int newIndex = passLevelMax + 1;
		buttonNumStr = this.activeButtonList [newIndex - 1].name;
		buttonSuffix = int.Parse (this.activeButtonList [newIndex - 1].name.Substring ("Button".Length));
		pageNumStr = "Page" + ((buttonSuffix % 10 == 0) ? (int)(buttonSuffix / 10) : ((int)(buttonSuffix / 10) + 1));

		Debug.Log (buttonNumStr + " buttonSuffix " + buttonSuffix + " pageNumStr " + pageNumStr);
			
		Transform buttonNewTran = this.gridObj.transform.FindChild (pageNumStr + "/" + buttonNumStr);
		if (buttonNewTran != null) {
			//隐藏New图片
			Transform newSpriteTran = buttonNewTran.FindChild ("New");
			if (newSpriteTran != null) {
				NGUITools.SetActive (newSpriteTran.gameObject, true);
			}
					
			//隐藏锁图片
			Transform lockSpriteTran = buttonNewTran.FindChild ("SpriteSuo");
			if (lockSpriteTran != null) {
				NGUITools.SetActive (lockSpriteTran.gameObject, false);
			}
					
			//显示过关图片
			Transform passSpriteTran = buttonNewTran.FindChild ("SpriteGuoguan");
			if (passSpriteTran != null) {
				NGUITools.SetActive (passSpriteTran.gameObject, false);
			}

			//选择框的缩放特效
			Transform selectSpriteTran = buttonNewTran.FindChild ("SpriteXuanzhongzhuangtai");
			if (selectSpriteTran != null) {
				selectSpriteTran.DOScale(1.08f,0.5f).SetUpdate(true).SetLoops(-1,LoopType.Yoyo);
			}
		}

		//3. 加锁状态----判断已经全部解锁的特殊情况，需要得到全部关卡的个数
		for (int index = newIndex + 1; index<=this.activeButtonList.Count; index++) {
			//Button是从1开始的，所以这里需要加1操作

			buttonNumStr = this.activeButtonList [index - 1].name;
			buttonSuffix = int.Parse (this.activeButtonList [index - 1].name.Substring ("Button".Length));
			pageNumStr = "Page" + ((buttonSuffix % 10 == 0) ? (int)(buttonSuffix / 10) : ((int)(buttonSuffix / 10) + 1));

//						buttonNumStr = "Button" + index;
//						pageNumStr = "Page" + ((index % 10 == 0) ? (int)(index / 10) : ((int)(index / 10) + 1));
						
			Transform buttonTran = this.gridObj.transform.FindChild (pageNumStr + "/" + buttonNumStr);
			if (buttonTran != null) {
				//隐藏New图片
				Transform newSpriteTran = buttonTran.FindChild ("New");
				if (newSpriteTran != null) {
					NGUITools.SetActive (newSpriteTran.gameObject, false);
				}
						
				//显示锁图片
				Transform lockSpriteTran = buttonTran.FindChild ("SpriteSuo");
				if (lockSpriteTran != null) {
					NGUITools.SetActive (lockSpriteTran.gameObject, true);
				}
						
				//隐藏过关图片
				Transform passSpriteTran = buttonTran.FindChild ("SpriteGuoguan");
				if (passSpriteTran != null) {
					NGUITools.SetActive (passSpriteTran.gameObject, false);
				}

				//选择框的缩放特效
				Transform selectSpriteTran = buttonTran.FindChild ("SpriteXuanzhongzhuangtai");
				if (selectSpriteTran != null) {
					selectSpriteTran.DOScale(1.08f,0.5f).SetUpdate(true).SetLoops(-1,LoopType.Yoyo);
				}

				//buttonTran.gameObject.GetComponent<UIButton> ().isEnabled = false;
			}
		}

		//4. new 图片 进行缩放特效
//		Sequence mySeq = DOTween.Sequence();
//		mySeq.Append(this.activeButtonList [passLevelMax].transform.FindChild ("New").DOScale(new Vector3(0.95f,0.95f,0.95f),0.15f).SetEase(Ease.Linear));
//		mySeq.Append(this.activeButtonList[passLevelMax].transform.FindChild ("New").DOScale(Vector3.one,0.85f).SetEase(Ease.Linear));
//
//		mySeq.Append(this.activeButtonList [passLevelMax].transform.FindChild ("New").GetComponent<UISprite>().alpha);
//		mySeq.Append(this.activeButtonList[passLevelMax].transform.FindChild ("New").DOScale(Vector3.one,0.85f).SetEase(Ease.Linear));
//		mySeq.SetLoops(-1);

		this.newTween = DOVirtual.Float(0.4f,1,0.8f,delegate(float value) {
			if(this.activeButtonList [passLevelMax]!=null && this.activeButtonList [passLevelMax].transform.FindChild ("New")!=null )
				this.activeButtonList [passLevelMax].transform.FindChild ("New").GetComponent<UISprite>().alpha = value;
		}).SetLoops(-1,LoopType.Yoyo).SetUpdate(true).SetEase(Ease.InOutQuad);

		//5. 设置选中状态图片，missionOfPreviousJuqing在开始游戏中设置
		this.oldID = MainState.Instance.playerInfo.missionOfPreviousJuqing;

		if(this.oldID.Equals("-1")==true)		//-1表示游戏程序刚刚启动
		{
			this.activeButtonList [newIndex - 1].GetComponent<UIToggle> ().value = true;
		}
		else if(this.oldID.Length>=4)
		{
			//历史记录关卡
			int temp = int.Parse (this.oldID);
			int xx = temp % 1000;		//xx就是button的后缀数

			//根据buttonSuffix 当前的pageIndex；pageIndex是从0开始的,所以需要减去1			
			pageIndex = ((xx % 10 == 0) ? (int)(xx / 10) - 1 : ((int)(xx / 10)));
			CheckButtonArrowEnable (pageIndex);										//可能有只有1个子节点，所以左右两边button都隐藏

//			//遍历  XXXX
//			string id = "";
//			foreach(StoryConfigData configData in this.storyConfigDataList)
//			{
//				if(configData.raceId == this.oldID)
//				{
//					id = configData.id;
//				}
//			}
//			
//			if(id.Length==0)
//			{
//				Debug.LogWarning("Can't found valid story id");
//			}
//			else
//			{
//				this.passLevelMax = id;
//			}
//			///

			RaceConfigData tempRaceConfigData = RaceConfigData.GetConfigData<RaceConfigData> (oldID);
			this.activeButtonList [tempRaceConfigData.storyIndex - 1].GetComponent<UIToggle> ().value = true;
		}
		else
		{
			Debug.LogError("Please check this.oldID " + this.oldID);
		}
	}

	/// <summary>
	/// 头像,名称，好心，金币，钻石个数------从数据层获得
	/// </summary>
	void UpdateTopStatusBar ()
	{
		//1.头像----后期用表格保存图片与ID的对应关系
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
	}

	/// <summary>
	/// 根据某个Index来，更新每个关卡的特殊UI值，如奖励值，关卡名称，篇章名称 等参数
	/// </summary>
	void UpdateUIValue()
	{
		if(this.storyConfigDataList==null)
			this.InitConfigData();

		//1.篇章
		this.LabelMoshixuanze.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].chapterIndex.ToString();

		//2. 篇章名称
		this.LabelZhangjieming.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].chapterName.ToString();

		//3. 章节-关卡  数字
		this.LabelZhangjie.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].stageIndex.ToString();

		//4. 比赛模式名称
		this.LabelBisaimoshi.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].stageName.ToString();

		//5.描述信息
		this.LabelGuankamiaoshu.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].stageDescription.ToString();

		//6.通关奖励数字
		this.LabelJianglishu.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].rewardValue.ToString();
	
		//7.消耗的体力
		this.LabelJikexing.GetComponent<UILabel>().text = this.storyConfigDataList[this.selectIndex].costPower.ToString();
	}

	/// <summary>
	/// 返回
	/// </summary>
	void OnClickButtonFanhui ()
	{
		if(this.newTween!=null)
			this.newTween.Kill(true);

		this.CloseUI ();
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebMainUI);
	}
	
	/// <summary>
	/// 购买金币
	/// </summary>
	void OnClickButtonJiahaobi ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}
		
	/// <summary>
	/// 购买好心
	/// </summary>
	void OnClickButtonJiahaoxin ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopPower;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}
		
	/// <summary>
	/// 购买钻石
	/// </summary>
	void OnClickButtonJiahaozuan ()
	{
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
	}

	/// <summary>
	/// 继续--开始游戏
	/// </summary>
	void OnClickButtonJixu ()
	{

		//1. 用户选择的关卡未解锁
		if(this.selectIndex>this.passLevelMax)
		{
			PanelMainUIController.Instance.ShowUIMsgBox("关卡未解锁，请完成前面关卡解锁此关卡",1.5f);
			return;
		}
			
		//2. 体力判断---体力不足弹出购买体力UI界面，或者弹出商店
		int configPower = this.storyConfigDataList[this.selectIndex].costPower;
		int leftPower = (int)MainState.Instance.playerInfo.power;
		if(leftPower < configPower)
		{
			PanelMainUIController.Instance.ShowUIMsgBox("体力不足,请购买体力继续游戏",2.0f);
			return;
		}
		else
		{
			MainState.Instance.playerInfo.ChangePower(-configPower);
			MainState.Instance.SavePlayerData();
		}

		//3. 得到章节和关卡
		string name = this.activeButtonList [this.selectIndex].name;
		int buttonSuffix = int.Parse (name.Substring ("Button".Length));
				
		int page = buttonSuffix / 10 + 1;
		int level = buttonSuffix % 10;
	
		//4. XXXX
//		int raceId = 1000 + (page-1) * 10 + level;
//		string id = "";
//		foreach(StoryConfigData configData in this.storyConfigDataList)
//		{
//			if(configData.raceId == raceId)
//			{
//				id = configData.id;
//			}
//		}
//
//		if(id.Length==0)
//		{
//			Debug.LogWarning("Can't found valid story id");
//		}

		RaceManager.Instance.startStoryGame (page, level);

		if(this.newTween!=null)
			this.newTween.Kill(true);
	}
	
	/// <summary>
	/// 检测左右按键的显示，特例：只有2个item，或1个item的时候，所以需要分别检测
	/// </summary>
	/// <param name="index">Index.</param>
	void CheckButtonArrowEnable (int index)
	{
		if (index <= 0)
			ButtonZuo.SetActive (false);
		else
			ButtonZuo.SetActive (true);
		
		if (index >= this.pageCount - 1)
			ButtonYou.SetActive (false);
		else
			ButtonYou.SetActive (true);
	}
	
	/// <summary>
	/// 右箭头
	/// </summary>
	void OnClickButtonYou ()
	{
		if (pageIndex >= pageCount - 1)
			return;

		this.pageIndex++;
		CheckButtonArrowEnable (this.pageIndex);
		
		scrollview.MoveRelative (new Vector3 (-grid.cellWidth, 0, 0));		//后续添加动画效果

		int raceId = 1000 + pageIndex * 10 + 1;
		foreach(StoryConfigData configData in this.storyConfigDataList)
		{
			if(configData.raceId.Equals(raceId.ToString()))
			{
				this.LabelMoshixuanze.GetComponent<UILabel>().text = configData.chapterIndex.ToString();
				this.LabelZhangjieming.GetComponent<UILabel>().text = configData.chapterName.ToString();
			}
		}

	}
	
	/// <summary>
	/// 左箭头
	/// </summary>
	void OnClickButtonZuo ()
	{
		if (pageIndex <= 0)
			return;

		this.pageIndex--;
		CheckButtonArrowEnable (this.pageIndex);
	
		scrollview.MoveRelative (new Vector3 (grid.cellWidth, 0, 0));		//后续添加动画效果

		int raceId = 1000 + pageIndex * 10 + 1;
		foreach(StoryConfigData configData in this.storyConfigDataList)
		{
			if(configData.raceId.Equals(raceId.ToString()))
			{
				this.LabelMoshixuanze.GetComponent<UILabel>().text = configData.chapterIndex.ToString();
				this.LabelZhangjieming.GetComponent<UILabel>().text = configData.chapterName.ToString();
			}
		}
	}

	/// <summary>
	/// 点击某个关卡Button
	/// </summary>
	void OnClickButtonLevel ()
	{
		UIToggle toggle = UIToggle.GetActiveToggle (this.mIntLevelButtonCheckBoxGroup);
		if (toggle == null || (toggle != null && toggle.value == false)) 
		{
			return;
		}

		this.selectIndex = this.activeButtonList.IndexOf (toggle.gameObject);
		if (this.selectIndex > this.passLevelMax) 
		{
			PanelMainUIController.Instance.ShowUIMsgBox ("关卡未解锁，请完成前面关卡解锁", 1.5f);
		}

		UpdateUIValue();
	}

	/// <summary>
	/// 更换头像
	/// </summary>
	void OnClickSpriteTouxiang ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTouxiang);
	}

	/// <summary>
	/// 处理多次点击，同一个LevelButton
	/// </summary>
	void OnClick () 
	{
		Debug.Log("Process  multiPress OnClick on a button");
	}
}
