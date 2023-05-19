using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 宠物选择UI界面控制代码
/// 2015年5月5日10:18:06
/// </summary>
public partial class ContainerChongwuUIController : UIControllerBase
{
	/// <summary>
	/// 状态：锁定 和 未锁定---{细分为选中状态，未选中状态}
	/// </summary>
	private	int itemCount = 0;
	private	int currentIndex = 0;										//左右箭头 用到的Index
	private int currentSelectIndex = 0;									//当前用户正在选中的车index
	private List<int> lockIndexList = new List<int> ();					//当前锁定对象的List列表，个数必须要<=itemCount;内部管理
	private List<GameObject> lockUIList = new List<GameObject> ();		//只保存锁定状态需要变更的UI对象，通过active激活
	private List<GameObject> unlockUIList = new List<GameObject> (); 	//只保存未锁定状态需要变更的UI对象，通过active激活
	
	private GameObject uiShow2D = null;
	private string 	   currentPetPrefebName;							//应该从配置表数据库中得到这种值

	private bool  isDraging = false;
	private float dragSum = 0.0f;

	//1. 拿到车辆信息
	private List<PetConfigData>  petConfigDataList = null;
	private long  countCoin = 0;
	private Tweener coinChangeTween = null;
	private long  countPower = 0;
	private Tweener powerChangeTween = null;
	private long  countDiamond = 0;
	private Tweener diamondChangeTween = null;


	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitUIStartAnimation ();

		InitConfigData ();

		InitLockUIList ();
		InitUnlockUIList ();

		InitLockIndexList ();
		InitCurrentIndex ();
		SetupRightUI (this.currentIndex);

		InitTopStatusBar ();

		InitPetPrefebName ();
		InitPetAndStartAnimation (currentPetPrefebName);

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
		
		this.ButtonJiesuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiesuo));						//解锁
		this.ButtonXuanzhong.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonXuanzhong));				//选中--与解锁是互斥，如果当前用户就是这个要灰色
//				this.ButtonShengjizuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengjizuo));				//升级
//				this.ButtonMaxzuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonMaxzuo));						//最大
		
		this.ButtonYou.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYou));							//右箭头
		this.ButtonZuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZuo));							//左箭头

		UIEventListener.Get (this.ContainerHuadong).onPress = this.OnPressButtonDrag;											//记录按下和抬起
		UIEventListener.Get (this.ContainerHuadong).onDrag = OnDragUI;															/// 初始化滚动事件，车辆，宠物，角色等信息进行旋转
		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));		//更换头像
	}
	
	void InitUIStartAnimation ()
	{
		//1.
		this.ButtonZuo.transform.DOLocalMoveX (UIOriginalPositionButtonZuo.x - 15, 2.5f).SetLoops (-1).SetEase (Ease.OutBounce);
		this.ButtonYou.transform.DOLocalMoveX (UIOriginalPositionButtonYou.x + 15, 2.5f).SetLoops (-1).SetEase (Ease.OutBounce);

		//2.
		this.LabelNpcduihuakuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelNpcduihuakuang.x, UIOriginalPositionLabelNpcduihuakuang.y + 40, UIOriginalPositionLabelNpcduihuakuang.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelNpcduihuakuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelNpcduihuakuang.x, UIOriginalPositionLabelNpcduihuakuang.y, UIOriginalPositionLabelNpcduihuakuang.z), 0.5f, true)
						.SetEase (Ease.Linear);
		});


		this.SpriteNpc.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteNpc.x, UIOriginalPositionSpriteNpc.y + 40, UIOriginalPositionSpriteNpc.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteNpc.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteNpc.x, UIOriginalPositionSpriteNpc.y, UIOriginalPositionSpriteNpc.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		//3.
		this.ButtonXuanzhong.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXuanzhong.x - 40, UIOriginalPositionButtonXuanzhong.y, UIOriginalPositionButtonXuanzhong.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonXuanzhong.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXuanzhong.x, UIOriginalPositionButtonXuanzhong.y, UIOriginalPositionButtonXuanzhong.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelMiaoshu.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelMiaoshu.x - 40, UIOriginalPositionLabelMiaoshu.y, UIOriginalPositionLabelMiaoshu.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelMiaoshu.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelMiaoshu.x, UIOriginalPositionLabelMiaoshu.y, UIOriginalPositionLabelMiaoshu.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelJinengming.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJinengming.x - 40, UIOriginalPositionLabelJinengming.y, UIOriginalPositionLabelJinengming.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelJinengming.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJinengming.x, UIOriginalPositionLabelJinengming.y, UIOriginalPositionLabelJinengming.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelCheliangmingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelCheliangmingcheng.x - 40, UIOriginalPositionLabelCheliangmingcheng.y, UIOriginalPositionLabelCheliangmingcheng.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelCheliangmingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelCheliangmingcheng.x, UIOriginalPositionLabelCheliangmingcheng.y, UIOriginalPositionLabelCheliangmingcheng.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ContainerBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerBackground.x - 40, UIOriginalPositionContainerBackground.y, UIOriginalPositionContainerBackground.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ContainerBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerBackground.x, UIOriginalPositionContainerBackground.y, UIOriginalPositionContainerBackground.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});
	}

	void InitConfigData ()
	{
		Debug.Log ("初始化了 InitConfigData");
		this.petConfigDataList = PetConfigData.GetConfigDatas<PetConfigData> ();
	}

	void InitPetPrefebName ()
	{
		//从数据层得到参数，暂时手动修改 carAvt----------暂时显示第1个
		currentPetPrefebName = this.petConfigDataList [this.currentSelectIndex].petAvt;
	}
	
	/// <summary>
	/// 添加3D角色
	/// </summary>
	/// <param name="petPrefebName">Pet prefeb name.</param>
	void InitPetAndStartAnimation (string petPrefebName)
	{
		this.uiShow2D = NGUITools.AddChild (null, GameResourcesManager.GetUIPrefab (UIControllerConst.UIPrefebUIShow2D));
		this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CreatePet (petPrefebName);
		this.uiShow2D.GetComponent<ui_show_2DUIController> ().StartPetAnim ();
	}

	/// <summary>
	/// 只保存锁定状态需要变更的UI对象
	/// </summary>
	void InitLockUIList ()
	{
		this.lockUIList.Clear ();
		this.lockUIList.Add (this.SpriteSuo);
		this.lockUIList.Add (this.ButtonJiesuo);

		this.lockUIList.Add (this.SpriteXiaojinbi);				//解锁的金币图标
		this.lockUIList.Add (this.SpriteXiaozuan);
		this.lockUIList.Add (this.SpriteXiaojuan);

		this.lockUIList.Add (this.LabelJiesuoshuzi);		//解锁的数字Label
	}
	
	/// <summary>
	/// 只保存未锁定状态需要变更的UI对象  max和升级 按钮，后面在根据currentIndex等进行是否激活判断
	/// 通过tag进行
	/// </summary>
	void InitUnlockUIList ()
	{
		this.unlockUIList.Clear ();

		this.unlockUIList.Add (ButtonXuanzhong);
		this.unlockUIList.Add (SpriteGou);
	}
	
	/// <summary>
	/// 从数据层得到那些车辆是加锁的---这里暂时模拟
	/// </summary>
	void InitLockIndexList ()
	{
		if (MainState.Instance.playerInfo != null) {
			int indexUnlock;
			
			//1. 默认所有的车辆信息都未解锁，然后用排除法
			for (indexUnlock=0; indexUnlock<this.petConfigDataList.Count; indexUnlock++) {
				this.lockIndexList.Add (int.Parse (this.petConfigDataList [indexUnlock].id) - 1);				//由于Id从1开始,而UI从0开始
			}
			
			//2. 排除法
			foreach (MyGameProto.PetData data in MainState.Instance.playerInfo.petDatas) {
				this.lockIndexList.Remove (int.Parse (data.id) - 1);
			}
		}

	}
	
	/// <summary>
	/// 从数据层得到那些车辆是加锁的---这里暂时模拟
	/// </summary>
	void InitCurrentIndex ()
	{
		if (MainState.Instance.playerInfo != null)
			this.currentSelectIndex = int.Parse (MainState.Instance.playerInfo.nowPetId) - 1;
		else
			this.currentSelectIndex = 0;
				
		currentIndex = this.currentSelectIndex;
		itemCount = this.petConfigDataList.Count;
				
		CheckButtonArrowEnable (currentIndex);//可能有只有1个子节点，所以左右两边button都隐藏

	}

	void InitTopStatusBar ()
	{
		if (MainState.Instance.playerInfo != null) {
			this.countCoin = MainState.Instance.playerInfo.gold;
			this.countPower = MainState.Instance.playerInfo.power;
			this.countDiamond = MainState.Instance.playerInfo.diamond;
		} else {
			this.countCoin = 0;
			this.countPower = 0;
			this.countDiamond = 0;
		}
		
		this.LabelXinshuzi.GetComponent<UILabel> ().text = this.countPower.ToString ();
		this.LabelBishuzi.GetComponent<UILabel> ().text = this.countCoin.ToString ();
		this.LabelZuanshuzi.GetComponent<UILabel> ().text = this.countDiamond.ToString ();
	}

	/// <summary>
	/// 根据当前index是否已经解锁，显示相应的UI
	/// </summary>
	void SetupRightUI (int index)
	{
		Debug.Log ("配置表是数据 " + this.petConfigDataList == null);

		if (index < 0 || index >= this.petConfigDataList.Count) {
			Debug.LogWarning ("Please check this code,valide range is [0," + (this.petConfigDataList.Count - 1) + "]" + "But param index is " + index);
			return;
		}

		//1. 根据当前的 currentIndex ，从Data层取数据
		this.LabelCheliangmingcheng.GetComponent<UILabel> ().text = this.petConfigDataList [index].petName;
		this.LabelJinengming.GetComponent<UILabel> ().text = this.petConfigDataList [index].skillName;
		this.LabelMiaoshu.GetComponent<UILabel> ().text = this.petConfigDataList [index].skillDescription;
		this.LabelNpcduihuakuang.GetComponent<UILabel> ().text = this.petConfigDataList [index].description;

		//2. 判断当前currentIndex是否已经加锁
		bool indexIsLock = isIndexLock (index);
		this.setLockUIDisplay (indexIsLock);
		
		//3. 如果是未锁定状态，则判断当前是否锁定状态
		if (indexIsLock == false) {
			if (currentSelectIndex == index) {
				SetSelectButtonEnable (false);
			} else {
				SetSelectButtonEnable (true);
			}
		}
	}
	
	/// <summary>
	/// 判断当前Index是否是锁定状态的车辆
	/// </summary>
	/// <returns><c>true</c>, if index lock was ised, <c>false</c> otherwise.</returns>
	/// <param name="index">Index.</param>
	bool isIndexLock (int index)
	{
		if (this.lockIndexList.Count <= 0) {
			Debug.Log ("lockIndexList count is 0,so all items have unlock ");
			return false;
		}
		
		foreach (int i in this.lockIndexList) {
			if (i == index)
				return true;
		}
		return false;
	}
	
	/// <summary>
	/// 设置是否显示锁相关的界面
	/// </summary>
	/// <param name="enable">If set to <c>true</c> enable.</param>
	void setLockUIDisplay (bool enable)
	{
		//1.
		if (enable == true) {
			// 1. 开始显示加锁状态的界面  enable
			//UpdatelockUIData(index)
			foreach (GameObject obj in lockUIList) {
				obj.SetActive (true);		//但是数值必须要先初始化好，更新好
			}

			//2. disable 
			foreach (GameObject obj in unlockUIList) {
				obj.SetActive (false);		//但是数值必须要先初始化好，更新好
			}

			//3. 解锁消耗币种(1金币、2钻石、3积分）
			NGUITools.SetActive (this.SpriteXiaojinbi, false);
			NGUITools.SetActive (this.SpriteXiaozuan, false);
			NGUITools.SetActive (this.SpriteXiaojuan, false);
			
			PetConfigData petConfigData = this.petConfigDataList [this.currentIndex];
			int costType = petConfigData.costTypeOfGain;
			if (costType == 1) {
				NGUITools.SetActive (this.SpriteXiaojinbi, true);
			} else if (costType == 2) {
				NGUITools.SetActive (this.SpriteXiaozuan, true);
			} else if (costType == 3) {
				NGUITools.SetActive (this.SpriteXiaojuan, true);
			}	

			//4.解锁需要的价格
			this.LabelJiesuoshuzi.GetComponent<UILabel> ().text = petConfigData.costValueOfGain.ToString ();

			this.SpriteSuo.transform.DOKill ();
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.SpriteSuo.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
			mySeq.Append (this.SpriteSuo.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));

		} else {
			//1 开始显示解锁状态的界面
			//UpdateUnlockUIData(index)
			foreach (GameObject obj in lockUIList) {
				obj.SetActive (false);		//但是数值必须要先初始化好，更新好
			}

			//2 enable
			foreach (GameObject obj in unlockUIList) {
				obj.SetActive (true);		//但是数值必须要先初始化好，更新好
			}

			//3 勾选状态的特效
			this.SpriteGou.transform.DOKill ();
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
			mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));
		}
	}
	
	/// <summary>
	/// 选中和未选中 的切换,参数enable表示Button是否使能
	/// </summary>
	void SetSelectButtonEnable (bool enable)
	{
		if (enable == false) {
			this.SpriteGou.SetActive (true);
			this.ButtonXuanzhong.GetComponent<Collider2D> ().enabled = false;
			this.ButtonXuanzhong.GetComponent<UIButton> ().SetState (UIButton.State.Disabled, true);
		} else {
			this.SpriteGou.SetActive (false);
			this.ButtonXuanzhong.GetComponent<Collider2D> ().enabled = true;
			this.ButtonXuanzhong.GetComponent<UIButton> ().SetState (UIButton.State.Normal, true);
		}
	}

	/// <summary>
	/// 头像,名称，好心，金币，钻石个数，邮件个数，任务个数------从数据层获得
	/// </summary>
	void UpdateTopStatusBar ()
	{
		//1.头像----后期用表格保存图片与ID的对应关系
		if (MainState.Instance.playerInfo != null) {
			string xx = MainState.Instance.playerInfo.userRoleImgID < 10 ? ("0" + MainState.Instance.playerInfo.userRoleImgID) : MainState.Instance.playerInfo.userRoleImgID.ToString ();
			this.SpriteTouxiang.GetComponent<UISprite> ().spriteName = "ui_role_" + xx.ToString ();
			//this.SpriteNpc.GetComponent<UISprite> ().spriteName = "ui_rolebanshen_" + xx.ToString ();
		}
			
		//2.昵称
		if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nickname.Length > 0) {
			this.LabelMingzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.nickname;
		}
		if (MainState.Instance.playerInfo != null) {
			if (MainState.Instance.playerInfo.power != this.countPower) {
				if (this.powerChangeTween != null && true == this.powerChangeTween.IsPlaying ()) {
					this.powerChangeTween.Kill ();
				}
				
				this.powerChangeTween = DOVirtual.Float (this.countPower, MainState.Instance.playerInfo.power, 1.0f, delegate(float value) {
					this.LabelXinshuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal, true);
				
				this.powerChangeTween.SetEase (Ease.Linear);
				this.countPower = MainState.Instance.playerInfo.power;
			}
		}
		
		if (MainState.Instance.playerInfo != null) {
			if (MainState.Instance.playerInfo.gold != this.countCoin) {
				if (this.coinChangeTween != null && true == this.coinChangeTween.IsPlaying ()) {
					this.coinChangeTween.Kill ();
				}
					
				this.coinChangeTween = DOVirtual.Float (this.countCoin, MainState.Instance.playerInfo.gold, 1.0f, delegate(float value) {
					this.LabelBishuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal, true);
					
				this.coinChangeTween.SetEase (Ease.Linear);
				this.countCoin = MainState.Instance.playerInfo.gold;
			}
		}

		if (MainState.Instance.playerInfo != null) {

			if (MainState.Instance.playerInfo.diamond != this.countDiamond) {
				if (this.diamondChangeTween != null && true == this.diamondChangeTween.IsPlaying ()) {
					this.diamondChangeTween.Kill ();
				}
				
				this.diamondChangeTween = DOVirtual.Float (this.countDiamond, MainState.Instance.playerInfo.diamond, 1.0f, delegate(float value) {
					this.LabelZuanshuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal, true);
				
				this.diamondChangeTween.SetEase (Ease.Linear);
				this.countDiamond = MainState.Instance.playerInfo.diamond;
			}
		}

	}

	void Clean3DRole ()
	{
		if (uiShow2D != null) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CleanAllAvt ();
			this.uiShow2D = null;
		}
	}
	
	/// <summary>
	/// 根据index得到角色模型的名称
	/// </summary>
	/// <returns>The pet Prefet name.</returns>
	/// <param name="index">Index.</param>
	string GetPetName (int index)
	{	
		if (index < 0 || index >= this.petConfigDataList.Count) {
			Debug.LogError ("Please check this code,valide range is [0," + (this.petConfigDataList.Count - 1) + "]" + "But param index is " + index);
			//默认返回第1个车辆
			if (this.petConfigDataList.Count > 0)
				return this.petConfigDataList [0].petAvt;
				
			return "PetAvt1";
		}
			
		return  this.petConfigDataList [index].petAvt;
	}
	
	
	/// <summary>
	/// 返回
	/// </summary>
	void OnClickButtonFanhui ()
	{
		this.Clean3DRole ();
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
	/// 解锁  ，更新lockIndexList ，同时通过Logic更新Logic层
	/// </summary>
	void OnClickButtonJiesuo ()
	{
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("petId", this.petConfigDataList [this.currentIndex].id);
		LogicManager.Instance.ActNewLogic<LogicGainPet> (logicPar, OnUnLockFinish);
	}


	// logicPar.Add("petId",this.petConfigDataList[this.currentIndex].id);
	void OnUnLockFinish (Hashtable logicPar)
	{
		LogicReturn result = (LogicReturn)logicPar ["logicReturn"];
		
		switch (result) {
		case LogicReturn.LR_NOTENOUGHDIAMOND:
			PanelMainUIController.Instance.ShowUIMsgBox (this.petConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);			
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;

			}, true);
			break;
		case LogicReturn.LR_NOTENOUGHGOLD:
			PanelMainUIController.Instance.ShowUIMsgBox (this.petConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_NOTENOUGHSCORE:
			// 后期改为积分商店
			PanelMainUIController.Instance.ShowUIMsgBox (this.petConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_REACHEDMAXLV:
			break;
		case LogicReturn.LR_SUCCESS:
			this.lockIndexList.Remove (currentIndex);
			//开始刷新界面
			this.SetupRightUI (currentIndex);
			break;
		}
	}

	/// <summary>
	/// 选中--与解锁是互斥，如果当前用户就是选择的这个要灰色
	/// </summary>
	void OnClickButtonXuanzhong ()
	{
		this.currentSelectIndex = this.currentIndex;
		this.SetSelectButtonEnable (false);

		if (MainState.Instance.playerInfo != null)
			MainState.Instance.playerInfo.nowPetId = (this.currentSelectIndex + 1).ToString ();
				
		//2添加一个出场缩放动画
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));
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
		
		if (index >= this.itemCount - 1)
			ButtonYou.SetActive (false);
		else
			ButtonYou.SetActive (true);
	}
	
	/// <summary>
	/// 右箭头
	/// </summary>
	void OnClickButtonYou ()
	{
		if (currentIndex >= itemCount - 1)
			return;
		
		///1 lock or not
		this.currentIndex++;
		this.SetupRightUI (this.currentIndex);
		///2. add new car 
		this.Clean3DRole ();
		this.currentPetPrefebName = this.GetPetName (this.currentIndex);
		InitPetAndStartAnimation (currentPetPrefebName);
		
		///3. hide arrow button or not
		CheckButtonArrowEnable (currentIndex);
	}
	
	/// <summary>
	/// 左箭头
	/// </summary>
	void OnClickButtonZuo ()
	{
		if (currentIndex <= 0)
			return;
		
		this.currentIndex--;
		this.SetupRightUI (currentIndex);
		
		this.Clean3DRole ();
		this.currentPetPrefebName = this.GetPetName (this.currentIndex);
		InitPetAndStartAnimation (currentPetPrefebName);
		
		CheckButtonArrowEnable (currentIndex);
	}

	void OnDragUI (GameObject obj, Vector2 delta)
	{
		if (isDraging == true) {
				
			dragSum += delta.x;
			float afterW = Screen.width * this.ContainerHuadong.GetComponent<UIWidget> ().width / 1280.0f;
			if (Mathf.Abs (dragSum) <= afterW)
				this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotatePet (delta.x, afterW);
		}
	}
		
	void OnPressButtonDrag (GameObject go, bool state)
	{
		isDraging = state;
		dragSum = 0.0f;
			
		if (state == false) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotatePet (0, 0, true);
		}
	}

	/// <summary>
	/// 更换头像
	/// </summary>
	void OnClickSpriteTouxiang ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTouxiang);
	}
}
