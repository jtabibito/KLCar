using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 商店UI控制代码
/// 2015年5月21日18:53:05
/// </summary>
public partial class ContainerShopUIController : UIControllerBase
{
	private int mIntTabButtonGroup = 0;
	private List<GameObject> containerList = new List<GameObject> (); 	

	public enum ShopType
	{
		ShopPower=1,			//体力				
		ShopDiamond = 2,		//钻石
		ShopCoin = 3,			//金币
		ShopProps = 4,			//道具
		ShopScore = 5,			//积分
	}

	public static ShopType _shopType = ShopType.ShopDiamond;

	public static ShopType shopType {
		get {
			return _shopType;
		}
		set {
			_shopType = value;
		}
	}

	private GameObject  LabelCountFeidan = null;
	private GameObject  LabelCountHudun = null;
	private GameObject  LabelCountYinshen = null;
	private GameObject  LabelCountJiasu = null;

	private GameObject  LabelGivingScore1 = null;
	private GameObject  LabelGivingScore2 = null;
	private GameObject  LabelGivingScore3 = null;
	private GameObject  LabelGivingScore4 = null;
	
	//记录道具的个数--action
	private int  countFeidan = 0;
	private int  countHudun = 0;
	private int  countYinshen = 0;
	private int  countJiasu = 0;
	private Tweener feidanChangeTween = null;
	private Tweener hudunChangeTween = null;
	private Tweener yinshenChangeTween = null;
	private Tweener jiasuChangeTween = null;

	//
	private int  countScore1 = 0;
	private int  countScore2 = 0;
	private int  countScore3 = 0;
	private int  countScore4 = 0;

	//coin
	private long  countCoin = 0;
	private Tweener coinChangeTween = null;

	//power
	private long  countPower = 0;
	private Tweener powerChangeTween = null;

	//diamond
	private long  countDiamond = 0;
	private Tweener diamondChangeTween = null;

	private List<GoodsConfigData>  goodsConfigDataList = null;
	private int   currentButtonIndex;
	
	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitTabButton ();
				
		InitConfigData ();
		InitPropsAndScoreNumsLabel ();
		InitTopStatusBar ();

		InitBuyButton ();
		InitShopData ();

// 		Find the existing UI Root
//				UIRoot root = NGUITools.FindInParents<UIRoot>(this.gameObject);
//				this.transform.localPosition = new Vector3 (0,root.activeHeight,0);
//				
//				this.transform.DOLocalMove (Vector3.zero, 0.55f).SetEase(Ease.Linear).SetEase(Ease.OutElastic);
//		this.transform.localScale = Vector3.zero;
//		this.transform.DOScale (Vector3.one, 0.05f).SetEase (Ease.OutBack).SetUpdate (true);
	}
	
	void InitButtonEvent ()
	{
		this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));
		this.ButtonJiahaoxin.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaoxin));
		this.ButtonJiahaozuan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaozuan));
		this.ButtonJiahaobi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaobi));

		this.mIntTabButtonGroup = this.Button1xin.GetComponent<UIToggle> ().group;			//选择groupID
		this.Button1xin.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickTabButton));
		this.Button2zuan.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickTabButton));
		this.Button3bi.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickTabButton));
		this.Button4daoju.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickTabButton));

		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));		//更换头像
	}

	void InitTabButton ()
	{
		switch (shopType) {
		case ShopType.ShopPower:
			this.Button1xin.GetComponent<UIToggle> ().value = true;
			break;
		case ShopType.ShopDiamond:
			this.Button2zuan.GetComponent<UIToggle> ().value = true;
			break;
		case ShopType.ShopCoin:
			this.Button3bi.GetComponent<UIToggle> ().value = true;
			break;
		case ShopType.ShopProps:
			this.Button4daoju.GetComponent<UIToggle> ().value = true;
			break;
		case ShopType.ShopScore:
			//this.Button5xin.GetComponent<UIToggle> ().value = true;
			break;
		default:
			this.Button1xin.GetComponent<UIToggle> ().value = true;
			break;
		}
		
	}
		
	void InitConfigData ()
	{
		this.goodsConfigDataList = GoodsConfigData.GetConfigDatas<GoodsConfigData> ();
	}

	/// <summary>
	/// 依次初始化购买按键
	/// </summary>
	void InitBuyButton ()
	{
		//1
		this.containerList.Clear ();
		this.containerList.Add (Container1Xin);
		this.containerList.Add (Container2zuan);
		this.containerList.Add (Container3bi);
		this.containerList.Add (Container4daoju);
		//this.containerList.Add (Container5jifen);
			
		//2
		foreach (GameObject obj in this.containerList) {
			Transform scrollViewTran = obj.transform.FindChild ("ScrollView");
			if (scrollViewTran != null) {
				foreach (Transform childTran in scrollViewTran) {
					foreach (Transform buttonTran  in childTran) {
						UIButton uiButton = buttonTran.GetComponent<UIButton> ();
						if (uiButton != null) {
							UIEventListener.Get (uiButton.gameObject).onPress = this.OnPressBuyButton;
						}
					}
				}   
			}
		}

	}

	/// <summary>
	/// 初始化
	/// </summary>
	void InitPropsAndScoreNumsLabel ()
	{
		Transform scrollViewTran2 = this.Container2zuan.transform.FindChild ("ScrollView");
		if (scrollViewTran2 != null) {
			this.LabelGivingScore1 = scrollViewTran2.transform.FindChild ("SpriteDiban1/LabelZengsongshuzi").gameObject;
			this.LabelGivingScore2 = scrollViewTran2.transform.FindChild ("SpriteDiban2/LabelZengsongshuzi").gameObject;
			this.LabelGivingScore3 = scrollViewTran2.transform.FindChild ("SpriteDiban3/LabelZengsongshuzi").gameObject;
			this.LabelGivingScore4 = scrollViewTran2.transform.FindChild ("SpriteDiban4/LabelZengsongshuzi").gameObject;
		}
		
		Transform scrollViewTran4 = this.Container4daoju.transform.FindChild ("ScrollView");
		if (scrollViewTran4 != null) {
			this.LabelCountFeidan = scrollViewTran4.transform.FindChild ("SpriteDiban1/Labelyiyoushuzi").gameObject;
			this.LabelCountHudun = scrollViewTran4.transform.FindChild ("SpriteDiban2/Labelyiyoushuzi").gameObject;
			this.LabelCountYinshen = scrollViewTran4.transform.FindChild ("SpriteDiban3/Labelyiyoushuzi").gameObject;
			this.LabelCountJiasu = scrollViewTran4.transform.FindChild ("SpriteDiban4/Labelyiyoushuzi").gameObject;
		}
	}
		
	void InitShopData ()
	{
		//1. 体力 
		Transform scrollViewTran1 = this.Container1Xin.transform.FindChild ("ScrollView");
		if (scrollViewTran1 != null) {
			//依次遍历ScrollView的子节点
			foreach (Transform childTran in scrollViewTran1) {
				string name = childTran.name;
				//返回1,2,3,4
				int i = int.Parse (name.Substring ("SpriteDiban".Length));

				string id = "1" + i;
				GoodsConfigData data = GoodsConfigData.GetConfigData<GoodsConfigData> (id);
				//数量
				GameObject obj = childTran.FindChild ("LabelShuzi").gameObject;
				obj.GetComponent<UILabel> ().text = data.goodsNum.ToString ();
					
				//消耗的货币数
				string button = "Button" + i + "/Label";
				GameObject objMoney = childTran.FindChild (button).gameObject;
				objMoney.GetComponent<UILabel> ().text = data.costValue.ToString ();
			} 
		}

		//2. 钻石
		Transform scrollViewTran2 = this.Container2zuan.transform.FindChild ("ScrollView");
		if (scrollViewTran2 != null) {
			//依次遍历ScrollView的子节点
			foreach (Transform childTran in scrollViewTran2) {
				string name = childTran.name;
				//返回1,2,3,4
				int i = int.Parse (name.Substring ("SpriteDiban".Length));
					
				string id = "2" + i;
				GoodsConfigData data = GoodsConfigData.GetConfigData<GoodsConfigData> (id);
				//数量
				GameObject obj = childTran.FindChild ("LabelShuzi").gameObject;
				obj.GetComponent<UILabel> ().text = data.goodsNum.ToString ();
					
				//消耗的货币数
				string button = "Button" + i + "/Label";
				GameObject objMoney = childTran.FindChild (button).gameObject;
				objMoney.GetComponent<UILabel> ().text = data.costValue.ToString ();
			} 
		}
			
		//3. 金币
		Transform scrollViewTran3 = this.Container3bi.transform.FindChild ("ScrollView");
		if (scrollViewTran3 != null) {
			//依次遍历ScrollView的子节点 
			foreach (Transform childTran in scrollViewTran3) {
				string name = childTran.name;
				//返回1,2,3,4
				int i = int.Parse (name.Substring ("SpriteDiban".Length));
					
				string id = "3" + i;
				GoodsConfigData data = GoodsConfigData.GetConfigData<GoodsConfigData> (id);
				//数量
				GameObject obj = childTran.FindChild ("LabelShuzi").gameObject;
				obj.GetComponent<UILabel> ().text = data.goodsNum.ToString ();
					
				//消耗的货币数
				string button = "Button" + i + "/Label";
				GameObject objMoney = childTran.FindChild (button).gameObject;
				objMoney.GetComponent<UILabel> ().text = data.costValue.ToString ();  
			} 
		}

		//4. 道具
		Transform scrollViewTran4 = this.Container4daoju.transform.FindChild ("ScrollView");
		if (scrollViewTran4 != null) {
			//依次遍历ScrollView的子节点
			foreach (Transform childTran in scrollViewTran4) {
				string name = childTran.name;
				//返回1,2,3,4
				int i = int.Parse (name.Substring ("SpriteDiban".Length));
					
				string id = "4" + i;
				GoodsConfigData data = GoodsConfigData.GetConfigData<GoodsConfigData> (id);
				//数量
				GameObject obj = childTran.FindChild ("LabelShuzi").gameObject;
				obj.GetComponent<UILabel> ().text = data.goodsNum.ToString ();
					
				//消耗的货币数
				string button = "Button" + i + "/Label";
				GameObject objMoney = childTran.FindChild (button).gameObject;
				objMoney.GetComponent<UILabel> ().text = data.costValue.ToString ();
			} 
		}
	}

	void InitTopStatusBar ()
	{
		if(MainState.Instance.playerInfo!=null)
		{
			this.countCoin = MainState.Instance.playerInfo.gold;
			this.countPower = MainState.Instance.playerInfo.power;
			this.countDiamond = MainState.Instance.playerInfo.diamond;
		}
		else
		{
			this.countCoin = 0;
			this.countPower = 0;
			this.countDiamond = 0;
		}

		this.LabelXinshuzi.GetComponent<UILabel> ().text = this.countPower.ToString ();
		this.LabelBishuzi.GetComponent<UILabel> ().text = this.countCoin.ToString ();
		this.LabelZuanshuzi.GetComponent<UILabel> ().text = this.countDiamond.ToString ();

		this.LabelCountFeidan.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.Feidanitem1Num.ToString ();
		this.LabelCountHudun.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.Hudunitem2Num.ToString ();
		this.LabelCountYinshen.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.Yinshenitem3Num.ToString ();
		this.LabelCountJiasu.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.Jiasuitem4Num.ToString ();
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateTopStatusBar ();
	}


	/// <summary>
	/// 头像,名称，体力，金币，钻石个数，邮件个数，任务个数
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
		
		//3.体力
		if (MainState.Instance.playerInfo != null) {
			if (MainState.Instance.playerInfo.power != this.countPower) {
				if (this.powerChangeTween != null && true == this.powerChangeTween.IsPlaying ()) {
					this.powerChangeTween.Kill ();
				}
				
				this.powerChangeTween = DOVirtual.Float (this.countPower, MainState.Instance.playerInfo.power, 1.0f, delegate(float value) {
					this.LabelXinshuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);
				
				this.powerChangeTween.SetEase (Ease.Linear);
				this.countPower = MainState.Instance.playerInfo.power;
			}
		}

		//4. 金币
		if (MainState.Instance.playerInfo != null) {

			if (_shopType == ShopType.ShopCoin || _shopType == ShopType.ShopProps) {
				if (MainState.Instance.playerInfo.gold != this.countCoin) {
					if (this.coinChangeTween != null && true == this.coinChangeTween.IsPlaying ()) {
						this.coinChangeTween.Kill ();
					}
					
					this.coinChangeTween = DOVirtual.Float (this.countCoin, MainState.Instance.playerInfo.gold, 1.0f, delegate(float value) {
						this.LabelBishuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
					}).SetUpdate (UpdateType.Normal,true);
					
					this.coinChangeTween.SetEase (Ease.Linear);
					this.countCoin = MainState.Instance.playerInfo.gold;
				}
			} else {
				this.LabelBishuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.gold.ToString ();
			}
		}

		//5. 钻石
		if (MainState.Instance.playerInfo != null) {

			if (_shopType == ShopType.ShopCoin || _shopType == ShopType.ShopPower ||_shopType == ShopType.ShopDiamond) {		//金币和体力Tab 是 钻石减少的
				if (MainState.Instance.playerInfo.diamond != this.countDiamond) {
					if (this.diamondChangeTween != null && true == this.diamondChangeTween.IsPlaying ()) {
						this.diamondChangeTween.Kill ();
					}
					
					this.diamondChangeTween = DOVirtual.Float (this.countDiamond, MainState.Instance.playerInfo.diamond, 1.0f, delegate(float value) {
						this.LabelZuanshuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
					}).SetUpdate (UpdateType.Normal,true);
					
					this.diamondChangeTween.SetEase (Ease.Linear);
					this.countDiamond = MainState.Instance.playerInfo.diamond;
				}
			} else {
				this.LabelZuanshuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.diamond.ToString ();
			}
		}

		//6.Prop
		if (MainState.Instance.playerInfo != null) {

			//a
			if (MainState.Instance.playerInfo.Feidanitem1Num != this.countFeidan) {
				if (this.feidanChangeTween != null && true == this.feidanChangeTween.IsPlaying ()) {
					this.feidanChangeTween.Kill ();
				}

				this.feidanChangeTween = DOVirtual.Float (this.countFeidan, MainState.Instance.playerInfo.Feidanitem1Num, 1.0f, delegate(float value) {
					this.LabelCountFeidan.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);

				this.feidanChangeTween.SetEase (Ease.Linear);
				this.countFeidan = MainState.Instance.playerInfo.Feidanitem1Num;
			}

			//b
			if (MainState.Instance.playerInfo.Hudunitem2Num !=  this.countHudun) {
				if (this.hudunChangeTween != null && true == this.hudunChangeTween.IsPlaying ()) {
					this.hudunChangeTween.Kill ();
				}
				
				this.hudunChangeTween = DOVirtual.Float (this.countHudun, MainState.Instance.playerInfo.Hudunitem2Num, 1.0f, delegate(float value) {
					this.LabelCountHudun.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);

				this.hudunChangeTween.SetEase (Ease.Linear);
				this.countHudun = MainState.Instance.playerInfo.Hudunitem2Num;
			}

			//c
			if (MainState.Instance.playerInfo.Yinshenitem3Num !=  this.countYinshen) {
				if (this.yinshenChangeTween != null && true == this.yinshenChangeTween.IsPlaying ()) {
					this.yinshenChangeTween.Kill ();
				}
				
				this.yinshenChangeTween = DOVirtual.Float (this.countYinshen, MainState.Instance.playerInfo.Yinshenitem3Num, 1.0f, delegate(float value) {
					this.LabelCountYinshen.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);

				this.yinshenChangeTween.SetEase (Ease.Linear);
				this.countYinshen = MainState.Instance.playerInfo.Yinshenitem3Num;
			}
			
			//d
			if (MainState.Instance.playerInfo.Jiasuitem4Num != this.countJiasu) {
				if (this.jiasuChangeTween != null && true == this.jiasuChangeTween.IsPlaying ()) {
					this.jiasuChangeTween.Kill ();
				}
				
				this.jiasuChangeTween = DOVirtual.Float (this.countJiasu, MainState.Instance.playerInfo.Jiasuitem4Num, 1.0f, delegate(float value) {
					this.LabelCountJiasu.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);
				this.jiasuChangeTween.SetEase (Ease.Linear);
				
				this.countJiasu = MainState.Instance.playerInfo.Jiasuitem4Num;
			}
		}
	}

	/// <summary>
	/// 购买
	/// </summary>
	void OnPressBuyButton (GameObject obj, bool state)
	{
//		根据NGUITools.GetHierarchy(obj)的值或者 _shopType ，进行商店的处理
//		UIMain\Camera\PanelMain\PanelButtom\ContainerShop\Container1Xin\ScrollView\SpriteDiban1\Button1

		if (state == false) {
			string num = obj.name.Substring ("Button".Length);
			this.currentButtonIndex = int.Parse (num);
					
			string goodsId = this.GetGoodsConfigId ((int)_shopType, int.Parse (num));
			Hashtable logicPar = new Hashtable ();
			logicPar.Add ("goodsId", goodsId);
			LogicManager.Instance.ActNewLogic<LogicShop> (logicPar, this.OnBuyOver);
		}
	}

	void OnBuyOver (Hashtable logicPar)
	{
		LogicReturn lr = (LogicReturn)logicPar ["logicReturn"];
		switch (lr) {
		case LogicReturn.LR_SUCCESS:
			PanelMainUIController.Instance.ShowUIMsgBox ("购买成功!", 1.15f);
			break;
		default:

			if (_shopType == ShopType.ShopProps) {
				PanelMainUIController.Instance.ShowUIMsgBox ("游戏道具 购买失败!  请准备充足的金币", 2.0f);

				DOVirtual.DelayedCall (1.0f, delegate {
					this.Button3bi.GetComponent<UIToggle> ().value = true;
				}, true);

			} else if (_shopType == ShopType.ShopCoin) {
				PanelMainUIController.Instance.ShowUIMsgBox ("金币 购买失败!   请准备充足的钻石", 1.15f);
			} else if (_shopType == ShopType.ShopPower) {
				PanelMainUIController.Instance.ShowUIMsgBox ("体力 购买失败!   请准备充足的钻石", 1.15f);
			} else if (_shopType == ShopType.ShopDiamond) {
				PanelMainUIController.Instance.ShowUIMsgBox ("钻石 购买失败!", 1.15f);
			} else {
				PanelMainUIController.Instance.ShowUIMsgBox ("积分 购买失败!", 1.15f);
			}

			break;
		}
	}


	/// <summary>
	/// 返回----自我销毁
	/// </summary>
	void OnClickButtonFanhui ()
	{
		//NGUITools.SetActive(this.ContainerBj,false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.feidanChangeTween.Kill ();
			this.hudunChangeTween.Kill ();
			this.yinshenChangeTween.Kill ();
			this.jiasuChangeTween.Kill ();
			this.coinChangeTween.Kill ();
			this.powerChangeTween.Kill ();
			this.diamondChangeTween.Kill ();

			PanelMainUIController.Instance.CloseUIMsgBox ();
			this.CloseUI ();
		}).SetUpdate (true);
	}

	/// <summary>
	/// 加好心-----效果同点击体力Tab一样
	/// </summary>
	void OnClickButtonJiahaoxin ()
	{
		this.Button1xin.GetComponent<UIToggle> ().value = true;
	}

	/// <summary>
	/// 加钻石-----效果同点击钻石Tab一样
	/// </summary>
	void OnClickButtonJiahaozuan ()
	{
		this.Button2zuan.GetComponent<UIToggle> ().value = true;
	}

	/// <summary>
	/// 加金币-----效果同点击钻金币ab一样
	/// </summary>
	void OnClickButtonJiahaobi ()
	{
		this.Button3bi.GetComponent<UIToggle> ().value = true;
	}

	void OnClickTabButton ()
	{
		UIToggle toggle = UIToggle.GetActiveToggle (this.mIntTabButtonGroup);
		if (toggle == null || (toggle != null && toggle.value == false)) {
			return;
		}
		
		//Debug.Log(toggle.name+"  Value: " +toggle.value);
		//根据类型设置相关的数据
		if (toggle.name.Equals (this.Button1xin.name)) {
			_shopType = ShopType.ShopPower;
		} else if (toggle.name.Equals (this.Button2zuan.name)) {
			_shopType = ShopType.ShopDiamond;
		} else if (toggle.name.Equals (this.Button3bi.name)) {
			_shopType = ShopType.ShopCoin;
		} else if (toggle.name.Equals (this.Button4daoju.name)) {
			_shopType = ShopType.ShopProps;
		}
//		else if (toggle.name.Equals (this.Button5jifen.name)) {
//			_shopType = ShopType.ShopScore;
//		} 
		else {
			Debug.LogWarning ("Please add new toggle to current group");
		}
	}

	/// <summary>
	/// 更换头像
	/// </summary>
	void OnClickSpriteTouxiang ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Top, UIControllerConst.UIPrefebTouxiang);
	}

	string GetGoodsConfigId (int shopTypeValue, int buttonIndex)
	{
		return (shopTypeValue * 10 + buttonIndex).ToString ();
	}
}
