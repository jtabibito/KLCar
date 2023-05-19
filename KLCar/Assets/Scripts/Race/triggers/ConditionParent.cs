using UnityEngine;
using System.Collections;

/// <summary>
/// 指定触发成功的概率.
/// </summary>
public class ConditionParent : ConditionBase
{
	public Type type;
	public bool isTrue=true;
	public enum Type
	{
		SameParent,
		SameRoot,
	}
		
	public override bool isMatch (GameObject col)
	{
		bool t = false;
		switch (type)
		{
			case Type.SameParent:
			 t= transform.parent == col.transform.parent;
			break;
			case Type.SameRoot:
			t= transform.root == col.transform.root;
			break;
		}
		if (!isTrue)
		{
			t=!t;
		}
		return t;
	}
}
