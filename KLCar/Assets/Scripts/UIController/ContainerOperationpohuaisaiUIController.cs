using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// ----------------------------------------------
/// 破坏模式的UI事件处理代码
/// Copyright © 2015
/// 2015-4-14 13:08:16
/// ----------------------------------------------
/// </summary>
public partial class ContainerOperationpohuaisaiUIController : UIControllerBase
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
	private int     mIntOtherPlayerCount = 5;
	private UISprite mUISpritePlayer;			//动态修改玩家角色形象图片

	private GameObject petSkillObject = null;
	private int  mIntScore = 0;
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

	private Tweener scorechangeTween;



	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitPropsButtonCDTime ();

		this.mVector3DashBoardPointerWorldSapceCenter = this.SpriteZhizhen.transform.TransformPoint (mVector3DashBoardPointerLocalOffset);

		this.mIntPropsFlyBomb = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Feidanitem1Num : 0;	//5;
		this.mIntPropsShield = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Hudunitem2Num : 0;		//4;
		this.mIntPropsHiding = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num : 0;		//0;
		this.mIntPropsSpeed = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num : 0;			//2;
		
		this.LabelTishifeidan.GetComponent<UILabel> ().text = this.mIntPropsFlyBomb.ToString ();
		this.LabelTishihudun.GetComponent<UILabel> ().text = this.mIntPropsShield.ToString ();
		this.LabelTishiyinshen.GetComponent<UILabel> ().text = this.mIntPropsHiding.ToString ();
		this.LabelTishijiasu.GetComponent<UILabel> ().text = this.mIntPropsSpeed.ToString ();

		if (MainState.Instance.playerInfo!=null && MainState.Instance.playerInfo.nowInputName.Length <= 0) {
			MainState.Instance.playerInfo.nowInputName = UIControllerConst.InputTouch;
			LocalDataByProto.SaveData<MyGameProto.MyPlayerInfo> ("playerInfo", MainState.Instance.playerInfo);
		}
				
		if (MainState.Instance.playerInfo!=null && MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputTouch)) {
			NGUITools.SetActive (this.ButtonZhongli, false);
			NGUITools.SetActive (this.ButtonChuping, true);
		} else if (MainState.Instance.playerInfo!=null && MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputGravity)) {
			NGUITools.SetActive (this.ButtonZhongli, true);
			NGUITools.SetActive (this.ButtonChuping, false);
		}
		else
		{
			NGUITools.SetActive (this.ButtonZhongli, false);		//default is Touch
			NGUITools.SetActive (this.ButtonChuping, true);;
		}

		InitPauseParam ();
		InitPetSkill ();
		InitScoreDisplayMode();
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

	void InitScoreDisplayMode()
	{
		if (RaceManager.Instance.RaceCounterInstance.CurMode == RaceCounter.RaceMode.RM_ArderDestroy)
		{
			NGUITools.SetActive(this.LabelFenshu,true);
			NGUITools.SetActive(this.LabelFenshuzuo,false);
			NGUITools.SetActive(this.LabelFenshuyou,false);

			//记分牌
			this.LabelFenshu.GetComponent<UILabel> ().text = this.mIntScore.ToString ();
		}
		else
		{
			NGUITools.SetActive(this.LabelFenshu,false);
			NGUITools.SetActive(this.LabelFenshuzuo,true);
			NGUITools.SetActive(this.LabelFenshuyou,true);

			//记分牌
			this.LabelFenshuzuo.GetComponent<UILabel> ().text = this.mIntScore.ToString ();
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
			//FIX ME 道具个数--应该从数据库中读出来
			this.mIntPropsFlyBomb = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Feidanitem1Num : 0;	//5;
			this.mIntPropsShield = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Hudunitem2Num : 0;		//4;
			this.mIntPropsHiding = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num : 0;		//0;
			this.mIntPropsSpeed = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num : 0;			//2;
			
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
		//Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
			
		if (this.mIntPropsFlyBomb <= 0) {
			Debug.LogWarning ("FlyBomb count is <=0 ,please check it");
			//进入购买界面，不释放效果,所以直接返回----分为购买成功，购买失败
			this.BuyGoods();
			//成功与否都返回，不释放道具效果,进入暂停状态
			return;
		} else {
			//1 修改data layer，暂时这里修改
			this.mIntPropsFlyBomb--;
			RaceManager.Instance.RaceCounterInstance.Feidanitem1Num=this.mIntPropsFlyBomb;
			this.LabelTishifeidan.GetComponent<UILabel> ().text = this.mIntPropsFlyBomb.ToString ();

			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonFeidan.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteFeidanProgress.fillAmount = 1.0f;

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
			this.mIntPropsShield--;
			RaceManager.Instance.RaceCounterInstance.Hudunitem2Num=this.mIntPropsShield;
			this.LabelTishihudun.GetComponent<UILabel> ().text = this.mIntPropsShield.ToString ();
				
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonHudun.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteHudunProgress.fillAmount = 1.0f;
				
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
			RaceManager.Instance.RaceCounterInstance.Yinshenitem3Num=this.mIntPropsHiding;
			this.LabelTishiyinshen.GetComponent<UILabel> ().text = this.mIntPropsHiding.ToString ();
				
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonYinshen.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteYinshenProgress.fillAmount = 1.0f;
				
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
			RaceManager.Instance.RaceCounterInstance.Jiasuitem4Num=this.mIntPropsSpeed;
			this.LabelTishijiasu.GetComponent<UILabel> ().text = this.mIntPropsSpeed.ToString ();
				
			//2 添加CD冷却特效 a. 按键不能点击 b.进度修改 c. Check 当百分百为0的时候 ，使能按键
			this.ButtonJiasu.GetComponent<Collider2D> ().enabled = false;
			this.mUISpriteJiasuProgress.fillAmount = 1.0f;
				
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
			//RaceManager.Instance.OnInput (RaceManager.InputType.it_leftDown);  	//down
			RaceManager.Instance.RaceCounterInstance.doTurnCar (-1);
		} else {
			//RaceManager.Instance.OnInput (RaceManager.InputType.it_noInput);		//up
			RaceManager.Instance.RaceCounterInstance.doTurnCar (0);
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
		//Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
		//				if (Mathf.Abs (Time.timeScale) > Mathf.Epsilon)
		//						Time.timeScale = 0.0f;
		//				else
		//						Time.timeScale = 1.0f;		//FIX ME或者恢复历史值 mOldtimeScale
		
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
			
		///宠物技能
		petSkillObject = GameResourcesManager.GetPetSkillPrefab (UIControllerConst.SkillPrefebOperationPet);
		RaceManager.Instance.RaceCounterInstance.doUseSkill (petSkillObject);
	}
	
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
		//LocalDataByProto.SaveData<MyGameProto.PlayerInfo> ("playerInfo", MainState.Instance.playerInfo);
	}
		
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
		//LocalDataByProto.SaveData<MyGameProto.PlayerInfo> ("playerInfo", MainState.Instance.playerInfo);
	}
	//////////////////////////////////////////////////////////////////////////
	void UpdateUIDisplay ()
	{
		if (IsPause == true)
			return;

		//更新宠物按键的使能百分百---后续放到Logic代码中
		if (this.mUISpritePetProgress.fillAmount < 1.0f - Mathf.Epsilon) {
			UpdatePetProgress (Time.deltaTime / this.mFloatPetCDTime);	//1秒的增加量就是 1.0f/this.mFloatPetCDTime
			CheckPetButtonEnable ();
		}
		
		UpdateDashBoardPointerRotate ();
		UpdateCollectCoinLabel ();
		UpdateScoreNumberLabel ();

		UpdateTotoalLapNumberLabel ();
		UpdateCurrentLapNumberLabel ();
		
		UpdateFlyBombProgress (Time.deltaTime / this.mFloatFeidanCDTime);		//飞弹		//1秒的减少量就是 1.0f/this.mFloatFeidanCDTime
		UpdateShieldProgress (Time.deltaTime / this.mFloatHudunCDTime);			// 护盾
		UpdateHidingProgress (Time.deltaTime / this.mFloatYinshenCDTime);		//隐身
		UpdateSpeedProgress (Time.deltaTime / this.mFloatJiasuCDTime);			//加

		UpdateScoreNumberLabel ();

		UpdateLapTip();

	}

	///////////////////////////////////////////////////////////////////////
	public void ChangeUIButtonEnableState (bool enable)
	{
		if (false == enable) {
			this.mDicButtonState.Clear ();
			
			foreach (Transform child in transform) {
				UIButton button = child.gameObject.GetComponent<UIButton> ();
				if (button != null) {
					mDicButtonState.Add (child.name, button.isEnabled);
					//button.collider2D = false;
					//button.SetState (UIButton.State.Disabled, true);
					button.isEnabled = false;
					
					//Debug.Log("UIButton获得成功"+child.name);
				}
			}
		} else {
			foreach (Transform child in transform) {
				UIButton button = child.gameObject.GetComponent<UIButton> ();
				if (button != null && child.name.Length > 0 && this.mDicButtonState.ContainsKey (child.name) == true) {
					button.isEnabled = this.mDicButtonState [child.name];
				}
			}
			
			this.mDicButtonState.Clear ();
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
		}
			
		return this.mUISpriteJiasuProgress.fillAmount;
	}

	/// <summary>
	/// 每次增加量百分比
	/// </summary>
	/// <returns>The pet progress.</returns>
	/// <param name="percent">Percent.</param>
	float UpdatePetProgress (float percent)
	{
		if (percent >= 1.0f)
			Debug.LogError ("Pet percent is too large ,Please check");
		
		this.mUISpritePetProgress.fillAmount = Mathf.Clamp01 (this.mUISpritePetProgress.fillAmount + Mathf.Clamp01 (percent));
		return this.mUISpritePetProgress.fillAmount;
	}
	
	bool CheckPetButtonEnable ()
	{
		if (this.mUISpritePetProgress.fillAmount >= (1.0f - Mathf.Epsilon)) {
			this.ContainerChongwu.GetComponent<Collider2D> ().enabled = true;
			this.ContainerChongwu.GetComponent<UIButton> ().SetState (UIButton.State.Normal, true);
			//完成后播放一个粒子或者Tween动画,后续添加
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
		
		frameCount = 0;;

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
	void UpdateTotoalLapNumberLabel ()
	{
		//this.LabelQuanshusm.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounterInstance != null ? RaceManager.Instance.RaceCounterInstance.maxCylinderNumber.ToString () : "0";
	}
		
	void UpdateCurrentLapNumberLabel ()
	{
		//this.LabelQuanshubig.GetComponent<UILabel> ().text = RaceManager.Instance.RaceCounter != null ? RaceManager.Instance.RaceCounter.roundNum.ToString () : "0";

//		float fCurrentLap = RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.getCarRound (0):0;
//		int currentLap = Mathf.CeilToInt(fCurrentLap);
//		
//		int max = (int) (RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.maxCylinderNumber:1);
//		
//		this.LabelQuanshubig.GetComponent<UILabel> ().text = (currentLap<=max)?currentLap.ToString():max.ToString();

	}

	///////////////////////////////////////////////////////////////////////////////////////////////
		
	/// <summary>
	/// 更新当前总分数
	/// </summary>
	void UpdateScoreNumberLabel ()
	{	
		int newScore =  RaceManager.Instance.RaceCounterInstance!=null?((int)RaceManager.Instance.RaceCounterInstance.currentScore):0;
		if(newScore>this.mIntScore)
		{
			if(this.scorechangeTween!=null && true == this.scorechangeTween.IsPlaying())
			{
				this.scorechangeTween.Kill();
			}

			this.scorechangeTween = DOVirtual.Float(this.mIntScore,newScore,1.0f,delegate(float value) {
				if (RaceManager.Instance.RaceCounterInstance.CurMode == RaceCounter.RaceMode.RM_ArderDestroy)
				{				
					this.LabelFenshu.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}
				else
				{
					this.LabelFenshuzuo.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}
			});

			this.mIntScore = newScore;
		}
	}

	
	private int oldLap = 0;
	void UpdateLapTip()
	{
		return;		//破坏赛没有提示圈数

		float fCurrentLap = RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.getCarRound (0):0;
		int max = (int) (RaceManager.Instance.RaceCounterInstance!=null?RaceManager.Instance.RaceCounterInstance.maxCylinderNumber:1);
		
		if(fCurrentLap>oldLap)
		{
			if(oldLap==max)
			{
				oldLap = int.MaxValue;		
				//PanelMainUIController.Instance.ShowUIMsgBox("完成",0.5f);
			}
			else
			{
				oldLap++;
				if(oldLap>1) 
					PanelMainUIController.Instance.ShowUIMsgBox("第"+oldLap+"圈",0.5f);
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
