using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 车辆选择界面UI控制代码
/// 2015年4月24日15:49:38
/// </summary>
public partial class ContainerCheliangUIController : UIControllerBase
{
	/// <summary>
	/// 状态：锁定 和 未锁定---{细分为选中状态，未选中状态}{细分为已经升级到MAX，未升级到MAX}
	/// </summary>
	private	int itemCount = 0;
	private	int currentIndex = 0;										//左右箭头 用到的Index
	private int currentSelectIndex = 0;									//当前用户正在选中的车index
	private List<int> lockIndexList = new List<int> ();					//当前锁定对象的List列表，个数必须要<=itemCount;内部管理
	private List<GameObject> lockUIList = new List<GameObject> ();		//只保存锁定状态需要变更的UI对象，通过active激活
	private List<GameObject> unlockUIList = new List<GameObject> (); 	//只保存未锁定状态需要变更的UI对象，通过active激活

	private GameObject uiShow2D = null;
	private string 	   currentCarPrefebName;							//应该从配置表数据库中得到这种值

	private bool  isDraging = false;
	private float dragSum = 0.0f;

	//锁定状态的Label和进度条
	private GameObject lockLabelZhihoushu1Handler;								//操控性
	private GameObject lockLabelZhihoushu2maxSpeed;								//最高速
	private GameObject lockLabelZhihoushu3jiasu;								//加速
	private GameObject lockSpriteShengjijindu1Handler;							//进度条1
	private GameObject lockSpriteShengjijindu2maxSpeed;							//进度条2
	private GameObject lockSpriteShengjijindu3jiasu;							//进度条3


	//解锁界面UI的3个升级箭头
	private GameObject SpriteShengjijiantou1Handler;
	private GameObject SpriteShengjijiantou2maxSpeed;
	private GameObject SpriteShengjijiantou3jiasu;

		
	//1. 拿到车辆信息
	private List<CarConfigData>  carConfigDataList = null;// = new List<CarConfigData> ();

	private long  countCoin = 0;
	private Tweener coinChangeTween = null;
	private long  countPower = 0;
	private Tweener powerChangeTween = null;
	private long  countDiamond = 0;
	private Tweener diamondChangeTween = null;

	//车辆信息进度条
	private float  levelupValue1 = 0;
	private Tweener levelupValue1ChangeTween = null;
	private float  levelupValue2 = 0;
	private Tweener levelupValue2ChangeTween = null;
	private float  levelupValue3 = 0;
	private Tweener levelupValue3ChangeTween = null;

	//二级进度条底图
	private Tweener value1MaxBGTween = null;
	private Tweener value2MaxBGTween = null;
	private Tweener value3MaxBGTween = null;
	
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

		InitCarPrefebName ();
		InitCarAndStartAnimation (currentCarPrefebName);

		UpdateTopStatusBar ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTopStatusBar ();
	}
	
	void InitButtonEvent ()
	{
		this.lockLabelZhihoushu1Handler = this.ContainerWeijiesuorongqi.transform.FindChild ("LabelZhihoushu1").gameObject;
		this.lockLabelZhihoushu2maxSpeed = this.ContainerWeijiesuorongqi.transform.FindChild ("LabelZhihoushu2").gameObject;
		this.lockLabelZhihoushu3jiasu = this.ContainerWeijiesuorongqi.transform.FindChild ("LabelZhihoushu3").gameObject;

		this.lockSpriteShengjijindu1Handler = this.ContainerWeijiesuorongqi.transform.FindChild ("SpriteShengjijindu1").gameObject;
		this.lockSpriteShengjijindu2maxSpeed = this.ContainerWeijiesuorongqi.transform.FindChild ("SpriteShengjijindu2").gameObject;
		this.lockSpriteShengjijindu3jiasu = this.ContainerWeijiesuorongqi.transform.FindChild ("SpriteShengjijindu3").gameObject;

		this.SpriteShengjijiantou1Handler = this.ContainerJiesuoBackground.transform.FindChild ("SpriteShengjijiantou1").gameObject;
		this.SpriteShengjijiantou2maxSpeed = this.ContainerJiesuoBackground.transform.FindChild ("SpriteShengjijiantou2").gameObject;
		this.SpriteShengjijiantou3jiasu = this.ContainerJiesuoBackground.transform.FindChild ("SpriteShengjijiantou3").gameObject;


		this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));						//返回
		this.ButtonJiahaobi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaobi));					//加金币
		this.ButtonJiahaoxin.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaoxin));				//加好心
		this.ButtonJiahaozuan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaozuan));				//加砖石
		
		this.ButtonJiesuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiesuo));						//解锁
		this.ButtonMax1.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonMax1));							//已经最大，应该为灰色不可以点击
		this.ButtonMax2.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonMax2));							//已经最大，应该为灰色不可以点击
		this.ButtonMax3.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonMax3));							//已经最大，应该为灰色不可以点击
		
		this.ButtonShengji1.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengji1));					//升级1--与max1互斥
		this.ButtonShengji2.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengji2));					//升级2--与max2互斥
		this.ButtonShengji3.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengji3));					//升级3--与max2互斥
		this.ButtonXuanzhong.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonXuanzhong));				//选中--与解锁是互斥，如果当前用户就是这个要灰色
		
		this.ButtonYou.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYou));							//右箭头
		this.ButtonZuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZuo));							//左箭头
				
		UIEventListener.Get (this.ContainerHuadong).onPress = this.OnPressButtonDrag;											//记录按下和抬起
		UIEventListener.Get (this.ContainerHuadong).onDrag = OnDragUI;															/// 初始化滚动事件，车辆，宠物，角色等信息进行旋转

		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));		//更换头像
	}

	void InitUIStartAnimation ()
	{

		this.ButtonZuo.transform.DOLocalMoveX (UIOriginalPositionButtonZuo.x - 15, 2.5f).SetLoops (-1).SetEase (Ease.OutBounce);
		this.ButtonYou.transform.DOLocalMoveX (UIOriginalPositionButtonYou.x + 15, 2.5f).SetLoops (-1).SetEase (Ease.OutBounce);

		//勾选状态的特效
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));

		//bottom to up action
		this.LabelNpcduihuakuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelNpcduihuakuang.x, UIOriginalPositionLabelNpcduihuakuang.y + 40, UIOriginalPositionLabelNpcduihuakuang.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelNpcduihuakuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelNpcduihuakuang.x, UIOriginalPositionLabelNpcduihuakuang.y, UIOriginalPositionLabelNpcduihuakuang.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});
		this.SpriteNpc1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteNpc1.x, UIOriginalPositionSpriteNpc1.y + 40, UIOriginalPositionSpriteNpc1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteNpc1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteNpc1.x, UIOriginalPositionSpriteNpc1.y, UIOriginalPositionSpriteNpc1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		//right to left action
		this.LabelCheliangmingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelCheliangmingcheng.x - 40, UIOriginalPositionLabelCheliangmingcheng.y, UIOriginalPositionLabelCheliangmingcheng.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelCheliangmingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelCheliangmingcheng.x, UIOriginalPositionLabelCheliangmingcheng.y, UIOriginalPositionLabelCheliangmingcheng.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelDangqianshu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu1.x - 40, UIOriginalPositionLabelDangqianshu1.y, UIOriginalPositionLabelDangqianshu1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelDangqianshu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu1.x, UIOriginalPositionLabelDangqianshu1.y, UIOriginalPositionLabelDangqianshu1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelDangqianshu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu2.x - 40, UIOriginalPositionLabelDangqianshu2.y, UIOriginalPositionLabelDangqianshu2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelDangqianshu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu2.x, UIOriginalPositionLabelDangqianshu2.y, UIOriginalPositionLabelDangqianshu2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelDangqianshu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu3.x - 40, UIOriginalPositionLabelDangqianshu3.y, UIOriginalPositionLabelDangqianshu3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelDangqianshu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDangqianshu3.x, UIOriginalPositionLabelDangqianshu3.y, UIOriginalPositionLabelDangqianshu3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelZhihoushu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu1.x - 40, UIOriginalPositionLabelZhihoushu1.y, UIOriginalPositionLabelZhihoushu1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelZhihoushu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu1.x, UIOriginalPositionLabelZhihoushu1.y, UIOriginalPositionLabelZhihoushu1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelZhihoushu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu2.x - 40, UIOriginalPositionLabelZhihoushu2.y, UIOriginalPositionLabelZhihoushu2.z), 0.25f, true)
				.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelZhihoushu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu2.x, UIOriginalPositionLabelZhihoushu2.y, UIOriginalPositionLabelZhihoushu2.z), 0.5f, true)
						.SetEase (Ease.Linear);
		});

		this.LabelZhihoushu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu3.x - 40, UIOriginalPositionLabelZhihoushu3.y, UIOriginalPositionLabelZhihoushu3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelZhihoushu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelZhihoushu3.x, UIOriginalPositionLabelZhihoushu3.y, UIOriginalPositionLabelZhihoushu3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});


		this.ContainerJiesuoBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerJiesuoBackground.x - 40, UIOriginalPositionContainerJiesuoBackground.y, UIOriginalPositionContainerJiesuoBackground.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ContainerJiesuoBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerJiesuoBackground.x, UIOriginalPositionContainerJiesuoBackground.y, UIOriginalPositionContainerJiesuoBackground.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ButtonShengji1.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji1.x - 40, UIOriginalPositionButtonShengji1.y, UIOriginalPositionButtonShengji1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonShengji1.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji1.x, UIOriginalPositionButtonShengji1.y, UIOriginalPositionButtonShengji1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ButtonShengji2.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji2.x - 40, UIOriginalPositionButtonShengji2.y, UIOriginalPositionButtonShengji2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonShengji2.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji2.x, UIOriginalPositionButtonShengji2.y, UIOriginalPositionButtonShengji2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ButtonShengji3.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji3.x - 40, UIOriginalPositionButtonShengji3.y, UIOriginalPositionButtonShengji3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonShengji3.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengji3.x, UIOriginalPositionButtonShengji3.y, UIOriginalPositionButtonShengji3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelShengji1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji1.x - 40, UIOriginalPositionLabelShengji1.y, UIOriginalPositionLabelShengji1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelShengji1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji1.x, UIOriginalPositionLabelShengji1.y, UIOriginalPositionLabelShengji1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelShengji2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji2.x - 40, UIOriginalPositionLabelShengji2.y, UIOriginalPositionLabelShengji2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelShengji2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji2.x, UIOriginalPositionLabelShengji2.y, UIOriginalPositionLabelShengji2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.LabelShengji3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji3.x - 40, UIOriginalPositionLabelShengji3.y, UIOriginalPositionLabelShengji3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.LabelShengji3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji3.x, UIOriginalPositionLabelShengji3.y, UIOriginalPositionLabelShengji3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteBeijingkuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteBeijingkuang.x - 40, UIOriginalPositionSpriteBeijingkuang.y, UIOriginalPositionSpriteBeijingkuang.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteBeijingkuang.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteBeijingkuang.x, UIOriginalPositionSpriteBeijingkuang.y, UIOriginalPositionSpriteBeijingkuang.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ButtonMax1.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax1.x - 40, UIOriginalPositionButtonMax1.y, UIOriginalPositionButtonMax1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonMax1.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax1.x, UIOriginalPositionButtonMax1.y, UIOriginalPositionButtonMax1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});
		this.ButtonMax2.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax2.x - 40, UIOriginalPositionButtonMax2.y, UIOriginalPositionButtonMax2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonMax2.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax2.x, UIOriginalPositionButtonMax2.y, UIOriginalPositionButtonMax2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});
		this.ButtonMax3.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax3.x - 40, UIOriginalPositionButtonMax3.y, UIOriginalPositionButtonMax3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonMax3.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMax3.x, UIOriginalPositionButtonMax3.y, UIOriginalPositionButtonMax3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.ButtonXuanzhong.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXuanzhong.x - 40, UIOriginalPositionButtonXuanzhong.y, UIOriginalPositionButtonXuanzhong.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.ButtonXuanzhong.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonXuanzhong.x, UIOriginalPositionButtonXuanzhong.y, UIOriginalPositionButtonXuanzhong.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteDangqianjindu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu1.x - 40, UIOriginalPositionSpriteDangqianjindu1.y, UIOriginalPositionSpriteDangqianjindu1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteDangqianjindu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu1.x, UIOriginalPositionSpriteDangqianjindu1.y, UIOriginalPositionSpriteDangqianjindu1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteDangqianjindu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu2.x - 40, UIOriginalPositionSpriteDangqianjindu2.y, UIOriginalPositionSpriteDangqianjindu2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteDangqianjindu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu2.x, UIOriginalPositionSpriteDangqianjindu2.y, UIOriginalPositionSpriteDangqianjindu2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteDangqianjindu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu3.x - 40, UIOriginalPositionSpriteDangqianjindu3.y, UIOriginalPositionSpriteDangqianjindu3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteDangqianjindu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteDangqianjindu3.x, UIOriginalPositionSpriteDangqianjindu3.y, UIOriginalPositionSpriteDangqianjindu3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteJindutiaodi1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi1.x - 40, UIOriginalPositionSpriteJindutiaodi1.y, UIOriginalPositionSpriteJindutiaodi1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteJindutiaodi1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi1.x, UIOriginalPositionSpriteJindutiaodi1.y, UIOriginalPositionSpriteJindutiaodi1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteJindutiaodi2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi2.x - 40, UIOriginalPositionSpriteJindutiaodi2.y, UIOriginalPositionSpriteJindutiaodi2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteJindutiaodi2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi2.x, UIOriginalPositionSpriteJindutiaodi2.y, UIOriginalPositionSpriteJindutiaodi2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteJindutiaodi3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi3.x - 40, UIOriginalPositionSpriteJindutiaodi3.y, UIOriginalPositionSpriteJindutiaodi3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteJindutiaodi3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteJindutiaodi3.x, UIOriginalPositionSpriteJindutiaodi3.y, UIOriginalPositionSpriteJindutiaodi3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteShengjijindu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu1.x - 40, UIOriginalPositionSpriteShengjijindu1.y, UIOriginalPositionSpriteShengjijindu1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteShengjijindu1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu1.x, UIOriginalPositionSpriteShengjijindu1.y, UIOriginalPositionSpriteShengjijindu1.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteShengjijindu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu2.x - 40, UIOriginalPositionSpriteShengjijindu2.y, UIOriginalPositionSpriteShengjijindu2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteShengjijindu2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu2.x, UIOriginalPositionSpriteShengjijindu2.y, UIOriginalPositionSpriteShengjijindu2.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});

		this.SpriteShengjijindu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu3.x - 40, UIOriginalPositionSpriteShengjijindu3.y, UIOriginalPositionSpriteShengjijindu3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
			this.SpriteShengjijindu3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijindu3.x, UIOriginalPositionSpriteShengjijindu3.y, UIOriginalPositionSpriteShengjijindu3.z), 0.5f, true)
					.SetEase (Ease.Linear);
		});
	}
		
	void InitConfigData ()
	{
		this.carConfigDataList = CarConfigData.GetConfigDatas<CarConfigData> ();
	}

	void InitCarPrefebName ()
	{
		//从数据层得到参数，暂时手动修改 carAvt----------暂时显示第1个
		currentCarPrefebName = this.carConfigDataList [this.currentSelectIndex].carAvt;
	}
		
	/// <summary>
	/// 添加3D车辆
	/// </summary>
	/// <param name="carPrefebName">Car prefeb name.</param>
	void InitCarAndStartAnimation (string carPrefebName)
	{
		this.uiShow2D = NGUITools.AddChild (null, GameResourcesManager.GetUIPrefab (UIControllerConst.UIPrefebUIShow2D));
		this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CreateCar (carPrefebName);
		this.uiShow2D.GetComponent<ui_show_2DUIController> ().StartCarAnim ();
	}

	/// <summary>
	/// 只保存锁定状态需要变更的UI对象
	/// </summary>
	void InitLockUIList ()
	{
		this.lockUIList.Clear ();
		this.lockUIList.Add (this.SpriteSuo);
		this.lockUIList.Add (this.ButtonJiesuo);
		this.lockUIList.Add (this.ContainerWeijiesuorongqi);		//但是内部的内容需要更新,如Label

		this.lockUIList.Add (this.SpriteXiaojinbi);
		this.lockUIList.Add (this.SpriteXiaozuan);
		this.lockUIList.Add (this.SpriteXiaojuan);

		this.lockUIList.Add (this.LabelJiesuoshuzi);
	}

	/// <summary>
	/// 只保存未锁定状态需要变更的UI对象  max和升级 按钮，后面在根据currentIndex等进行是否激活判断
	/// </summary>
	void InitUnlockUIList ()
	{
		this.unlockUIList.Clear ();
		this.unlockUIList.Add (ButtonMax1);
		this.unlockUIList.Add (ButtonMax2);
		this.unlockUIList.Add (ButtonMax3);

		this.unlockUIList.Add (ButtonShengji1);
		this.unlockUIList.Add (ButtonShengji2);
		this.unlockUIList.Add (ButtonShengji3);

		this.unlockUIList.Add (ButtonXuanzhong);
		this.unlockUIList.Add (LabelShengji1);
		this.unlockUIList.Add (LabelShengji2);
		this.unlockUIList.Add (LabelShengji3);

		this.unlockUIList.Add (SpriteGou);

		this.unlockUIList.Add (LabelDangqianshu1);			//当前数值
		this.unlockUIList.Add (LabelDangqianshu2);
		this.unlockUIList.Add (LabelDangqianshu3);
		this.unlockUIList.Add (LabelZhihoushu1);			//升级后可以得到的数值
		this.unlockUIList.Add (LabelZhihoushu2);
		this.unlockUIList.Add (LabelZhihoushu3);

		this.unlockUIList.Add (SpriteDangqianjindu1);		//黄色进度条
		this.unlockUIList.Add (SpriteDangqianjindu2);
		this.unlockUIList.Add (SpriteDangqianjindu3);

		this.unlockUIList.Add (SpriteJindutiaodi1);			//进度条底图
		this.unlockUIList.Add (SpriteJindutiaodi2);
		this.unlockUIList.Add (SpriteJindutiaodi3);

		this.unlockUIList.Add (SpriteShengjijindu1);		//红色进度
		this.unlockUIList.Add (SpriteShengjijindu2);
		this.unlockUIList.Add (SpriteShengjijindu3);

		//this.unlockUIList.Add (LabelCheliangmingcheng);

		this.unlockUIList.Add (ContainerJiesuoBackground);
		this.unlockUIList.Add (SpriteBeijingkuang);
	}

	/// <summary>
	/// 从数据层得到那些车辆是加锁的---这里暂时模拟
	/// </summary>
	void InitLockIndexList ()
	{
		if (MainState.Instance.playerInfo != null) {
			int indexUnlock;
				
			//1. 默认所有的车辆信息都未解锁，然后用排除法
			for (indexUnlock=0; indexUnlock<this.carConfigDataList.Count; indexUnlock++) {
				this.lockIndexList.Add (int.Parse (this.carConfigDataList [indexUnlock].id) - 1);				//由于Id从1开始,而UI从0开始
			}

			//2. 排除法
			foreach (MyGameProto.CarData data in MainState.Instance.playerInfo.carDatas) {
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
			this.currentSelectIndex = int.Parse (MainState.Instance.playerInfo.nowCarId) - 1;
		else
			this.currentSelectIndex = 0;

		currentIndex = this.currentSelectIndex;
		itemCount = this.carConfigDataList.Count;

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
		Debug.Log ("index is " + index);
		Debug.Log ("carDataList is " + (this.carConfigDataList == null));

		if (index < 0 || index >= this.carConfigDataList.Count) {
			Debug.LogWarning ("Please check this code,valide range is [0," + (this.carConfigDataList.Count - 1) + "]" + "But param index is " + index);
			return;
		}

		//1. 根据当前的 currentIndex ，从Data层取数据----更新名称，描述等信息
		this.LabelCheliangmingcheng.GetComponent<UILabel> ().text = this.carConfigDataList [index].carName;		//车辆名称
		this.LabelNpcduihuakuang.GetComponent<UILabel> ().text = this.carConfigDataList [index].description;		//车辆描述信息
				
		//2. 判断当前currentIndex是否已经加锁---刷新UI数据
		bool isLock = isIndexLock (index);
		this.setLockUIDisplay (isLock);

		//3. 如果是未锁定状态，则判断当前是否选中状态
		if (isLock == false) {
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
		if (enable == true) {
			//1. 开始显示加锁状态的界面  enable
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

			CarConfigData carConfigData = this.carConfigDataList [this.currentIndex];
			int costType = carConfigData.costTypeOfGain;
			if (costType == 1) {
				NGUITools.SetActive (this.SpriteXiaojinbi, true);
			} else if (costType == 2) {
				NGUITools.SetActive (this.SpriteXiaozuan, true);
			} else if (costType == 3) {
				NGUITools.SetActive (this.SpriteXiaojuan, true);
			}	

			//4.解锁需要的价格
			this.LabelJiesuoshuzi.GetComponent<UILabel> ().text = carConfigData.costValueOfGain.ToString ();

			//5.更新属性的最高值---或者直接用arConfigData.handlerLvupMaxValue 等属性
			this.lockLabelZhihoushu1Handler.GetComponent<UILabel> ().text = carConfigData.GetValueOnHandlerLv (carConfigData.handlerMaxLv - 1).ToString ();
			this.lockLabelZhihoushu2maxSpeed.GetComponent<UILabel> ().text = carConfigData.GetValueOnSpeedLv (carConfigData.speedMaxLv - 1).ToString ();
			this.lockLabelZhihoushu3jiasu.GetComponent<UILabel> ().text = carConfigData.GetValueOnAccLv (carConfigData.accMaxLv - 1).ToString ();

			//6.更新二级进度条图片
			float value1Handler = (float)carConfigData.GetValueOnHandlerLv (carConfigData.handlerMaxLv - 1) / carConfigData.handlerLvupMaxValue;
			float value2maxSpeed = (float)carConfigData.GetValueOnSpeedLv (carConfigData.speedMaxLv - 1) / carConfigData.speedLvupMaxValue;
			float value3jiasu = (float)carConfigData.GetValueOnAccLv (carConfigData.accMaxLv - 1) / carConfigData.accLvupMaxValue;

			this.lockSpriteShengjijindu1Handler.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value1Handler, 1.0f);
			this.lockSpriteShengjijindu2maxSpeed.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value2maxSpeed, 1.0f);
			this.lockSpriteShengjijindu3jiasu.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value3jiasu, 1.0f);

			//7. action
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

			CarConfigData carConfigData = this.carConfigDataList [this.currentIndex];
			MyGameProto.CarData carData = null;

			if (MainState.Instance.playerInfo == null || MainState.Instance.playerInfo.carDatas == null) {
				Debug.LogError ("playerInfo or carDatas is null error,debug mode,Please Fix it");		//这里可能报错
				Debug.LogError ((MainState.Instance.playerInfo == null).ToString () + " : " + 
					((MainState.Instance.playerInfo != null) ? (MainState.Instance.playerInfo.carDatas == null).ToString () : "playerInfo is null"));
				return;
			}


			foreach (MyGameProto.CarData myCarData in  MainState.Instance.playerInfo.carDatas) {
				if (carConfigData.id.Equals (myCarData.id) == true) {
					carData = myCarData;
				}
			}

			if (carData == null) {
				Debug.LogError ("carData is null error,debug mode,Please Fix it");
				return;
			}

			//3. 更新二级进度条图片---显示当前车辆的最大值
			float value1Handler = (float)carConfigData.GetValueOnHandlerLv (carConfigData.handlerMaxLv - 1) / carConfigData.handlerLvupMaxValue;
			float value2maxSpeed = (float)carConfigData.GetValueOnSpeedLv (carConfigData.speedMaxLv - 1) / carConfigData.speedLvupMaxValue;
			float value3jiasu = (float)carConfigData.GetValueOnAccLv (carConfigData.accMaxLv - 1) / carConfigData.accLvupMaxValue;
			
			this.SpriteShengjijindu1.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value1Handler, 1.0f);
			this.SpriteShengjijindu2.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value2maxSpeed, 1.0f);
			this.SpriteShengjijindu3.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value3jiasu, 1.0f);

			//4.当前值
			this.LabelDangqianshu1.GetComponent<UILabel> ().text = carConfigData.GetValueOnHandlerLv (carData.handlerLv).ToString ();
			this.LabelDangqianshu2.GetComponent<UILabel> ().text = carConfigData.GetValueOnSpeedLv (carData.speedLv).ToString ();
			this.LabelDangqianshu3.GetComponent<UILabel> ().text = carConfigData.GetValueOnAccLv (carData.accLv).ToString ();

			//5.升级后的值 carData.handlerLv索引从0开始，carConfigData.handlerMaxLv 索引值从1开始,所以要减去1
			if (carData.handlerLv < carConfigData.handlerMaxLv - 1) {
				this.LabelZhihoushu1.GetComponent<UILabel> ().text = carConfigData.GetValueOnHandlerLv (carData.handlerLv + 1).ToString ();
				NGUITools.SetActive (this.ButtonMax1, false);
			} else {
				//a. 已经最大，隐藏处理 隐藏升级后的值，箭头，隐藏金币，升级button，显示max并变为灰色
				NGUITools.SetActive (this.LabelZhihoushu1, false);
				NGUITools.SetActive (this.SpriteShengjijiantou1Handler, false);
				NGUITools.SetActive (this.LabelShengji1, false);
				NGUITools.SetActive (this.ButtonShengji1, false);
				NGUITools.SetActive (this.ButtonMax1, true);
				this.ButtonMax1.GetComponent<UIButton> ().isEnabled = false;
			}

			if (carData.speedLv < carConfigData.speedMaxLv - 1) {
				this.LabelZhihoushu2.GetComponent<UILabel> ().text = carConfigData.GetValueOnSpeedLv (carData.speedLv + 1).ToString ();
				NGUITools.SetActive (this.ButtonMax2, false);
			} else {
				//a. 已经最大，隐藏处理 隐藏升级后的值，箭头，隐藏金币，升级button，显示max并变为灰色
				NGUITools.SetActive (this.LabelZhihoushu2, false);
				NGUITools.SetActive (this.SpriteShengjijiantou2maxSpeed, false);
				NGUITools.SetActive (this.LabelShengji2, false);
				NGUITools.SetActive (this.ButtonShengji2, false);
				NGUITools.SetActive (this.ButtonMax2, true);
				this.ButtonMax2.GetComponent<UIButton> ().isEnabled = false;
			}

			if (carData.accLv < carConfigData.accMaxLv - 1) {
				this.LabelZhihoushu3.GetComponent<UILabel> ().text = carConfigData.GetValueOnAccLv (carData.accLv + 1).ToString ();
				NGUITools.SetActive (this.ButtonMax3, false);
			} else {
				//a. 已经最大，隐藏处理 隐藏升级后的值，箭头，隐藏金币，升级button，显示max并变为灰色
				NGUITools.SetActive (this.LabelZhihoushu3, false);
				NGUITools.SetActive (this.SpriteShengjijiantou3jiasu, false);
				NGUITools.SetActive (this.LabelShengji3, false);
				NGUITools.SetActive (this.ButtonShengji3, false);
				NGUITools.SetActive (this.ButtonMax3, true);
				this.ButtonMax3.GetComponent<UIButton> ().isEnabled = false;
			}

			//6. 一级进度条
			float value1 = (float)carConfigData.GetValueOnHandlerLv (carData.handlerLv) / carConfigData.handlerLvupMaxValue;
			float value2 = (float)carConfigData.GetValueOnSpeedLv (carData.speedLv) / carConfigData.speedLvupMaxValue;
			float value3 = (float)carConfigData.GetValueOnAccLv (carData.accLv) / carConfigData.accLvupMaxValue;

//			this.SpriteDangqianjindu1.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value1,1.0f);
//			this.SpriteDangqianjindu2.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value2,1.0f);
//			this.SpriteDangqianjindu3.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value3,1.0f);
//
//			this.SpriteDangqianjindu1.GetComponent<UISprite> ().enabled = value1 > 0.001f;
//			this.SpriteDangqianjindu2.GetComponent<UISprite> ().enabled = value2 > 0.001f;			
//			this.SpriteDangqianjindu3.GetComponent<UISprite> ().enabled = value3 > 0.001f;

			if (this.levelupValue1 != value1) {
				if (this.levelupValue1ChangeTween != null && true == this.levelupValue1ChangeTween.IsPlaying ()) {
					this.levelupValue1ChangeTween.Kill ();
				}
				
				this.levelupValue1ChangeTween = DOVirtual.Float (levelupValue1, value1, 0.75f, delegate(float value) {
					this.SpriteDangqianjindu1.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value, 1.0f);
					this.SpriteDangqianjindu1.GetComponent<UISprite> ().enabled = value1 > 0.001f;
				});
				
				this.levelupValue1ChangeTween.SetEase (Ease.InQuad);
				this.levelupValue1 = value1;

				//
				if (this.value1MaxBGTween != null && this.value1MaxBGTween.IsPlaying () == true) {
					this.value1MaxBGTween.Kill ();
				}
				
				this.value1MaxBGTween = DOVirtual.Float (0.5f, 1, 0.8f, delegate(float value) {
					this.SpriteShengjijindu1.GetComponent<UIWidget> ().alpha = value;
				}).SetLoops (-1, LoopType.Yoyo).SetUpdate (true).SetEase (Ease.InOutQuad);
			}

			if (this.levelupValue2 != value2) {
				if (this.levelupValue2ChangeTween != null && true == this.levelupValue2ChangeTween.IsPlaying ()) {
					this.levelupValue2ChangeTween.Kill ();
				}
				
				this.levelupValue2ChangeTween = DOVirtual.Float (levelupValue2, value2, 0.75f, delegate(float value) {
					this.SpriteDangqianjindu2.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value, 1.0f);
					this.SpriteDangqianjindu2.GetComponent<UISprite> ().enabled = value2 > 0.001f;
				});
				
				this.levelupValue2ChangeTween.SetEase (Ease.InQuad);
				this.levelupValue2 = value2;

				//
				if (this.value2MaxBGTween != null && this.value2MaxBGTween.IsPlaying () == true) {
					this.value2MaxBGTween.Kill ();
				}
				
				this.value2MaxBGTween = DOVirtual.Float (0.5f, 1, 0.8f, delegate(float value) {
					this.SpriteShengjijindu2.GetComponent<UIWidget> ().alpha = value;
				}).SetLoops (-1, LoopType.Yoyo).SetUpdate (true).SetEase (Ease.InOutQuad);
			}

			if (this.levelupValue3 != value3) {
				if (this.levelupValue3ChangeTween != null && true == this.levelupValue3ChangeTween.IsPlaying ()) {
					this.levelupValue3ChangeTween.Kill ();
				}
				
				this.levelupValue3ChangeTween = DOVirtual.Float (levelupValue3, value3, 0.75f, delegate(float value) {
					this.SpriteDangqianjindu3.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value, 1.0f);
					this.SpriteDangqianjindu3.GetComponent<UISprite> ().enabled = value3 > 0.001f;
				});
				
				this.levelupValue3ChangeTween.SetEase (Ease.InQuad);
				this.levelupValue3 = value3;

				//
				if (this.value3MaxBGTween != null && this.value3MaxBGTween.IsPlaying () == true) {
					this.value3MaxBGTween.Kill ();
				}
				
				this.value3MaxBGTween = DOVirtual.Float (0.5f, 1, 0.8f, delegate(float value) {
					this.SpriteShengjijindu3.GetComponent<UIWidget> ().alpha = value;
				}).SetLoops (-1, LoopType.Yoyo).SetUpdate (true).SetEase (Ease.InOutQuad);
			}

			//7. 当前升级需要的金币
			this.LabelShengji1.GetComponent<UILabel> ().text = carConfigData.GetCostGoldOnHandlerLv (carData.handlerLv).ToString ();
			this.LabelShengji2.GetComponent<UILabel> ().text = carConfigData.GetCostGoldOnSpeedLv (carData.speedLv).ToString ();
			this.LabelShengji3.GetComponent<UILabel> ().text = carConfigData.GetCostGoldOnAccLv (carData.accLv).ToString ();
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

	void Clean3DCarRole ()
	{
		if (uiShow2D != null) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CleanAllAvt ();
			this.uiShow2D = null;
		}
	}

	/// <summary>
	/// 根据
	/// </summary>
	/// <returns>The car name.</returns>
	/// <param name="index">Index.</param>
	string GetCarName (int index)
	{	
		if (index < 0 || index >= this.carConfigDataList.Count) {
			Debug.LogError ("Please check this code,valide range is [0," + (this.carConfigDataList.Count - 1) + "]" + "But param index is " + index);
			//默认返回第1个车辆
			if (this.carConfigDataList.Count > 0)
				return this.carConfigDataList [0].carAvt;

			return "CarAvt9";
		}
				
		return  this.carConfigDataList [index].carAvt;
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

			//this.SpriteNpc1.GetComponent<UISprite> ().spriteName = "ui_rolebanshen_" + xx.ToString ();
		}
		//2.昵称
		if (MainState.Instance.playerInfo != null && MainState.Instance.playerInfo.nickname.Length > 0) {
			this.LabelMingzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.nickname;
		}
			
//		//3.好心
//		if (MainState.Instance.playerInfo != null) {
//			this.LabelXinshuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.power.ToString ();
//		}
//		//4. 金币
//		if (MainState.Instance.playerInfo != null) {
//			this.LabelBishuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.gold.ToString ();
//		}
//		//5. 钻石
//		if (MainState.Instance.playerInfo != null) {
//			this.LabelZuanshuzi.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.diamond.ToString ();
//		}

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
		
	/// <summary>
	/// 返回
	/// </summary>
	void OnClickButtonFanhui ()
	{
		if (this.value1MaxBGTween != null) {
			this.value1MaxBGTween.Kill (true);
		}

		if (this.value2MaxBGTween != null) {
			this.value2MaxBGTween.Kill (true);
		}

		if (this.value3MaxBGTween != null) {
			this.value3MaxBGTween.Kill (true);
		}

		//
		if (this.levelupValue3ChangeTween != null) {
			this.levelupValue3ChangeTween.Kill (true);
		}
		
		if (this.levelupValue3ChangeTween != null) {
			this.levelupValue3ChangeTween.Kill (true);
		}
		
		if (this.levelupValue3ChangeTween != null) {
			this.levelupValue3ChangeTween.Kill (true);
		}

		//
		if (this.coinChangeTween != null) {
			this.coinChangeTween.Kill (true);
		}
		
		if (this.diamondChangeTween != null) {
			this.diamondChangeTween.Kill (true);
		}
		
		if (this.powerChangeTween != null) {
			this.powerChangeTween.Kill (true);
		}

		this.Clean3DCarRole ();
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
	/// 解锁，更新lockIndexList ，同时通过Logic更新Logic层
	/// </summary>
	void OnClickButtonJiesuo ()
	{
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("carId", this.carConfigDataList [this.currentIndex].id);
		LogicManager.Instance.ActNewLogic<LogicGainCar> (logicPar, OnUnLockFinish);
	}

	void OnUnLockFinish (Hashtable logicPar)
	{
		LogicReturn result = (LogicReturn)logicPar ["logicReturn"];

		switch (result) {
		case LogicReturn.LR_NOTENOUGHDIAMOND:
			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_NOTENOUGHGOLD:
			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;
			}, true);

			break;
		case LogicReturn.LR_NOTENOUGHSCORE:
			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].unlockFailTip, 1.5f);
				
			this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = false;
			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_REACHEDMAXLV:
			break;
		case LogicReturn.LR_SUCCESS:
			this.lockIndexList.Remove (currentIndex);
			this.SetupRightUI (currentIndex);
			break;
		}
	}
	
	/// <summary>
	/// 已经最大 no use
	/// </summary>
	void OnClickButtonMax1 ()
	{
		
	}

	/// <summary>
	/// 已经最大 no use
	/// </summary>
	void OnClickButtonMax2 ()
	{
		
	}

	/// <summary>
	/// 已经最大 no use
	/// </summary>
	void OnClickButtonMax3 ()
	{
		
	}

	/// <summary>
	/// 升级1--与max1互斥
	/// </summary>
	void OnClickButtonShengji1 ()
	{
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("carId", this.carConfigDataList [this.currentIndex].id);
		logicPar.Add ("atrType", LogicLvupCar.AtrType.Handler);
		LogicManager.Instance.ActNewLogic<LogicLvupCar> (logicPar, OnShengji1Finish);
	}

	void OnShengji1Finish (Hashtable logicPar)
	{
		LogicReturn result = (LogicReturn)logicPar ["logicReturn"];
		
		switch (result) {
		case LogicReturn.LR_NOTENOUGHDIAMOND:

			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].levelupFailTip, 1.5f);
			this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_NOTENOUGHGOLD:
			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].levelupFailTip, 1.5f);
			this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = true;
			}, true);
			break;
		case LogicReturn.LR_NOTENOUGHSCORE:
			PanelMainUIController.Instance.ShowUIMsgBox (this.carConfigDataList [this.currentIndex].levelupFailTip, 1.5f);
			this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = false;
			this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = false;

			DOVirtual.DelayedCall (1.0f, delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);				
				this.ButtonShengji1.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji2.GetComponent<UIButton> ().isEnabled = true;
				this.ButtonShengji3.GetComponent<UIButton> ().isEnabled = true;
			}, true);

			break;
		case LogicReturn.LR_REACHEDMAXLV:
			break;
		case LogicReturn.LR_SUCCESS:
			this.SetupRightUI (currentIndex);
			break;
		}
	}

	/// <summary>
	/// 升级2--与max2互斥
	/// </summary>
	void OnClickButtonShengji2 ()
	{
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("carId", this.carConfigDataList [this.currentIndex].id);
		logicPar.Add ("atrType", LogicLvupCar.AtrType.Speed);
		LogicManager.Instance.ActNewLogic<LogicLvupCar> (logicPar, OnShengji1Finish);
	}

//	void OnShengji2Finish(Hashtable logicPar)
//	{
//		LogicReturn result=(LogicReturn)logicPar["logicReturn"];
//		
//		switch(result)
//		{
//		case LogicReturn.LR_NOTENOUGHDIAMOND:
//			//弹出商店或弹框
//			ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
//			PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
//			break;
//		case LogicReturn.LR_NOTENOUGHGOLD:
//			//弹出商店或弹框
//			ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
//			PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
//			break;
//		case LogicReturn.LR_NOTENOUGHSCORE:
//			break;
//		case LogicReturn.LR_REACHEDMAXLV:
//			break;
//		case LogicReturn.LR_SUCCESS:
//			//this.lockIndexList.Remove (currentIndex);
//			//开始刷新界面
//			this.SetupRightUI (currentIndex);
//			break;
//		}
//	}

	/// <summary>
	/// 升级3--与max3互斥   no use
	/// </summary>
	void OnClickButtonShengji3 ()
	{
		Hashtable logicPar = new Hashtable ();
		logicPar.Add ("carId", this.carConfigDataList [this.currentIndex].id);
		logicPar.Add ("atrType", LogicLvupCar.AtrType.Acc);
		LogicManager.Instance.ActNewLogic<LogicLvupCar> (logicPar, OnShengji1Finish);
	}

//	void OnShengji3Finish(Hashtable logicPar)
//	{
//		LogicReturn result=(LogicReturn)logicPar["logicReturn"];
//		
//		switch(result)
//		{
//		case LogicReturn.LR_NOTENOUGHDIAMOND:
//			//弹出商店或弹框
//			ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
//			PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
//			break;
//		case LogicReturn.LR_NOTENOUGHGOLD:
//			//弹出商店或弹框
//			ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
//			PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
//			break;
//		case LogicReturn.LR_NOTENOUGHSCORE:
//			break;
//		case LogicReturn.LR_REACHEDMAXLV:
//			break;
//		case LogicReturn.LR_SUCCESS:
//			//this.lockIndexList.Remove (currentIndex);
//			//开始刷新界面
//			this.SetupRightUI (currentIndex);
//			break;
//		}
//	}

	/// <summary>
	/// 选中--与解锁是互斥，如果当前用户就是选择的这个要灰色
	/// </summary>
	void OnClickButtonXuanzhong ()
	{
		this.currentSelectIndex = this.currentIndex;
		this.SetSelectButtonEnable (false);

		if (MainState.Instance.playerInfo != null)
			MainState.Instance.playerInfo.nowCarId = (this.currentSelectIndex + 1).ToString ();

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
			
		this.levelupValue1 = 0;
		this.levelupValue2 = 0;
		this.levelupValue3 = 0;

		this.currentIndex++;
		this.SetupRightUI (this.currentIndex);

		//勾选状态的特效
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));

		this.Clean3DCarRole ();
		this.currentCarPrefebName = this.GetCarName (this.currentIndex);
		InitCarAndStartAnimation (currentCarPrefebName);

		CheckButtonArrowEnable (currentIndex);
	}
	
	/// <summary>
	/// 左箭头
	/// </summary>
	void OnClickButtonZuo ()
	{
		if (currentIndex <= 0)
			return;

		this.levelupValue1 = 0;
		this.levelupValue2 = 0;
		this.levelupValue3 = 0;

		this.currentIndex--;
		this.SetupRightUI (currentIndex);

		//勾选状态的特效
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));

		this.Clean3DCarRole ();
		this.currentCarPrefebName = this.GetCarName (this.currentIndex);
		InitCarAndStartAnimation (currentCarPrefebName);

		CheckButtonArrowEnable (currentIndex);
	}

	void OnDragUI (GameObject obj, Vector2 delta)
	{
		if (isDraging == true) {
						
			dragSum += delta.x;
			float afterW = Screen.width * this.ContainerHuadong.GetComponent<UIWidget> ().width / 1280.0f;
			if (Mathf.Abs (dragSum) <= afterW)
				this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCar (delta.x, afterW);
		}
	}

	void OnPressButtonDrag (GameObject go, bool state)
	{
		isDraging = state;
		dragSum = 0.0f;

		if (state == false) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateCar (0, 0, true);
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


