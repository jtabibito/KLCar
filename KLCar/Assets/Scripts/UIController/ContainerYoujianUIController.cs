using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/// <summary>
/// 邮件主要容器UI控制代码
/// 2015年6月1日10:04:05
/// </summary>
public partial class ContainerYoujianUIController : UIControllerBase
{
	private List<GameObject>   userEmailItemList= new List<GameObject>();
	private List<GameObject>  systemEmailItemList = new List<GameObject>();
	
	private GameObject userEmailContainer = null;
	private GameObject systemEmailContainer = null;

	// Use this for initialization
	void Start () {
		InitButtonEvent();
		InitUIAnim();
		InitEmailItem();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitButtonEvent()
	{
		this.ButtonGuanbi.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonGuanbi));			//返回
		
		Transform  trans =  this.Container1Lingqu.transform.FindChild("ButtonYijianlingqu");							//一键领取
		if(trans!=null)
		{
			trans.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonYijianlingqu));	
		}
	}

	void InitUIAnim()
	{
		this.transform.localScale = Vector3.zero;
		this.transform.DOScale(Vector3.one,0.25f).SetEase(Ease.OutBack);
	}

	void InitEmailItem()
	{
		//0. Init Task Item Container
		this.userEmailContainer = this.Container1Lingqu.transform.FindChild("ScrollView/Grid").gameObject;
		this.systemEmailContainer = this.Container2Xitong.transform.FindChild("ScrollView/Grid").gameObject;

//		//1. Clear List
		this.userEmailItemList.Clear();
		this.systemEmailItemList.Clear();
//		
//		//		Debug.Log(MainState.Instance.playerInfo.missionOfRichang.Count);
//		//		Debug.Log(MainState.Instance.playerInfo.missionOfChengjiu.Count);
//		
//		//2. 暂时确定是5个日常任务  
//		int i = 0;
//		for(i=0;i<=5;i++)
//		{
//			GameObject item = NGUITools.AddChild (this.everyDayTaskContainer, PrefabManager.Instance.GetUIPrefab ("SpriteRichangrenwutiao"));
//			item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ().SetTaskType(SpriteRichangrenwutiaoUIController.UITaskType.EveryDayTask);
//			
//			item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ().SetTaskStatus(SpriteRichangrenwutiaoUIController.UITaskStatus.TaskComplete);
//			
//			this.everyDayTaskItemList.Add(item);
//		}
//		
//		//3. 成就任务--生涯任务
//		for(i=0;i<=5;i++)
//		{
//			GameObject item = NGUITools.AddChild (this.lifeTaskContainer, PrefabManager.Instance.GetUIPrefab ("SpriteRichangrenwutiao"));
//			item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ().SetTaskType(SpriteRichangrenwutiaoUIController.UITaskType.LifeTask);
//			item.AddMissingComponent<SpriteRichangrenwutiaoUIController> ().SetTaskStatus(SpriteRichangrenwutiaoUIController.UITaskStatus.TaskInProgress);
//			this.lifeTaskItemList.Add(item);
//		}
	}

	/// <summary>
	/// 关闭
	/// </summary>
	void OnClickButtonGuanbi()
	{
		NGUITools.SetActive(this.ContainerBj,false);
		this.transform.DOScale(Vector3.zero,0.25f).OnComplete(delegate (){
			this.CloseUI();
		}).SetEase(Ease.InBack);
	}

	/// <summary>
	/// 一键领取
	/// </summary>
	void OnClickButtonYijianlingqu()
	{

	}
}
