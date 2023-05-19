using UnityEngine;
using System.Collections;

public class ActionUtils
{
	/// <summary>
	/// 在指定的对象上运行给予的动作集合.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="codes">Codes.</param>
	public static void runAction (GameObject target, GameObject codes)
	{
		ActionBase.isWating = true;
		ActionBase fast = null;
		ActionBase last = null;
		ActionBase[] actions = codes.GetComponents <ActionBase> ();
		foreach (ActionBase a in actions)
		{
			System.Type t = a.GetType ();
			ActionBase b = (ActionBase)target.AddComponent (t);
			b.distoryOnOver = true;
//			b.notInit=true;
			a.copyData (b);
			if (fast == null)
			{
				fast = b;
			}
			last = b;
		}
		ActionBase.isWating = false;
		if (fast != null)
		{
			fast.enabled = true;
		}
	}
	/// <summary>
	/// 关闭某个对象上某一个名称下的动作脚本.以runNext为结束.也就是说.从actionName指定的脚本往后找,如果找到正在执行的脚本就停止.直到遇到runNext=false就不再往后找.
	/// </summary>
	/// <returns>返回关闭的数量.</returns>
	/// <param name="target">Target.</param>
	/// <param name="actionName">Action name.</param>
	public static int stopAction (GameObject target, string actionName)
	{
		ActionBase[] ab = target.GetComponents <ActionBase> ();
		bool isStart = false;
		int n = 0;
		foreach (ActionBase a in ab)
		{
			if (a.info == actionName)
			{
				isStart = true;
			}
			if (isStart)
			{
				if (!a.openNext)
				{
					break;
				} else
				{
					if (a.enabled)
					{
						a.doOver ();
						n++;
					}
				}
			}
		}
		return n;
	}
	/// <summary>
	/// 从指定的脚本开始关闭.直到openNext=false.如果index<0,则表示全部关闭.
	/// </summary>
	/// <returns>The action.</returns>
	/// <param name="target">Target.</param>
	/// <param name="index">Index.</param>
	public static int stopAction (GameObject target, int index)
	{
		ActionBase[] ab = target.GetComponents <ActionBase> ();
		bool isStart = false;
		int n = 0;
		for (int i=index; i<ab.Length; i++)
		{
			ActionBase a = ab [i];
			if (!a.openNext)
			{
				break;
			} else
			{
				if (a.enabled)
				{
					a.doOver ();
					n++;
				}
			}
		}
		return n;
	}
	/// <summary>
	/// 关闭所有的脚本.
	/// </summary>
	/// <returns>The all action.</returns>
	/// <param name="target">Target.</param>
	public static int stopAllAction (GameObject target)
	{
		ActionBase[] ab = target.GetComponents <ActionBase> ();
		int n = 0;
		foreach (ActionBase a in ab)
		{
			if (a.enabled)
			{
				a.doOver ();
				n++;
			}
		}
		return n;
	}
	/// <summary>
	/// 加载指定的prefab的名称,并且代码.
	/// </summary>
	/// <returns>The prefab.</returns>
	/// <param name="name">Name.</param>
	/// <param name="childName">Child name.</param>
	public static GameObject runPrefab(string name,string childName)
	{
		string n = name;
		int i=name.LastIndexOf ("/");
		if (i >= 0)
		{
			n=name.Substring(i+1);
		}
		GameObject obj= GameObject.Find (n);
		if (obj == null)
		{
			obj=GameResourcesManager.getActionPrefab(name);
			if(obj==null)
			{
				Debug.LogError("没有找到指定名称的动作脚本."+name);
				return null;
			}
			obj=(GameObject)GameObject.Instantiate(obj);
		}
		if (childName != null)
		{
			Transform o = obj.transform.FindChild (childName);
			if (o == null)
			{
				return null;
			} else
			{
				runAction (o.gameObject, 0);
				return o.gameObject;
			}
		} else
		{
			runAction(obj,0);
			return obj;
		}
	}
	/// <summary>
	/// 加载指定的prefab的名称,并且代码.
	/// </summary>
	/// <returns>The prefab.</returns>
	/// <param name="name">Name.</param>
	/// <param name="childName">Child name.</param>
	public static GameObject runPrefab(string name )
	{
		return runPrefab (name,null);
	}
	public static bool runAction (GameObject target, string actionName)
	{
		if (actionName == null)
		{
			return runAction (target, 0);
		} else
		{
			ActionBase[] ab = target.GetComponents <ActionBase> ();
			foreach (ActionBase a in ab)
			{
				if (a.info == actionName)
				{
					a.enabled = true;
					return true;
				}
			}
			return false;
		}
	}

	public static bool runAction (GameObject target, int index)
	{
		ActionBase[] ab = target.GetComponents <ActionBase> ();
		if (index >= 0 && index < ab.Length)
		{
			ab [index].enabled = true;
			return true;
		}
		return false;
	}
}
