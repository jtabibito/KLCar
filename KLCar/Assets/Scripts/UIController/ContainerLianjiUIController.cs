using UnityEngine;
using System.Collections;
using DG.Tweening;

public partial class ContainerLianjiUIController : UIControllerBase {

	private static ContainerLianjiUIController _instance = null;
	private static readonly object lockHelper = new object();
	
	public static ContainerLianjiUIController Instance {
		get {
			if(_instance==null)
			{
				lock(lockHelper)
				{
					if(_instance==null)
					{
						GameObject uiMessageBox = PanelMainUIController.Instance.AddUI(PanelMainUIController.UILayer.L_Top,"ContainerLianji");
						_instance = uiMessageBox.AddMissingComponent<ContainerLianjiUIController> ();
					}
				}
			}
			return _instance;
		}
	}

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

		if(Random.Range(0,2)==0)
			this.transform.localPosition = new Vector3(-189,75,0);
		else
			this.transform.localPosition = new Vector3(287,30,0);  //right

		//2.
		this.SpriteLianjiditu.transform.DOKill();
		this.LabelShuzi.transform.DOKill();
		
		//3.
		this.SpriteLianjiditu.transform.localPosition = new Vector3(this.UIOriginalPositionSpriteLianjiditu.x,this.UIOriginalPositionSpriteLianjiditu.y - 500,this.UIOriginalPositionSpriteLianjiditu.z);
		this.LabelShuzi.transform.localPosition = new Vector3(this.UIOriginalPositionLabelShuzi.x,this.UIOriginalPositionLabelShuzi.y - 500,this.UIOriginalPositionLabelShuzi.z);
		
		//4.
		this.SpriteLianjiditu.transform.DOLocalMove(this.UIOriginalPositionSpriteLianjiditu,0.4f).SetUpdate(true).SetEase(Ease.OutBack);
		this.LabelShuzi.transform.DOLocalMove(this.UIOriginalPositionLabelShuzi,0.4f).SetUpdate(true).SetEase(Ease.OutBack).OnComplete(delegate ()
		                                                                                                                     {
			this.LabelShuzi.transform.DOLocalMove(this.UIOriginalPositionLabelShuzi,timeDelay).SetUpdate(true).OnComplete(delegate (){
				this.transform.DOScale(Vector3.zero,0.10f).OnComplete(delegate () 
				                                                      {
					this.CloseUI();
				}).SetUpdate(true);
			});
		});
		
		//5.
		this.LabelShuzi.GetComponent<UILabel>().text = msg;
	}
	
	/// <summary>
	/// 停止本UI界面上的所有Aciton，并销毁对象
	/// </summary>
	public void StopAllAction()
	{
		this.SpriteLianjiditu.transform.DOKill();
		this.LabelShuzi.transform.DOKill();
		this.transform.DOKill();
		this.CloseUI();
	}
}
