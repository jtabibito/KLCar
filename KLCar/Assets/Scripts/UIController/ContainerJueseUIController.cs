using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 角色选择界面.
/// 2015年4月29日19:45:19
/// </summary>
public partial class ContainerJueseUIController : UIControllerBase
{
	/// <summary>
	/// 状态：锁定 和 未锁定---{细分为选中状态，未选中状态}{细分为已经升级到MAX，未升级到MAX}
	///  LabelCheliangmingcheng 角色名称
	/// LabelDengji  等级数据
	/// LabelNpcduihuakuang  人物描述信息
	/// LabelRenwushuxing1 LabelRenwushuxing2 LabelRenwushuxing3    属性名称，每个角色的属性都是不同的
	/// LabelZuigaoshu1  LabelZuigaoshu2  LabelZuigaoshu3 			最高数字
	/// LabelDangqianshu1  LabelDangqianshu2  LabelDangqianshu3     当前数字
	/// LabelZhihoushu1 LabelZhihoushu2 LabelZhihoushu3             升级后的数字
	/// SpriteShengjijiantou1 SpriteShengjijiantou2  SpriteShengjijiantou3  升级的箭头
	/// SpriteDangqianjindu1 SpriteDangqianjindu2 SpriteDangqianjindu3       当前进度，但是要小于二级进度条
	/// SpriteShengjijindu1 SpriteShengjijindu2 SpriteShengjijindu3          二级进度条,当前角色可以升级的最大值，配置表中配置，是个固定的值
	/// 满格表示当前车辆中的最大车辆的进度值，所以需要获的所有角色中车辆中最大的值作为100%
	/// 解锁状态显示最高等级状态---所以MAX状态可以显示为锁定状态的数值+解除一些特殊状态的图片如锁头
	/// </summary>

	private	int itemCount = 0;
	private	int currentIndex = 0;										//左右箭头 用到的Index
	private int currentSelectIndex = 0;									//当前用户正在选中的车index
	private List<int> lockIndexList = new List<int> ();					//当前锁定对象的List列表，个数必须要<=itemCount;内部管理
	private List<GameObject> lockUIList = new List<GameObject> ();		//只保存锁定状态需要变更的UI对象，通过active激活
	private List<GameObject> unlockUIList = new List<GameObject> (); 	//只保存未锁定状态需要变更的UI对象，通过active激活
	
	private GameObject uiShow2D = null;
	private string 	   currentRolePrefebName;							//应该从配置表数据库中得到这种值
	
	private bool  isDraging = false;
	private float dragSum = 0.0f;

	//1. 拿到角色信息
	private List<RoleConfigData>  roleConfigDataList = null;// = new List<CarConfigData> ();\

	private long  countCoin = 0;
	private Tweener coinChangeTween = null;
	
	private long  countPower = 0;
	private Tweener powerChangeTween = null;
	
	private long  countDiamond = 0;
	private Tweener diamondChangeTween = null;

	//车辆信息
	private float  levelupValue1 = 0;
	private Tweener levelupValue1ChangeTween = null;
	
	private float  levelupValue2 = 0;
	private Tweener levelupValue2ChangeTween = null;
	
	private float  levelupValue3 = 0;
	private Tweener levelupValue3ChangeTween = null;

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

		InitRolePrefebName ();
		InitRoleAndStartAnimation (currentRolePrefebName);
				
		UpdateTopStatusBar ();
				
		HideNoUseGameObject ();
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
		this.ButtonShengjizuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengjizuo));				//升级
		this.ButtonMaxzuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonMaxzuo));						//最大

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

		//bottom to up
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
		
		//right to left
		this.LabelJuesemingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJuesemingcheng.x - 40, UIOriginalPositionLabelJuesemingcheng.y, UIOriginalPositionLabelJuesemingcheng.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelJuesemingcheng.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJuesemingcheng.x, UIOriginalPositionLabelJuesemingcheng.y, UIOriginalPositionLabelJuesemingcheng.z), 0.5f, true)
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
		
		
		this.LabelJiesuoshuzi.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJiesuoshuzi.x - 40, UIOriginalPositionLabelJiesuoshuzi.y, UIOriginalPositionLabelJiesuoshuzi.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelJiesuoshuzi.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelJiesuoshuzi.x, UIOriginalPositionLabelJiesuoshuzi.y, UIOriginalPositionLabelJiesuoshuzi.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
		
		this.ButtonJiesuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJiesuo.x - 40, UIOriginalPositionButtonJiesuo.y, UIOriginalPositionButtonJiesuo.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonJiesuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonJiesuo.x, UIOriginalPositionButtonJiesuo.y, UIOriginalPositionButtonJiesuo.z), 0.5f, true)
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
		
		this.LabelRenwushuxing1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing1.x - 40, UIOriginalPositionLabelRenwushuxing1.y, UIOriginalPositionLabelRenwushuxing1.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelRenwushuxing1.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing1.x, UIOriginalPositionLabelRenwushuxing1.y, UIOriginalPositionLabelRenwushuxing1.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
		
		this.LabelRenwushuxing2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing2.x - 40, UIOriginalPositionLabelRenwushuxing2.y, UIOriginalPositionLabelRenwushuxing2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelRenwushuxing2.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing2.x, UIOriginalPositionLabelRenwushuxing2.y, UIOriginalPositionLabelRenwushuxing2.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
		
		this.LabelRenwushuxing3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing3.x - 40, UIOriginalPositionLabelRenwushuxing3.y, UIOriginalPositionLabelRenwushuxing3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelRenwushuxing3.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelRenwushuxing3.x, UIOriginalPositionLabelRenwushuxing3.y, UIOriginalPositionLabelRenwushuxing3.z), 0.5f, true)
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

		this.SpriteShengjijiantou1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou1.x - 40, UIOriginalPositionSpriteShengjijiantou1.y, UIOriginalPositionSpriteShengjijiantou1.z), 0.25f, true)
				.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteShengjijiantou1.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou1.x, UIOriginalPositionSpriteShengjijiantou1.y, UIOriginalPositionSpriteShengjijiantou1.z), 0.5f, true)
						.SetEase (Ease.Linear);
				});
		
		this.SpriteShengjijiantou2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou2.x - 40, UIOriginalPositionSpriteShengjijiantou2.y, UIOriginalPositionSpriteShengjijiantou2.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteShengjijiantou2.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou2.x, UIOriginalPositionSpriteShengjijiantou2.y, UIOriginalPositionSpriteShengjijiantou2.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
		
		this.SpriteShengjijiantou3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou3.x - 40, UIOriginalPositionSpriteShengjijiantou3.y, UIOriginalPositionSpriteShengjijiantou3.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteShengjijiantou3.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteShengjijiantou3.x, UIOriginalPositionSpriteShengjijiantou3.y, UIOriginalPositionSpriteShengjijiantou3.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});

		this.LabelDengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDengji.x - 40, UIOriginalPositionLabelDengji.y, UIOriginalPositionLabelDengji.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelDengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelDengji.x, UIOriginalPositionLabelDengji.y, UIOriginalPositionLabelDengji.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});

		this.ContainerBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerBackground.x - 40, UIOriginalPositionContainerBackground.y, UIOriginalPositionContainerBackground.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ContainerBackground.transform.DOLocalMove (new Vector3 (UIOriginalPositionContainerBackground.x, UIOriginalPositionContainerBackground.y, UIOriginalPositionContainerBackground.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});

		this.ButtonShengjizuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengjizuo.x - 40, UIOriginalPositionButtonShengjizuo.y, UIOriginalPositionButtonShengjizuo.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonShengjizuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonShengjizuo.x, UIOriginalPositionButtonShengjizuo.y, UIOriginalPositionButtonShengjizuo.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});

		this.LabelShengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji.x - 40, UIOriginalPositionLabelShengji.y, UIOriginalPositionLabelShengji.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.LabelShengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionLabelShengji.x, UIOriginalPositionLabelShengji.y, UIOriginalPositionLabelShengji.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
		this.SpriteXiaojinbiShengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteXiaojinbiShengji.x - 40, UIOriginalPositionSpriteXiaojinbiShengji.y, UIOriginalPositionSpriteXiaojinbiShengji.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.SpriteXiaojinbiShengji.transform.DOLocalMove (new Vector3 (UIOriginalPositionSpriteXiaojinbiShengji.x, UIOriginalPositionSpriteXiaojinbiShengji.y, UIOriginalPositionSpriteXiaojinbiShengji.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});

		this.ButtonMaxzuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMaxzuo.x - 40, UIOriginalPositionButtonMaxzuo.y, UIOriginalPositionButtonMaxzuo.z), 0.25f, true)
			.SetEase (Ease.OutCubic).OnComplete (delegate() {
				this.ButtonMaxzuo.transform.DOLocalMove (new Vector3 (UIOriginalPositionButtonMaxzuo.x, UIOriginalPositionButtonMaxzuo.y, UIOriginalPositionButtonMaxzuo.z), 0.5f, true)
					.SetEase (Ease.Linear);
			});
	}

	void InitConfigData ()
	{
		this.roleConfigDataList = RoleConfigData.GetConfigDatas<RoleConfigData> ();
	}

	void InitRolePrefebName ()
	{
		//从数据层得到参数，暂时手动修改
		//currentRolePrefebName = "RoleAvt7";
		currentRolePrefebName = this.roleConfigDataList [this.currentSelectIndex].roleAvt;
	}
	
	/// <summary>
	/// 添加3D角色
	/// </summary>
	/// <param name="rolePrefebName">role prefeb name.</param>
	void InitRoleAndStartAnimation (string rolePrefebName)
	{
		this.uiShow2D = NGUITools.AddChild (null, GameResourcesManager.GetUIPrefab (UIControllerConst.UIPrefebUIShow2D));
		this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().CreateRole (rolePrefebName);
		this.uiShow2D.GetComponent<ui_show_2DUIController> ().StartRoleAnim ();
	}

	/// <summary>
	/// 只保存锁定状态需要变更的UI对象
	/// </summary>
	void InitLockUIList ()
	{
		this.lockUIList.Clear ();
		this.lockUIList.Add (this.SpriteSuo);
		this.lockUIList.Add (this.ButtonJiesuo);
		this.lockUIList.Add (this.SpriteXiaojinbi);			//解锁的金币图标
		this.lockUIList.Add (this.SpriteXiaozuan);			//钻石
		this.lockUIList.Add (this.SpriteXiaojuan);			//积分

		this.lockUIList.Add (this.LabelJiesuoshuzi);		//解锁的数字Label

		this.lockUIList.Add (LabelZuigaoshu1);				//最高数值
		this.lockUIList.Add (LabelZuigaoshu2);
		this.lockUIList.Add (LabelZuigaoshu3);
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
		this.unlockUIList.Add (ButtonShengjizuo);			//升级Button
		this.unlockUIList.Add (LabelShengji);				//升级的金币数字
		this.unlockUIList.Add (SpriteXiaojinbiShengji);		//升级的金币图片
		this.unlockUIList.Add (ButtonMaxzuo);				//升级

		this.unlockUIList.Add (LabelDangqianshu1);			//当前数值 
		this.unlockUIList.Add (LabelDangqianshu2);
		this.unlockUIList.Add (LabelDangqianshu3);
		this.unlockUIList.Add (LabelZhihoushu1);			//升级后可以得到的数值
		this.unlockUIList.Add (LabelZhihoushu2);
		this.unlockUIList.Add (LabelZhihoushu3);
		this.unlockUIList.Add (SpriteShengjijiantou1);		//升级的箭头
		this.unlockUIList.Add (SpriteShengjijiantou2);
		this.unlockUIList.Add (SpriteShengjijiantou3);
		
//				this.unlockUIList.Add (SpriteDangqianjindu1);		//蓝色进度条
//				this.unlockUIList.Add (SpriteDangqianjindu2);
//				this.unlockUIList.Add (SpriteDangqianjindu3);
//
//				this.unlockUIList.Add (SpriteShengjijindu1);		//黄色进度
//				this.unlockUIList.Add (SpriteShengjijindu2);
//				this.unlockUIList.Add (SpriteShengjijindu3);
	}
	
	/// <summary>
	/// 从数据层得到那些车辆是加锁的---这里暂时模拟
	/// </summary>
	void InitLockIndexList ()
	{
		if (MainState.Instance.playerInfo != null) {
			int indexUnlock;
				
			//1. 默认所有的车辆信息都未解锁，然后用排除法
			for (indexUnlock=0; indexUnlock<this.roleConfigDataList.Count; indexUnlock++) {
				this.lockIndexList.Add (int.Parse (this.roleConfigDataList [indexUnlock].id) - 1);				//由于Id从1开始,而UI从0开始
			}
				
			//2. 排除法
			foreach (MyGameProto.RoleData data in MainState.Instance.playerInfo.roleDatas) {
				this.lockIndexList.Remove (int.Parse (data.id) - 1);
			}
		}
	}
	
	/// <summary>
	/// 当前index赋值
	/// </summary>
	void InitCurrentIndex ()
	{
		if (MainState.Instance.playerInfo != null)
			this.currentSelectIndex = int.Parse (MainState.Instance.playerInfo.nowRoleId) - 1;
		else
			this.currentSelectIndex = 0;
				
		currentIndex = this.currentSelectIndex;
		itemCount = this.roleConfigDataList.Count;
				
		CheckButtonArrowEnable (currentIndex);//可能有只有1个子节点，所以左右两边button都隐藏
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
	}


	/// <summary>
	/// 根据当前index是否已经解锁，显示相应的UI
	/// </summary>
	void SetupRightUI (int index)
	{
		Debug.Log ("Config is NULL? " + (this.roleConfigDataList == null));		//这里测试有时候为空，不是100%复现
		//1. 根据当前的 currentIndex ，从Data层取数据更新UI界面
		this.LabelJuesemingcheng.GetComponent<UILabel> ().text = this.roleConfigDataList [this.currentIndex].roleName;
		this.LabelNpcduihuakuang.GetComponent<UILabel> ().text = this.roleConfigDataList [this.currentIndex].description;

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

		//4. 
				
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
						
			RoleConfigData roleConfigData = this.roleConfigDataList [this.currentIndex];

			int costType = roleConfigData.costTypeOfGain;
			if (costType == 1) {
				NGUITools.SetActive (this.SpriteXiaojinbi, true);
			} else if (costType == 2) {
				NGUITools.SetActive (this.SpriteXiaozuan, true);
			} else if (costType == 3) {
				NGUITools.SetActive (this.SpriteXiaojuan, true);
			}	

			//4.解锁需要的价格
			this.LabelJiesuoshuzi.GetComponent<UILabel> ().text = roleConfigData.costValueOfGain.ToString ();
			
			//5.属性的最高值
			this.LabelZuigaoshu1.GetComponent<UILabel> ().text = roleConfigData.GetAtr1AddValueOnLv (roleConfigData.maxLv - 1).ToString ();
			this.LabelZuigaoshu2.GetComponent<UILabel> ().text = roleConfigData.GetAtr2AddValueOnLv (roleConfigData.maxLv - 1).ToString ();
			this.LabelZuigaoshu3.GetComponent<UILabel> ().text = roleConfigData.GetAtr3AddValueOnLv (roleConfigData.maxLv - 1).ToString ();

			//6.锁定状态只显示最高等级---考虑是否减去1？？
			this.LabelDengji.GetComponent<UILabel> ().text = "等级" + this.roleConfigDataList [this.currentIndex].maxLv;

			//7.进度条满格显示---其实这一步骤可以不做的
			this.SpriteShengjijindu1.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, 1.0f, 1.0f);
			this.SpriteShengjijindu2.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, 1.0f, 1.0f);
			this.SpriteShengjijindu3.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, 1.0f, 1.0f);

			//8. 属性名称
			this.LabelRenwushuxing1.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr1);
			this.LabelRenwushuxing2.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr2);
			this.LabelRenwushuxing3.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr3);

			//9. action
			this.SpriteSuo.transform.DOKill ();
			Sequence mySeq = DOTween.Sequence ();
			mySeq.Append (this.SpriteSuo.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
			mySeq.Append (this.SpriteSuo.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));

		} else {
			//1. 开始显示解锁状态的界面
			//UpdateUnlockUIData(index)
			foreach (GameObject obj in lockUIList) {
				obj.SetActive (false);		//但是数值必须要先初始化好，更新好
			}
			//2. enable
			foreach (GameObject obj in unlockUIList) {
				obj.SetActive (true);		//但是数值必须要先初始化好，更新好
			}

			//3. 角色等级
			RoleConfigData roleConfigData = this.roleConfigDataList [this.currentIndex];
			MyGameProto.RoleData roleData = null;
						
			if (MainState.Instance.playerInfo == null || MainState.Instance.playerInfo.roleDatas == null) {
				Debug.LogError ("playerInfo or roleDatas is null error,debug mode,Please Fix it");
				Debug.LogError ((MainState.Instance.playerInfo == null).ToString () + "  " + 
					((MainState.Instance.playerInfo != null) ? (MainState.Instance.playerInfo.carDatas == null).ToString () : "playerInfo is null"));
				return;
			}
						
			foreach (MyGameProto.RoleData myRoleData in  MainState.Instance.playerInfo.roleDatas) {		//这里可能报错
				if (roleConfigData.id.Equals (myRoleData.id) == true) {
					roleData = myRoleData;
				}
			}
						
			if (roleData == null) {
				Debug.LogError ("roleData is null error,debug mode,Please Fix it");
				return;
			}
						
			//4.当前等级---等级的下标是从1开始的
			this.LabelDengji.GetComponent<UILabel> ().text = "等级" + (roleData.lv + 1);

			//5. 当前升级需要的金币
			this.LabelShengji.GetComponent<UILabel> ().text = roleConfigData.GetCostGoldOnLv (roleData.lv).ToString ();
						
			//6. 当前数字Label
			this.LabelDangqianshu1.GetComponent<UILabel> ().text = roleConfigData.GetAtr1AddValueOnLv (roleData.lv).ToString ();
			this.LabelDangqianshu2.GetComponent<UILabel> ().text = roleConfigData.GetAtr2AddValueOnLv (roleData.lv).ToString ();
			this.LabelDangqianshu3.GetComponent<UILabel> ().text = roleConfigData.GetAtr3AddValueOnLv (roleData.lv).ToString ();

			//7. 升级之后的Label
			if (roleData.lv < roleConfigData.maxLv - 1) {
				this.LabelZhihoushu1.GetComponent<UILabel> ().text = roleConfigData.GetAtr1AddValueOnLv (roleData.lv + 1).ToString ();
				this.LabelZhihoushu2.GetComponent<UILabel> ().text = roleConfigData.GetAtr2AddValueOnLv (roleData.lv + 1).ToString ();
				this.LabelZhihoushu3.GetComponent<UILabel> ().text = roleConfigData.GetAtr3AddValueOnLv (roleData.lv + 1).ToString ();

				NGUITools.SetActive (this.ButtonMaxzuo, false);	//隐藏MAX,显示升级
				NGUITools.SetActive (this.ButtonShengjizuo, true);
			} else {
				//a. 已经最大，隐藏处理 隐藏升级后的值，箭头，隐藏金币，升级button，显示max并变为灰色
							
				//显示MAX,隐藏升级
				NGUITools.SetActive (this.ButtonMaxzuo, true);
				this.ButtonMaxzuo.GetComponent<UIButton> ().isEnabled = false;

				NGUITools.SetActive (this.ButtonShengjizuo, false);

				//隐藏三个升级后的值Label
				NGUITools.SetActive (this.LabelZhihoushu1, false);
				NGUITools.SetActive (this.LabelZhihoushu2, false);
				NGUITools.SetActive (this.LabelZhihoushu3, false);
				//隐藏三个箭头SpriteXiaojinbiShengji
				NGUITools.SetActive (this.SpriteShengjijiantou1, false);
				NGUITools.SetActive (this.SpriteShengjijiantou2, false);
				NGUITools.SetActive (this.SpriteShengjijiantou3, false);
				//隐藏金币Sprite 
				NGUITools.SetActive (this.SpriteXiaojinbiShengji, false);				
				NGUITools.SetActive (this.LabelShengji, false);

			}
						
			//8. 进度条更
			float value1 = (float)roleConfigData.GetAtr1AddValueOnLv (roleData.lv) / roleConfigData.GetAtr1AddValueOnLv (roleConfigData.maxLv - 1);
			float value2 = (float)roleConfigData.GetAtr2AddValueOnLv (roleData.lv) / roleConfigData.GetAtr2AddValueOnLv (roleConfigData.maxLv - 1);
			float value3 = (float)roleConfigData.GetAtr3AddValueOnLv (roleData.lv) / roleConfigData.GetAtr3AddValueOnLv (roleConfigData.maxLv - 1);

//			this.SpriteShengjijindu1.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value1, 1.0f);
//			this.SpriteShengjijindu2.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value2, 1.0f);
//			this.SpriteShengjijindu3.GetComponent<UISprite> ().drawRegion = new Vector4 (0.0f, 0.0f, value3, 1.0f);
//
//			this.SpriteShengjijindu1.GetComponent<UISprite> ().enabled = value1 > 0.001f;
//			this.SpriteShengjijindu2.GetComponent<UISprite> ().enabled = value2 > 0.001f;			
//			this.SpriteShengjijindu3.GetComponent<UISprite> ().enabled = value3 > 0.001f;

			if(this.levelupValue1!=value1)
			{

				 // && true == this.levelupValue1ChangeTween.IsPlaying ()
				if (this.levelupValue1ChangeTween != null) {
					this.levelupValue1ChangeTween.Kill ();
				}
				
				this.levelupValue1ChangeTween = DOVirtual.Float(levelupValue1,value1,0.75f,delegate(float value) 
				{
					this.SpriteShengjijindu1.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value,1.0f);
					this.SpriteShengjijindu1.GetComponent<UISprite> ().enabled = value1 > 0.001f;
				});
				
				this.levelupValue1ChangeTween.SetEase(Ease.InQuad);
				this.levelupValue1 = value1;
			}

			if(this.levelupValue2!=value2)
			{
				//&& true == this.levelupValue2ChangeTween.IsPlaying ()
				if (this.levelupValue2ChangeTween != null ) {
					this.levelupValue2ChangeTween.Kill ();
				}
				
				this.levelupValue2ChangeTween = DOVirtual.Float(levelupValue2,value2,0.75f,delegate(float value) 
				{
					this.SpriteShengjijindu2.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value,1.0f);
					this.SpriteShengjijindu2.GetComponent<UISprite> ().enabled = value2 > 0.001f;
				});
				
				this.levelupValue2ChangeTween.SetEase(Ease.InQuad);
				this.levelupValue2 = value2;
			}


			if(this.levelupValue3!=value3)
			{
				//&& true == this.levelupValue3ChangeTween.IsPlaying ()
				if (this.levelupValue3ChangeTween != null) {
					this.levelupValue3ChangeTween.Kill ();
				}
				
				this.levelupValue3ChangeTween = DOVirtual.Float(levelupValue3,value3,0.75f,delegate(float value) 
				{
					this.SpriteShengjijindu3.GetComponent<UISprite>().drawRegion = new Vector4(0.0f,0.0f,value,1.0f);
					this.SpriteShengjijindu3.GetComponent<UISprite> ().enabled = value3 > 0.001f;
				});
				
				this.levelupValue3ChangeTween.SetEase(Ease.InQuad);
				this.levelupValue3 = value3;
			}

			//9. 属性名称
			this.LabelRenwushuxing1.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr1);
			this.LabelRenwushuxing2.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr2);
			this.LabelRenwushuxing3.GetComponent<UILabel> ().text = GetPropName (roleConfigData.atr3);

		}
	}
	
	/// <summary>
	/// 选中和未选中 的切换,参数enable表示Button是否使能
	/// false 表示"选中"按键要变灰色，同时勾选图片要出现
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
	/// 头像,名称，好心，金币，钻石个数------从数据层获得
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
				}).SetUpdate (UpdateType.Normal,true);
				
				this.powerChangeTween.SetEase (Ease.Linear);
				this.countPower = MainState.Instance.playerInfo.power;
			}
		}
		if (MainState.Instance.playerInfo != null) {
			if (MainState.Instance.playerInfo.gold != this.countCoin) 
			{
				if (this.coinChangeTween != null && true == this.coinChangeTween.IsPlaying ()) {
					this.coinChangeTween.Kill ();
				}
				
				this.coinChangeTween = DOVirtual.Float (this.countCoin, MainState.Instance.playerInfo.gold, 1.0f, delegate(float value) {
					this.LabelBishuzi.GetComponent<UILabel> ().text = ((int)value).ToString ();
				}).SetUpdate (UpdateType.Normal,true);
				
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
				}).SetUpdate (UpdateType.Normal,true);
				
				this.diamondChangeTween.SetEase (Ease.Linear);
				this.countDiamond = MainState.Instance.playerInfo.diamond;
			}
		}


	}

	/// <summary>
	/// 暂时只需要一个进度条，且这3个进度条图片丢失，所以干脆隐藏了
	/// </summary>
	void HideNoUseGameObject ()
	{
		NGUITools.SetActive (SpriteDangqianjindu1, false);
		NGUITools.SetActive (SpriteDangqianjindu2, false);
		NGUITools.SetActive (SpriteDangqianjindu3, false);
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
	/// <returns>The car name.</returns>
	/// <param name="index">Index.</param>
	string GetRoleName (int index)
	{	
		if (index < 0 || index >= this.roleConfigDataList.Count) {
			Debug.LogError ("Please check this code,valide range is [0," + (this.roleConfigDataList.Count - 1) + "]" + "But param index is " + index);
			//默认返回第1个车辆
			if(this.roleConfigDataList.Count>0)
				return this.roleConfigDataList[0].roleAvt;
			
			return "RoleAvt1";
		}
		
		return  this.roleConfigDataList [index].roleAvt;


	}
	
	
	/// <summary>
	/// 返回
	/// </summary>
	void OnClickButtonFanhui ()
	{
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

		if (this.coinChangeTween != null) {
			this.coinChangeTween.Kill (true);
		}

		if (this.diamondChangeTween != null) {
			this.diamondChangeTween.Kill (true);
		}

		if (this.powerChangeTween != null) {
			this.powerChangeTween.Kill (true);
		}
		
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
		Hashtable logicPar=new Hashtable();
		logicPar.Add("roleId",this.roleConfigDataList[this.currentIndex].id);
		LogicManager.Instance.ActNewLogic<LogicGainRole>(logicPar,OnUnLockFinish);
	}


	void OnUnLockFinish(Hashtable logicPar)
	{
		LogicReturn result=(LogicReturn)logicPar["logicReturn"];
		
		switch(result)
		{
		case LogicReturn.LR_NOTENOUGHDIAMOND:
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].unlockFailTip,1.5f);
			this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = true;
			},true);

			break;

		case LogicReturn.LR_NOTENOUGHGOLD:
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].unlockFailTip,1.5f);
			this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = true;
			},true);
			break;

		case LogicReturn.LR_NOTENOUGHSCORE:
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].unlockFailTip,1.5f);
			this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonJiesuo.GetComponent<UIButton>().isEnabled = true;
			},true);
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
	/// 选中--与解锁是互斥，如果当前用户就是选择的这个要灰色
	/// </summary>
	void OnClickButtonXuanzhong ()
	{
		this.currentSelectIndex = this.currentIndex;
		this.SetSelectButtonEnable (false);

		if (MainState.Instance.playerInfo != null)
			MainState.Instance.playerInfo.nowRoleId = (this.currentSelectIndex + 1).ToString ();
				
		//2添加一个出场缩放动画
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));
				
	}

	/// <summary>
	/// 已经最大，应该为灰色不可以点击
	/// </summary>
	void OnClickButtonMaxzuo ()
	{
			
	}
	
	/// <summary>
	/// 升级---当前车辆的数据，如果到了最大值，将显示一个灰色的max
	/// </summary>
	void OnClickButtonShengjizuo ()
	{
		Hashtable logicPar=new Hashtable();
		logicPar.Add("roleId",this.roleConfigDataList[this.currentIndex].id);
		LogicManager.Instance.ActNewLogic<LogicLvupRole>(logicPar,OnShengjiFinish);
	}
	
	void OnShengjiFinish(Hashtable logicPar)
	{
		LogicReturn result=(LogicReturn)logicPar["logicReturn"];
		Debug.Log (result);

		switch(result)
		{
		case LogicReturn.LR_NOTENOUGHDIAMOND:
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].levelupFailTip,1.5f);
			this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopDiamond;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = true;
			},true);

			break;
		case LogicReturn.LR_NOTENOUGHGOLD:
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].levelupFailTip,1.5f);
			this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType = ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = true;
			},true);
			break;
		case LogicReturn.LR_NOTENOUGHSCORE:
			//后期改为积分商店
			PanelMainUIController.Instance.ShowUIMsgBox(this.roleConfigDataList[this.currentIndex].levelupFailTip,1.5f);
			this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = false;

			DOVirtual.DelayedCall(1.0f,delegate() {
				ContainerShopUIController.shopType =  ContainerShopUIController.ShopType.ShopCoin;
				PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebUIShop);
				this.ButtonShengjizuo.GetComponent<UIButton>().isEnabled = true;
			},true);

			break;
		case LogicReturn.LR_REACHEDMAXLV:
			break;
		case LogicReturn.LR_SUCCESS:
			//开始刷新界面
			this.SetupRightUI (currentIndex);
			break;
		}
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

		///1 lock or not
		this.currentIndex++;
		this.SetupRightUI (this.currentIndex);


		//2 勾选状态的特效
		this.SpriteGou.transform.DOKill ();
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.SpriteGou.transform.DOScale (new Vector3 (6, 6, 6), 0.001f));
		mySeq.Append (this.SpriteGou.transform.DOScale (Vector3.one, 0.4f).SetEase (Ease.OutBounce));


		///3. add new car 
		this.Clean3DRole ();
		this.currentRolePrefebName = this.GetRoleName (this.currentIndex);
		InitRoleAndStartAnimation (currentRolePrefebName);
		
		///4. hide arrow button or not
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


		this.Clean3DRole ();
		this.currentRolePrefebName = this.GetRoleName (this.currentIndex);
		InitRoleAndStartAnimation (currentRolePrefebName);
		
		CheckButtonArrowEnable (currentIndex);
	}
		
	void OnDragUI (GameObject obj, Vector2 delta)
	{
		//Debug.Log ("滑动" + delta.x + " " + delta.y);
		if (isDraging == true) {
				
			dragSum += delta.x;
			//由于delta参数的单位 是 屏幕坐标像素，但是采用FixedSize NGUI缩放策略，图片的大小是不缩放的大小，所以需要自己进行处理
			float afterW = Screen.width * this.ContainerHuadong.GetComponent<UIWidget> ().width / 1280.0f;
			//只旋转360度
			if (Mathf.Abs (dragSum) <= afterW)
				this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateRole (delta.x, afterW);
		}
	}
		
	void OnPressButtonDrag (GameObject go, bool state)
	{
		isDraging = state;
		dragSum = 0.0f;
			
		if (state == false) {
			this.uiShow2D.AddMissingComponent<ui_show_2DUIController> ().ManualRotateRole (0, 0, true);
		}
	}

	/// <summary>
	/// 更换头像
	/// </summary>
	void OnClickSpriteTouxiang ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupTip, UIControllerConst.UIPrefebTouxiang);
	}

	/// <summary>
	/// 得到属性的名称---临时方案,需要策划给出，
	/// </summary>
	/// <returns>The property name.</returns>
	string GetPropName (int id)
	{
		string name = "";
		switch(id)
		{
		case 1:
			name = "金手指";
			break;
		case 2:
			name = "破坏";
			break;
		case 3:
			name = "聚能甲";
			break;
		case 4:
			name = "藏匿";
			break;
		case 5:
			name = "节能";
			break;
		case 6:
			name = "爆发";
			break;
		case 7:
			name = "幻影";
			break;
		case 8:
			name = "超急速";
			break;
		default:
			name = "金手指";
			break;

		}

		return name;
	}
}
