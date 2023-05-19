using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
	 * 用于播放声音的管理类. 针对游戏开发对声音进行了详细的分类管理.
	 * <br/>可以方便的对某一类声音进行停止,调整音量等操作.
	 * <br/>游戏中的声音可能分为背景音乐,音效,UI音效.
	 * @author Administrator
	 * 
	 */
public class SoundManager
{
	internal static Hashtable allSound = new Hashtable ();
	private static Hashtable allGroupInfo = new Hashtable ();
	private static float _volume = 1;
	internal static GameObject soundPlayerObject;
	private static SoundGroup _bg;
	private static SoundGroup _effect;
	private static AudioListener listener;

	private static void init ()
	{
		GameObject obj = GameObject.Find ("SoundPlayerObject");
		if (obj != null)
		{
			GameObject.DestroyObject (obj);
		} 
		soundPlayerObject = new GameObject ("SoundPlayerObject");
		GameObject.DontDestroyOnLoad (soundPlayerObject);
		soundPlayerObject.AddComponent <SoundRetriever> ();
		if (listener == null)
		{
			resetListener ();
		}
	}
	/// <summary>
	/// 重新设置监听者的位置.附加到当前主摄像机上.
	/// </summary>
	public static void resetListener ()
	{
		Camera[] cs = Camera.allCameras;
		foreach (Camera c in cs)
		{
			GameObject g = c.gameObject;
			AudioListener o = g.GetComponent<AudioListener> ();
			if (o != null)
			{
				setListener(o);
			}
		}
		if (listener == null)
		{
			if(Camera.main)
			{
				setListener(Camera.main.gameObject);
			}else
			{
				setListener(soundPlayerObject);
			}
		}
	}
	/// <summary>
	/// 将监听者添加到指定的对象上.
	/// </summary>
	/// <param name="target">Target.</param>
	public static void setListener (GameObject target)
	{
		if (listener)
		{
			GameObject.Destroy (listener);
		}
		listener = target.AddComponent <AudioListener> ();
	}
	public static void setListener(AudioListener audio)
	{
		if (listener!=audio)
		{
			GameObject.Destroy (listener);
		}
		listener = audio;
	}
	public static SoundGroup bg
	{
		get
		{
			if (_bg == null)
			{
				_bg = getSoundGroup ("bg");
			}
			return _bg;
		}
	}

	public static SoundGroup effect
	{
		get
		{
			if (_effect == null)
			{
				_effect = getSoundGroup ("effect");
			}
			return _effect;
		}
	}

	public static bool onReleaseSound (string url)
	{
		SoundPlayer sound = (SoundPlayer)allSound [url];
		if (sound.soundCount <= 0)
		{
			allSound.Remove (url);
			return true;
		} else
		{
			return false;
		}
	}
	/**
		 * 播放指定的声音.并且可以指定一个控制组. 
		 * @param url
		 * @param times 0值播一次.-1无限循环
		 * @param group
		 * @return 返回一个声音元素.
		 */
	public static SoundItem play (string url, bool loop =false, string group="effect")
	{
		SoundGroup lb = getSoundGroup (group);
		return lb.play (url, loop);
	}
	/**
		 * 以声音渐变缓冲的形式切换播放的声音. 会自动将原来的声音切换成新的声音.
		 * @param url 
		 * @param times 0值播一次.-1无限循环
		 * @param tweenDuration 声音变小然后变大的总持续时间.
		 * @param group 要切换的组.
		 * @return 
		 *  
		 */		
	public static void playByVolumeTween (string url, int times=1, int tweenDuration=1000, string group=null)
	{
		SoundGroup lb = getSoundGroup (group);
		lb.playByVolumeTween (url, times, tweenDuration);
	}
	/**
		 * 取得指定声音的播放器. 
		 * @param url
		 * @return 
		 * 
		 */		
	public static SoundPlayer getSound (string url)
	{
		SoundPlayer s = (SoundPlayer)allSound [url];
		if (s == null)
		{
			s = new SoundPlayer (url);
			allSound [url] = s;
		}
		return s;
	}
	/**
		 * 取得声音的播放标签.可以对某一类声音进行操作. 
		 * @param name
		 * @return 
		 * 
		 */		
	public static SoundGroup getSoundGroup (string name="effect")
	{
		if (soundPlayerObject == null)
		{
			init ();
		}
		SoundGroup l = (SoundGroup)allGroupInfo [name];
		if (l == null)
		{
			l = new SoundGroup (name);
			allGroupInfo [name] = l;
		}
		return l;
	}
	/**
		 * 停止指定路径音乐文件的所有声道. 
		 * @param url
		 * 
		 */		
	public static void stopSound (string url)
	{
		SoundPlayer s = getSound (url);
		if (s != null)
		{
			s.stopAll ();
		}
	}
	/**
		 * 停止指定组中的所有正在播放的声音. 
		 * @param url
		 * 
		 */		
	public static void stopGroup (string name)
	{
		getSoundGroup (name).stopAll ();
	}
	/**
		 * 快捷设置某个组中所有声音的音量. 
		 * @param name
		 * @param value
		 * @param time 在多长时间内调整到指定的值.给予缓冲效果.
		 * 
		 */		
	public static void setGroupVolume (string name, float value=1, int time=1000)
	{
		getSoundGroup (name).volume = value;
	}
	/**
		 * 停止所有的声音. 
		 * 
		 */
	public static void stopAll ()
	{
		foreach (SoundPlayer p in allSound.Values)
		{
			p.stopAll ();
		}
	}
	/**
		 * 设置全局的声音大小. 
		 * @return 
		 * 
		 */		
	public static float  volume
	{
		get
		{
			return _volume;
		}
		set
		{
			_volume = value;
			AudioListener.volume = _volume;
		}
	}

	private static bool _mute;

	public static bool mute
	{
		get
		{
			return _mute;
		}	
		set
		{
			if (value != _mute)
			{
				_mute = value;
				foreach (SoundGroup g in allGroupInfo.Values)
				{
					g.mute = value;
				}
			}
		}
	}

	public static void gc ()
	{
		foreach (SoundPlayer p in allSound.Values)
		{
			p.gc ();
		}
	}
}
 
 