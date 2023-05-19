using System;
using UnityEngine;

/// <summary>
/// GameObejct的常用工具类.
/// </summary>
public class GameObjectUtils
{
	public delegate bool  CallOnChilden<T> (GameObject target,T  param);

	public delegate bool  CallOnChilden (GameObject target);
	
	/// <summary>
	/// 该方法遍历对象性能很低.不推荐使用.通常用于测试.
	/// </summary>
	/// <returns>The on childen.</returns>
	/// <param name="target">Target.</param>
	/// <param name="andSelf">If set to <c>true</c> and self.</param>
	/// <param name="all">If set to <c>true</c> all.</param>
	/// <param name="callBack">Call back.</param>
	/// <param name="param">Parameter.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static int callOnChildren<T> (GameObject target, bool andSelf, bool all, CallOnChilden<T> callBack, T  param)
	{
		int num = 0;
		if (andSelf)
		{
			num += callBack (target, param) ? 1 : 0;
		}
		foreach (Transform t in target.transform)
		{
			if (all)
			{
				num += callOnChildren (t.gameObject, true, true, callBack, param);
			} else
			{
				num += callBack (t.gameObject, param) ? 1 : 0;
			}
		}
		return num;
	}
	/// <summary>
	/// 该方法遍历对象性能很低.不推荐使用.通常用于测试.
	/// </summary>
	/// <returns>The on childen.</returns>
	/// <param name="target">Target.</param>
	/// <param name="andSelf">If set to <c>true</c> and self.</param>
	/// <param name="all">If set to <c>true</c> all.</param>
	/// <param name="callBack">Call back.</param>
	/// <param name="param">Parameter.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static int callOnChildren (GameObject target, bool andSelf, bool all, CallOnChilden callBack)
	{
//			Debug.Log (""+target);
		int num = 0;
		if (andSelf)
		{
			num += callBack (target) ? 1 : 0;
		}
		foreach (Transform t in target.transform)
		{
			if (all)
			{
				num += callOnChildren (t.gameObject, true, true, callBack);
			} else
			{
				num += callBack (t.gameObject) ? 1 : 0;
			}
		}
		return num;
	}
	/// <summary>
	/// 在指定的子节点中执行回调函数.
	/// </summary>
	/// <returns>The on children.</returns>
	/// <param name="target">null表示全局</param>
	/// <param name="andSelf">是否也在自身执行.</param>
	/// <param name="all">是否遍历子孙节点</param>
	/// <param name="callBack">需要执行的回调函数</param>
	/// <param name="children">子节点列表</param>
	public static int callOnChildren (GameObject target, bool andSelf, bool all, CallOnChilden callBack, params string[] children)
	{
		int num = 0;
		if (andSelf && target != null)
		{
			num += callBack (target) ? 1 : 0;
		}
		foreach (string n in children)
		{
			GameObject obj = null;
			if (target == null)
			{
				obj = (GameObject)GameObject.Find (n);
			} else
			{
				Transform t = target.transform.FindChild (n);
				if (t != null)
				{
					obj = t.gameObject;
				}
			}
			if (obj != null)
			{
				if (all)
				{
					num += callOnChildren (obj, true, true, callBack);
				} else
				{
					num += callBack (obj) ? 1 : 0;
				}
			}
		}
		return num;
	}

	public static string getGameObjectPath (GameObject obj)
	{
		Transform t = obj.transform;
		string s = null;
		while (t!=null)
		{
			s = t.name + (s == null ? "" : ("/" + s));
			t = t.parent;
		} 
		return s;
	}
	/// <summary>给某个对象创建一个子孙节点.
	/// </summary>
	/// <returns>如果乜有找到,或者路径错误.则返回null.</returns>
	/// <param name="target">null表示创建全局跟路径下的子节点</param>
	/// <param name="childrenName">子节点的名字,".."表示取父对象."..."表示取跟路径root."...."表示取全局.</param>
	/// <param name="create">是不是在没有找到路径的时候,自动创建</param>
	/// <param name="isfind">因为null也可以表示舞台.所以isfind可以用来确定是否没有找到对象</param>
	public static GameObject getGameObjectByPath (GameObject target, string childrenName, bool create)
	{
		bool isfind;
		return getGameObjectByPath (target,childrenName,create,out isfind);
	}
	/// <summary>给某个对象创建一个子孙节点.
	/// </summary>
	/// <returns>如果乜有找到,或者路径错误.则返回null.</returns>
	/// <param name="target">null表示创建全局跟路径下的子节点</param>
	/// <param name="childrenName">子节点的名字,".."表示取父对象."..."表示取跟路径root."...."表示取全局.</param>
	/// <param name="create">是不是在没有找到路径的时候,自动创建</param>
	/// <param name="isfind">因为null也可以表示舞台.所以isfind可以用来确定是否没有找到对象</param>
	public static GameObject getGameObjectByPath (GameObject target, string childrenName, bool create,out bool isfind)
	{
		Transform t = target == null ? null : target.transform;
		string[] cs = childrenName.Split ('/');
		for (int i=0; i<cs.Length; i++)
		{
			string c = cs [i];
			if (c == "..")
			{
				if (t == null)
				{
					isfind=false;
					return null;
				} else
				{
					t = t.parent;
				}
			} else if (c == "...")
			{
				if (t == null)
				{
					isfind=false;
					return null;
				} else
				{
					t = t.root;
				}
			} else if (c == "....")
			{
				t = null;
			} else
			{
				if (t == null)
				{
					GameObject o = GameObject.Find (c);
					if (o == null)
					{
						if (create)
						{
							o = new GameObject (c);
						} else
						{
							isfind=false;
							return null;
						}
					}
					t = o.transform;
				} else
				{
					Transform tr = t.FindChild (c);
					if (tr == null)
					{
						if (create)
						{
							GameObject g = new GameObject (c);
							g.transform.parent = t;
							tr = g.transform;
						} else
						{
							isfind=false;
							return null;
						}
					}
					t = tr;
				}
			}
		}
		isfind = true;
		if (t == null)
		{
			return null;
		} else
		{
			return t.gameObject;
		}
	}
}
 

