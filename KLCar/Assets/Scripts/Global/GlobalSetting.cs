using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class GlobalSetting{
	//public static string NetAssetsUrl="http://120.24.218.58:8080/resource/netAssets/";
	public static string NetAssetsUrl="file://D:/KLSaiche/Assets/NetAssets/";
	public static bool UseLocalResources=true;

	public static string PlatformResourcesUrl
	{
		get
		{
			switch(Application.platform)
			{
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.WindowsPlayer:
				return NetAssetsUrl+"Windows32/";
			case RuntimePlatform.WindowsWebPlayer:
				return NetAssetsUrl+"WebPlayer/";
			case RuntimePlatform.Android:
				return NetAssetsUrl+"Android/";
			case RuntimePlatform.IPhonePlayer:
				return NetAssetsUrl+"IOS/";
			}
			return NetAssetsUrl+"Other/";
		}
	}
	
	public static Coroutine StartCoroutine(IEnumerator routine)
	{
		GameObject coroutineObj=GameObject.Find("coroutineInstance");
		if(coroutineObj==null)
		{
			coroutineObj=new GameObject();
			coroutineObj.AddComponent<MonoBehaviour>();
			coroutineObj.name="coroutineInstance";
			GameObject.DontDestroyOnLoad(coroutineObj);
		}
		MonoBehaviour mb=coroutineObj.GetComponent<MonoBehaviour>();
		return mb.StartCoroutine(routine);
	}

	public static string GetNameByWholePath(string path)
	{
		return path.Substring(path.LastIndexOf('/') + 1, path.LastIndexOf('.') - path.LastIndexOf('/') - 1);
	}
	
}
