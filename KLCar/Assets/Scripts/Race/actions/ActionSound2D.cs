using UnityEngine;
using System.Collections;
/// <summary>
/// 在当前对象上播放声音.
/// </summary>
public class ActionSound2D : ActionBase {
	/// <summary>
	/// T声音剪辑.
	/// </summary>
	public string sound;
	/// <summary>
	/// 音效组的名词.
	/// </summary>
	public string group="effect";
	/// <summary>
	/// 是否循环播放.
	/// </summary>
	public bool loop;
	/// <summary>
	/// 持续时间根据声音的实际长度设置.
	/// </summary>
	public bool autoTime;
	/// <summary>
	/// 是否替换原来的组声音.
	/// </summary>
	public bool replace;
	private SoundItem it;
	protected override void onStart ()
	{
		if (group == null || group == "")
		{
			group = "effect";
		} 
		if (replace)
		{
			SoundManager.getSoundGroup (group).stopAll();
		}
		it=SoundManager.getSoundGroup (group).play (sound, loop);
		if(autoTime) {
//			if(loop)
//			{
//			}
			time=it.getSoundChanel().clip.length;
		}
		base.onStart ();
	}
	protected override void onOver ()
	{
		base.onOver ();
		if (time != 0) {
			it.stop();
		}
		it = null;
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{
//		ActionSound s = (ActionSound)cloneTo;
//		s.sound = sound;
//		s.autoTime = autoTime;
	}
}
