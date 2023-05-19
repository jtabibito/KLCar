using UnityEngine;
using System.Collections;

/// <summary>
/// 旋转一定的角度.
/// </summary>
public class ActionRotate : ActionBase
{
	public enum RotateType
	{
		RotateAdd,
		RotateTo,
		RotateFrom,
		RotateBy,
		SetTo
	}
	public RotateType rotateType;
	/// <summary>
	/// 要旋转的角度.
	/// </summary>
	public Vector3 values;
	public Easetype easetype;
	/// <summary>
	/// 是自身坐标轴的旋转还是相对于全局的旋转.
	/// </summary>
	public bool islocalModel;
		
	protected override void onStart ()
	{
		iTween t=gameObject.GetComponent <iTween>();
		base.onStart ();
		if (rotateType == RotateType.SetTo)
		{
			if (islocalModel)
			{
				transform.localEulerAngles = values;

			} else
			{
				transform.eulerAngles = values;
			}
			return;
		}
		Hashtable h = iTween.Hash ();
		h.Add (rotateType == RotateType.RotateAdd ? "amount" : "rotation", values);
		h.Add ("time", time);
		if (easetype != Easetype.Default)
		{
			h.Add ("easetype", easetype.ToString ());
		}
		if (rotateType == RotateType.RotateAdd)
		{
			h.Add ("space", islocalModel ? Space.Self : Space.World);
		} else
		{
			h.Add ("islocal", islocalModel);
		}
		switch (rotateType)
		{
		case RotateType.RotateAdd:
			iTween.RotateAdd (gameObject, h);
			break;
		case RotateType.RotateFrom:
			iTween.RotateFrom (gameObject, h);
			break;
		case RotateType.RotateBy:
			iTween.RotateBy (gameObject, h);
			break;
		case RotateType.RotateTo:
			iTween.RotateTo (gameObject, h);
			break;
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionRotate r = (ActionRotate)cloneTo;
		r.rotateType = rotateType;
		r.values = values;
		r.easetype = easetype;
		r.islocalModel = islocalModel;
	}
}
