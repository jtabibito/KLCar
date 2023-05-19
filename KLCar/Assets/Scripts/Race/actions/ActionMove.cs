using UnityEngine;
using System.Collections;

/// <summary>
/// 移动指定的位置.不可多个同时运行.
/// </summary>
public class ActionMove : ActionBase
{
	public enum MoveType
	{
		MoveAdd,
		MoveTo,
		MoveFrom,
		MoveBy,
		SetTo,
		MoveFromOffset
	}
	public MoveType moveType;
	/// <summary>
	/// T要移动的值.
	/// </summary>
	public Vector3 values;
	public Transform pos;
	public Easetype easetype;
	/// <summary>
	/// 在移动的过程中,是否一直注视某个位置.通常使用lookTarget
	/// </summary>
	public Vector3 looktargetPos;
	/// <summary>
	/// 在移动的过程中,是否一直注视某个目标.支持GameObjectAgent.
	/// </summary>
	public GameObject lookTarget;
	/// <summary>
	/// 在多长时间类注视到目标.
	/// </summary>
	public float looktime = 1;
	/// <summary>
	/// 设定的移动位置是本地位置,还是相对于全局的位置.
	/// </summary>
	public bool islocalModel;
		
	protected override void onStart ()
	{
		if (moveType == MoveType.SetTo)
		{
			if (islocalModel)
			{
				transform.localPosition = values;
			} else
			{
				transform.position = values;
			}
			if (lookTarget != null)
			{
				transform.LookAt (GameObjectAgent.getAgentTransform (gameObject, lookTarget));
			} else if (looktargetPos != Vector3.zero)
			{
				transform.LookAt (looktargetPos);
			}
		} else
		{
				
			Hashtable h = iTween.Hash ();
			if (pos != null)
			{
				if(moveType==MoveType.MoveAdd)
				{
					h.Add ( "amount"  , pos.position );
				}else{
					h.Add ( "position",  pos);
				}
								
			} else
			{
				if (values == Vector3.zero)
				{
					h.Add ("x", 0);
					h.Add ("y", 0);
					h.Add ("z", 0);
				} else
				{
					if (moveType == MoveType.MoveAdd)
					{
						h.Add ("amount", pos.position);
					} else
					{
						if(moveType==MoveType.MoveFromOffset)
						{
							h.Add("position",(islocalModel?gameObject.transform.localPosition:gameObject.transform.position)+values);
						}else
						{
							h.Add ("position",values);
						}
					}
				}
			}
			h.Add ("time", time);
			if (easetype != Easetype.Default)
			{
				h.Add ("easetype", easetype.ToString ());
			}
			if (lookTarget != null)
			{
				h.Add ("looktarget", GameObjectAgent.getAgentTransform (gameObject, lookTarget));
			} else if (looktargetPos != Vector3.zero)
			{
				h.Add ("looktarget", looktargetPos);		
			}
			//if (looktime != 0) {
			h.Add ("looktime", looktime);
			//}
			if (moveType == MoveType.MoveAdd)
			{
				h.Add ("space", islocalModel ? Space.Self : Space.World);
			} else
			{
				h.Add ("islocal", islocalModel);
			}
						 
			switch (moveType)
			{
			case MoveType.MoveAdd:
				iTween.MoveAdd (gameObject, h);
				break;
			case MoveType.MoveFrom:
			case MoveType.MoveFromOffset:
				iTween.MoveFrom (gameObject, h);
				break;
			case MoveType.MoveBy:
				iTween.MoveBy (gameObject, h);
				break;
			case MoveType.MoveTo:
				iTween.MoveTo (gameObject, h);
				break;
			}
			if (time == 0 && lookTarget)
			{
				transform.LookAt (GameObjectAgent.getAgentTransform (gameObject, lookTarget).position);
			}
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionMove a = (ActionMove)cloneTo;
		a.moveType = moveType;
		a.values = values;
		a.easetype = easetype;
		a.looktargetPos = looktargetPos;
		a.lookTarget = lookTarget;
		a.looktime = looktime;
		a.islocalModel = islocalModel;
	}
}
