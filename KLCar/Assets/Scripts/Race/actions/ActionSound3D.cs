using UnityEngine;
using System.Collections;
/// <summary>
/// 在当前对象上播放声音.
/// </summary>
public class ActionSound3D : ActionBase {
	/// <summary>
	/// T声音剪辑.
	/// </summary>
	public string sound;

	public string group="effect";
	/// <summary>
	/// 如果设置了位置,则为全局坐标位置下的声音.
	/// </summary>
	public Vector3 globalPos; 
	/// <summary>
	/// 是否循环播放.
	/// </summary>
	public bool loop;
	/// <summary>
	/// 持续时间根据声音的实际长度设置.
	/// </summary>
	public bool autoTime;

	private SoundItem it;
	protected override void onStart ()
	{
		if (group == null || group == "")
		{
			group = "effect";
		}
		if (globalPos != Vector3.zero)
		{
			it=SoundManager.getSoundGroup (group).play (sound,globalPos, loop);
		} else
		{
			it=SoundManager.getSoundGroup (group).play (sound, loop,gameObject);
		}
		if(autoTime) {
//			if(loop)
//			{
//			}
			time=it.getSoundChanel().clip.length;
		}
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
////		s.loop = loop;
//		s.autoTime = autoTime;
	}
}
