using UnityEngine;
using System.Collections;

/// <summary>
/// 设置对象的可显示状态. time=0表示永久.
/// </summary>
public class ActionSetVisible : ActionBase
{
	public bool isVisible;
 	
	protected override void onStart ()
	{
		base.onStart ();
//		if (renderer != null) {
//				renderer.enabled = isVisible;
//			}
		MeshRenderer[] rs = gameObject.GetComponentsInChildren <MeshRenderer> ();
		for (int i=0; i<rs.Length; i++)
		{
			rs [i].enabled = isVisible;
		}
		//gameObject.SetActive(isActive);
			 
	}

	protected override void onOver ()
	{
		base.onOver ();
		if (time != 0)
		{
			MeshRenderer[] rs = gameObject.GetComponentsInChildren <MeshRenderer> ();
			for (int i=0; i<rs.Length; i++)
			{
				rs [i].enabled = !isVisible;
			}
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionSetVisible s = (ActionSetVisible)cloneTo;
		s.isVisible = isVisible;
	}
}
