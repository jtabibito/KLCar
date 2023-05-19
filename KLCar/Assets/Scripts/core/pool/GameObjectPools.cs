using UnityEngine;
using System.Collections;

public class GameObjectPools   {
	private static bool isInit;
	private static Hashtable table;
//	private static Hashtable tableByName;
	private static Transform parent;
	public static GameObjectPool getPool(GameObject obj)
	{
		return getPool (obj,obj.name);
	}
	public static GameObjectPool getPool(GameObject obj,string name)
	{
		validate ();
		GameObjectPool pool=(GameObjectPool)table[name];
		if (pool == null) {
			pool=addPool(obj,name);
		}
		return pool;
	}

	private static GameObjectPool addPool(GameObject obj,string name)
	{
		GameObjectPool pool=new GameObjectPool (obj);
		table[name]=pool;
		return pool;
	}
	public static GameObjectPool getPool(string name)
	{
		validate ();
		GameObjectPool pool=(GameObjectPool)table[name];
		return pool;

	}
	private static void validate()
	{
		if(table==null)
		{
			table=new Hashtable();
			 
		}
	}
	internal static Transform getPoolParent()
	{
		if (parent == null) {
				parent=new GameObject("objectPools").transform;
				parent.gameObject.SetActive(true);
				Object.DontDestroyOnLoad(parent);
			}
		return parent;
	}
}
