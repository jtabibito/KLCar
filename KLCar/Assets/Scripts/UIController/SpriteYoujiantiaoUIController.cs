using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 邮件UI中的单个邮件Item
/// 2015年6月2日20:09:00
/// </summary>
public partial class SpriteYoujiantiaoUIController : UIControllerBase 
{
	public enum  UIEmailType
	{
		UserEmail = 0,
		SystemEmail = 1,
	}
	public UIEmailType emailType = UIEmailType.UserEmail;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 邮件类型
	/// </summary>
	/// <returns><c>true</c>, if task type was set, <c>false</c> otherwise.</returns>
	public void SetEmailType(UIEmailType type)
	{
		NGUITools.SetActive(SpriteTouxiang1,false);
		NGUITools.SetActive(SpriteJiangpin,false);
		switch(type)
		{
		case UIEmailType.UserEmail:
			NGUITools.SetActive(SpriteJiangpin,true);
			emailType = UIEmailType.UserEmail;
			break;
		case UIEmailType.SystemEmail:
			NGUITools.SetActive(SpriteTouxiang1,true);
			emailType = UIEmailType.SystemEmail;
			break;
		default:
			NGUITools.SetActive(SpriteJiangpin,true);
			emailType = UIEmailType.UserEmail;
			break;
		}
	}

	/// <summary>
	/// 设置邮件标题
	/// </summary>
	/// <param name="title">Title.</param>
	public void SetEmailTitle(string title)
	{
		this.LabelRenwumingzi.GetComponent<UILabel>().text = title;
	}

	/// <summary>
	/// 设置邮件内容前缀，这里经过处理，只显示前几个字
	/// </summary>
	/// <param name="content">Content.</param>
	public void SetEmailPreContents(string content)
	{
		this.LabelRenwumiaoshu.GetComponent<UILabel>().text = content.Substring(0,content.Length<10?content.Length:10)+"......";
	}


}
