using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool
{
	public int defaultSize = 100;
	private GameObject model;
	private Stack<GameObject> list;
	private Transform parent;
	private static Vector3 farPos=new Vector3(1000,1000,1000);
	public GameObjectPool (GameObject obj)
	{
		list = new  Stack<GameObject> ();
		model = obj;
		parent = new GameObject (obj.name).transform;
		parent.transform.parent = GameObjectPools.getPoolParent ();
		Object.DontDestroyOnLoad (parent);
		validate ();
	}

	public GameObject newInstance ()
	{
		validate ();
		GameObject obj = list.Pop ();
		obj.SetActive (true);
//		obj.transform.parent = null;
		return obj;
	}

	public void validate ()
	{
		if (list.Count == 0)
		{
			for (int i=0; i<defaultSize; i++)
			{
				GameObject o = (GameObject)GameObject.Instantiate (model);
				o.transform.parent = parent;
				o.SetActive (false);
				list.Push (o);
			}
		}
	}

	public void destoryObject (GameObject obj)
	{
		obj.SetActive (false);
		obj.transform.parent = parent;
		obj.transform.position = farPos;
		list.Push (obj);
	}
}
