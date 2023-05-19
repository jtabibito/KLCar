using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 竞赛模式的UI事件处理代码
/// 2015-4-9 16:40:22
/// </summary>
public partial class ContainerOperationjinsumoshiUIController : UIControllerBase
{
	/// 宠物CD恢复进度相关变量
	private UISprite   mUISpritePetProgress;
	private float	   mFloatPetCDTime = 5.0f;
	
	/// 飞弹道具CD恢复进度相关变量
	private UISprite   mUISpriteFeidanProgress;
	private float	   mFloatFeidanCDTime = 3.0f;

	/// 护盾道具CD恢复进度相关变量
	private UISprite   mUISpriteHudunProgress;
	private float	   mFloatHudunCDTime = 3.0f;

	/// 隐身道具CD恢复进度相关变量
	private UISprite   mUISpriteYinshenProgress;
	private float	   mFloatYinshenCDTime = 3.0f;

	/// 加速道具CD恢复进度相关变量
	private UISprite   mUISpriteJiasuProgress;
	private float	   mFloatJiasuCDTime = 3.0f;
	private Vector3	   mVector3DashBoardPointerLocalOffset = UIControllerConst.Vector3DashBoardPointerLocalOffset;
	//new Vector3 (44.0f, 12.0f, 0.0f);
	private Vector3	   mVector3DashBoardPointerWorldSapceCenter;
	private float 	   mFloatDashBoardMaxSpeed = 450.0f;
	private float      mFloatDashBoardMinSpeed = 0.0f;
	private float 	   mFloatDashBoardStartAngle = UIControllerConst.FloatDashBoardStartAngle;
	private float      mFloatDashBoardEndAngle = UIControllerConst.FloatDashBoardEndAngle;
	private float 	   mFloatDashBoardOldAngle = 0.0f;
	private int		   mIntPropsFlyBomb = 0;							//飞弹
	private int		   mIntPropsShield = 0;								//护盾
	private int		   mIntPropsHiding = 0;								//隐身
	private int		   mIntPropsSpeed = 0;								//加速
		
	//里程表相关
	private float mFloatOdoMeterLeftWorldPositionX = 0.0f;
	private float mFloatOdoMeterRightWorldPositionX = 0.0f;
	private float mFloatOdoMeterPlayerPercent = 0.0f;
	private List<float>  mListOdoMeterOtherPlayerPercent = new List<float> ();
	private GameObject petSkillObject = null;
	Dictionary<string, bool> mDicButtonState = new Dictionary<string, bool> ();
	private bool 	  mBoolIsPause = false;

	public bool IsPause {
		get {
			return mBoolIsPause;
		}
		set {
			mBoolIsPause = value;
		}
	}

	///anim
	private GameObject  spriteFrameFeidan = null;
	private GameObject  spriteFrameHudun = null;
	private GameObject  spriteFrameYinshen = null;
	private GameObject  spriteFrameJiasu = null;
	private GameObject  spriteFramePet = null;

	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitPropsButtonCDTime ();

		this.mVector3DashBoardPointerWorldSapceCenter = this.SpriteZhizhen.transform.TransformPoint (mVector3DashBoardPointerLocalOffset);

		this.mIntPropsFlyBomb = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Feidanitem1Num : 0;
		this.mIntPropsShield = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Hudunitem2Num : 0;
		this.mIntPropsHiding = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num : 0;
		this.mIntPropsSpeed = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num : 0;

		this.LabelTishifeidan.GetComponent<UILabel> ().text = this.mIntPropsFlyBomb.ToString ();
		this.LabelTishihudun.GetComponent<UILabel> ().text = this.mIntPropsShield.ToString ();
		this.LabelTishiyinshen.GetComponent<UILabel> ().text = this.mIntPropsHiding.ToString ();
		this.LabelTishijiasu.GetComponent<UILabel> ().text = this.mIntPropsSpeed.ToString ();

		Transform spriteJindu = this.ContainerBackground.transform.FindChild ("SpriteJindu");
		if (spriteJindu != null) {
			this.mFloatOdoMeterLeftWorldPositionX = spriteJindu.GetComponent<UIWidget> ().worldCorners [0].x;
			this.mFloatOdoMeterRightWorldPositionX = spriteJindu.GetComponent<UIWidget> ().worldCorners [3].x * 0.7f;	//美术需要重新出图
			this.mFloatOdoMeterPlayerPercent = 0.0f;
		}
				
		this.mListOdoMeterOtherPlayerPercent.Clear ();
		int count = RaceManager.Instance.RaceCounterInstance!=null?(RaceManager.Instance.RaceCounterInstance.getRaceCarNum()-1):5;
		//其他玩家的进度数据
		for (int i=0; i<count; i++) {
			this.mListOdoMeterOtherPlayerPercent.Add (0.0f);
		}
		 
		/////////////////////应该根据数据库中的用户选择进行配置---这里是临时代码

		//MyGameProto.PlayerInfo playerInfo = MainState.Instance.playerInfo;	//LocalDataByProto.LoadData<MyGameProto.PlayerInfo> ("playerInfo");
		//MainState.Instance.playerInfo = LocalDataByProto.LoadData<MyGameProto.MyPlayerInfo> ("playerInfo");

		if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nowInputName.Length <= 0) {
			MainState.Instance.playerInfo.nowInputName = UIControllerConst.InputTouch;
			LocalDataByProto.SaveData<MyGameProto.MyPlayerInfo> ("playerInfo", MainState.Instance.playerInfo);
		}
				
		//同时输入方式也要进行相应的修改处理
		if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputTouch)) {
			NGUITools.SetActive (this.ButtonZhongli, false);
			NGUITools.SetActive (this.ButtonChuping, true);
		} else if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputGravity)) {
			NGUITools.SetActive (this.ButtonZhongli, true);
			NGUITools.SetActive (this.ButtonChuping, false);
		} else {
			NGUITools.SetActive (this.ButtonZhongli, false);		//default is Touch
			NGUITools.SetActive (this.ButtonChuping, true);
		}

		mDicButtonState.Clear ();

		InitPauseParam ();
		InitPetSkill ();

		InitSpriteAnim();

		InitOtherPlayerImage();

		UpdateOdoMeterPlayerImage();
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateUIDisplay ();
	}
		
	/// <summary>
	/// 初始化Button按键事件
	/// </summary>
	void InitButtonEvent ()
	{
		this.ButtonFeidan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFeidan));			//道具：飞弹 Button
		this.ButtonHudun.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonHudun));			//道具：护盾 Button
		this.ButtonYinshen.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYinshen));		//道具：隐身 Button
		this.ButtonJiasu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiasu));			//道具：加速 Button
			
		UIEventListener.Get (this.ButtonLeft).onPress = this.OnPressButtonLeft;											//左方向键  Button
		UIEventListener.Get (this.ButtonRight).onPress = this.OnPressButtonRight;										//右方向	键  Button
			
		this.ButtonZanting.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZanting));		//暂停游戏 Button
			
		this.ContainerChongwu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonContainerChongwu));	
		this.ContainerChongwu.GetComponent<UIButton> ().isEnabled = false;
			
		this.ButtonZhongli.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZhongli));
		this.ButtonChuping.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChuping));	
	}

	/// <summary>
	/// 初始化一些具有CD特殊button的属性
	/// </summary>
	void InitPropsButtonCDTime ()
	{
		//飞弹 CDTime
		this.mUISpriteFeidanProgress = this.SpriteFeidanmengzhu.GetComponent<UISprite> ();
		if (this.mUISpriteFeidanProgress != null) {
			this.mUISpriteFeidanProgress.type = UISprite.Type.Filled;
			this.mUISpriteFeidanProgress.fillDirection = UISprite.FillDirection.Radial360;
			this.mUISpriteFeidanProgress.flip = UISprite.Flip.Nothing;
			this.mUISpriteFeidanProgress.invert = false;
			this.mUISpriteFeidanProgress.fillAmount = 0;
		}

		//护盾 CDTime
		this.mUISpriteHudunProgress = this.SpriteHudunmengzhu.GetComponent<UISprite> ();
		if (this.mUISpriteHudunProgress != null) {
			this.mUISpriteHudunProgress.type = UISprite.Type.Filled;
			this.mUISpriteHudunProgress.fillDirection = UISprite.FillDirection.Radial360;
			this.mUISpriteHudunProgress.flip = UISprite.Flip.Nothing;
			this.mUISpriteHudunProgress.invert = false;
			this.mUISpriteHudunProgress.fillAmount = 0;
		}

		//隐身 CDTime
		this.mUISpriteYinshenProgress = this.SpriteYinshenmengzhu.GetComponent<UISprite> ();
		if (this.mUISpriteYinshenProgress != null) {
			this.mUISpriteYinshenProgress.type = UISprite.Type.Filled;
			this.mUISpriteYinshenProgress.fillDirection = UISprite.FillDirection.Radial360;
			this.mUISpriteYinshenProgress.flip = UISprite.Flip.Nothing;
			this.mUISpriteYinshenProgress.invert = false;
			this.mUISpriteYinshenProgress.fillAmount = 0;
		}

		//加速 CDTime
		this.mUISpriteJiasuProgress = this.SpriteJiasumengzhu.GetComponent<UISprite> ();
		if (this.mUISpriteJiasuProgress != null) {
			this.mUISpriteJiasuProgress.type = UISprite.Type.Filled;
			this.mUISpriteJiasuProgress.fillDirection = UISprite.FillDirection.Radial360;
			this.mUISpriteJiasuProgress.flip = UISprite.Flip.Nothing;
			this.mUISpriteJiasuProgress.invert = false;
			this.mUISpriteJiasuProgress.fillAmount = 0;
		}

		//宠物 CDTime 
		this.mUISpritePetProgress = this.SpriteXiaoxiong.GetComponent<UISprite> ();
		if (this.mUISpritePetProgress != null) {
			this.mUISpritePetProgress.type = UISprite.Type.Filled;
			this.mUISpritePetProgress.fillDirection = UISprite.FillDirection.Vertical;
			this.mUISpritePetProgress.flip = UISprite.Flip.Nothing;
			this.mUISpritePetProgress.invert = false;
			this.mUISpritePetProgress.fillAmount = 0.0f;
		}
	}

	void InitPauseParam ()
	{
		///刚进入游戏界面暂停文字button是不显示的  SpriteZhanTingWenzhi
		this.ButtonZhanTingWenzhi.SetActive (false);
		this.ButtonZhanTingWenzhi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZhanTingWenzhi));

		GameObject obj = this.ButtonZhanTingWenzhi.transform.FindChild ("SpriteZhanTingWenzhi").gameObject;
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (obj.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 1).SetEase (Ease.Linear));
		mySeq.Append (obj.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1).SetEase (Ease.Linear));
		mySeq.SetUpdate (UpdateType.Normal, true);
		mySeq.SetLoops (-1);
	}

	void InitSpriteAnim()
	{
		this.spriteFrameFeidan = this.ButtonFeidan.transform.FindChild("SpriteFrame").gameObject;
		this.spriteFrameHudun = this.ButtonHudun.transform.FindChild("SpriteFrame").gameObject;
		this.spriteFrameYinshen = this.ButtonYinshen.transform.FindChild("SpriteFrame").gameObject;
		this.spriteFrameJiasu = this.ButtonJiasu.transform.FindChild("SpriteFrame").gameObject;
		this.spriteFramePet = this.ContainerChongwu.transform.FindChild("SpriteFrame").gameObject;

		NGUITools.SetActive(this.spriteFramePet,false);
	}
	
	/// <summary>
	/// 初始化宠物技能对象
	/// </summary>
	void InitPetSkill ()
	{
		petSkillObject = GameResourcesManager.GetPetSkillPrefab (UIControllerConst.SkillPrefebOperationPet);
		if (petSkillObject != null)
			this.mFloatPetCDTime = petSkillObject.GetComponent<SkillBase> ().playCdTime;

		if (this.mFloatPetCDTime == 0)
			mFloatPetCDTime = 5.0f;

		Debug.Log (this.mFloatPetCDTime);
	}

	/// <summary>
	/// 玩家最多个数
	/// </summary>
	void InitOtherPlayerImage()
	{
		int count = RaceManager.Instance.RaceCounterInstance!=null?(RaceManager.Instance.RaceCounterInstance.getRaceCarNum ()-1):5;

		switch(count)
		{
		case  0:
			NGUITools.SetActive(this.SpriteWanjiadian1,false);
			NGUITools.SetActive(this.SpriteWanjiadian2,false);
			NGUITools.SetActive(this.SpriteWanjiadian3,false);
			NGUITools.SetActive(this.SpriteWanjiadian4,false);
			NGUITools.SetActive(this.SpriteWanjiadian5,false);
			break;
		case  1:
			NGUITools.SetActive(this.SpriteWanjiadian2,false);
			NGUITools.SetActive(this.SpriteWanjiadian3,false);
			NGUITools.SetActive(this.SpriteWanjiadian4,false);
			NGUITools.SetActive(this.SpriteWanjiadian5,false);
			break;
		case  2:
			NGUITools.SetActive(this.SpriteWanjiadian3,false);
			NGUITools.SetActive(this.SpriteWanjiadian4,false);
			NGUITools.SetActive(this.SpriteWanjiadian5,false);
			break;
		case  3:
			NGUITools.SetActive(this.SpriteWanjiadian4,false);
			NGUITools.SetActive(this.SpriteWanjiadian5,false);
			break;
		case  4:
			NGUITools.SetActive(this.SpriteWanjiadian5,false);
			break;
		default  :
			break;
		}
	}

	//////////按键的OnClickButton 事件处理/// 
	/// <summary>
	/// 暂停文字
	/// </summary>
	void OnClickButtonZhanTingWenzhi ()
	{
		//UIControllerConst.UIPrefebPauseUIActive=false 且 home键 = true，则显示暂停文字或暂停界面
		if (UIControllerConst.UIPrefebPauseUIActive == false) {
			IsPause = false;
			//ChangeUIButtonEnableState (false);
			this.ButtonZhanTingWenzhi.SetActive (false);
			//调用恢复API
			if (RaceManager.Instance.RaceCounterInstance != null)
				RaceManager.Instance.RaceCounterInstance.doResumeGame ();

			//3.在暂停界面更新数据
			this.mIntPropsFlyBomb = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Feidanitem1Num : 0;
			this.mIntPropsShield = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Hudunitem2Num : 0;
			this.mIntPropsHiding = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num : 0;
			this.mIntPropsSpeed = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num : 0;
			
			this.LabelTishifeidan.GetComponent<UILabel> ().text = this.mIntPropsFlyBomb.ToString ();
			this.LabelTishihudun.GetComponent<UILabel> ().text = this.mIntPropsShield.ToString ();
			this.LabelTishiyinshen.GetComponent<UILabel> ().text = this.mIntPropsHiding.ToString ();
			this.LabelTishijiasu.GetComponent<UILabel> ().text = this.mIntPropsSpeed.ToString ();
		}
	}

	/// <summary>
	/// 道具：飞弹 Button  只有1个的时候特殊处理，进入Disable状态，不跑CD了
	/// </summary>
	void OnClickButtonFeidan ()
	{
		if (this.mIntPropsFlyBomb <= 0) {
			Debug.LogWarning ("FlyBomb count is <=0 ,please check it");
			this.BuyGoods();
			return;
		} else {
			//1
			this.mIntPropsFlyBomb--;
			this.LabelTishifeidan.GetComponent<UILabel> ().text = this.mIntPropsFlyBomb.ToString ();
			RaceManager.Instance.RaceCounterInstance.Feidanitem1Num=this.mIntPropsFlyBomb;
			//2
			this.ButtonFeidan.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteFeidanProgress.fillAmount = 1.0f;

			//3.
			NGUITools.SetActive(this.spriteFrameFeidan,false);

			GameObject skillObject = GameResourcesManager.GetSkillPrefab (UIControllerConst.SkillPrefebOperationFeiDan);
			RaceManager.Instance.RaceCounterInstance.doUseSkill (skillObject);
		}
	}
	
	/// <summary>
	/// 道具：护盾 the click button hudun event.
	/// </summary>
	void OnClickButtonHudun ()
	{
		//Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		if (this.mIntPropsShield <= 0) {
			Debug.LogWarning ("Shidle count is <=0 ,please check it");
			this.BuyGoods();
		} else {
			//这里应该添加逻辑代码
			this.mIntPropsShield--;
			this.LabelTishihudun.GetComponent<UILabel> ().text = this.mIntPropsShield.ToString ();
			RaceManager.Instance.RaceCounterInstance.Hudunitem2Num=this.mIntPropsShield;
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonHudun.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteHudunProgress.fillAmount = 1.0f;

			NGUITools.SetActive(this.spriteFrameHudun,false);

			GameObject skillObject = GameResourcesManager.GetSkillPrefab (UIControllerConst.SkillPrefebOperationHuDun);
			RaceManager.Instance.RaceCounterInstance.doUseSkill (skillObject);
		}
	}
	
	/// <summary>
	/// 道具：隐身  click button yinshen event.
	/// </summary>
	void OnClickButtonYinshen ()
	{
		//Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		if (this.mIntPropsHiding <= 0) {
			Debug.LogWarning ("Hiding count is <=0 ,please check it");
			this.BuyGoods();
		} else {
			this.mIntPropsHiding--;
			this.LabelTishiyinshen.GetComponent<UILabel> ().text = this.mIntPropsHiding.ToString ();
			RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num=this.mIntPropsHiding;
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonYinshen.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteYinshenProgress.fillAmount = 1.0f;
			NGUITools.SetActive(this.spriteFrameYinshen,false);

			GameObject skillObject = GameResourcesManager.GetSkillPrefab (UIControllerConst.SkillPrefebOperationYinSheng);
			RaceManager.Instance.RaceCounterInstance.doUseSkill (skillObject);
		}
	}
	
	/// <summary>
	/// 道具：加速  the click button jiasu event.
	/// </summary>
	void OnClickButtonJiasu ()
	{
		//Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		if (this.mIntPropsSpeed <= 0) {
			Debug.LogWarning ("Speed count is <=0 ,please check it");
			this.BuyGoods();
		} else {
			this.mIntPropsSpeed--;
			this.LabelTishijiasu.GetComponent<UILabel> ().text = this.mIntPropsSpeed.ToString ();
			RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num=this.mIntPropsSpeed;
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonJiasu.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteJiasuProgress.fillAmount = 1.0f;
			NGUITools.SetActive(this.spriteFrameJiasu,false);

			GameObject skillObject = GameResourcesManager.GetSkillPrefab (UIControllerConst.SkillPrefebOperationJiaSu);
			RaceManager.Instance.RaceCounterInstance.doUseSkill (skillObject);
		}
	}

	/// <summary>
	/// 左方向键  button left event.
	/// </summary>
	/// <param name="go">Go.</param>
	/// <param name="state">If set to <c>true</c> state.</param>
	void OnPressButtonLeft (GameObject go, bool state)
	{
		Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		if (state) {
			RaceManager.Instance.RaceCounterInstance.doTurnCar (-1);  	//down
		} else {
			RaceManager.Instance.RaceCounterInstance.doTurnCar (0);		//up
		}
	}
	
	/// <summary>
	/// 右方向键  Button right event.
	/// </summary>
	/// <param name="go">Go.</param>
	/// <param name="state">If set to <c>true</c> state.</param>
	void OnPressButtonRight (GameObject go, bool state)
	{
		Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		if (state) {
			RaceManager.Instance.RaceCounterInstance.doTurnCar (1);
		} else {
			RaceManager.Instance.RaceCounterInstance.doTurnCar (0);
		}
	}

	/// <summary>
	/// 暂停游戏 Button event.
	/// </summary>
	void OnClickButtonZanting ()
	{
		if (IsPause == false) {
			IsPause = true;
			//ChangeUIButtonEnableState (false);
			this.ButtonZhanTingWenzhi.SetActive (true);
			PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationzantingshezhi);
		}
	}

	/// <summary>
	/// 宠物点击事件
	///  此函数只有当能量进度恢复之后才能点击触发
	/// </summary>
	void OnClickButtonContainerChongwu ()
	{
		this.ContainerChongwu.GetComponent<UIButton> ().isEnabled = false;
		this.mUISpritePetProgress.fillAmount = 0.0f;

		NGUITools.SetActive(this.spriteFramePet,false);

		///宠物技能
		petSkillObject = GameResourcesManager.GetPetSkillPrefab (UIControllerConst.SkillPrefebOperationPet);
		RaceManager.Instance.RaceCounterInstance.doUseSkill (petSkillObject);
	}
		
	/// <summary>
	/// 切换到触摸方式----显示左右箭头
	/// </summary>
	void OnClickButtonZhongli ()
	{
		NGUITools.SetActive (this.ButtonZhongli, false);
		NGUITools.SetActive (this.ButtonChuping, true);

		NGUITools.SetActive (this.ButtonLeft, true);
		NGUITools.SetActive (this.ButtonRight, true);
		if (MainState.Instance.playerInfo != null)
		{
			MainState.Instance.playerInfo.nowInputName = UIControllerConst.InputTouch;
			MainState.Instance.SavePlayerData();
		}

		//RaceManager.Instance.openVRModel (false);
		//NGUITools.SetActive (this.SpriteZhizhen, true);
		//NGUITools.SetActive (this.ContainerBackground, true);
		//LocalDataByProto.SaveData<MyGameProto.PlayerInfo> ("playerInfo", MainState.Instance.playerInfo);

//				GameObject toast = PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip,"ContainerToast");
//				toast.GetComponent<ContainerToastUIController> ().ShowMessage ("已切换到重力操作",1.0f);
	}
		
	/// <summary>
	/// 切换到重力方式--隐藏左右箭头     
	/// </summary>
	void OnClickButtonChuping ()
	{
				
		NGUITools.SetActive (this.ButtonChuping, false);
		NGUITools.SetActive (this.ButtonZhongli, true);

		NGUITools.SetActive (this.ButtonLeft, false);
		NGUITools.SetActive (this.ButtonRight, false);

		if (MainState.Instance.playerInfo != null)
		{
			MainState.Instance.playerInfo.nowInputName = UIControllerConst.InputGravity;
			MainState.Instance.SavePlayerData();
		}

//		RaceManager.Instance.openVRModel (true);
//		NGUITools.SetActive (this.SpriteZhizhen, false);
//		NGUITools.SetActive (this.ContainerBackground, false);
			
		//LocalDataByProto.SaveData<MyGameProto.PlayerInfo> ("playerInfo", MainState.Instance.playerInfo);

//				GameObject toast = PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip,"ContainerToast");
//				toast.GetComponent<ContainerToastUIController> ().ShowMessage ("已切换到触摸操作",1.0f);
	}

	//////////////////////////////////////////////////////////////////////////
	void UpdateUIDisplay ()
	{
		if (IsPause == true)
			return;

		//更新宠物按键的使能百分百
		if (this.mUISpritePetProgress.fillAmount < 1.0f - Mathf.Epsilon) {
			UpdatePetProgress (Time.deltaTime / this.mFloatPetCDTime);	//1秒的增加量就是 1.0f/this.mFloatPetCDTime
			CheckPetButtonEnable ();
		}
		
		UpdateDashBoardPointerRotate ();
		UpdateCollectCoinLabel ();
		
		UpdateTotoalPlayerNumberLabel ();
		UpdateMyPlayerNumberLabel ();
		UpdateTotoalLapNumberLabel ();
		UpdateCurrentLapNumberLabel ();
				
		UpdateFlyBombProgress (Time.deltaTime / this.mFloatFeidanCDTime);		//飞弹		//1秒的减少量就是 1.0f/this.mFloatFeidanCDTime
		UpdateShieldProgress (Time.deltaTime / this.mFloatHudunCDTime);			// 护盾
		UpdateHidingProgress (Time.deltaTime / this.mFloatYinshenCDTime);		//隐身
		UpdateSpeedProgress (Time.deltaTime / this.mFloatJiasuCDTime);			//加速

		UpdateOdoMeterPlayerProgress ();
		UpdateOdoMeterOtherPlayerProgress ();

		UpdateLapTip();
	}
		
	/// <summary>
	/// no use ,有替代方案了
	/// </summary>
	/// <param name="enable">If set to <c>true</c> enable.</param>
	public void ChangeUIButtonEnableState (bool enable)
	{
		if (false == enable) {
			this.mDicButtonState.Clear ();

			foreach (Transform child in transform) {
				UIButton button = child.gameObject.GetComponent<UIButton> ();
				if (button != null) {
					mDicButtonState.Add (child.name, button.isEnabled);
					button.isEnabled = false;
				}
			}
		} else {
			foreach (Transform child in transform) {
				UIButton button = child.gameObject.GetComponent<UIButton> ();
				if (button != null && child.name.Length > 0 && this.mDicButtonState.ContainsKey (child.name) == true) {
					button.isEnabled = this.mDicButtonState [child.name];
				}
			}
		}
	}

	void UpdateOdoMeterPlayerProgress ()
	{
		if (this.mFloatOdoMeterPlayerPercent >= 1.0f - Mathf.Epsilon) {
			//已经胜利了，播放特效或者粒子效果
			return;
		}
			
		//this.mFlaotOdoMeterPlayerPercent = getXXXX();		//应该从竞赛中获得当前的进度
		//0表示Player玩家
		if(RaceManager.Instance.RaceCounterInstance!=null)
			this.mFloatOdoMeterPlayerPercent = RaceManager.Instance.RaceCounterInstance.getCarRound (0) / RaceManager.Instance.RaceCounterInstance.maxCylinderNumber;

		//Debug.Log (this.mFloatOdoMeterPlayerPercent);

		float newX = Mathf.Lerp (this.mFloatOdoMeterLeftWorldPositionX, this.mFloatOdoMeterRightWorldPositionX, this.mFloatOdoMeterPlayerPercent);
		this.SpriteWuxin.transform.position = new Vector3 (newX, this.SpriteWuxin.transform.position.y, this.SpriteWuxin.transform.position.z);
	}

	void UpdateOdoMeterOtherPlayerProgress ()
	{
		float newX = 0.0f;
		GameObject obj = null;
		//总车辆个数包括玩家车辆，所以要减1
		int count = RaceManager.Instance.RaceCounterInstance!=null?(RaceManager.Instance.RaceCounterInstance.getRaceCarNum () - 1):5;

		for (int i=0; i<count; i++) {
			if(RaceManager.Instance.RaceCounterInstance!=null)
				this.mListOdoMeterOtherPlayerPercent [i] = RaceManager.Instance.RaceCounterInstance.getCarRound (i + 1) / RaceManager.Instance.RaceCounterInstance.maxCylinderNumber;
			else
				this.mListOdoMeterOtherPlayerPercent [i] = 0.0f;

			newX = Mathf.Lerp (this.mFloatOdoMeterLeftWorldPositionX, this.mFloatOdoMeterRightWorldPositionX, this.mListOdoMeterOtherPlayerPercent [i]);

			//假设支持5辆车----UI层限制的，后期可以动态创建UISprite对象
			if (i == 0)
				obj = this.SpriteWanjiadian1;
			else if (i == 1)
				obj = this.SpriteWanjiadian2;
			else if (i == 2)
				obj = this.SpriteWanjiadian3;
			else if (i == 3)
				obj = this.SpriteWanjiadian4;
			else if (i == 4)
				obj = this.SpriteWanjiadian5;

			obj.transform.position = new Vector3 (newX, obj.transform.position.y, obj.transform.position.z);
		}
	}

	//
	void UpdateOdoMeterPlayerImage()
	{
		if (MainState.Instance.playerInfo != null) 
		{
			string xx = MainState.Instance.playerInfo.userRoleImgID < 10 ? ("0" + MainState.Instance.playerInfo.userRoleImgID) : MainState.Instance.playerInfo.userRoleImgID.ToString ();
			this.SpriteWuxin.GetComponent<UISprite> ().spriteName = "ui_role_" + xx.ToString ();
		}
	}

	///////////////////////////////////////////////////////////////////////

	/// <summary>
	/// 飞弹道具 -- 每次减少量百分比
	/// </summary>
	/// <returns>The  progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdateFlyBombProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogWarning ("Feidan percent is too large ,Please check (" + percent + ")");
			
		if (this.mUISpriteFeidanProgress.fillAmount <= Mathf.Epsilon) {
			return 0.0f;
		}

		this.mUISpriteFeidanProgress.fillAmount = Mathf.Clamp01 (this.mUISpriteFeidanProgress.fillAmount - Mathf.Clamp01 (percent));
	
		if (this.mUISpriteFeidanProgress.fillAmount <= Mathf.Epsilon) {
			this.ButtonFeidan.GetComponent<Collider2D> ().enabled = true;
			//完成后播放一个粒子或者Tween动画
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.ButtonFeidan.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 0.02f).SetEase (Ease.Linear));
			mySeq.Append (this.ButtonFeidan.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.75f).SetEase (Ease.OutElastic));

			NGUITools.SetActive(this.spriteFrameFeidan,true);
		}

		return this.mUISpriteFeidanProgress.fillAmount;
	}
		
	/// <summary>
	/// 护盾道具--每次减少量百分比
	/// </summary>
	/// <returns>The  progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdateShieldProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogWarning ("Hudun percent is too large ,Please check (" + percent + ")");
			
		if (this.mUISpriteHudunProgress.fillAmount <= Mathf.Epsilon) {
			return 0.0f;
		}

		this.mUISpriteHudunProgress.fillAmount = Mathf.Clamp01 (this.mUISpriteHudunProgress.fillAmount - Mathf.Clamp01 (percent));
			
		if (this.mUISpriteHudunProgress.fillAmount <= Mathf.Epsilon) {
			this.ButtonHudun.GetComponent<Collider2D> ().enabled = true;
			//完成后播放一个粒子或者Tween动画
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.ButtonHudun.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 0.02f).SetEase (Ease.Linear));
			mySeq.Append (this.ButtonHudun.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.75f).SetEase (Ease.OutElastic));

			NGUITools.SetActive(this.spriteFrameHudun,true);
		}

		return this.mUISpriteHudunProgress.fillAmount;
	}

	/// <summary>
	/// 隐藏道具 每次减少量百分比
	/// </summary>
	/// <returns>The  progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdateHidingProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogWarning ("Yinshen percent is too large ,Please check (" + percent + ")");
			
		if (this.mUISpriteYinshenProgress.fillAmount <= Mathf.Epsilon) {
			return 0.0f;
		}

		this.mUISpriteYinshenProgress.fillAmount = Mathf.Clamp01 (this.mUISpriteYinshenProgress.fillAmount - Mathf.Clamp01 (percent));
			
		if (this.mUISpriteYinshenProgress.fillAmount <= Mathf.Epsilon) {
			this.ButtonYinshen.GetComponent<Collider2D> ().enabled = true;
			//完成后播放一个粒子或者Tween动画
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.ButtonYinshen.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 0.02f).SetEase (Ease.Linear));
			mySeq.Append (this.ButtonYinshen.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.75f).SetEase (Ease.OutElastic));
			NGUITools.SetActive(this.spriteFrameYinshen,true);
		}

		return this.mUISpriteYinshenProgress.fillAmount;
	}

	/// <summary>
	/// 加速道具 每次减少量百分比
	/// </summary>
	/// <returns>The  progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdateSpeedProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogWarning ("Jiasu percent is too large ,Please check (" + percent + ")");
			
		if (this.mUISpriteJiasuProgress.fillAmount <= Mathf.Epsilon) {
			return 0.0f;
		}

		this.mUISpriteJiasuProgress.fillAmount = Mathf.Clamp01 (this.mUISpriteJiasuProgress.fillAmount - Mathf.Clamp01 (percent));
			
		if (this.mUISpriteJiasuProgress.fillAmount <= Mathf.Epsilon) {
			this.ButtonJiasu.GetComponent<Collider2D> ().enabled = true;
			//完成后播放一个粒子或者Tween动画
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.ButtonJiasu.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 0.02f).SetEase (Ease.Linear));
			mySeq.Append (this.ButtonJiasu.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.75f).SetEase (Ease.OutElastic));
			NGUITools.SetActive(this.spriteFrameJiasu,true);
		}

		return this.mUISpriteJiasuProgress.fillAmount;
	}

	/// <summary>
	/// 宠物每次增加量百分比
	/// </summary>
	/// <returns>The pet progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdatePetProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogError ("Pet percent is too large ,Please check (" + percent + ")");

		this.mUISpritePetProgress.fillAmount = Mathf.Clamp01 (this.mUISpritePetProgress.fillAmount + Mathf.Clamp01 (percent));
		return this.mUISpritePetProgress.fillAmount;
	}
	
	bool CheckPetButtonEnable ()
	{
		if (this.mUISpritePetProgress.fillAmount >= (1.0f - Mathf.Epsilon)) {
			this.ContainerChongwu.GetComponent<UIButton> ().isEnabled = true;
			//完成后播放一个粒子或者Tween动画,后续添加
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.ContainerChongwu.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 0.02f).SetEase (Ease.Linear));
			mySeq.Append (this.ContainerChongwu.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 0.75f).SetEase (Ease.OutElastic));

			NGUITools.SetActive(this.spriteFramePet,true);
			return true;
		}
		return false;
	}

	///////////////////////////////////////////////////////////////////////////////

	float GetSpeed ()
	{
		return RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.currentSpeed : 0.0f;
	}

	private int frameCount = 4;
	
	void UpdateDashBoardPointerRotate ()
	{
		frameCount++;
		
		if(frameCount<5)
		{
			return;
		}

		frameCount = 0;

		float speed = GetSpeed ();
		float rangeMaxAngle = this.mFloatDashBoardEndAngle - this.mFloatDashBoardStartAngle;	
		float rangeMaxSpeed = this.mFloatDashBoardMaxSpeed - this.mFloatDashBoardMinSpeed;
		float newAngle = this.mFloatDashBoardStartAngle + rangeMaxAngle * (speed / rangeMaxSpeed);
		
		newAngle = Mathf.Clamp (newAngle, this.mFloatDashBoardStartAngle, this.mFloatDashBoardEndAngle);

		this.SpriteZhizhen.transform.RotateAround (this.mVector3DashBoardPointerWorldSapceCenter, Vector3.back, (newAngle - this.mFloatDashBoardOldAngle));
		this.mFloatDashBoardOldAngle = newAngle;
	
		this.LabelQuanSudu.GetComponent<UILabel> ().text = Mathf.RoundToInt (speed).ToString ();
	}

	///////////////////////金币收集后需要添加特效///////////////////////////////////////////////////////
	void UpdateCollectCoinLabel ()
	{
		this.LabelJinbishuzi.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.gainGoldNum.ToString () : "0";
	}
	
	///////////////////////////////////////////////////////////////////////////////////////////////
			
	/// <summary>
	/// 参数选手总人数
	/// </summary>
	void UpdateTotoalPlayerNumberLabel ()
	{
		this.LabelMingcism.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.getRaceCarNum ().ToString () : "0";
	}

	/// <summary>
	/// 当前玩家名次
	/// </summary>
	void UpdateMyPlayerNumberLabel ()
	{
		this.LabelMingcibig.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.getUserRaceRanking ().ToString () : "0";
	}
	
	///////////////////////////////////////////////////////////////////////////////////////////////
		
	/// <summary>
	/// 总圈数
	/// </summary>
	void UpdateTotoalLapNumberLabel ()
	{
		this.LabelQuanshusm.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.maxCylinderNumber.ToString ():"0";
	}

	/// <summary>
	/// 当前圈数
	/// </summary>
	void UpdateCurrentLapNumberLabel ()
	{
		float fCurrentLap = RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.getCarRound (0):0;
		int currentLap = Mathf.CeilToInt(fCurrentLap);

		int max = (int) (RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.maxCylinderNumber:1);

		this.LabelQuanshubig.GetComponent<UILabel> ().text = (currentLap<=max)?currentLap.ToString():max.ToString();
		//RaceManager.Instance.RaceCounter!=null?RaceManager.Instance.RaceCounter.roundNum.ToString ():"0";
	}

	private int oldLap = 0;
	void UpdateLapTip()
	{
		float fCurrentLap = RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.getCarRound (0):0;
		int max = (int) (RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.maxCylinderNumber:1);

		if(fCurrentLap>oldLap)
		{
			oldLap++;

			if(oldLap>=2 && max>=2)	 //第1圈不显示
			{
				if(oldLap == max)
				{
					oldLap = int.MaxValue;
					ContainerQuanshuxianshiUIController.Instance.ShowMsg("",1.0f,true);
					//PanelMainUIController.Instance.ShowUIMsgBox(" 最后一圈 ",0.5f);
				}
				else
				{
					ContainerQuanshuxianshiUIController.Instance.ShowMsg( oldLap.ToString(),1.0f,false);
					//PanelMainUIController.Instance.ShowUIMsgBox("第 "+oldLap+" 圈" ,0.5f);
				}
			}
		}
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////
		
	/// <summary>
	/// Buy the goods.  暂时都购买成功
	/// </summary>
	/// <returns><c>是否成功</c>, if goods was bought, <c>false</c> otherwise.</returns>
	bool BuyGoods ()
	{
		//1. 弹出商店
		ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopProps;
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
		
		//2. 暂停游戏
		this.IsPause = true;
		this.ButtonZhanTingWenzhi.SetActive (true);
		
		//3.调用暂停API
		if(RaceManager.Instance.RaceCounterInstance!=null)
			RaceManager.Instance.RaceCounterInstance.doPauseGame();

		return true;
	}

}
