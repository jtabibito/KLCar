using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class ResourceManager
{
	private static ResourceManager instance;

	public static ResourceManager Instance {
		get {
			if(instance==null)
			{
				instance=new ResourceManager();
			}
			return instance;
		}
	}

	// 已解压的Asset列表 [prefabPath, asset]
	private Dictionary<string, UnityEngine.Object> dicAsset = new Dictionary<string, UnityEngine.Object>();

	public Dictionary<string, UnityEngine.Object> DicAsset {
		get {
			return dicAsset;
		}
	}

	// "正在"加载的资源列表 [prefabPath, www]
	private Dictionary<string, WWW> dicLoadingReq = new Dictionary<string, WWW>();
	
	public UnityEngine.Object GetResource(string name)
	{
		UnityEngine.Object obj = null;
		if (dicAsset.TryGetValue(name, out obj) == false)
		{
			Debug.LogWarning("<GetResource Failed> Res not exist, res.Name = " + name);
			if (dicLoadingReq.ContainsKey(name))
			{
				Debug.LogWarning("<GetResource Failed> The res is still loading");
			}
		}
		return obj;
	}
	
	// name表示prefabPath，eg:Prefab/Pet/ABC
	public void LoadAsync(XmlElement assetElement)
	{
		// 如果已经下载，则返回
		if (dicAsset.ContainsKey(assetElement.GetAttribute("FilePath")))
			return;
		
		// 如果正在下载，则返回
		if (dicLoadingReq.ContainsKey(assetElement.GetAttribute("FilePath")))
			return;

		// 添加引用
//		RefAsset(assetName);
		// 如果没下载，则开始下载
		GlobalSetting.StartCoroutine(AsyncLoadCoroutine(assetElement));
	}
	
//	private IEnumerator AsyncLoadCoroutine(string assetName, GlobalSetting.GetAssetType at)
//	{
//		System.Type type=GlobalSetting.GetLoadTypeByAssetType(at);
//		string assetBundlePath = GlobalSetting.ConvertToAssetBundleName(assetName,at);
//		string url = GlobalSetting.ConverToFtpPathByBundlePath(assetBundlePath);
//		int verNum = GlobalSetting.GetVersionNum(assetBundlePath);
//		
//		Debug.Log("WWW AsyncLoad name = " + assetName + " assetBundleName ="+ assetBundlePath+" url = "+url+" versionNum = " + verNum+" type = "+type.ToString());
//		if (Caching.IsVersionCached(url, verNum) == false)
//			Debug.Log("Version Is not Cached, which will download from net!");
//		
//		WWW www = WWW.LoadFromCacheOrDownload(url,verNum);
//		dicLoadingReq.Add(assetName, www);
//		while (www.isDone == false)
//			yield return null;
//		
//		AssetBundleRequest req = www.assetBundle.LoadAsync(assetName, type);
//		while (req.isDone == false)
//			yield return null;
//
//		Debug.Log("assetType="+req.asset.GetType().Name);
//
//		dicAsset.Add(assetName, req.asset);
//		dicLoadingReq.Remove(assetName);
//		www.assetBundle.Unload(false);
//		www = null;
//		// Debug.Log("WWW AsyncLoad Finished " + assetBundleName + " versionNum = " + verNum);
//	}

	private IEnumerator AsyncLoadCoroutine(XmlElement assetElemnt)
	{
		string filePath=assetElemnt.GetAttribute("FilePath");
		string typeStr=assetElemnt.GetAttribute("AssetType");
		string loadUrl=GlobalSetting.PlatformResourcesUrl+filePath;
		if(typeStr=="unity3d")
		{
			loadUrl+=".unity3d";
		}
		else
		{
			loadUrl+=".assetBundle";
		}
		int verNum=int.Parse(assetElemnt.GetAttribute("Num"));
		if (Caching.IsVersionCached(loadUrl, verNum) == false)
			Debug.Log("Version Is not Cached, which will download from net!");

		Debug.Log("BeginLoad loadUrl = " + loadUrl + " versionNum = " + verNum);
		
		WWW www = WWW.LoadFromCacheOrDownload(loadUrl,verNum);
		dicLoadingReq.Add(filePath, www);
		while (www.isDone == false)
			yield return null;

		string assetName=assetElemnt.GetAttribute("AssetName");


		if(typeStr=="unity3d" || typeStr=="UIAtlas" || typeStr=="UICommon" || typeStr=="UIFont")
		{
			dicAsset.Add(filePath,www.assetBundle);
			dicLoadingReq.Remove(filePath);
		}
		else
		{
			AssetBundleRequest req = www.assetBundle.LoadAsync(assetName, GetLoadType(typeStr));
			while (req.isDone == false)
				yield return null;
			dicAsset.Add(filePath, req.asset);
			dicLoadingReq.Remove(filePath);
			www.assetBundle.Unload(false);
			www = null;
		}
		Debug.Log("WWW AsyncLoad Finished " + filePath);
	}

	Type GetLoadType(string typeStr)
	{
		switch(typeStr)
		{
		case "txt":
		case "csv":
		case "xml":
			return typeof(TextAsset);
		case "mp3":
		case "wav":
			return typeof(AudioClip);
		}
		return typeof(GameObject);
	}

	public bool IsResLoading(string name)
	{
		return dicLoadingReq.ContainsKey(name);
	}
	
	public bool IsResLoaded(string name)
	{
		return dicAsset.ContainsKey(name);
	}
	
	public WWW GetLoadingWWW(string name)
	{
		WWW www = null;
		dicLoadingReq.TryGetValue(name, out www);
		return www;
	}
	
	// 移除Asset资源的引用，name表示prefabPath
	public void UnrefAsset(string name)
	{
		dicAsset.Remove(name);
	}
	
	public void UnloadUnusedAsset()
	{
//		bool effectNeedUnload = GameApp.GetEffectManager().UnloadAsset();
//		bool worldNeedUnload = GameApp.GetWorldManager().UnloadAsset();
//		bool sceneNeedUnload = GameApp.GetSceneManager().UnloadAsset();
//		if (effectNeedUnload || worldNeedUnload || sceneNeedUnload)
//		{
//			Resources.UnloadUnusedAssets();
//		}
	}
	
	// 根据资源路径添加资源引用，每个管理器管理自己的引用
	private void RefAsset(string name)
	{
//		// 模型之类的
//		if (name.Contains(GlobalSetting.CharacterPath))
//			GameApp.GetWorldManager().RefAsset(name);
//		// 图片之类的
//		else if (name.Contains(GlobalSetting.TexturePath))
//			GameApp.GetUIManager().RefPTexture(name);// 特效之类的
//		else if (name.Contains(GlobalSetting.EffectPath))
//			GameApp.GetEffectManager().RefAsset(name);
//		　　　　 ......
//			　　　　 else
//				Debug.LogWarning("<Res not ref> name = " + name);
	}


	/// <summary>
	/// Loads the dynamic.
	/// 加载动态资源,加载前需确保资源已载入
	/// </summary>
	/// <returns>The dynamic.</returns>
	/// <param name="assetName">Asset name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T Load<T>(string assetPath) where T:UnityEngine.Object
	{
		if(GlobalSetting.UseLocalResources)
		{
			return Resources.Load<T>(assetPath);
		}
		else
		{
			if(!ResourceManager.Instance.IsResLoaded(assetPath))
			{
				throw new UnityException("res \""+assetPath+"\" not load over");
			}
			return (T)ResourceManager.Instance.GetResource(assetPath);
		}
	}

}
