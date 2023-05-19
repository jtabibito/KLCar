using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadResourceManager  {
	static LoadResourceManager instance;
	public static LoadResourceManager Instance {
		get {
			if(instance==null)
			{
				throw new UnityException("not init");
			}
			return instance;
		}
	}

	private LoadResourceManager()
	{
	}

	public delegate void OnInitOver();
	public static IEnumerator Init(OnInitOver onInitOver)
	{
		WWW loadResourceElementDatabase = new WWW (AssetbundleBaseURL + "LoadResourceDatabase.assetbundle");
		yield return loadResourceElementDatabase;
		instance = new LoadResourceManager ();
		ResourceElementHolder reh = (ResourceElementHolder) loadResourceElementDatabase.assetBundle.mainAsset;
		foreach(LoadResourceElement lre in reh.content)
		{
			instance.elementDic.Add(lre.ResourceName,lre);
		}
		onInitOver ();
	}

	public static string AssetbundleBaseURL
	{
		get
		{
			//				if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
			//					return Application.dataPath+"/assetbundles/";
			//				else
			//					return "file://" + Application.dataPath + "/assetbundles/";
			
			
			string path = null;
			
			if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
				path = Application.dataPath+"/assetbundles/";
			else if(Application.platform == RuntimePlatform.Android)
				path = Application.streamingAssetsPath + "/assetbundles/";
			else
				path = "file://" + Application.dataPath + "/assetbundles/";
			
			//				path = Application.streamingAssetsPath + "/assetbundles/";//"file://" + Application.streamingAssetsPath + "/assetbundles/";
			
			//				Debug.Log("www path = " + path);
			
			return path;
		}
	}

	public Dictionary<string,LoadResourceElement> elementDic=new Dictionary<string, LoadResourceElement>();
}
