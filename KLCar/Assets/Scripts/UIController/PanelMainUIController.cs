using UnityEngine;
using System.Collections;

public partial class PanelMainUIController : UIControllerBase
{

	static PanelMainUIController instance;
	private static readonly object lockHelper = new object ();

	public static PanelMainUIController Instance {
		get {
			if (instance == null) 
			{
				lock (lockHelper) 		//modify by maojudong at 2015年7月6日16:29:21，单利有出现2个UIMain的低概率
				{
					if (instance == null) 
					{
						GameObject uiMain = GameObject.Find ("UIMain");
						if(uiMain==null)
						{
							uiMain = GameObject.Find("UIMain(Clone)");
						}

						if (uiMain == null) 
						{
							uiMain = (GameObject)(GameObject.Instantiate (GameResourcesManager.GetUIPrefab ("UIMain")));
						}
						instance = uiMain.transform.FindChild ("Camera/PanelMain").GetComponent<PanelMainUIController> ();
					}
//					return instance;
				}
			}
			return instance;
		}
	}
	
	public enum UILayer:int
	{
		L_Background=1,
		L_Buttom=2,
		L_PopupWindow=3,
		L_PopupTip=4,
		L_Top=5,
	}
	
	// Use this for initialization
	void Start ()
	{
//		this.AddUI(UILayer.L_Top,"ContainerDebugMsg");
		InitDebugLabel ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateDebugLabel ();
	}

	void OnDestroy ()
	{
		instance = null;
	}
	
	public void ShowDebug (string msg)
	{
		ContainerDebugMsgUIController.AddMsg (msg);
	}

	/// <summary>
	/// 显示一个UI提示信息，可以是警告，错误报告等信息
	/// add by maojudong
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="timeDelay">Time delay.</param>
	public void ShowUIMsgBox (string msg, float timeDelay)
	{
		MessageboxUIController.Instance.ShowMsg (msg, timeDelay);
	}

	/// <summary>
	/// 关闭一个UI提示信息
	/// add by maojudong
	/// </summary>
	public void CloseUIMsgBox ()
	{
		MessageboxUIController.Instance.StopAllAction ();
	}
	
	/// <summary>
	/// 只显示一个UIlayer层，其他的暂时隐藏
	/// add by maojudong
	/// </summary>
	/// <param name="ul">Ul.</param>
	public void ShowOneUIContainer (UILayer ul)
	{
		NGUITools.SetActive (this.PanelBackground, false);
		NGUITools.SetActive (this.PanelButtom, false);
		NGUITools.SetActive (this.PanelPopupWindow, false);
		NGUITools.SetActive (this.PanelPopupTip, false);
		NGUITools.SetActive (this.PanelTop, false);

		switch (ul) {
		case UILayer.L_Background:
			NGUITools.SetActive (this.PanelBackground, true);
			break;
		case UILayer.L_Buttom:
			NGUITools.SetActive (this.PanelButtom, true);
			break;
		case UILayer.L_PopupWindow:
			NGUITools.SetActive (this.PanelPopupWindow, true);
			break;
		case UILayer.L_PopupTip:
			NGUITools.SetActive (this.PanelPopupTip, true);
			break;
		case UILayer.L_Top:
			NGUITools.SetActive (this.PanelTop, true);
			break;
		}

	}

	/// <summary>
	/// 只显示一个UIlayer层，其他的暂时隐藏----后续添加具有记忆功能的恢复，有选择性能的恢复
	/// add by maojudong
	/// </summary>
	/// <param name="ul">Ul.</param>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void ShowAllUIContainer ()
	{
		NGUITools.SetActive (this.PanelBackground, true);
		NGUITools.SetActive (this.PanelButtom, true);
		NGUITools.SetActive (this.PanelPopupWindow, true);
		NGUITools.SetActive (this.PanelPopupTip, true);
		NGUITools.SetActive (this.PanelTop, true);
	}

	public GameObject GetLayerContainer (UILayer ul)
	{
		switch (ul) {
		case UILayer.L_Background:
			return this.PanelBackground;
			break;
		case UILayer.L_Buttom:
			return this.PanelButtom;
			break;
		case UILayer.L_PopupWindow:
			return this.PanelPopupWindow;
			break;
		case UILayer.L_PopupTip:
			return this.PanelPopupTip;
			break;
		case UILayer.L_Top:
			return this.PanelTop;
			break;
		}
		return null;
	}

	public GameObject AddUI (UILayer layer, string uiName)
	{
		GameObject addUI = GameResourcesManager.GetUIPrefab (uiName);
		addUI = NGUITools.AddChild (this.GetLayerContainer (layer), addUI);		//change by maojudong ,should return Instantiated Object,
		return addUI;
	}

	/// <summary>
	/// Cleans all U.
	/// 清除所有UI
	/// </summary>
	public void CleanAllUI ()
	{
		foreach (Transform tf in this.PanelBackground.transform) {
			Destroy (tf.gameObject);
		}
		foreach (Transform tf in this.PanelButtom.transform) {
			Destroy (tf.gameObject);
		}
		foreach (Transform tf in this.PanelPopupWindow.transform) {
			Destroy (tf.gameObject);
		}
		foreach (Transform tf in this.PanelPopupTip.transform) {
			Destroy (tf.gameObject);
		}
		foreach (Transform tf in this.PanelTop.transform) {
			Destroy (tf.gameObject);
		}
//		this.AddUI(UILayer.L_Top,"ContainerDebugMsg");
	}

	/// <summary>
	/// Enters the game.
	/// 进入游戏
	/// </summary>
	public void EnterGame ()
	{
		this.AddUI (UILayer.L_Top, "ContainerBeforeLogin");
	}

	/// <summary>
	/// Enters the login.
	/// 进入登陆界面
	/// </summary>
	public void EnterLogin ()
	{
		this.CleanAllUI ();
		this.AddUI (UILayer.L_Buttom, "ContainerLogindenglu");
//		this.AddUI (UILayer.L_Background, "ContainerLoginBackground");
	}

	/// <summary>
	/// Enters the hall.
	/// 进入游戏大厅
	/// </summary>
	public void EnterHall ()
	{
		this.CleanAllUI ();
		this.AddUI (UILayer.L_Buttom, "ContainerMainzhujiemian");
	}

	/// <summary>
	/// Enters the race.
	/// 创建赛程UI
	/// </summary>
	public void EnterRace ()
	{
		switch (RaceManager.Instance.RaceCounterInstance.CurMode) {
		case  RaceCounter.RaceMode.RM_Test:
		case  RaceCounter.RaceMode.RM_ArderSpeed:
		case  RaceCounter.RaceMode.RM_Speed:
			this.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebOperationjinsumoshi);
			break;
		case  RaceCounter.RaceMode.RM_ArderInfinite:
		case  RaceCounter.RaceMode.RM_Infinite:
			this.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebOperationjixiansai);
			break;
		case  RaceCounter.RaceMode.RM_ArderDestroy:
		case  RaceCounter.RaceMode.RM_Destroy:
			this.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebOperationpohuaisai);
			break;
		case  RaceCounter.RaceMode.RM_ArderChallenge:
		case  RaceCounter.RaceMode.RM_Challenge:
			this.AddUI (PanelMainUIController.UILayer.L_Buttom, UIControllerConst.UIPrefebOperationtiaozhansai);
			break;
		default :
			break;
		}
		
	}
	
	/// <summary>
	/// Overs the race.
	/// 创建赛程结束UI
	/// </summary>
	public void OverRace ()
	{
		this.CleanAllUI ();

		switch (RaceManager.Instance.RaceCounterInstance.CurMode) {
		case  RaceCounter.RaceMode.RM_Test:
		case  RaceCounter.RaceMode.RM_ArderSpeed:
			this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanMingci);
			break;
		case  RaceCounter.RaceMode.RM_ArderInfinite:
			this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanXingcheng);
			break;
		case  RaceCounter.RaceMode.RM_ArderDestroy:
			this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanFenshu);
			break;
		case  RaceCounter.RaceMode.RM_ArderChallenge:
			this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanHaoshi);
			break;
		case  RaceCounter.RaceMode.RM_Challenge:
			if (RaceManager.Instance.RaceCounterInstance.curResult == RaceCounter.RaceResult.RR_Lose) {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWeiwancheng);
			} else {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWancheng);
			}
			break;
		case  RaceCounter.RaceMode.RM_Destroy:
			if (RaceManager.Instance.RaceCounterInstance.curResult == RaceCounter.RaceResult.RR_Lose) {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWeiwancheng);
			} else {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWancheng);
			}
			break;
		case  RaceCounter.RaceMode.RM_Infinite:
			if (RaceManager.Instance.RaceCounterInstance.curResult == RaceCounter.RaceResult.RR_Lose) {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWeiwancheng);
			} else {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWancheng);
			}
			break;
		case  RaceCounter.RaceMode.RM_Speed:
			if (RaceManager.Instance.RaceCounterInstance.curResult == RaceCounter.RaceResult.RR_Lose) {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWeiwancheng);
			} else {
				this.AddUI (PanelMainUIController.UILayer.L_PopupWindow, UIControllerConst.UIPrefebOperationJiesuanWancheng);
			}
			break;
		default :
			break;
		}
	}

	//帧率处理
	private GameObject fps1Label = null;
	private float f_LastInterval;
	private int i_Frames = 0;
	private float f_Fps1;
		
	/// <summary>
	/// 初始化帧率
	/// </summary>
	private void InitDebugLabel ()
	{
		if (UIControllerConst.DEBUG_FPS == true && this.PanelDebug != null) {
			this.PanelDebug.SetActive (true);
			Transform trans = this.PanelDebug.transform.FindChild ("fps1Label");
			if (trans != null) {
				fps1Label = trans.gameObject;
			}
				
			f_LastInterval = Time.realtimeSinceStartup;
			i_Frames = 0;
		} else if (this.PanelDebug != null) {
			this.PanelDebug.SetActive (false);
		}
	}
		
	private void  UpdateDebugLabel ()
	{
		if (UIControllerConst.DEBUG_FPS == true && this.PanelDebug != null) {
			++i_Frames;
			if (Time.realtimeSinceStartup > f_LastInterval + 0.5f) {   //0.5秒更新一次UI显示
				f_Fps1 = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
				f_Fps1 = ((int)(f_Fps1 * 100)) / 100.0f;
				i_Frames = 0;
				f_LastInterval = Time.realtimeSinceStartup;			//记录上一次的时间
			}
				
			if (this.fps1Label != null) {
				this.fps1Label.GetComponent<UILabel> ().text = "fps: " + f_Fps1.ToString ();
			}
		}
	}
}
