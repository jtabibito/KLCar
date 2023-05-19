using UnityEngine;
using System.Collections;

public class LogicRaceDetermine :LogicBase {
	
	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();
		RaceCounter rc = (RaceCounter)logicPar ["raceCounter"];
		switch(rc.CurMode)
		{
		case RaceCounter.RaceMode.RM_Test:
			rc.curResult=RaceCounter.RaceResult.RR_Continue;
			break;
		case RaceCounter.RaceMode.RM_ArderSpeed:
			rc.curResult=GetArderSpeedModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_ArderInfinite:
			rc.curResult=GetArderInfiniteModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_ArderDestroy:
			rc.curResult=GetArderDestroyModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_ArderChallenge:
			rc.curResult=GetArderChallengeModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_Speed:
			rc.curResult=GetSpeedModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_Infinite:
			rc.curResult=GetInfiniteModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_Destroy:
			rc.curResult=GetDestroyModeResult(rc);
			break;
		case RaceCounter.RaceMode.RM_Challenge:
			rc.curResult=GetChallengeModeResult(rc);
			break;
		}

		this.FinishLogic (null);
	}

	/// <summary>
	/// 竞速模式
	/// 胜利:跑完指定圈数后在指定名次之前
	/// 失败:跑完指定圈数后在指定名次之后
	/// </summary>
	/// <returns>The speed mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetSpeedModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			int rank=rc.getUserRaceRanking();
			if(rank!=0 && rank<=rc.ModePar2)
			{
				return RaceCounter.RaceResult.RR_Victory;
			}
			else
			{
				return RaceCounter.RaceResult.RR_Lose;
			}
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	/// <summary>
	/// 休闲竞速模式
	/// 完成:跑完指定圈数
	/// </summary>
	/// <returns>The arder speed mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetArderSpeedModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			return RaceCounter.RaceResult.RR_Victory;
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	/// <summary>
	/// 无尽模式
	/// 胜利:燃油耗尽时跑完指定距离
	/// 失败:燃油耗尽时未跑完指定距离
	/// </summary>
	/// <returns>The infinite mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetInfiniteModeResult(RaceCounter rc)
	{
		if(rc.oil>0)
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
		else
		{
			if(rc.raceDistance>=rc.ModePar2)
			{
				return RaceCounter.RaceResult.RR_Victory;
			}
			else
			{
				return RaceCounter.RaceResult.RR_Lose;
			}
		}
	}


	/// <summary>
	/// 休闲无尽模式
	/// 完成:燃油耗尽
	/// </summary>
	/// <returns>The arder infinite mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetArderInfiniteModeResult(RaceCounter rc)
	{
		if(rc.oil>0)
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
		else
		{
			return RaceCounter.RaceResult.RR_Victory;
		}
	}

	/// <summary>
	/// 破坏模式
	/// 胜利:指定圈数中达到N分数
	/// 失败:指定圈数中未达到N分数
	/// </summary>
	/// <returns>The destroy mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetDestroyModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			if(rc.bounceScore>=rc.ModePar2)
			{
				return RaceCounter.RaceResult.RR_Victory;
			}
			else
			{
				return RaceCounter.RaceResult.RR_Lose;
			}
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	/// <summary>
	/// 休闲破坏模式
	/// 完成:跑完指定圈数
	/// </summary>
	/// <returns>The destroy mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetArderDestroyModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			return RaceCounter.RaceResult.RR_Victory;
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	/// <summary>
	/// 挑战模式
	/// 胜利:指定时间内跑完指定圈数
	/// 失败:指定时间内未跑完指定圈数
	/// </summary>
	/// <returns>The speed mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetChallengeModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			return RaceCounter.RaceResult.RR_Victory;
		}
		else if(Time.time-rc.beginTime>rc.ModePar2)
		{
			return RaceCounter.RaceResult.RR_Lose;
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	/// <summary>
	/// 休闲挑战模式
	/// 完成:跑完指定圈数
	/// </summary>
	/// <returns>The arder challenge mode result.</returns>
	/// <param name="rc">Rc.</param>
	RaceCounter.RaceResult GetArderChallengeModeResult(RaceCounter rc)
	{
		if(rc.roundNum>=rc.ModePar1)
		{
			return RaceCounter.RaceResult.RR_Victory;
		}
		else
		{
			return RaceCounter.RaceResult.RR_Continue;
		}
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}
}
