using UnityEngine;
using System.Collections;

/// <summary>
/// 在场景中创建一个物品.
/// </summary>
public class ActionCreateObj : ActionBase
{
	/// <summary>
	/// 给创建的对象取一个新名字.将来可以通过名字删除对象.
	/// </summary>
	public string newName;
	/// <summary>
	/// T需要创建的物体.支持代理.
	/// </summary>
	public GameObject createObject;
	/// <summary>
	/// T创建后,添加到什么对象中.支持代理.
	/// </summary>
	public GameObject parent;
	/// <summary>
	/// 如果不为null,表示把对象放到这个地方.
	/// </summary>
	public Transform transformPos;
	/// <summary>
	/// 如果不为null,表示把对象放到这个地方.可以使代理.
	/// </summary>
	public GameObject gameObjectPos;
	/// <summary>
	/// 是否在结束的时候,将此对象删除
	/// </summary>
	public bool destoryOnOver;
	private GameObject last;
	/// <summary>
	/// 是否保持预设体中的方向和位置。也就是保留原始位置。
	/// </summary>
	//public bool keepLocalState=true;
	protected override void onStart ()
	{
		base.onStart ();
		if (createObject != null)
		{
//						Vector3 v = createObject.transform.localPosition;
//						GameObject o = (GameObject)Instantiate (createObject);
//						if (parent != null) {
//								o.transform.localPosition = v;
//						}
//						if (transformPos != null) {
//								o.transform.position = transformPos.position;
//						} 
			Vector3 pos = createObject.transform.localPosition;
			Quaternion dic=createObject.transform.localRotation;
			last = (GameObject)GameObject.Instantiate (createObject);
			GameObject pa=GameObjectAgent.GetAgentGameObject(gameObject,parent);
			if (pa != null)
			{
				last.transform.parent = pa.transform;
				last.transform.localPosition = pos;
				last.transform.localRotation=dic;
			}
			if (transformPos != null)
			{
				last.transform.position = transformPos.position;
				last.transform.eulerAngles = transformPos.eulerAngles;
			} else if (gameObjectPos != null)
			{
				Transform o=GameObjectAgent.getAgentTransform(gameObject,gameObjectPos);
				if(o!=null)
				{
					last.transform.position = o.position;
					last.transform.eulerAngles = o.eulerAngles;
				}
			}

			if (newName != null&&newName.Length>0)
			{
				last.name = newName;
			}
		}
	}

	protected override void onOver ()
	{
		base.onOver ();
		if (destoryOnOver && last != null)
		{
			DestroyObject (last);
		}
	}

	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionCreateObj c = (ActionCreateObj)cloneTo;
		c.createObject = createObject;
		c.parent = parent;
		c.transformPos = transformPos;
	}
}
