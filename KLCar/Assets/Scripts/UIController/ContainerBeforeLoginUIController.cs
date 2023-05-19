using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 开机logo及其动画
/// 2015年6月8日10:22:04
/// </summary>
public partial class ContainerBeforeLoginUIController : UIControllerBase {

	private UISpriteAnimation myAnim = null;
	private float totalTime = 0.0f;

	// Use this for initialization
	void Start () {
//		TweenScale ts = this.comLogo.GetComponent<TweenScale> ();
//		ts.onFinished.Add (new EventDelegate (this.OnFinishedScaleAlpha));

//		myAnim = this.logo.AddMissingComponent<UISpriteAnimation>();
//		myAnim.framesPerSecond = 6;
//		myAnim.loop = false;
//
//		int count = this.logo.AddMissingComponent<UISprite>().atlas.spriteList.Count;
//		totalTime = count / myAnim.framesPerSecond;
//
//		DOVirtual.DelayedCall(totalTime*1.1f,delegate() {
//			PanelMainUIController.Instance.EnterLogin ();
//		},true);

		DOVirtual.DelayedCall(1.2f,delegate()
		{
			this.logo.transform.DOScale(Vector3.zero,1).OnComplete(delegate(){
				PanelMainUIController.Instance.EnterLogin ();
			});
		},true);

		DOTween.useSafeMode = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnFinishedTweenAlpha()
//	{
////		this.StartCoroutine ("WaitAndClose");
//	}
//
//	void OnFinishedScaleAlpha()
//	{
//		PanelMainUIController.Instance.EnterLogin ();
//	}
}
