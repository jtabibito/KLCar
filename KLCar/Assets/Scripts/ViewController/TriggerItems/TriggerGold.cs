using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerGold : TriggerItemBase{

	static GameObject prefab;
	static GameObject creatPlatform;
	static int poolSize;
	static List<GameObject> GoldObjPool=new List<GameObject>();

	void Start () {
	
	}
	
	void Update () {
	
	}

	public override void OnTriggerCarHandler (CarEngine car)
	{
//		throw new System.NotImplementedException ();
		//Debug.Log("car trigger gold");
//		car.addGlod (1);
//		this.gameObject.SetActive (false);
//		ActionBase ak=GetComponent <ActionBase>();
//		if (ak != null) {
//			ak.enabled=true;
//				}
		///给予金币的效果都用触发器实现.所以这里没有任何代码了.详情请看prefab.
	}

//	void DestroyGold()
//	{
//		if(GoldObjPool.Count>=poolSize)
//		{
//			Destroy(this.gameObject);
//		}
//		else
//		{
//			this.gameObject.SetActive(false);
//			GoldObjPool.Add(this.gameObject);
//		}
//	}

	static GameObject CreatGoldObject()
	{
		GameObject returnObj;
		if(GoldObjPool.Count>0)
		{
			returnObj=GoldObjPool[0];
			GoldObjPool.RemoveAt(0);
		}
		else
		{
			returnObj=CreatNewGoldObject();
		}
		return returnObj;
	}

	static GameObject CreatNewGoldObject()
	{
		if(prefab==null)
		{
			prefab = (GameObject) GameResourcesManager.GetRaceObject ("TriggerGold");
		}
		GameObject go = (GameObject)GameObject.Instantiate (prefab);
		if(creatPlatform==null)
		{
			creatPlatform=GameObject.Find("TriggerItems");
			if(creatPlatform==null)
			{
				creatPlatform=new GameObject();
				creatPlatform.name="TriggerItems";
			}
		}
		go.transform.parent = creatPlatform.transform;
		return go;
	}

	public static void CreatGoldObjectByTransform(Transform tf)
	{
		GameObject go = CreatGoldObject ();
		Transform gotf = go.GetComponent<Transform> ();
		gotf.position = tf.position;
		gotf.rotation = tf.rotation;
		go.SetActive (true);
	}
}
