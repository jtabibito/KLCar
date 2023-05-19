using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 选人界面-性别选择-昵称输入 此界面必须在联网状态才弹出
/// 2015-4-20 16:29:05
/// </summary>
public partial class ContainerLoginxuanrenUIController : UIControllerBase
{
		public GameObject LabelTiShi;
//		public enum SEX:int
//		{
//			girl=0,
//			boy=1,
//		}
//		
//		public enum ROLE:int
//		{
//			role1=0,
//			role2=1,
//			role1=2,
//			role2=3,
//			role1=4,
//		}

		/// <summary>
		/// 性别选择的结果--如果未选择，默认为男
		/// </summary>
		public string mStringSexResult = "boy";

		/// <summary>
		/// 角色选择的结果--如果未选择，默认为第1个
		/// </summary>
		public int mIntRoleResult = 0;
		
		//昵称相关变量
		private string mStringName;
		List<NickNameConfigData> nameList;
		List<NickNameConfigData> boyList = new List<NickNameConfigData> ();
		List<NickNameConfigData> girlList = new List<NickNameConfigData> ();
		
		///
		private int mIntRoleSelectCheckBoxGroup = 0;
		private int mIntSexSelectCheckBoxGroup = 0;
		private string ErrorTip = "请输入昵称或随机产生一个昵称，昵称不可为空";
	
		// Use this for initialization
		void Start ()
		{
				InitButtonEvent ();
				InitNickName ();
				InitHeadAndSex ();
		}
	
		// Update is called once per frame
		void Update ()
		{

		}
		
		void InitButtonEvent ()
		{
				this.ButtonDianjijinru.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonDianjijinru));	//点击进入游戏
				this.ButtonShaizi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonShaizi));				//点击随机产生名字
			
				this.ButtonHejiongyi.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSelectRole));
				this.ButtonXienaer.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSelectRole));
				this.ButtonWuxinsan.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSelectRole));
				this.ButtonWeijiasi.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSelectRole));
				this.ButtonDuhaitaowu.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSelectRole));
			
				//开始游戏的缩放动画
				Sequence mySeq = DOTween.Sequence ();
				mySeq.Append (this.ButtonDianjijinru.transform.DOScale (new Vector3 (0.75f, 0.75f, 0.75f), 1).SetEase (Ease.Linear));
				mySeq.Append (this.ButtonDianjijinru.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1).SetEase (Ease.OutElastic));
				mySeq.SetLoops (-1);

				//错误提示信息的缩放动画
				Sequence mySeq1 = DOTween.Sequence ();
				mySeq1.Append (this.LabelTiShi.transform.DOScale (new Vector3 (0.85f, 0.85f, 0.85f), 1).SetEase (Ease.Linear));
				mySeq1.Append (this.LabelTiShi.transform.DOScale (new Vector3 (1.0f, 1.0f, 1.0f), 1).SetEase (Ease.Linear));
				mySeq1.SetLoops (-1);

				this.LabelWanjiaming.GetComponent<UIInput> ().onSubmit.Add (new EventDelegate (this.OnSubmit));
				this.LabelWanjiaming.GetComponent<UIInput> ().onChange.Add (new EventDelegate (this.OnChange));
		
				this.ContainerXuanzenv.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSexRole));
				this.ContainerXuanzenan.GetComponent<UIToggle> ().onChange.Add (new EventDelegate (this.OnClickButtonSexRole));
		
				this.mIntRoleSelectCheckBoxGroup = this.ButtonHejiongyi.GetComponent<UIToggle> ().group;			//角色选择groupID
				this.mIntSexSelectCheckBoxGroup = this.ContainerXuanzenv.GetComponent<UIToggle> ().group;			//性别选择groupID
		}
	
		void InitNickName ()
		{
				Debug.Log("开始解析非法昵称....");
				//NickNameFilterConfigDa.GetConfigDatas<NickNameFilterConfigDa> ();
				Debug.Log("解析完成");
		
				this.nameList = NickNameConfigData.GetConfigDatas<NickNameConfigData> ();
				boyList.Clear ();
				girlList.Clear ();
				foreach (NickNameConfigData data in this.nameList) {
						if (data.sex.Equals ("0") == true)		//0表示女， 1表示男
								girlList.Add (data);
						else
								boyList.Add (data);
				}
				//首次为用户产生一个昵称
				OnClickButtonShaizi ();
		}

		void InitHeadAndSex ()
		{
				this.ContainerXuanzenan.GetComponent<UIToggle> ().value = true;
				this.ButtonHejiongyi.GetComponent<UIToggle> ().value = true;
		}

		void OnSubmit ()
		{

		}

		void OnChange ()
		{
				this.mStringName = this.LabelWanjiaming.GetComponent<UIInput> ().value;
				if (this.mStringName.Length > 0) {
						this.LabelTiShi.GetComponent<UILabel> ().text = "";
				} else {
						this.LabelTiShi.GetComponent<UILabel> ().text = ErrorTip;
				}
		}

	
		/// <summary>
		/// 点击进入游戏---场景切换或者弹出另外一个UI界面
		/// 统计角色，性别，名称
		/// </summary>
		void OnClickButtonDianjijinru ()
		{
				this.mStringName = this.LabelWanjiaming.GetComponent<UIInput> ().value;
				
				if (MainState.Instance.playerInfo != null && this.mStringName.Length > 0) {
						MainState.Instance.playerInfo.selectRoleSexFlag = 1;				//是否已经进入到了此界面，并点击了开始游戏
						MainState.Instance.playerInfo.userSex = mStringSexResult;			//玩家性别
						MainState.Instance.playerInfo.userRoleImgID = mIntRoleResult;		//玩家图片ID---注意不是赛车中司机角色
						MainState.Instance.playerInfo.nickname = this.mStringName;			//玩家的昵称
						
						//只有首次设置相同
						if(MainState.Instance.playerInfo.roleDatas!=null && MainState.Instance.playerInfo.roleDatas.Count>0)
						{
							MainState.Instance.playerInfo.roleDatas[0].id = mIntRoleResult.ToString();
							MainState.Instance.playerInfo.nowRoleId = mIntRoleResult.ToString();          //配置表格中角色模型的ID顺序必须和此UI界面人物顺序保存一致
						}

						//网络检测通信--昵称唯一，如果不唯一，将提示用户重新输入
						PanelMainUIController.Instance.EnterHall ();
				} else {
						//请重新输入昵称提示框
						this.LabelTiShi.GetComponent<UILabel> ().text = ErrorTip;
				}
		}

		/// <summary>
		/// 点击随机产生名字，并赋值到text上面
		/// </summary>
		void OnClickButtonShaizi ()
		{
				if (mStringSexResult.Equals ("girl") == true) {
						int size = girlList.Count;
						this.mStringName = girlList [Random.Range (0, size)].symbol + girlList [Random.Range (0, size)].name1 + girlList [Random.Range (0, size)].name2 + girlList [Random.Range (0, size)].name3;
				} else {
						int size = boyList.Count;
						this.mStringName = boyList [Random.Range (0, size)].symbol + boyList [Random.Range (0, size)].name1 + boyList [Random.Range (0, size)].name2 + boyList [Random.Range (0, size)].name3;
				}
				this.LabelWanjiaming.GetComponent<UIInput> ().value = mStringName;
				
				//清空错误提示信息
				this.LabelTiShi.GetComponent<UILabel> ().text = "";
		}

		/// <summary>
		/// 角色选择
		/// </summary>
		void OnClickButtonSelectRole ()
		{
				UIToggle toggle = UIToggle.GetActiveToggle (this.mIntRoleSelectCheckBoxGroup);
				if (toggle == null || (toggle != null && toggle.value == false)) {
						return;
				}

				if (toggle.name.Equals (this.ButtonHejiongyi.name)) {
						//Debug.Log (toggle.name);
						mIntRoleResult = 1;
				} else if (toggle.name.Equals (this.ButtonXienaer.name)) {
						//Debug.Log (toggle.name);
						mIntRoleResult = 2;
				} else if (toggle.name.Equals (this.ButtonWeijiasi.name)) {
						//Debug.Log (toggle.name);
						mIntRoleResult = 3;
				} else if (toggle.name.Equals (this.ButtonWuxinsan.name)) {
						//Debug.Log (toggle.name);
						mIntRoleResult = 4;
				} else if (toggle.name.Equals (this.ButtonDuhaitaowu.name)) {
						//Debug.Log (toggle.name);
						mIntRoleResult = 5;
				}
		}

		/// <summary>
		/// 性别选择
		/// </summary>
		void OnClickButtonSexRole ()
		{
				UIToggle toggle = UIToggle.GetActiveToggle (this.mIntSexSelectCheckBoxGroup);
				if (toggle == null || (toggle != null && toggle.value == false)) {
						return;
				}

				if (toggle.name.Equals (this.ContainerXuanzenv.name)) {
						//Debug.Log (toggle.name);
						mStringSexResult = "girl";
				} else if (toggle.name.Equals (this.ContainerXuanzenan.name)) {
						//Debug.Log (toggle.name);
						mStringSexResult = "boy";
				}
		}

}
