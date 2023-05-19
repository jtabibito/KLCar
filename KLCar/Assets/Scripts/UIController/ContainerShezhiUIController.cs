using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 设置UI控制代码
/// 2015年6月2日15:33:39
/// </summary>
public partial class ContainerShezhiUIController : UIControllerBase
{

	// Use this for initialization
	void Start ()
	{
		InitButtonEvent ();
	
		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.25f).SetEase (Ease.OutBack);

		UIWidget rootWidget = GetComponent<UIWidget>();
		DOVirtual.Float(0.0f,1,0.25f,delegate(float value) {
			rootWidget.alpha = value;
		}).SetEase(Ease.Linear).SetUpdate(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void InitButtonEvent ()
	{
		this.ButtonGuanbi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));
		//背景音乐
		this.ButtonYinyuekai.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYinyuekai));
		this.ButtonYinyueguan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonYinyueguan));
		//音效
		this.ButtonShengyinkai.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengyinkai));
		this.ButtonShengyinguan.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShengyinguan));

		//根据数据库设置的值，设置是否显示
		if (MainState.Instance.playerInfo != null) {
			if (MainState.Instance.playerInfo.bgMusicMute == 1) {
				OnClickButtonYinyuekai ();			//切换到静音
			} else {
				OnClickButtonYinyueguan ();			//切换到开启
			}

			if (MainState.Instance.playerInfo.effectMute == 1) {
				OnClickButtonShengyinkai ();		//切换到静音
			} else {
				OnClickButtonShengyinguan ();		//切换到开启
			}
		}

		//关闭按钮特效
		Sequence mySeq = DOTween.Sequence ();
		mySeq.Append (this.ButtonGuanbi.transform.DOScale (new Vector3 (0.9f, 0.9f, 0.9f), 1).SetEase (Ease.Linear));
		mySeq.Append (this.ButtonGuanbi.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1).SetEase (Ease.OutElastic));
		mySeq.SetLoops (-1);
	}

	/// <summary>
	/// 返回----自我销毁
	/// </summary>
	void OnClickButtonFanhui ()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack);

	}

	/// <summary>
	/// 背景音乐开button被点击，切换到 音乐关状态
	/// 游戏状态应该保存到data数据层
	/// </summary>
	void OnClickButtonYinyuekai ()
	{
		NGUITools.SetActive (this.ButtonYinyuekai, false);
		NGUITools.SetActive (this.ButtonYinyueguan, true);
		SoundManager.bg.mute = true;
				
		if (MainState.Instance.playerInfo != null) {
			MainState.Instance.playerInfo.bgMusicMute = 1;
			MainState.Instance.SavePlayerData ();
		}
	}

	/// <summary>
	/// 背景音乐关button被点击，切换到 音乐开状态
	/// </summary>
	void OnClickButtonYinyueguan ()
	{
		NGUITools.SetActive (this.ButtonYinyuekai, true);
		NGUITools.SetActive (this.ButtonYinyueguan, false);
		SoundManager.bg.mute = false;

		if (MainState.Instance.playerInfo != null) {
			MainState.Instance.playerInfo.bgMusicMute = 0;
			MainState.Instance.SavePlayerData ();
		}
	}
	
	/// <summary>
	/// 音效开button被点击，切换到 音效关状态
	/// </summary>
	void OnClickButtonShengyinkai ()
	{
		NGUITools.SetActive (this.ButtonShengyinkai, false);
		NGUITools.SetActive (this.ButtonShengyinguan, true);
		SoundManager.effect.mute = true;

		if (MainState.Instance.playerInfo != null) {
			MainState.Instance.playerInfo.effectMute = 1;
			MainState.Instance.SavePlayerData ();
		}
	}
	
	/// <summary>
	/// 音效音乐关闭button被点击，切换到 音效开状态
	/// </summary>
	void OnClickButtonShengyinguan ()
	{
		NGUITools.SetActive (this.ButtonShengyinkai, true);
		NGUITools.SetActive (this.ButtonShengyinguan, false);
		SoundManager.effect.mute = false;

		if (MainState.Instance.playerInfo != null) {
			MainState.Instance.playerInfo.effectMute = 0;
			MainState.Instance.SavePlayerData ();
		}

	}
}
