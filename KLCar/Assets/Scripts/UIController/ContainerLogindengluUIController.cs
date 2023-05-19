using UnityEngine;
using System.Collections;

/// <summary>
/// 登陆界面UI 控制代码
/// 2015年4月21日11:17:16
/// </summary>
public partial class ContainerLogindengluUIController : UIControllerBase
{

		private bool  mBoolPlatformHaveLogin;

		// Use this for initialization
		void Start ()
		{
				this.ButtonKaishiyouxi.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonKaishiyouxi));
				this.ButtonQiehuanzhanghao.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonQiehuanzhanghao));
				this.ButtonPingtaidenglu.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonPingtaidenglu));

				//从数据层得到是否已经登陆了
				this.mBoolPlatformHaveLogin = false;
				this.SetPlatformLoginSwitch (this.mBoolPlatformHaveLogin);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		/// <summary>
		/// 开始游戏
		/// </summary>
		void OnClickButtonKaishiyouxi ()
		{
				Debug.Log ("OnClickButtonKaishiyouxi");
				LogicManager.Instance.ActNewLogic<LogicLoginServer> (null, this.OnLogicOver);
		}

		void OnLogicOver (Hashtable logicPar)
		{
				Debug.Log ("----登陆消息----");
				Debug.Log ("userId:" + MainState.Instance.playerInfo.userID);
				Debug.Log ("userName:" + MainState.Instance.playerInfo.userName);
				Debug.Log ("updateTime:" + DateTimeExtensions.DateTimeFromSeconds (MainState.Instance.playerInfo.updateTime).ToLocalTime ());
				Debug.Log ("---------------");

				this.CheckAudioEnable();

				//首次启动app，清空
				MainState.Instance.playerInfo.missionOfPreviousJuqing = "-1";
				MainState.Instance.playerInfo.missionOfPreviousRelaxation = "-1";

				//应该用网络进行判断
				if (MainState.Instance.playerInfo.selectRoleSexFlag == 0 && MainState.Instance.netSupport==true) {
						PanelMainUIController.Instance.CleanAllUI ();
						PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Buttom, "ContainerLoginxuanren");
				} else {
						PanelMainUIController.Instance.EnterHall ();
				}
		}

		/// <summary>
		/// 平台切换账号
		/// </summary>
		void OnClickButtonQiehuanzhanghao ()
		{
				Debug.Log ("OnClickButtonQiehuanzhanghao");
				this.mBoolPlatformHaveLogin = false;
				SetPlatformLoginSwitch (this.mBoolPlatformHaveLogin);
		}

		/// <summary>
		/// 平台登陆
		/// </summary>
		void OnClickButtonPingtaidenglu ()
		{
				Debug.Log ("OnClickButtonPingtaidenglu");
				this.mBoolPlatformHaveLogin = true;
				SetPlatformLoginSwitch (this.mBoolPlatformHaveLogin);
		}

		/// <summary>
		/// 平台登陆和 切换登陆按钮2个button，切换显示，同一时间只能显示一个
		/// </summary>
		/// <param name="platfromLoginEnable">平台登陆按钮是否使能</param>
		void SetPlatformLoginSwitch (bool platfromLoginEnable)
		{
				if (platfromLoginEnable) {
						this.ButtonPingtaidenglu.SetActive (false);
						this.ButtonQiehuanzhanghao.SetActive (true);
				} else {	
						this.ButtonPingtaidenglu.SetActive (true);
						this.ButtonQiehuanzhanghao.SetActive (false);
				}
		}

		/// <summary>
		/// 根据数据库底层判断是否开启音乐，默认是开启背景音乐和音频特效Effect
		/// </summary>
		void CheckAudioEnable()
		{
			//根据数据库设置的值，设置是否显示
			if (MainState.Instance.playerInfo != null) 
			{
				//bgMusicMute和effectMute 变量第一次是false值，所以音频默认是开启的
//				if (MainState.Instance.playerInfo.bgMusicMute == true) {
//					SoundManager.bg.mute = true;
//				} else {
//					SoundManager.bg.mute = false;
//				}
//				
//				if (MainState.Instance.playerInfo.effectMute == true) {
//					SoundManager.effect.mute = true;
//				} else {
//					SoundManager.effect.mute = false;
//				}

				SoundManager.bg.mute = (MainState.Instance.playerInfo.bgMusicMute==1);
				SoundManager.effect.mute = (MainState.Instance.playerInfo.effectMute==1);
			}
		}

}
