using UnityEngine;
using System.Collections;
/// <summary>
///给予碰撞车辆一定金币的效果.
/// </summary>
public class ActionCarExplode : ActionBase {
	public GameObject explodeEffect;
	public bool destorySelf;
	 internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionCarExplode car = (ActionCarExplode)cloneTo;

	}
	protected override void onStart ()
	{
		CarEngine car=gameObject.GetComponent <CarEngine>();
		if (car != null)
		{//如果我是车辆.
			GameObject.DestroyObject( gameObject.transform.root.gameObject);
		} else
		{//别的简单碰撞体.
			DestroyObject(gameObject);
		}
	}
}
