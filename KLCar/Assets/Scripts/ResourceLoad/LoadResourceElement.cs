using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LoadResourceElement  {

	public enum LoadState
	{
		LS_WaitLoad=1,
		LS_IsLoading=2,
		LS_LoadOver=3
	}
	public LoadState nowState=LoadState.LS_WaitLoad;

	public enum ResourceType
	{
		role=1,//角色模型资源
		pet=2,//宠物模型资源
		car=3,//车子模型资源
	}

	public string bundlePath;
	public string resourceName;

	public string ResourceName {
		get {
			return resourceName;
		}
	}

	ResourceType rt;
	WWW resourceWWW;
	AssetBundleRequest abr;

	public LoadResourceElement(string resourceName,string bundlePath,ResourceType rt)
	{
		this.bundlePath = bundlePath;
		this.resourceName = resourceName;
		this.rt = rt;
	}

	/// <summary>
	/// Begins the load.
	/// 加载协程
	/// </summary>
	/// <returns>The load.</returns>
	public IEnumerator BeginLoad()
	{
		this.nowState = LoadState.LS_IsLoading;
		Debug.Log ("资源 " + resourceName + " 路径为:" + LoadResourceManager.AssetbundleBaseURL + this.bundlePath);
		this.resourceWWW = new WWW (LoadResourceManager.AssetbundleBaseURL+this.bundlePath);
		yield return this.resourceWWW;
		this.abr = this.resourceWWW.assetBundle.LoadAsync (this.resourceName, typeof(GameObject));
		yield return this.abr;
		this.nowState = LoadState.LS_LoadOver;
	}

	/// <summary>
	/// Gets the prefab.
	/// 获得资源预设
	/// </summary>
	/// <returns>The prefab.</returns>
	public GameObject GetPrefab()
	{
		if(this.nowState!=LoadState.LS_LoadOver)
		{
			throw new Exception("not load over");
		}
		return (GameObject)this.abr.asset;
	}

	/// <summary>
	/// Gets the game object.
	/// 获得资源实例
	/// </summary>
	/// <returns>The game object.</returns>
	public GameObject GetGameObject()
	{
		return (GameObject)UnityEngine.Object.Instantiate(this.GetPrefab());
	}
}
