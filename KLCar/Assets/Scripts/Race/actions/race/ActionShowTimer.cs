using UnityEngine;
using System.Collections;
/// <summary>
/// 显示一个3秒倒计时.
/// </summary>
public class ActionShowTimer : ActionBase {

	/// <summary>
	/// autoTime==true时,不需要指定time.time的时间就是倒计时的时间.如果为false,表示time指定的时间后执行后面的代码.
	/// </summary>
	public bool autoTime = true;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionShowTimer t = (ActionShowTimer)cloneTo;
		t.autoTime = t;
	}
	protected override void onStart ()
	{
		//GameObject obj  = PrefabManager.Instance.GetUIPrefab(UIControllerConst.UIPrefebOperationKaishiDaoJiShi);
		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_PopupWindow,UIControllerConst.UIPrefebOperationKaishiDaoJiShi);
		if (autoTime) {
			time=3;//obj.GetComponent<ContainerOperationKaishiDaoJiShiUIController>().mFloatAccumulateTime;
		}
	}
} 
