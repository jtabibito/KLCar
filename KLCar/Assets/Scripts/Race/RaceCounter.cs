using UnityEngine;
using System.Collections;

/// <summary>
/// Race counter.
/// 赛程数据记录
/// </summary>
public class RaceCounter
{
	/// <summary>
	/// 是否使用屏幕按键控制.
	/// </summary>
	public static bool useKeyInput = true;
	/// <summary>
	/// The items number.
	/// 四种道具的数量
	/// </summary>
	public int Feidanitem1Num
	{
		get
		{
			//导弹
			return MainState.Instance.playerInfo.Feidanitem1Num;
		}
		set
		{
			MainState.Instance.playerInfo.Feidanitem1Num=value;
			LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
		}
	}
	public int Hudunitem2Num
	{
		get
		{  
			//护盾
			return MainState.Instance.playerInfo.Hudunitem2Num;
		}
		set
		{
			MainState.Instance.playerInfo.Hudunitem2Num=value;
			LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
		}
	}
	public int Yinshenitem3Num
	{
		get
		{
			//隐身
			return MainState.Instance.playerInfo.Yinshenitem3Num;
		}
		set
		{
			MainState.Instance.playerInfo.Yinshenitem3Num=value;
			LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
		}
	}
	public int Jiasuitem4Num
	{
		get
		{
			//加速
			return MainState.Instance.playerInfo.Jiasuitem4Num;
		}
		set
		{
			MainState.Instance.playerInfo.Jiasuitem4Num=value;
			LogicManager.Instance.ActNewLogic<LogicStoreData>(null,null);
		}
	}
	
	public enum RaceResult
	{
		RR_Continue,
		RR_Victory,
		RR_Lose,
	}

	/// <summary>
	/// The current result.
	/// 比赛结果
	/// </summary>
	public RaceResult curResult = RaceResult.RR_Continue;


	public enum RaceMode:int
	{
		RM_Test=0,
		RM_ArderSpeed=1,//休闲竞速
		RM_ArderInfinite=2,//休闲无尽
		RM_ArderDestroy=3,//休闲破坏
		RM_ArderChallenge=4,//休闲挑战
		RM_Speed=5,//竞速
		RM_Infinite=6,//无尽
		RM_Destroy=7,//破坏
		RM_Challenge=8,//挑战
	}

	RaceMode curMode = RaceMode.RM_Test;

	/// <summary>
	/// Gets the current mode.
	/// 当前比赛模式
	/// </summary>
	/// <value>The current mode.</value>
	public RaceMode CurMode
	{
		get
		{
			return curMode;
		}
	}

	int modePar1;

	/// <summary>
	/// Gets the mode par1.
	/// 比赛模式参数1
	/// </summary>
	/// <value>The mode par1.</value>
	public int ModePar1
	{
		get
		{
			return modePar1;
		}
	}

	int modePar2;

	/// <summary>
	/// Gets the mode par2.
	/// 比赛模式参数2
	/// </summary>
	/// <value>The mode par2.</value>
	public int ModePar2
	{
		get
		{
			return modePar2;
		}
	}

	int modePar3;

	/// <summary>
	/// Gets the mode par3.
	/// 比赛模式参数3
	/// </summary>
	/// <value>The mode par3.</value>
	public int ModePar3
	{
		get
		{
			return modePar3;
		}
	}

	/// <summary>
	/// 设定比赛模式
	/// </summary>
	/// <param name="rm">Rm.</param>
	/// <param name="modePar1">Mode par1.</param>
	/// <param name="modePar2">Mode par2.</param>
	/// <param name="modePar3">Mode par3.</param>
	public void SetRaceMode (RaceMode rm, int modePar1, int modePar2, int modePar3)
	{
		this.curMode = rm;
		this.modePar1 = modePar1;
		this.modePar2 = modePar2;
		this.modePar3 = modePar3;
		switch(curMode)
		{
		case RaceMode.RM_ArderSpeed:
		case RaceMode.RM_ArderDestroy:
		case RaceMode.RM_ArderChallenge:
		case RaceMode.RM_Speed:
		case RaceMode.RM_Destroy:
		case RaceMode.RM_Challenge:
			this.maxCylinderNumber=this.modePar1;
			break;
		}
	}

	public int beginWaypoint;//开始路点
	public float beginTime;//开始时间
//		public int roundNum = 0;//圈数
	//public int gainGoldNum = 0;//获取金币数量
//		public int gainItemNum = 0;//获取道具数量
//		public int bounceScore = 0;//撞击分数
//		public int oil = 0;//燃油
//		public int raceDistance = 0;//总行程数
	private CarEngine userCar;

 
	
 
	/// <summary>
	/// 每一场比赛之前,调用此函数重置数据.
	/// </summary>
	public void reSetValues (CarEngine car)
	{
		userCar = car;
		beginTime = Time.time;
	}
	/// <summary>
	/// 总距离.
	/// </summary>
	/// <value>The race distance.</value>
	public int raceDistance
	{
		get
		{
			return (int)(userCar.getCurrentProgress () * RaceManager.Instance.wayPointNumber);
		}
	}
	/// <summary>
	/// 用户当前的圈数.
	/// </summary>
	/// <returns>The round number.</returns>
	public int roundNum
	{
		get
		{
			return userCar.currentRoad;
		}
	}
	/// <summary>
	/// 玩家获得的道具数量.
	/// </summary>
	/// <returns>The gain item.</returns>
	public int currentGainItem
	{
		get
		{
			return userCar.currentGainItem;
		}
	}
	/// <summary>
	/// 用户当前的速度.
	/// </summary>
	/// <value>The current speed.</value>
	public float currentSpeed
	{
		get
		{
			return userCar.currentSpeed;
		}
	}
	/// <summary>
	/// 当前剩余油量.
	/// </summary>
	/// <value>The current oil.</value>
	public float oil
	{
		get
		{
			return userCar.currentOil;
		}
	}
	public float maxOil
	{
		get
		{
			return userCar.maxOil;
		}
	}

	/// <summary>
	/// G碰撞分数.
	/// </summary>
	/// <value>The bounce score.</value>
	public int bounceScore
	{
		get
		{
			return userCar.currentScore;
		}
	}
	/// <summary>
	/// 当前分数.关卡中的积分.
	/// </summary>
	/// <value>The current score.</value>
	public float currentScore
	{
		get
		{
			return userCar.currentScore;
		}
	}
	
	/// <summary>
	/// 本场比赛的最大圈数.
	/// </summary>
	/// <returns>1表示1圈.0表示无限.</returns>
	public int maxCylinderNumber=0;

	/// <summary>
	/// 所有人的比赛进度,玩家的进度索引为0,根据长度可以取得比赛人数.
	/// 玩家的名词根据排序获得.玩家的圈数可以根据进度和最大圈数取得.
	/// </summary>
	/// <value>是一个百分比.1表示100%.0表示刚开始没有进度.不会为null,至少有一个长度,保存玩家的进度.</value>
	public float[] allProgress
	{
		get
		{
			return new float[1];
		}
	}
	/// <summary>
	/// 用户此次比赛中一共获得的金币数量.
	/// </summary>
	/// <value>The current get glod.</value>
	public int gainGoldNum
	{
		get
		{
			if (userCar == null)
			{
				return 0;
			} else
			{
				return userCar.currentGetGlod;
			}
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
	public void doEndCurrentGame ()
	{
			
	}
	/// <summary>
	/// G当前游戏是不是被暂停了.
	/// </summary>
	/// <value><c>true</c> if is game pause; otherwise, <c>false</c>.</value>
	public bool isGamePause
	{
		get
		{
			return Time.timeScale == 0;
		}
	}
	/// <summary>
	/// 控制车辆的方向,让车转弯.
	/// </summary>
	/// <param name="direction">需要转向的方向. 0表示暂停转向,小于0表示左转,大于0表示右转.(暂时用1表示强度.以后可能需要使用强度信息.)</param>
	public void doTurnCar (float direction)
	{
		if (MainState.Instance.playerInfo.nowInputName.Equals (UIControllerConst.InputGravity))
		{
			return;
		}
		userCar.doTurnCar (direction);
			 
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

	public float getCarRound (int index)
	{
		return RaceManager.Instance.getCarRound (index);
	}
	/// <summary>
	/// 取得当前比赛车辆的数量.
	/// </summary>
	/// <returns>The race car number.</returns>
	public int getRaceCarNum ()
	{
		return RaceManager.Instance.getRaceCarNum ();
	}
	/// <summary>
	/// 取得用户的排名.
	/// </summary>
	/// <returns>1表示第一名.0表示错误或者没有名次(暂时不会出现).</returns>
	public int getUserRaceRanking ()
	{
		return RaceManager.Instance.getRaceRanking (0);
	}

	public bool overRace = false;
}
