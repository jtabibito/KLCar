using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 任务item
/// 2015年5月28日10:48:51
/// </summary>
public partial class SpriteRichangrenwutiaoUIController : UIControllerBase
{

	/// SpriteJinxingzhong 进行中
	/// SpriteDianjilingqu 点击领取
	/// SpriteYiwancheng 已经完成
	/// SpriteJiangli  奖励文字图片
	/// LabelShuzi    奖励金币的数字
	/// LabelRenwumingzi 人物名称
	private string TaskId = "-1";

	public enum  UITaskType
	{
		EveryDayTask = 0,
		LifeTask = 1,
	}
	public UITaskType taskType = UITaskType.EveryDayTask;

	public enum  UITaskStatus
	{
		TaskInProgress = 0,
		TaskRewards = 1,
		TaskComplete = 2,
	}
	public UITaskStatus taskStatus = UITaskStatus.TaskInProgress;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 生涯任务不显示“奖励”和奖励的数字
	/// </summary>
	/// <returns><c>true</c>, if task type was set, <c>false</c> otherwise.</returns>
	public void SetTaskType(UITaskType type)
	{
		switch(type)
		{
		case UITaskType.EveryDayTask:
			NGUITools.SetActive(SpriteJiangli,true);
			NGUITools.SetActive(LabelShuzi,true);
			taskType = UITaskType.EveryDayTask;
			break;
		case UITaskType.LifeTask:
			NGUITools.SetActive(SpriteJiangli,false);
			NGUITools.SetActive(LabelShuzi,false);
			taskType = UITaskType.LifeTask;
			break;
		default:
			NGUITools.SetActive(SpriteJiangli,true);
			NGUITools.SetActive(LabelShuzi,true);
			taskType = UITaskType.EveryDayTask;
			break;
		}
	}

	/// <summary>
	/// 设置任务状态
	/// </summary>
	/// <param name="status">Status.</param>
	public void SetTaskStatus(UITaskStatus status)
	{
		NGUITools.SetActive(SpriteJinxingzhong,false);
		NGUITools.SetActive(SpriteDianjilingqu,false);
		NGUITools.SetActive(SpriteYiwancheng,false);

		switch(status)
		{
		case UITaskStatus.TaskInProgress:
			NGUITools.SetActive(SpriteJinxingzhong,true);
			taskStatus = UITaskStatus.TaskInProgress;
			break;
		case UITaskStatus.TaskRewards:
			NGUITools.SetActive(SpriteDianjilingqu,true);
			taskStatus = UITaskStatus.TaskRewards;

			this.SpriteDianjilingqu.transform.DOScale(1.1f, 0.75f).SetLoops(-1,LoopType.Yoyo);

			break;
		case UITaskStatus.TaskComplete:
			NGUITools.SetActive(SpriteYiwancheng,true);
			taskStatus = UITaskStatus.TaskComplete;
			this.SpriteYiwancheng.transform.localScale = new Vector3(8.0f,8.0f,3.0f);
			this.SpriteYiwancheng.transform.DOScale(Vector3.one,0.8f).SetEase(Ease.OutBounce);
			//this.GetComponent<UIButton>().SetState(UIButton.State.Disabled,true);
			break;
		default:
			NGUITools.SetActive(SpriteJinxingzhong,true);
			taskStatus = UITaskStatus.TaskInProgress;
			break;
		}
	}

	/// <summary>
	/// 设置任务名称
	/// </summary>
	/// <param name="name">Name.</param>
	public void SetTaskName(string name)
	{
		this.LabelRenwumingzi.GetComponent<UILabel>().text = name;
	}

	/// <summary>
	/// 设置任务描述
	/// </summary>
	/// <param name="name">Name.</param>
	public void SetTaskDescriptionName(string name)
	{
		this.LabelRenwumiaoshu.GetComponent<UILabel>().text = name;
	}

	/// <summary>
	/// 设置奖励的金币
	/// </summary>
	/// <param name="name">Name.</param>
	public void SetTaskRewardCoin(string name)
	{
		this.LabelShuzi.GetComponent<UILabel>().text = name;
	}

	/// <summary>
	/// 设置任务的进度
	/// </summary>
	/// <param name="name">Name.</param>
	public void SetTaskProgress(int current,int total)
	{
		if(current>total)
		{
			current = total;
		}

		this.LabelJindu.GetComponent<UILabel>().text = current.ToString() + "/" + total.ToString();
		//进度条图片
		this.SpriteJindutiao.GetComponent<UISprite>().fillAmount = (float)current/(float)total;
	}

	/// <summary>
	/// 设置任务Id
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void SetTaskId(string id)
	{
		if(id.Equals("-1")==true){
			Debug.LogError("id is -1,Please check it");
			return ;
		}
		TaskId = id;
	}

	/// <summary>
	/// 得到任务Id
	/// </summary>
	/// <returns>The task identifier.</returns>
	public string GetTaskId()
	{
		return TaskId;
	}



}
