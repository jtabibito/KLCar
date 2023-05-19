using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 头像选择
/// 2015年6月4日11:26:09
/// </summary>
public partial class ContainerTouxiangUIController : UIControllerBase 
{

	// Use this for initialization
	void Start () {
		this.ButtonGuanbi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonGuanbi));

		foreach(Transform child in this.ScrollView.transform)
		{
			UIEventListener.Get (child.gameObject).onPress = this.OnPressButtonTouxiang;											//记录按下和抬起
		}

		InitUIAnim ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitUIAnim ()
	{
		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.25f).SetEase (Ease.OutBack).SetUpdate(true);
	}

	/// <summary>
	/// 关闭
	/// </summary>
	void OnClickButtonGuanbi ()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack).SetUpdate(true);;
	}

	/// <summary>
	/// Raises the press button touxiang event.
	/// </summary>
	void OnPressButtonTouxiang(GameObject obj, bool state)
	{
		if(state==false)
		{
			int buttonSuffix = int.Parse (obj.name.Substring ("ButtonRoal".Length));

			//暂时只支持7个人物头像
			if(buttonSuffix>=1 && buttonSuffix<=7)
			{
				if(MainState.Instance.playerInfo!=null)
				{
					MainState.Instance.playerInfo.userRoleImgID = buttonSuffix;
				}
				else
				{
					Debug.LogWarning("Please check ,playerInfo is NULL");
				}
			}
			//点击后关闭本界面
			OnClickButtonGuanbi();
		}
	}
}
