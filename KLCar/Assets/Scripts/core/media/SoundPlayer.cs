using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
	 * 一个声音的播放器.只能播放一个声音文件. 
	 * @author Administrator
	 * 
	 */	
public class SoundPlayer
{
	internal int soundCount = 0;
	private bool _loadOver;
	private string urlStr;
	private bool isError = false;
	/**
	* 音量 
	 */		
	private float _voi = 1;
	private Dictionary<SoundItem,SoundItem> allChannel = new Dictionary<SoundItem,SoundItem> ();
//	private List<AudioSource> usedPool;
	private List<AudioSource> unUsedPool;
	private AudioClip clip;
	internal GameObject playObject;
	/**
	 * 音量控制. 
	 */
	public SoundPlayer (string url)
	{
//		usedPool=new List<AudioSource>();
		unUsedPool = new List<AudioSource> ();
		urlStr = url;
		clip = ResourceManager.Load<AudioClip> (url);
		if (clip == null)
		{
			Debug.LogError ("not find sound file:" + url);
		}
		playObject = new GameObject (url);
		playObject.transform.parent = SoundManager.soundPlayerObject.transform;
//		URLRequest stream = new URLRequest (ResourceManager.getVersionURL (url));
//		super (stream);
//		addEventListener (Event.COMPLETE, onOver);
//		addEventListener (IOErrorEvent.IO_ERROR, onError);
	}

//	private void onError (IOErrorEvent e)
//	{
//		isError = true;
////			Log.error(SoundPlayer,"声音资源加载失败，找不到指定的声音!"+urlStr);
//	}

	private void onOver (Event e)
	{
		_loadOver = true;
	}

	public string url
	{
		get
		{
			return urlStr;
		}
	}
	/**
		 * 停止. 
		 * 
		 */		
	public void stopAll ()
	{
		foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
		{
			kv.Key.stop ();
		}
		allChannel.Clear ();
	}
	/**
		 * 播放指定次数的声音. 
		 * @param times
		 * @param onLoadOver 是否如果声音没有加载完,就不要播放.如果times=0则忽略这个属性.
		 * @return 
		 * 
		 */		
	public SoundItem playSound (bool loop=false, bool onLoadOver=true,GameObject obj=null)
	{
		if (!loop && onLoadOver && !loadOver)
		{
			return null;
		} else
		{
			SoundItem s = new SoundItem (0, loop, this, obj);
			if (volume != 1)
			{
				s.volume = volume;
			}
				 
			allChannel [s] = s;
//			s.addEventListener (Event.SOUND_COMPLETE, onSoundOver);
			soundCount++;
			return s;
		}
	}

//	public SoundItem playSound (GameObject obj, bool loop=false)
//	{
//		if (!loop && !loadOver)
//		{
//			return null;
//		} else
//		{
//			SoundItem s = new SoundItem (0, loop, this, obj);
//			if (volume != 1)
//			{
//				s.volume = volume;
//			}
//			allChannel [s] = s;
//			soundCount++;
//			return s;
//		}
//	}

	internal void onSoundOver (SoundItem c,bool remove=true)
	{

		soundCount--;
		GameObject o=c.getSoundChanel ().gameObject;
		if (o == playObject)
		{
			unUsedPool.Add (c.getSoundChanel ());
			c.getSoundChanel ().enabled = false;
		}else if(o.name=="SoundClip")
		{
			GameObject.DestroyObject(o);
		}else
		{
			GameObject.Destroy(c.getSoundChanel());
		}
		if (c.group != null)
		{
			c.group.onSoundOver(c);
		}
		if (remove)
		{
			allChannel.Remove(c);
		}
	}
	/**
		 * 设置音乐的总体音量. 
		 * @return 
		 * 
		 */		
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
			float off = value / _voi;
			foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
			{
				kv.Key.volume *= off;
			}
			_voi = value;
		}
	}
 
	public bool loadOver
	{
		get
		{
			return _loadOver;
		}
	}

//	public AudioSource play (float startTime =0, bool loop=false)//, SoundTransform sndTransform=null)
//	{
//		if (isError)
//		{
//			return null;
//		} else
//		{
//			AudioSource c=getNextAudioSource();
//			c.loop=loop;
//			c.Play();
//			return c;
//		}
//	}

	internal AudioSource getNextAudioSource (GameObject obj)
	{
		AudioSource c;
		if (obj)
		{
			c = obj.AddComponent <AudioSource> ();
			c.clip = clip;
			c.volume = volume;
			c.playOnAwake = false;
			c.minDistance=0;
			c.maxDistance=30;
			c.rolloffMode=AudioRolloffMode.Linear;
			return c;
		} else
		{
			if (unUsedPool.Count != 0)
			{
				c = unUsedPool [unUsedPool.Count - 1];
				unUsedPool.RemoveAt (unUsedPool.Count - 1);
				c.enabled = true;
			} else
			{
				c = playObject.AddComponent <AudioSource> ();
				c.clip = clip;
				c.volume = volume;
				c.playOnAwake = false;
				c.minDistance=10000;
			}
//			usedPool.Add (c);
			return c;
		}
	}

	public void gc ()
	{
		if (allChannel.Count == 0)
		{
			return;
		}
		List<SoundItem> list=new List<SoundItem>();
		foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
		{
			if(kv.Value.isOver())
			{
				list.Add(kv.Key);
			}else if(!kv.Value.getSoundChanel().isPlaying)
			{
				list.Add(kv.Key);
				onSoundOver(kv.Key,false);
			}
		}
		if (list.Count != 0)
		{
			int l=list.Count;
			for(int i=0;i<l;i++)
			{
				allChannel.Remove(list[i]);
			}
		}
//		if (usedPool.Count == 0)
//		{
//			return;
//		} else
//		{
//			int l=usedPool.Count;
//			for(int i=0;i<l;i++)
//			{
//				AudioSource c=usedPool[i];
//				if(!c.isPlaying)
//				{
//					usedPool.RemoveAt(i);
//					l--;
//					i--;
//					unUsedPool.Add(c);
//					c.enabled=false;
//				}
//			}
//		}
	}
}
 