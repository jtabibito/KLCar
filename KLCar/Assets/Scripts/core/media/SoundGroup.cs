using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
	 * 声音的播放标签.里面包含了同一个标签下的所有声音的播放.<br/> 
	 * 操作播放标签类就可以对里面所有的声音进行统一的管理操作.<br/>
	 * @author Administrator
	 */
public class SoundGroup
{
	private float _voi = 1;
	private bool _mute = false;
	/**
		 * 当前组中播放的声音,是否在声音加载完时候才可以播放.通常背景声音可以一边加载一边播放. 
		 */		
	public bool loadOverToPlay;
	private Dictionary<SoundItem,SoundItem> allChannel = new Dictionary<SoundItem,SoundItem> ();
	/**
		 * 是否启用单通道模式.如果是,则在短时间内,同一个声音只会被播放一次. <br>
		 * <br>这里针对的是同一个声音.不同声音不受影响.<br>
		 * 0:表示不启用单声道模式.
		 * -1:表示当一个声音播放完毕后,才播放另一个声道.
		 * >0表示在指定毫秒内,不再重复播放. 推荐100
		 */		
	public float singleChangleDuration = 0f;
	private Hashtable singleChangleCatch = new Hashtable ();
//	private GameObject gameObject;
	public SoundGroup (string name)
	{
//			gameObject = new GameObject (name);
//			gameObject.transform.parent=SoundManager.soundPlayerObject.transform;
	}

	public void stopAll ()
	{
		foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
		{
			kv.Key.stop ();
		}
		allChannel.Clear ();
	}

//	public SoundItem play (string url, bool loop=false)
//	{
//		if (singleChangleDuration != 0)
//		{
//			SoundItem sd = (SoundItem)singleChangleCatch [url];
//			if (sd != null)
//			{
//				if (singleChangleDuration == -1 && !sd.isOver ())
//				{
//					return sd;
//				} else if (Time.time - sd.lastPlayTime < singleChangleDuration)
//				{
//					return sd;
//				}
//			}
//		}
//		SoundPlayer s = SoundManager.getSound (url);
//		SoundItem c = s.playSound (loop, loadOverToPlay);
//		if (mute)
//		{
//			c.mute=true;
//		}
//		addSoundItem (c);
//		if (singleChangleDuration != 0)
//		{
//			singleChangleCatch [url] = c;
//		}
//		if (_voi != 1)
//		{
//			c.updateVolume ();
//		}
//		return c;
//	}
	public SoundItem play(string url,Vector3 pos,bool loop=false)
	{
		GameObject obj = new GameObject ("SoundClip");
		obj.transform.position = pos;
		SoundItem s= play (url,loop,obj );
		if (s == null)
		{
			GameObject.DestroyObject (obj);
		} else
		{
			obj.transform.parent=s.player.playObject.transform;
		}
		return s;
	}
	/// <summary>
	/// 如果播放失败,比如限制了重复时间,返回null.
	/// </summary>
	/// <param name="url">URL.</param>
	/// <param name="loop">If set to <c>true</c> loop.</param>
	/// <param name="obj">Object.</param>
	public SoundItem play(string url,bool loop=false,GameObject obj=null)
	{
		if (url == null || url == "")
		{
			return null;
		}
		if (singleChangleDuration != 0)
		{
			SoundItem sd = (SoundItem)singleChangleCatch [url];
			if (sd != null)
			{
				if (singleChangleDuration == -1 && !sd.isOver ())
				{
					return null;
				} else if (Time.time - sd.lastPlayTime < singleChangleDuration)
				{
					return null;
				}
			}
		}
		SoundPlayer s = SoundManager.getSound (url);
		SoundItem c = s.playSound (loop, loadOverToPlay,obj);
		if (mute)
		{
			c.mute=true;
		}
		addSoundItem (c);
		if (singleChangleDuration != 0)
		{
			singleChangleCatch [url] = c;
		}
		if (_voi != 1)
		{
			c.updateVolume ();
		}
		return c;
	}
//		private TimelineLite lastChange;
	/**
		 * 以声音渐变缓冲的形式切换播放的声音. 会自动将原来的声音切换成新的声音.
		 * @param url 
		 * @param times
		 * @param tweenDuration 声音变小然后变大的总持续时间.
		 * @return 
		 * 
		 */		
	public void  playByVolumeTween (string url, int times=1, int tweenDuration=1000)
	{
//			var tl:TimelineLite=new TimelineLite(); 
//			var v:Number=volume;
//			 
//			var t1:TweenLite=new TweenLite(this,tweenDuration/2/1000,{volume:0,onComplete:changeURL,ease:Linear.easeNone,onCompleteParams:[url,times]});
//			var t2:TweenLite=new TweenLite(this,tweenDuration/2/1000,{volume:v,ease:Linear.easeNone});
//			tl.append(t1);
//			tl.append(t2);
			 
	}

	private void changeURL (String url, bool loop=false, bool colseOhter=true)
	{
		stopAll ();
		play (url, loop);
	}

	private void addSoundItem (SoundItem c)
	{
		allChannel [c] = c;
		c.group = this;
	}

	private void onPlayOver (Event e)
	{
			
	}
	internal void onSoundOver (SoundItem c)
	{
		allChannel.Remove (c);
		singleChangleCatch.Remove (c.player.url);
	}
	public float volume
	{
		get
		{
			return _voi;
		}
		set
		{
			if (value == _voi)
				return;
			_voi = value;
			foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
			{
				kv.Key.updateVolume ();
			}
		}
	}

	public bool mute
	{
		get
		{
			return _mute;
		}
		set
		{
			if(value!=_mute)
			{
				_mute=value;
				foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
				{
					kv.Key.mute=value;
				}
			}
		}
	}
}
 