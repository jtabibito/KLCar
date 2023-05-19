using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 提示框，类似Android中的Toast 精简版
/// 2015年5月6日16:23:55
/// </summary>
public partial class ContainerToastUIController : UIControllerBase 
{
	private string mString = "";
	// Use this for initialization
	void Start () {
		this.LabelMesage.GetComponent<UILabel> ().text = "";
	}
	
	// Update is called once per frame
	void Update () {
		this.LabelMesage.GetComponent<UILabel> ().text = mString;
	}

	//修改需要显示的内容
	public void ShowMessage(string msg,float timeOut = 1.0f)
	{
		this.mString = msg;

		if (timeOut <= 0.0f)
				timeOut = 1.0f;
		Destroy (this.gameObject, timeOut);
	}
}
