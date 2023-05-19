using UnityEngine;
using System.Collections;
using DG.Tweening;

public partial class ContainerQuanshuxianshiUIController : UIControllerBase
{
	private static ContainerQuanshuxianshiUIController _instance = null;
	private static readonly object lockHelper = new object ();
	
	public static ContainerQuanshuxianshiUIController Instance {
		get {
			if (_instance == null) {
				lock (lockHelper) {
					if (_instance == null) {
						GameObject uiMessageBox = PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Top, "ContainerQuanshuxianshi");
						_instance = uiMessageBox.AddMissingComponent<ContainerQuanshuxianshiUIController> ();
					}
				}
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	/// <summary>
	/// 显示一个圈数
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="timeDelay">Time delay.</param>
	/// <param name="lastLap">If set to <c>true</c> last lap.</param>
	public void ShowMsg (string msg, float timeDelay, bool lastLap)
	{
		//1.
		if (timeDelay <= 0.0f)
			timeDelay = 1.0f;

		NGUITools.SetActive (this.LabelShuzi, false);
		NGUITools.SetActive (this.SpriteZuihouyiquan, false);

		//2.
		this.LabelShuzi.transform.DOKill ();
		this.SpriteZuihouyiquan.transform.DOKill ();

		if (lastLap == false) {
			NGUITools.SetActive (this.LabelShuzi, true);

			//this.LabelShuzi.transform.localPosition = new Vector3 (this.UIOriginalPositionLabelShuzi.x, this.UIOriginalPositionLabelShuzi.y - 500, this.UIOriginalPositionLabelShuzi.z);
			this.LabelShuzi.transform.localScale = new Vector3(6,6,6);

			this.LabelShuzi.transform.DOScale(Vector3.one,0.5f).SetUpdate (true).SetEase (Ease.OutBack).OnComplete (delegate () {
				this.LabelShuzi.transform.DOLocalMove (this.UIOriginalPositionLabelShuzi, timeDelay).SetUpdate (true).OnComplete (delegate () {
//					this.LabelShuzi.transform.DOLocalMove( new Vector3 (this.UIOriginalPositionLabelShuzi.x, this.UIOriginalPositionLabelShuzi.y + 800, this.UIOriginalPositionLabelShuzi.z),0.4f)
//						.OnComplete (delegate () {
//							this.CloseUI ();
//						}).SetUpdate (true).SetEase (Ease.InBack);

					this.LabelShuzi.transform.DOScale(Vector3.zero,0.4f).OnComplete (delegate () {
						this.CloseUI ();
					}).SetUpdate (true).SetEase (Ease.InBack);
				});
			});

			this.LabelShuzi.GetComponent<UILabel> ().text = msg;
		} else {
			NGUITools.SetActive (this.SpriteZuihouyiquan, true);
		
			//this.SpriteZuihouyiquan.transform.localPosition = new Vector3 (this.UIOriginalPositionSpriteZuihouyiquan.x, this.UIOriginalPositionSpriteZuihouyiquan.y - 500, this.UIOriginalPositionSpriteZuihouyiquan.z);
			this.SpriteZuihouyiquan.transform.localScale = new Vector3(6,6,6);

			this.SpriteZuihouyiquan.transform.DOScale(Vector3.one,0.5f).SetUpdate (true).SetEase (Ease.OutBack).OnComplete (delegate () {
				this.SpriteZuihouyiquan.transform.DOLocalMove (this.UIOriginalPositionSpriteZuihouyiquan, timeDelay).SetUpdate (true).OnComplete (delegate () {
//					this.SpriteZuihouyiquan.transform.DOLocalMove( new Vector3 (this.UIOriginalPositionLabelShuzi.x, this.UIOriginalPositionLabelShuzi.y + 800, this.UIOriginalPositionLabelShuzi.z),0.4f)
//						.OnComplete (delegate () {
//							this.CloseUI ();
//						}).SetUpdate (true).SetEase (Ease.InBack);
					this.SpriteZuihouyiquan.transform.DOScale(Vector3.zero,0.4f).OnComplete (delegate () {
						this.CloseUI ();
					}).SetUpdate (true).SetEase (Ease.InBack);
				});
			});
		}
	}
	
	/// <summary>
	/// 停止本UI界面上的所有Aciton，并销毁对象
	/// </summary>
	public void StopAllAction ()
	{
		this.SpriteZuihouyiquan.transform.DOKill ();
		this.LabelShuzi.transform.DOKill ();
		this.CloseUI ();
	}
}
