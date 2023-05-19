using UnityEngine;
using System.Collections;

/// <summary>
/// 用户的比赛数据.用于比赛和ui之间的交互.单例模式,任何比赛都共用这个实例.
/// </summary>
public class UserRaceInfo
{
		public static UserRaceInfo _instance;
		private CarEngine userCar;
		/// <summary>
		/// 所有人的比赛进度.0表示玩家进度.
		/// </summary>
		private float[] progress;
		/// <summary>
		/// G获得当前比赛信息的实例.任何比赛都共用这个实例.
		/// </summary>
		/// <value>The instance.</value>
		public static UserRaceInfo instance {
				get {
						if (_instance == null) {
								_instance = new UserRaceInfo ();
						}
						return _instance;
				}
		}

		public UserRaceInfo ()
		{
				
		}
		/// <summary>
		/// 每一场比赛之前,调用此函数重置数据.
		/// </summary>
		public void reSetValues (CarEngine car)
		{
			userCar = car;
		}
		/// <summary>
		/// 用户当前的速度.
		/// </summary>
		/// <value>The current speed.</value>
		public float currentSpeed {
				get {
						return userCar.currentSpeed;
				}
		}
		/// <summary>
		/// 当前剩余油量.
		/// </summary>
		/// <value>The current oil.</value>
		public float currentOil {
				get {
						return 0;
				}
		}
		/// <summary>
		/// 当前分数.关卡中的积分.
		/// </summary>
		/// <value>The current score.</value>
		public float currentScore {
				get {
						return 0;
				}
		}
		/// <summary>
		/// 本场比赛的最大圈数.
		/// </summary>
		/// <returns>1表示1圈.0表示无限.</returns>
		public int maxCylinderNumber {
				get {
						return 1;
				}
		}
		/// <summary>
		/// 所有人的比赛进度,玩家的进度索引为0,根据长度可以取得比赛人数.
		/// 玩家的名词根据排序获得.玩家的圈数可以根据进度和最大圈数取得.
		/// </summary>
		/// <value>是一个百分比.1表示100%.0表示刚开始没有进度.不会为null,至少有一个长度,保存玩家的进度.</value>
		public float[] allProgress {
				get {
						return new float[1];
				}
		}
		/// <summary>
		/// 用户此次比赛中一共获得的金币数量.
		/// </summary>
		/// <value>The current get glod.</value>
		public int currentGetGlod {
				get {
						return 0;
				}
		}
		//	/// <summary>
		//	/// 用户当前的名词.
		//	/// </summary>
		//	/// <value>The current ranking.</value>
		//	public float currentRanking
		//	{
		//		get
		//		{
		//			return 0;
		//		}
		//	}
		/// <summary>
		/// 暂停当前游戏.
		/// </summary>
		public void doPauseGame ()
		{
			Time.timeScale = 0;
		}
		/// <summary>
		/// 继续游戏,结束暂停.
		/// </summary>
		public void doResumeGame ()
		{
			Time.timeScale = 1;
		}
		/// <summary>
		/// 重新开始关卡
		/// </summary>
		public void doReStart ()
		{
			
		}
		/// <summary>
		/// 退出当前比赛.
		/// </summary>
		public void doEndCurrentGame()
		{
			
		}
		/// <summary>
		/// G当前游戏是不是被暂停了.
		/// </summary>
		/// <value><c>true</c> if is game pause; otherwise, <c>false</c>.</value>
		public bool isGamePause {
				get {
						return Time.timeScale==0;
				}
		}
		/// <summary>
		/// 控制车辆的方向,让车转弯.
		/// </summary>
		/// <param name="direction">需要转向的方向. 0表示暂停转向,小于0表示左转,大于0表示右转.(暂时用1表示强度.以后可能需要使用强度信息.)</param>
		public void doTurnCar (float direction)
		{
			userCar.doTurnCar(direction);
		}
		/// <summary>
		/// D释放一个技能(不同宠物也是不同的技能).可以传入任何一个GameObject作为技能对象.如果该GameObject拥有PlayAble组件.将直接启动PlayAble.否则将直接添加到车辆内部.
		/// </summary>
		/// <returns><c>true</c>技能释放成功. <c>false</c>通常技能不是释放失败.将来可能有特殊技能.</returns>
		/// <param name="skill">Skill.</param>
		public bool doUseSkill (GameObject skill)
		{
			return userCar.doUseSkill (skill);
		}

}
