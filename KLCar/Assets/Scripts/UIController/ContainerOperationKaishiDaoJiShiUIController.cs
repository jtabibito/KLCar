using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 倒计时 3 2 1 预制件
/// 2015-4-9 16:40:22
/// </summary>
public partial class ContainerOperationKaishiDaoJiShiUIController : UIControllerBase
{
		/// <summary>
		/// 此预制件累计显示的时间
		/// </summary>
		public  float mFloatAccumulateTime = 0.0f;
		private bool startTimeOutOver = false;

		void Start ()
		{
				Sequence mySeq = DOTween.Sequence ();
				mySeq.Append (this.transform.DOScale (3.0f,0.01f).SetEase(Ease.OutBack));
				mySeq.Append (this.transform.DOScale (1.0f,0.99f).SetEase(Ease.OutBack));
				//mySeq.Append (this.transform.DOScaleX (33,0.02f).SetEase(Ease.OutBack));
				mySeq.SetLoops (4);
		}

		// Update is called once per frame
		void Update ()
		{
				if (startTimeOutOver == false) {
						UpdateStartTimeOutNumberLabel (Time.deltaTime);
				}
		}
	
		void UpdateStartTimeOutNumberLabel (float deltaTime)
		{
				if (startTimeOutOver == true) {
						return;
				}

				if (this.mFloatAccumulateTime < 1.0f) {
						NGUITools.SetActive (this.SpriteNum3, true);
				} else if (this.mFloatAccumulateTime < 2.0f) {
						NGUITools.SetActive (this.SpriteNum3, false);
						NGUITools.SetActive (this.SpriteNum2, true);
				} else if (this.mFloatAccumulateTime < 3.0f) {
						NGUITools.SetActive (this.SpriteNum2, false);
						NGUITools.SetActive (this.SpriteNum1, true);
				} else if (this.mFloatAccumulateTime >= 3.0f && this.mFloatAccumulateTime <= 4.0f) {
						NGUITools.SetActive (this.SpriteNum1, false);
						NGUITools.SetActive (this.SpriteNumgo, true);
				} else if (this.mFloatAccumulateTime >= 4.0f) {
						NGUITools.SetActive (this.SpriteNumgo, false);
						this.startTimeOutOver = true;
						Destroy (this.gameObject);
				}
				
				this.mFloatAccumulateTime += deltaTime;
		}

}
