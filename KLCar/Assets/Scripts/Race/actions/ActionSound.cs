using UnityEngine;
using System.Collections;
/// <summary>
/// 在当前对象上播放声音.
/// </summary>
public class ActionSound : ActionBase {
	/// <summary>
	/// T声音剪辑.
	/// </summary>
	public AudioClip sound;
////	public string soundPath;
//	/// <summary>
//	///是否循环播放.
//	/// </summary>
//	public bool loop;
	private AudioSource source;
	/// <summary>
	/// 持续时间根据声音的实际长度设置.
	/// </summary>
	public bool autoTime;
 	
	protected override void onStart ()
	{
		base.onStart ();
		if (autoTime) {
			time=sound.length;
		}
		AudioSource.PlayClipAtPoint (sound, transform.position);
	}
	protected override void onOver ()
	{
		base.onOver ();
		if (time != 0) {
//			source.Stop ();
		}
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionSound s = (ActionSound)cloneTo;
		s.sound = sound;
//		s.loop = loop;
		s.autoTime = autoTime;
	}
}
