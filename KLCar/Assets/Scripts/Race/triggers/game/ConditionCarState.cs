using UnityEngine;
using System.Collections;

/// <summary>
/// 判断车的状态.
/// </summary>
public class ConditionCarState : ConditionBase
{
	public int stateIndex;
	public bool match=true;
	/// <summary>
	/// stateIndex是不是一个mask.
	/// </summary>
	public bool isMask;
	public override bool isMatch (GameObject col)
	{
		CarEngine e = col.GetComponent <CarEngine> ();
		if (e == null)
		{
			return false;
		} else
		{
			if(isMask)
			{
				return match?e.isInCarState(stateIndex):!e.isInCarState(stateIndex);
			}else
			{
				return match?e.getCarState(stateIndex):!e.getCarState(stateIndex);
			}
		}
	}
}
