using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 弹出一个消息框，类型Android中的Toast消息类型
/// 2015年5月21日20:12:48
/// </summary>
public partial class MessageboxUIController : UIControllerBase {

	private static MessageboxUIController _instance = null;
	private static readonly object lockHelper = new object();
	private MessageboxUIController()
	{

	}
	
	public static MessageboxUIController Instance {
		get {
			if(_instance==null)
			{
				lock(lockHelper)
				{
					if(_instance==null)
					{
						GameObject uiMessageBox = PanelMainUIController.Instance.AddUI(PanelMainUIController.UILayer.L_Top,"Messagebox");
						_instance = uiMessageBox.AddMissingComponent<MessageboxUIController> ();
						//_instance=(MessageboxUIController)GameObject.FindObjectOfType(typeof(MessageboxUIController));
					}
				}
			}
			return _instance;
		}
	}
	
	// LENGTH_SHORT 和 LENGTH_LONG 两常量，分别表示短时间显示和长时间显示。
	public static int  LENGTH_SHORT = 0;
	public static int  LENGTH_LONG = 1;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 显示一个消息---暂时只支持自动销毁的方式，且同时只能显示一个
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="timeDelay">Time delay.</param>
	public void ShowMsg(string msg,float timeDelay)
	{
		//1.
		if(timeDelay<=0.0f)
			timeDelay = 1.0f;

		//2.
		this.SpriteDi.transform.DOKill();
		this.Label.transform.DOKill();

		//3.
		this.SpriteDi.transform.localPosition = new Vector3(this.UIOriginalPositionSpriteDi.x,this.UIOriginalPositionSpriteDi.y - 500,this.UIOriginalPositionSpriteDi.z);
		this.Label.transform.localPosition = new Vector3(this.UIOriginalPositionLabel.x,this.UIOriginalPositionLabel.y - 500,this.UIOriginalPositionLabel.z);

		//4.
		this.SpriteDi.transform.DOLocalMove(this.UIOriginalPositionLabel,0.4f).SetUpdate(true).SetEase(Ease.OutBack);
		this.Label.transform.DOLocalMove(this.UIOriginalPositionLabel,0.4f).SetUpdate(true).SetEase(Ease.OutBack).OnComplete(delegate ()
		{
			this.Label.transform.DOLocalMove(this.UIOriginalPositionLabel,timeDelay).SetUpdate(true).OnComplete(delegate (){
				this.transform.DOScale(Vector3.zero,0.10f).OnComplete(delegate () 
			  	{
					this.CloseUI();
				}).SetUpdate(true);
			});
		});

		//5.
		this.Label.GetComponent<UILabel>().text = msg;
	}

	/// <summary>
	/// 停止本UI界面上的所有Aciton，并消耗对象
	/// </summary>
	public void StopAllAction()
	{
		this.SpriteDi.transform.DOKill();
		this.Label.transform.DOKill();
		this.transform.DOKill();
		this.CloseUI();
	}
}
