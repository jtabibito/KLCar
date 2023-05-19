using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
///  挑战赛模式选择界面---三种模式可以选择，地图选择
/// 2015-4-23 19:42:09
/// </summary>
public partial class ContainerMoshixuanzeUIController : UIControllerBase
{
	private	GameObject gridObj;
	private	UIScrollView scrollview;
	private	UIGrid 		grid;
	private	int childCount = 0;

	///
	private string oldID = "-1";
	private	int currentIndex = 0;
	private int mIntModeSelectCheckBoxGroup = 0;
	private int mIntModeSelectResult = 1;

	//
	private bool  isDraging = false;
	private float dragSum = 0.0f;

	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
		InitUIStartAnimation ();
		InitScrollViewParam ();

		InitScrollViewOffset ();
		UpdateTopStatusBar ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTopStatusBar ();
	}
	
	void InitButtonEvent ()
	{
		this.mIntModeSelectCheckBoxGroup = this.ButtonJixiansai.GetComponent<UIToggle> ().group;			//模式选择groupID

		this.ButtonKaishi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonKaishi));
		this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));
		this.ButtonJiahaobi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaobi));
		this.ButtonJiahaoxin.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaoxin));
		this.ButtonJiahaozuan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJiahaozuan));
				
		this.ButtonJingsusai.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickMoshixuanze));
		this.ButtonJixiansai.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickMoshixuanze));
		this.ButtonPohuaisai.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickMoshixuanze));

		this.ButtonZuo.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonZuo));
		this.ButtonYou.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYou));

		UIEventListener.Get (this.ContainerHuadong).onPress = this.OnPressButtonDrag;											//记录按下和抬起
		UIEventListener.Get (this.ContainerHuadong).onDrag = OnDragUI;															/// 初始化滚动事件，车辆，宠物，角色等信息进行

		this.SpriteTouxiang.AddMissingComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickSpriteTouxiang));		//更换头像
	}

	void InitUIStartAnimation ()
	{
		this.ButtonJingsusai.transform.localPosition = new Vector3 (Screen.width + UIOriginalPositionButtonJingsusai.x, UIOriginalPositionButtonJingsusai.y, 0);
		this.ButtonJixiansai.transform.localPosition = new Vector3 (Screen.width + UIOriginalPositionButtonJixiansai.x, UIOriginalPositionButtonJixiansai.y, 0);
		this.ButtonPohuaisai.transform.localPosition = new Vector3 (Screen.width + UIOriginalPositionButtonPohuaisai.x, UIOriginalPositionButtonPohuaisai.y, 0);
		this.ButtonKaishi.transform.localPosition = new Vector3 (Screen.width + UIOriginalPositionButtonKaishi.x, UIOriginalPositionButtonKaishi.y, 0);	
		this.LabelJikexing.transform.localPosition = new Vector3 (Screen.width + UIOriginalPositionLabelJikexing.x, UIOriginalPositionLabelJikexing.y, 0);	

		Sequence rightButtonSequence = DOTween.Sequence ();
		rightButtonSequence.Append (this.ButtonJingsusai.transform.DOLocalMove (UIOriginalPositionButtonJingsusai, 0.25f).SetEase (Ease.OutBack));
		rightButtonSequence.Append (this.ButtonJixiansai.transform.DOLocalMove (UIOriginalPositionButtonJixiansai, 0.25f).SetEase (Ease.OutBack));
		rightButtonSequence.Append (this.ButtonPohuaisai.transform.DOLocalMove (UIOriginalPositionButtonPohuaisai, 0.25f).SetEase (Ease.OutBack).OnComplete(delegate (){
			this.LabelJikexing.transform.DOLocalMove (UIOriginalPositionLabelJikexing, 0.25f).SetEase (Ease.OutBack);
		}));
		rightButtonSequence.Append (this.ButtonKaishi.transform.DOLocalMove (UIOriginalPositionButtonKaishi, 0.25f).SetEase (Ease.OutBack));


		this.ButtonZuo.transform.DOLocalMoveX (UIOriginalPositionButtonZuo.x - 25, 1.5f).SetLoops (-1,LoopType.Restart).SetEase (Ease.OutBounce).SetUpdate(true);
		this.ButtonYou.transform.DOLocalMoveX (UIOriginalPositionButtonYou.x + 25, 1.5f).SetLoops (-1,LoopType.Restart).SetEase (Ease.OutBounce).SetUpdate(true);
	}

	void InitScrollViewParam ()
	{
		this.scrollview = this.ScrollDituxuanze.GetComponent<UIScrollView> ();
		this.gridObj = this.ScrollDituxuanze.transform.FindChild ("Grid").gameObject;		//FIX ME,暂时这么做
		this.grid = gridObj.GetComponent<UIGrid> ();
		this.childCount = gridObj.transform.childCount;
	}

	void  InitScrollViewOffset ()
	{

		//是否有历史记录
		if (MainState.Instance.playerInfo != null) {
			this.oldID = MainState.Instance.playerInfo.missionOfPreviousRelaxation;
			if (this.oldID.Equals ("-1") == true) {
				this.ButtonJingsusai.GetComponent<UIToggle> ().value = true;
				this.currentIndex = 0;											//根据名称得到，应该有个字典或者hashmap
			} else if (this.oldID.Length >= 1 && this.oldID.Length <= 3) {
				int temp = int.Parse (this.oldID);
				int mapIndex = temp % 100;
				int modeIndex = (int)(temp / 100);
				
				this.currentIndex = mapIndex-1;
				switch (modeIndex) {
				case 1:
					this.ButtonJingsusai.GetComponent<UIToggle> ().value = true;
					break;
				case 2:
					this.ButtonJixiansai.GetComponent<UIToggle> ().value = true;
					break;
				case 3:
					this.ButtonPohuaisai.GetComponent<UIToggle> ().value = true;
					break;
				default:
					this.ButtonJingsusai.GetComponent<UIToggle> ().value = true;
					break;
				}
			} else {
				Debug.LogError ("Relaxation Mode,Please check this.oldID " + this.oldID);
			}
		}

		//2. start move
		this.currentIndex = NGUIMath.ClampIndex (this.currentIndex, childCount);
		//scrollview.MoveAbsolute ();
		scrollview.MoveRelative (new Vector3 (-grid.cellWidth * this.currentIndex, 0, 0));		//prefeb初始化的起点必须是left
		
		CheckButtonArrowEnable (this.currentIndex);												//可能有只有1个子节点，所以左右两边button都隐藏
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

		//6. 体力消耗
		this.LabelJikexing.GetComponent<UILabel> ().text = UIControllerConst.relaxationConsumpPower.ToString ();
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

		if (index >= this.childCount - 1)
			ButtonYou.SetActive (false);
		else
			ButtonYou.SetActive (true);
	}

	/// <summary>
	/// 左边Button 物体向右边移动，变小currentIndex
	/// </summary>
	void OnClickButtonZuo ()
	{
		if (currentIndex <= 0)
			return;
		currentIndex--;
		scrollview.MoveRelative (new Vector3 (grid.cellWidth, 0, 0));		//后续添加动画效果
				
		CheckButtonArrowEnable (currentIndex);
	}

	/// <summary>
	/// 右边Button 物体向左边移动，变大currentIndex
	/// </summary>
	void OnClickButtonYou ()
	{
		if (currentIndex >= childCount - 1)
			return;
		
		currentIndex++;
		scrollview.MoveRelative (new Vector3 (-grid.cellWidth, 0, 0));

		CheckButtonArrowEnable (currentIndex);
	}
		
	/// <summary>
	/// 开始游戏
	/// </summary>
	void OnClickButtonKaishi ()
	{
		int configPower = UIControllerConst.relaxationConsumpPower;
		int leftPower = (int)(MainState.Instance.playerInfo.power);
		if (leftPower < configPower) {
			PanelMainUIController.Instance.ShowUIMsgBox ("体力不足,请购买体力继续游戏", 2.0f);
			return;
		}

		MainState.Instance.playerInfo.ChangePower(-configPower);
		MainState.Instance.SavePlayerData ();

		RaceManager.Instance.startRaceGame (currentIndex + 1, mIntModeSelectResult);
	}

	/// <summary>
	/// 返回
	/// </summary>
	void OnClickButtonFanhui ()
	{
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebMainUI);
		this.CloseUI ();
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

	void OnClickMoshixuanze ()
	{
		UIToggle toggle = UIToggle.GetActiveToggle (this.mIntModeSelectCheckBoxGroup);
		if (toggle == null || (toggle != null && toggle.value == false)) {
			return;
		}

		//根据类型设置相关的数据
		if (toggle.name.Equals (this.ButtonJingsusai.name)) {
			mIntModeSelectResult = 1;
		} else if (toggle.name.Equals (this.ButtonJixiansai.name)) {
			mIntModeSelectResult = 2;
		} else if (toggle.name.Equals (this.ButtonPohuaisai.name)) {
			mIntModeSelectResult = 3;
		} else {
			Debug.LogWarning ("Please add new toggle to current group");
		}
	}

	void OnDragUI (GameObject obj, Vector2 delta)
	{
		dragSum += delta.x;
	}
	
	void OnPressButtonDrag (GameObject go, bool state)
	{
		isDraging = state;

		if (state == false) 
		{
			if(dragSum<-4)
			{
				OnClickButtonYou ();
			}
			else if(dragSum>4)
			{
				OnClickButtonZuo ();
			}
		}
		else
		{
			dragSum = 0.0f;
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
