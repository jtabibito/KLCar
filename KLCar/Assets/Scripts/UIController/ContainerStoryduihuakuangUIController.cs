using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 故事模式控制UI代码
/// 2015年6月12日10:25:34
/// </summary>
public partial class ContainerStoryduihuakuangUIController : UIControllerBase 
{
	private static ContainerStoryduihuakuangUIController _instance = null;
	private static readonly object lockHelper = new object();
	private ContainerStoryduihuakuangUIController()
	{
		
	}
	
	public static ContainerStoryduihuakuangUIController Instance {
		get {
			if(_instance==null)
			{
				lock(lockHelper)
				{
					if(_instance==null)
					{
						GameObject uiBox = PanelMainUIController.Instance.AddUI(PanelMainUIController.UILayer.L_Top,"ContainerStoryduihuakuang");
						_instance = uiBox.AddMissingComponent<ContainerStoryduihuakuangUIController> ();
						//_instance=(MessageboxUIController)GameObject.FindObjectOfType(typeof(MessageboxUIController));
					}
				}
			}
			return _instance;
		}
	}

	private long count = 0;
	private long retainCount()
	{
		count++;
		return count;
	}

	private long releaseCount()
	{
		count--;
		return count;
	}

	//是否已经跳过剧情了----回调函数
	public delegate void OnSkipStory (Hashtable logicPar);
	public OnSkipStory onSkipStory = null;

	// Use this for initialization
	void Start () {
		this.ButtonTiaoguojuqing.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonTiaoguojuqing));
		this.ButtonTiaoguojuqing.transform.DOScale(0.85f,1.0f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo).SetUpdate(UpdateType.Normal,true);

		this.SpriteJiantou.transform.DOLocalMoveX(UIOriginalPositionSpriteJiantou.x-10,1.0f,true).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo).SetUpdate(UpdateType.Normal,true);

		NGUITools.SetActive(this.LabelJingqingqidai,false);

		NGUITools.SetActive(this.ContainerBackground,false); 
		NGUITools.SetActive(this.LabelXingming,false);
		NGUITools.SetActive(this.LabelWenzimiaoshu,false);
		NGUITools.SetActive(this.SpriteJiantou,false);
		NGUITools.SetActive(this.SpriteBanshenxiangzuo,false);
		NGUITools.SetActive(this.SpriteBanshenxiangyou,false);

		NGUITools.SetActive(this.Sprite,false); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 退出chat模式
	/// </summary>
	public void OnClickButtonTiaoguojuqing()
	{
		PanelMainUIController.Instance.ShowAllUIContainer();

		if(onSkipStory!=null)
			onSkipStory(null);

		DOVirtual.DelayedCall(0.5f,delegate {
			onSkipStory = null;
			this.CloseUI();
		},true);

	}

	public void Begin(OnSkipStory myCallBack)
	{
		onSkipStory = myCallBack;
		PanelMainUIController.Instance.ShowOneUIContainer(PanelMainUIController.UILayer.L_Top);
	}

	/// <summary>
	/// 修改tips内容
	/// </summary>
	/// <param name="msg">Message.</param>
	public void ShowMiddleTips(string msg)
	{
		this.retainCount();
		NGUITools.SetActive(this.LabelJingqingqidai,true);
		this.LabelJingqingqidai.GetComponent<UILabel>().text = msg;
	}

	/// <summary>
	/// 隐藏tips内容
	/// </summary>
	public void HideMiddleTips()
	{
		this.releaseCount();
		NGUITools.SetActive(this.LabelJingqingqidai,false);
		this.LabelJingqingqidai.GetComponent<UILabel>().text = "";
	}

	/// <summary>
	/// 显示一个图片Tips
	/// 图片的名称必须要在Atlas中
	/// </summary>
	/// <param name="">.</param>
	public void showImageTips(string name)
	{
		NGUITools.SetActive(this.Sprite,true); 
		this.Sprite.GetComponent<UISprite>().spriteName = name;
	}

	/// <summary>
	/// 隐藏图片Tips
	/// </summary>
	public void hiddenImageTips()
	{
		this.Sprite.GetComponent<UISprite>().spriteName = "";
		NGUITools.SetActive(this.Sprite,false); 
	}


	/// <summary>
	/// 开始对话
	/// </summary>
	/// <param name="roleName">Role name.</param>
	/// <param name="imageName">Image name.</param>
	/// <param name="position">If set to <c>true</c> position.</param>
	/// <param name="info">Info.</param>
	public void ShowChat(string roleName,string imageName,bool position,string info)
	{
		this.retainCount();

		if(imageName!=null && imageName.Length>0)
		{
			if(position==true)	//left
			{
				NGUITools.SetActive(this.SpriteBanshenxiangyou,false);
				NGUITools.SetActive(this.SpriteBanshenxiangzuo,true);
				this.SpriteBanshenxiangzuo.GetComponent<UISprite>().spriteName = imageName;
			}
			else
			{
				NGUITools.SetActive(this.SpriteBanshenxiangyou,true);
				NGUITools.SetActive(this.SpriteBanshenxiangzuo,false);
				this.SpriteBanshenxiangyou.GetComponent<UISprite>().spriteName = imageName;
			}
		}
		else
		{
			NGUITools.SetActive(this.SpriteBanshenxiangyou,false);
			NGUITools.SetActive(this.SpriteBanshenxiangzuo,false);
		}

		NGUITools.SetActive(this.ContainerBackground,true); 
		NGUITools.SetActive(this.SpriteJiantou,true);
		NGUITools.SetActive(this.LabelXingming,true);
		NGUITools.SetActive(this.LabelWenzimiaoshu,true);

		this.LabelXingming.GetComponent<UILabel>().text = roleName;
		this.LabelWenzimiaoshu.GetComponent<UILabel>().text = info;
	}
	
	/// <summary>
	/// 结束Chat
	/// </summary>
	public void EndChat()
	{
		PanelMainUIController.Instance.ShowAllUIContainer();

		DOVirtual.DelayedCall(0.5f,delegate {
			onSkipStory = null;
			this.CloseUI();
		},true);
	}
}
