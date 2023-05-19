using UnityEngine;
using System.Collections;
/// <summary>
///显示一个对话.
/// </summary>
public class ActionScreenTips : ActionBase {
	/// <summary>
	/// 是不是关闭屏幕提示.如果是,info将无效.
	/// </summary>
	public bool isClose;
	/// <summary>
	/// 需要显示的屏幕提示内容.
	/// </summary>
	public string infoOrImage;
	/// <summary>
	/// 当前指定的提示是不是一个图像提示.
	/// </summary>
	public bool isImage;
	internal override void onCopyTo (ActionBase cloneTo)
	{

	}
	protected override void onStart ()
	{
		if (isClose)
		{
			if(isImage)
			{
				RaceManager.Instance.hiddenImageTips(); 
			}else
			{
				RaceManager.Instance.hiddrenScreenTips();
			}
		} else
		{
			if(isImage)
			{
				RaceManager.Instance.showImageTips(infoOrImage);
			}else
			{
				RaceManager.Instance.showScreenTips(infoOrImage);
			}
		}
	}
}
