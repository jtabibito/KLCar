using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 保存可叠加状态的表.某些情况下,优于32位int取位的做法.
/// </summary>
public class StateMap<T>
{
	private Dictionary<T,int> map;
	public StateMap()
	{
		map = new Dictionary<T, int> ();
	}
	public void addState(T type)
	{
		if (map.ContainsKey (type))
		{
			map [type]++;
		}else
		{
			map[type]=1;
		}

	}
	public void removeState(T type)
	{
		if (map.ContainsKey (type))
		{
			map [type]--;
		}else
		{
			map[type]=-1;
		}
	}
	public bool isHasState(T type)
	{
		if (map.ContainsKey (type))
		{
			int a=map[type];
			return a>0;
		}else
		{
			return false;
		}
	}
	public void clearState(T type)
	{
		map.Remove (type);
	}
}

