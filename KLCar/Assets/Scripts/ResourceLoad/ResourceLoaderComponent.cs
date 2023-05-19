using UnityEngine;
using System.Collections;

/// <summary>
/// Resource loader component.
/// 资源加载组件
/// </summary>
using System.Collections.Generic;


public class ResourceLoaderComponent : MonoBehaviour {

	static ResourceLoaderComponent instance;

	public static ResourceLoaderComponent Instance {
		get {
			return instance;
		}
	}

	public delegate void OnGameObjectCreatOver(string resourceName,GameObject creatGameObject);
	Dictionary<LoadResourceElement,OnGameObjectCreatOver> waitDic = new Dictionary<LoadResourceElement, OnGameObjectCreatOver>();
	
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		List<LoadResourceElement> removeList=new List<LoadResourceElement>();
		foreach(KeyValuePair<LoadResourceElement,OnGameObjectCreatOver> kvp in waitDic)
		{
			if(kvp.Key.nowState==LoadResourceElement.LoadState.LS_LoadOver)
			{
				kvp.Value(kvp.Key.ResourceName,kvp.Key.GetGameObject());
				removeList.Add(kvp.Key);
			}
		}
		foreach(LoadResourceElement lre in removeList)
		{
			waitDic.Remove(lre);
		}
	}

	/// <summary>
	/// Creats the game object.
	/// 用资源创建游戏对象,需要指定创建完成后的回调方法
	/// </summary>
	/// <param name="resourceName">Resource name.</param>
	/// <param name="onGameObjectCreatOver">On game object creat over.</param>
	public void CreatGameObject(string resourceName,OnGameObjectCreatOver onGameObjectCreatOver)
	{
		LoadResourceElement lre;
		if(!LoadResourceManager.Instance.elementDic.TryGetValue(resourceName,out lre))
		{
			throw new UnityException("no resource");
		}
		if(lre.nowState==LoadResourceElement.LoadState.LS_LoadOver)
		{
			onGameObjectCreatOver(resourceName,lre.GetGameObject());
			return;
		}
		else if(lre.nowState==LoadResourceElement.LoadState.LS_WaitLoad)
		{
			this.StartCoroutine(lre.BeginLoad());
		}
		waitDic.Add (lre, onGameObjectCreatOver);
	}
}
