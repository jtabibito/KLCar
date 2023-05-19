using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

/// <summary>
/// 每日登陆UI控制代码----------奖励发出，这里只显示奖励之后的结果
/// 2015年6月8日16:10:48
/// </summary>
public partial class ContainerMeiridengluUIController : UIControllerBase
{
	private int currentDay = 0;

	private long yesterdayTicks = 0;
	
	private List<DailyLogInConfigData> dataList = null;
	private DailyLogInConfigData data = null;

	private List<int> daysTypeList = new List<int>();
	private List<int> daysValueList = new List<int>();
	private List<GameObject> daysList = new List<GameObject>();

	private bool configDataHasFlag = false;
	
	// Use this for initialization
	void Start () {
		this.ButtonYijianlingqu.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonYijianlingqu));
		this.ButtonYijianlingqu.transform.DOScale(0.90f,1.0f).SetUpdate(true).SetLoops(-1 ,LoopType.Yoyo);



		this.transform.localScale = Vector3.zero;
		this.transform.DOScale (Vector3.one, 0.1f).SetEase (Ease.OutBack).SetUpdate(true);

		InitConfigData();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void InitConfigData()
	{
		if(configDataHasFlag==false)
		{
			this.daysList.Clear();
			this.daysList.Add(this.ButtonDiyitian1);
			this.daysList.Add(this.ButtonDiyitian2);
			this.daysList.Add(this.ButtonDiyitian3);
			this.daysList.Add(this.ButtonDiyitian4);
			this.daysList.Add(this.ButtonDiyitian5);
			this.daysList.Add(this.ButtonDiyitian6);
			this.daysList.Add(this.ButtonDiyitian7);

			this.dataList = DailyLogInConfigData.GetConfigDatas<DailyLogInConfigData> ();
			if(this.dataList.Count>0)
				this.data = this.dataList[0];

			//奇葩的配置表，写成7行配置数据不就行了
			this.daysTypeList.Clear();
			this.daysTypeList.Add(this.data.day1type);
			this.daysTypeList.Add(this.data.day2type);
			this.daysTypeList.Add(this.data.day3type);
			this.daysTypeList.Add(this.data.day4type);
			this.daysTypeList.Add(this.data.day5type);
			this.daysTypeList.Add(this.data.day6type);
			this.daysTypeList.Add(this.data.day7type);

			this.daysValueList.Clear();
			this.daysValueList.Add(this.data.day1value);
			this.daysValueList.Add(this.data.day2value);
			this.daysValueList.Add(this.data.day3value);
			this.daysValueList.Add(this.data.day4value);
			this.daysValueList.Add(this.data.day5value);
			this.daysValueList.Add(this.data.day6value);
			this.daysValueList.Add(this.data.day7value);

			int index;
			int count = this.daysTypeList.Count;

			for(index=0; index<count; index++)
			{
				if(this.daysTypeList[index]==1)
				{
					NGUITools.SetActive(this.daysList[index].transform.FindChild("SpriteZuanshi").gameObject,false);
					NGUITools.SetActive(this.daysList[index].transform.FindChild("SpriteJinbi").gameObject,true);
				}
				else
				{					
					NGUITools.SetActive(this.daysList[index].transform.FindChild("SpriteZuanshi").gameObject,true);
					NGUITools.SetActive(this.daysList[index].transform.FindChild("SpriteJinbi").gameObject,false);
				}

				//this.ButtonDiyitian1.transform.FindChild("SpriteYilingqu").gameObject.SetActive(true);		//已经登陆默认全部显示
				NGUITools.SetActive(this.daysList[index].transform.FindChild("ButtonHuangdi").gameObject,false);
				this.daysList[index].transform.FindChild("LabelJianglishu").GetComponent<UILabel>().text = this.daysValueList[index].ToString();
			}

			configDataHasFlag = true;
		}

	}

	/// <summary>
	/// 关闭button事件
	/// </summary>
	void OnClickButtonYijianlingqu()
	{
		NGUITools.SetActive (this.ContainerBj, false);
		this.transform.DOScale (Vector3.zero, 0.25f).OnComplete (delegate () {
			this.CloseUI ();
		}).SetEase (Ease.InBack);
	}

	/// <summary>
	/// 当前签到的天数范围1~7
	/// </summary>
	/// <param name="days">Days.</param>
	public void SetDailyRewardsDays(int days)
	{
		if(days<=0 || days>7)
		{
			Debug.LogWarning("Please check this code,valid value is [1~7],but param is "+days);
			return;
		}

		if(this.daysList.Count<=0)
		{
			InitConfigData();
		}

		//黄色高亮图片
		if(this.daysList[days-1].transform.FindChild("ButtonHuangdi")!=null)
		{
			NGUITools.SetActive(this.daysList[days-1].transform.FindChild("ButtonHuangdi").gameObject,true);
		}

		//当天登陆的缩放动画
		if(this.daysList[days-1].transform.FindChild("SpriteYilingqu")!=null)
		{
			this.daysList[days-1].transform.FindChild("SpriteYilingqu").localScale = Vector3.zero;
			DOVirtual.DelayedCall(0.5f,delegate() {
				this.daysList[days-1].transform.FindChild("SpriteYilingqu").localScale = new Vector3(5,5,5);
				this.daysList[days-1].transform.FindChild("SpriteYilingqu").DOScale(Vector3.one,0.85f).SetEase(Ease.OutBounce).SetUpdate(true).OnComplete(delegate (){
					this.daysList[days-1].transform.FindChild("SpriteYilingqu").DOScale(0.85f,1.0f).SetLoops(-1,LoopType.Yoyo);
				});
			},true);
		}

		int index;
		for(index=days; index<7;index++)
		{
			if(this.daysList[index].transform.FindChild("SpriteYilingqu")!=null)
				NGUITools.SetActive(this.daysList[index].transform.FindChild("SpriteYilingqu").gameObject,false);
		}
	}
}
