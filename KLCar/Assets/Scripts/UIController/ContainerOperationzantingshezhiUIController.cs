using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 暂停界面的UI事件处理代码
/// 2015-4-14 15:15:57
/// </summary>
public partial class ContainerOperationzantingshezhiUIController : UIControllerBase
{
		//private Transform	mTransformOperation = null;
		// Use this for initialization
		void Start ()
		{
				this.ButtonFanhui.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonFanhui));
				this.ButtonJixuyouxi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonJixuyouxi));
				this.ButtonChongxinkaishi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonChongxinkaishi));
				//this.mTransformOperation = null;
				UIControllerConst.UIPrefebPauseUIActive = true;

				// Find the existing UI Root
				UIRoot root = NGUITools.FindInParents<UIRoot> (this.gameObject);
				this.transform.localPosition = new Vector3 (0, root.activeHeight, 0);
				this.transform.DOLocalMove (Vector3.zero, 0.3f).SetEase (Ease.OutBack).SetUpdate (UpdateType.Normal, true);

				//调用暂停API
				if (RaceManager.Instance.RaceCounterInstance != null)
						RaceManager.Instance.RaceCounterInstance.doPauseGame ();
		}

		// Update is called once per frame
		void Update ()
		{

		}

		/// <summary>
		/// 返回到游戏准备界面  --- 销毁相应的界面对象再返回，加载指定的游戏场景，根据界面flag加载标志
		/// </summary>
		void OnClickButtonFanhui ()
		{	
				Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
				UIControllerConst.UIPrefebPauseUIActive = false;
				this.CloseUI ();
				//暂时用来离开比赛
				LogicManager.Instance.ActNewLogic<LogicLeaveRace> (null, null);
				//调用恢复API
				if (RaceManager.Instance.RaceCounterInstance != null)
						RaceManager.Instance.RaceCounterInstance.doResumeGame ();
		}
	
		/// <summary>
		///  继续游戏----需要根据游戏界面模式进行返回操作
		/// </summary>
		void OnClickButtonJixuyouxi ()
		{
				Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
//				//根据类型判断---或者单利类---或者
//				mTransformOperation = PanelMainUIController.Instance.PanelButtom.transform.FindChild (UIControllerConst.UIPrefebOperationjinsumoshi+"(Clone)");
//				if (mTransformOperation != null) {
//						ContainerOperationjinsumoshiUIController context = mTransformOperation.GetComponent<ContainerOperationjinsumoshiUIController> ();
//						context.IsPause = false;
//						context.ChangeUIButtonEnableState (true);
//				}
//
//				mTransformOperation = PanelMainUIController.Instance.PanelButtom.transform.FindChild (UIControllerConst.UIPrefebOperationpohuaisai+"(Clone)");
//				if (mTransformOperation != null) {
//					ContainerOperationpohuaisaiUIController context = mTransformOperation.GetComponent<ContainerOperationpohuaisaiUIController> ();
//					context.IsPause = false;
//					context.ChangeUIButtonEnableState (true);
//				}
//
//				mTransformOperation = PanelMainUIController.Instance.PanelButtom.transform.FindChild (UIControllerConst.UIPrefebOperationtiaozhansai+"(Clone)");
//				if (mTransformOperation != null) {
//					ContainerOperationtiaozhansaiUIController context = mTransformOperation.GetComponent<ContainerOperationtiaozhansaiUIController> ();
//					context.IsPause = false;
//					context.ChangeUIButtonEnableState (true);
//				}
//
//				mTransformOperation = PanelMainUIController.Instance.PanelButtom.transform.FindChild (UIControllerConst.UIPrefebOperationtiaozhansai+"(Clone)");
//				if (mTransformOperation != null) {
//					ContainerOperationwujinmoshiUIController context = mTransformOperation.GetComponent<ContainerOperationwujinmoshiUIController> ();
//					context.IsPause = false;
//					context.ChangeUIButtonEnableState (true);
//				}
				
				// Find the existing UI Root
				UIRoot root = NGUITools.FindInParents<UIRoot> (this.gameObject);
				
				this.transform.DOLocalMove (new Vector3 (0, root.activeHeight, 0), 0.3f).SetEase (Ease.InBack).SetUpdate (UpdateType.Normal, true).OnComplete (delegate () {
						UIControllerConst.UIPrefebPauseUIActive = false;
						this.CloseUI ();		//子节点内存泄露？？？ ，子节点未释放                                                                                               {
				});
			
//				UIControllerConst.UIPrefebPauseUIActive = false;
//				this.CloseUI ();		//子节点内存泄露？？？ ，子节点未释放
		}

		/// <summary>
		///  重新开始--------重新加载游戏场景就可以了
		/// </summary>
		void OnClickButtonChongxinkaishi ()
		{
				Debug.Log ("function:" + new System.Diagnostics.StackTrace (true).GetFrame (0).GetMethod ().Name);
				UIControllerConst.UIPrefebPauseUIActive = false;
				this.CloseUI ();

				if (RaceManager.Instance.RaceCounterInstance != null)
						RaceManager.Instance.RaceCounterInstance.doResumeGame ();
				LogicManager.Instance.ActNewLogic<LogicRestart> (null, null);
		}
}
