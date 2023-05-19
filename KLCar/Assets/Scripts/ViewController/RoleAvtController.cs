using UnityEngine;
using System.Collections;

public class RoleAvtController : MonoBehaviour
{
	Animator animator;
	
	public enum RoleAnimatorState:int
	{
		RAS_Ready=0,
		RAS_Hyk=1,
		RAS_Left=2,
		RAS_Right=3,
		RAS_DriftL=4,
		RAS_DriftR=5,
	}
	RoleAnimatorState curState;

	public RoleAnimatorState CurState
	{
		get
		{
			return curState;
		}
		set
		{
			curState = value;
			animator.SetInteger ("roleState", (int)curState);
		}
	}

	void Awake ()
	{
		animator = this.GetComponent<Animator> ();
	}

	void Statr ()
	{
		curState = (RoleAnimatorState)animator.GetInteger ("roleState");
	}

	/// <summary>
	/// Acts the turn left.
	/// 车辆左转
	/// </summary>
	public void ActTurnLeft ()
	{
		this.CurState = RoleAnimatorState.RAS_Left;
	}

	/// <summary>
	/// Acts the turn right.
	/// 车辆右转
	/// </summary>
	public void ActTurnRight ()
	{
		this.CurState = RoleAnimatorState.RAS_Right;
	}

	/// <summary>
	/// Acts the speed up.
	/// 车辆瞬间加速
	/// </summary>
	public void ActSpeedUp ()
	{
		this.CurState = RoleAnimatorState.RAS_Hyk;
	}

	/// <summary>
	/// Acts the drift left.
	/// 车辆左漂移
	/// </summary>
	public void ActDriftLeft ()
	{
		this.CurState = RoleAnimatorState.RAS_DriftR;
	}

	/// <summary>
	/// Acts the drift right.
	/// 车辆右漂移
	/// </summary>
	public void ActDriftRight ()
	{
		this.CurState = RoleAnimatorState.RAS_DriftL;
	}

	/// <summary>
	/// Stops the turn left.
	/// 车辆停止左转
	/// </summary>
	public void StopTurnLeft ()
	{
		if (this.curState == RoleAnimatorState.RAS_Left)
		{
			this.CurState = RoleAnimatorState.RAS_Ready;
		}
	}

	/// <summary>
	/// Stops the turn right.
	/// 车辆停止右转
	/// </summary>
	public void StopTurnRight ()
	{
		if (this.curState == RoleAnimatorState.RAS_Right)
		{
			this.CurState = RoleAnimatorState.RAS_Ready;
		}
	}

	/// <summary>
	/// Stops the speed up.
	/// 车辆停止瞬间加速
	/// </summary>
	public void StopSpeedUp ()
	{
		if (this.curState == RoleAnimatorState.RAS_Hyk)
		{
			this.CurState = RoleAnimatorState.RAS_Ready;
		}
	}

	/// <summary>
	/// Stops the drift left.
	/// 车辆停止向左漂移
	/// </summary>
	public void StopDriftLeft ()
	{
		if (this.curState == RoleAnimatorState.RAS_DriftR)
		{
			this.CurState = RoleAnimatorState.RAS_Ready;
		}
	}

	/// <summary>
	/// Stops the drift right.
	/// 车辆停止向右漂移
	/// </summary>
	public void StopDriftRight ()
	{
		if (this.curState == RoleAnimatorState.RAS_DriftL)
		{
			this.CurState = RoleAnimatorState.RAS_Ready;
		}
	}

	/// <summary>
	/// Triggers the hit f.
	/// 触发前方碰撞
	/// </summary>
	public void TriggerHitF ()
	{
		this.animator.SetTrigger ("t_hitF");
	}

	/// <summary>
	/// Triggers the hit b.
	/// 触发后方碰撞
	/// </summary>
	public void TriggerHitB ()
	{
		this.animator.SetTrigger ("t_hitB");
	}

	/// <summary>
	/// Triggers the hit l.
	/// 触发左方碰撞
	/// </summary>
	public void TriggerHitL ()
	{
		this.animator.SetTrigger ("t_hitL");
	}

	/// <summary>
	/// Triggers the hit r.
	/// 触发右方碰撞
	/// </summary>
	public void TriggerHitR ()
	{
		this.animator.SetTrigger ("t_hitR");
	}

	/// <summary>
	/// Triggers the look l.
	/// 触发向左回头看
	/// </summary>
	public void TriggerLookL ()
	{
		this.animator.SetTrigger ("t_lookL");
	}

	/// <summary>
	/// Triggers the look r.
	/// 触发向右回头看
	/// </summary>
	public void TriggerLookR ()
	{
		this.animator.SetTrigger ("t_lookR");
	}

	/// <summary>
	/// Triggers the attack.
	/// 触发投掷道具使用
	/// </summary>
	public void TriggerAttack ()
	{
		this.animator.SetTrigger ("t_attack");
	}

	/// <summary>
	/// Triggers the victory.
	/// 触发胜利
	/// </summary>
	public void TriggerVictory ()
	{
		this.animator.SetTrigger ("t_victory");
	}

	/// <summary>
	/// Triggers the failed.
	/// 触发失败
	/// </summary>
	public void TriggerFailed ()
	{
		this.animator.SetTrigger ("t_failed");
	}
	/// <summary>
	/// 播放跟另一个动画相同的动作.
	/// </summary>
	/// <param name="last">Last.</param>
	public void replayce(RoleAvtController last)
	{
		int state=last.animator.GetInteger ("roleState");
		Debug.Log ("播放动画:>>>>>"+state);
		animator.SetInteger ("roleState",state);
	}
}
