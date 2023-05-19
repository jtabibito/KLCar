using UnityEngine;
using System.Collections;
/// <summary>
/// 冲刺技能效果.
/// duration表示冲刺的时长.
/// </summary>
public class JiaSu : SkillBase {
	
	protected override void onPlay()
	{
		base.onPlay ();
		carEngine.playFire(duration);
	}
}
