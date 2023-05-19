using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// "本功能未开发完成提示框"
/// 2015年5月4日19:19:26
/// </summary>
public partial class ContainerTankuangUIController : UIControllerBase {

	// Use this for initialization
	void Start () {
		this.ButtonGuanbi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonGuanbi));

		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.25f).SetEase (Ease.OutBack);

		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.ButtonGuanbi.transform.DOScale (new Vector3 (0.65f, 0.65f, 0.65f), 1).SetEase(Ease.Linear));
		mySeq.Append (this.ButtonGuanbi.transform.DOScale(new Vector3 (1.0f, 1.0f, 1.0f),1).SetEase(Ease.OutElastic));
		mySeq.SetLoops(-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 关闭当前对话框
	/// </summary>
	void OnClickButtonGuanbi()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack);
	}
}
