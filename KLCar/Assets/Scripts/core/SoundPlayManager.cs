using UnityEngine;
using System.Collections;

public class SoundPlayManager{
	/**
	 * 在指定的对象上播放声音.默认会调用之前的声道.
	 */
	 public static AudioSource playSound(Transform parent,AudioClip clip,bool loop)
	{
		AudioSource[] sound=parent.GetComponents<AudioSource> ();
		AudioSource last = null;
		foreach (AudioSource s in sound) {
			if(s.clip==clip)
			{
				s.loop=loop;
				s.Play();
				return s;
			}
			last=s;
		};
		AudioSource ac=	parent.gameObject.AddComponent<AudioSource>();
		initSoundPlayer (ac);
		ac.clip = clip;
		ac.loop = loop;
		ac.Play ();
		return ac;
	}
	/**
	 * 自动获得声音播放器.
	 */
	public static AudioSource getSoundPlayer(Transform parent,Object sound)
	{
		if( sound ==null)
		{
			return null;
		}else if(sound is AudioClip)
		{
			AudioSource ac=	parent.gameObject.AddComponent<AudioSource>();
			initSoundPlayer(ac);
			ac.clip=(AudioClip)sound;
			return ac;
		}else if(sound is AudioSource)
		{
			return (AudioSource)sound;
		}else if(sound is GameObject)
		{
			GameObject g=(GameObject)sound;
			return g.GetComponent<AudioSource>();
		}else{
			throw new UnityException("错误的声音类型.");
		}
	}
	/**
	 * 关闭指定的声音.
	 */
	public static AudioSource stopSound(Transform parent,AudioClip clip)
	{
		AudioSource[] sound=parent.GetComponents<AudioSource> ();
		foreach (AudioSource s in sound) {
			if(s.clip==clip)
			{
				s.Stop();
				return s;
			}
		};
		return null;
	}
	/**
	 * 关闭指定对象上的所有声音.
	 */
	public static AudioSource stopSound(Transform parent)
	{
		AudioSource[] sound=parent.GetComponents<AudioSource> ();
		foreach (AudioSource s in sound) {
				s.Stop();
				return s;
		};
		return null; 
	}
	 static void initSoundPlayer(AudioSource ac)
	{
		ac.rolloffMode = AudioRolloffMode.Custom;
		ac.maxDistance = 50;
		ac.volume = 0.5f;
	}
}
