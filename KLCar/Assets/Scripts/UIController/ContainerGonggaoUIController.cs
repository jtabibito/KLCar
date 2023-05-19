using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/// <summary>
/// 公告UI
/// 2015年6月9日09:26:17
/// </summary>
public partial class ContainerGonggaoUIController : UIControllerBase 
{

	// Use this for initialization
	void Start () {
		this.ButtonGuanbi.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonYijianlingqu));

		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.25f).SetEase (Ease.OutBack);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 关闭button事件
	/// </summary>
	void OnClickButtonYijianlingqu()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack);
	}

	/// <summary>
	/// 更新公告内容-----------内容从数据层获得
	/// </summary>
	public void UpdateAnnouncementLabel(string msg)
	{
		this.LabelGuanyu.GetComponent<UILabel>().text = msg;
	}
}
