using UnityEngine;
using System.Collections;

/// <summary>
/// 改变当前对象的父对象.让改对象添加到别的容器中.
/// </summary>
public class ActionSetParent : ActionBase
{
	/// <summary>
	/// 要添加到的目标.支持GameObjectAgent,如果为null,表示以自己为根节点.按照childrenName指定的路径查找.如果childrenName也为null,则添加到舞台.
	/// </summary>
	public GameObject parent;
	/// <summary>
	/// 如果不为null,表示取parent的某个名称的子节点.详细请查看GameObjectUtils.getGameObjectByPath()
	/// </summary>
	public string childrenName;
	/// <summary>
	/// 是否在设置结束后,摄像机恢复到原来的父对象.
	/// </summary>
	public bool recover;
	/// <summary>
	///是否改变父对象后,保留原来的本地位置.如果为true,则在父对象中.使用自己原来的本地位置.
	/// </summary>
	public bool keepLocalPos ;
	private GameObject lastParent;
	/// <summary>
	/// 如果没有找到指定的子节点,是否自动创建一个这样的子节点.
	/// </summary>
	public bool createIfNotFind = false;
	protected override void onStart ()
	{
		if (childrenName != null && childrenName.Length <= 0)
		{
			childrenName=null;
		}
		base.onStart ();
		if (recover)
		{
			lastParent = transform.parent.gameObject;
		}
		Vector3 lastPos = transform.localPosition;
		Transform pa = null;
		GameObject obj=null;
		if (parent != null)
		{
			pa = GameObjectAgent.getAgentTransform (gameObject, parent);
		}
		if (childrenName != null)
		{
			bool isFind = true;
			obj = GameObjectUtils.getGameObjectByPath (pa == null ? gameObject : pa.gameObject, childrenName, createIfNotFind, out isFind);
			if (!isFind)
			{
				return;
			}
		} else if (parent == null)
		{
			obj = null;
		} else
		{
			if(pa!=null)
			{
				obj=pa.gameObject;
			}
		}
		transform.parent =obj==null?null:obj.transform;
		if (keepLocalPos)
		{
			transform.localPosition = lastPos;
		}
	}

	protected override void onOver ()
	{
		base.onOver ();
		Vector3 lastPos = transform.localPosition;
		if (recover)
		{
			transform.parent = lastParent.transform;
			if (keepLocalPos)
			{
				transform.localPosition = lastPos;
			}
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionSetParent s = (ActionSetParent)cloneTo;
		s.parent = parent;
		s.recover = recover;
		s.keepLocalPos = keepLocalPos;
	}
}
