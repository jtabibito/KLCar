using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
	 * 一个声音播放通道. 
	 * @author Administrator
	 * 
	 */
public class SoundItem
{
	private SoundPlayer _player;
	private SoundGroup _group;
	private float _weaken = 2000;
	private float _x = 0;
	private float _y = 0;
	public bool autoRemove;
	private float _volume = 1;
	private AudioSource chanel;
	private bool loop = false;
	private float _lastPlayTime;

	public SoundItem (int startTime, bool loop, SoundPlayer player,GameObject obj)
	{
		_lastPlayTime = Time.time;//getTimer();
//			if(times<=0)
//			{
//				times=int.MaxValue;
//			}
		this.loop = loop;
		this.chanel = player.getNextAudioSource (obj);
		this.chanel.loop = loop;
		this.chanel.Play ();
		_player = player;
		volume = 1;//chanel.soundTransform.volume;
		if (chanel == null)
		{
//				onPlayOver(new Event(Event.SOUND_COMPLETE));
//				Debug.Log(SoundItem+"声道用完"+player.url);
		} else
		{
//				chanel.addEventListener(Event.SOUND_COMPLETE,onPlayOver);
		}
	}

	public float  lastPlayTime
	{
		get
		{
			return _lastPlayTime;
		}
	}

	private void onPlayOver (Event e)
	{
//			times--;
		if (!loop)
		{
			destory ();
		}
	}

	public void stop ()
	{
		if (chanel != null)
		{
			chanel.Stop ();
		}
//		destory ();
	}

	protected void destory ()
	{
//		_player.onSoundOver (this);
		chanel = null;
		_player = null;
		_group = null;
	}

	public bool isOver ()
	{
		return chanel == null;
	}
	/**
		 * 设置音量. 声音的最终大小还受SoundPlayer和SoundGroup的音量影响.
		 * @return 
		 * 
		 */		
	public float  volume
	{
		get
		{
			return _volume;	
		}
		set
		{
			_volume = value;
			updateVolume ();
		}
	}
		
	public void updateVolume ()
	{
		if (chanel == null)
			return;
		if (_group != null)
		{
			chanel.volume = _volume * _group.volume;
		} else
		{
			chanel.volume = _volume;
		}
	}
	/**
		 * 这个声音的播放器. 
		 * @return 
		 * 
		 */		
	public SoundPlayer player
	{
		get
		{
			return _player;
		}
	}
	/**
		 * 属于的组,如果没有加入组则为null. 
		 * @return 
		 * 
		 */		
	internal SoundGroup group
	{
		get
		{

			return _group;
		}
		set
		{
			_group = value;
		}
	}
	 
	/**
		 * 声音距离屏幕的位置. 
		 * @return 
		 * 
		 */		
	public float  x
	{
		get
		{
			return _x;
		}
		set
		{
			_x = value;
		}
	}

	/**
		 * 声音距离屏幕的位置. 
		 * @return 
		 * 
		 */	
	public float  y
	{
		get
		{
			return _y;
		}
		set
		{
			_y = value;
		}
	}

	/**
		 * 衰减半径.音量从1衰减到0的半径. 距离屏幕的半径.
		 */
	public float weaken
	{
		get
		{
			return _weaken;
		}
		set
		{
			_weaken = value;
		}
	}

	public AudioSource getSoundChanel ()
	{
		return chanel;
	}
	public bool mute
	{
		get
		{
			if(isOver())
			{
				return false;
			}else
			{
				return chanel.mute;
			}
		}
		set
		{
			if(!isOver())
			{
				chanel.mute = value;
			}
		}
	}
}
 