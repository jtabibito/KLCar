using UnityEngine;
using System.Collections;
/// <summary>
/// 对声音的全局控制.
/// </summary>
public class ActionSoundManager : ActionBase {
 
	/// <summary>
	/// 要控制的组的名词.不填表示所有组.
	/// </summary>
	public string group;
	/// <summary>
	/// 是否控制音量.
	/// </summary>
	public bool useVolume;
	public float volume;

	/// <summary>
	/// 是否控制静音.
	/// </summary>
	public bool useMute;
	public bool mute;

	/// <summary>
	/// 是否停止这个组或者所有声音.
	/// </summary>
	public bool stopAll;
	/// <summary>
	/// 如果指定了声音.则停止这个声音.
	/// </summary>
	public string stopSoundURL;
	protected override void onStart ()
	{
		base.onStart ();
		if (group == null || group == "")
		{//全局
			if(useVolume)
			{
				SoundManager.volume=volume;
			}
			if(useMute)
			{
				SoundManager.mute=mute;
			}
			if(stopAll)
			{
				SoundManager.stopAll();
			}
		} else
		{
			SoundGroup g= SoundManager.getSoundGroup(group);
			if(useVolume)
			{
				g.volume=volume;
			}
			if(useMute)
			{
				g.mute=mute;
			}
			if(stopAll)
			{
				g.stopAll();
			}
		}
		if (stopSoundURL != null && stopSoundURL != "")
		{
			SoundManager.stopSound(stopSoundURL);
		}
	}
	protected override void onOver ()
	{
 
	}
	 internal override void onCopyTo (ActionBase cloneTo)
	{ 
	}
}
