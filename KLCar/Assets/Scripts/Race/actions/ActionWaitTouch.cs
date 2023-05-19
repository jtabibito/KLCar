using UnityEngine;
using System.Collections;

/// <summary>
/// A等待指定长度的时间.什么都不做.如果指定了minTime和maxTime.则time自动取随机.
/// </summary>
public class ActionWaitTouch : ActionBase
{
	/// <summary>
	/// 如果为true,表示需要点击目标对象.否则只要点击屏幕即可.
	/// </summary>
	public bool touchObject;

	protected override void onStart ()
	{
		base.onStart ();
		if (time == 0)
		{
			setWait (10000000);
		}
	}

	void Update ()
	{
		 
		if (Input.GetMouseButton (0))
		{
			if (touchObject)
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit))
				{
					if (hit.transform.gameObject == gameObject)
					{
						over ();
					}
				}
			} else
			{
				over ();
			}
		}
		 
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		 
	}
}
