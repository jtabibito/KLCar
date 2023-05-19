using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class AssetBundleResourceLoader {

	public AssetBundleResourceLoader(List<XmlElement> lstRes,LoadFinishDelegate onLoadFinish)
	{
		this.lstRes=lstRes;
		this.OnLoadFinish=onLoadFinish;
		LoadAsset();
	}


	// 同时开启的Coroutine的数目
	private const int ThreadNum = 10;
	// 记录每个加载线程的进度，只有每个线程都加在结束了，场景加载才算完成
	private int[] arrThreadProggress = new int[ThreadNum];
	private bool[] arrThreadStates=new bool[ThreadNum];

	// 加载完成后的回掉
	public delegate void LoadFinishDelegate();
	public LoadFinishDelegate OnLoadFinish = null;
	
	// 需要下载的资源列表
	private List<XmlElement> lstRes = new List<XmlElement>();
	
	private void LoadAsset()
	{for (int i = 0; i < ThreadNum; ++i)
		{
			arrThreadProggress[i]=i;
			GlobalSetting.StartCoroutine(LoadAssetCoroutine(i));
		}
	}
	
	private IEnumerator LoadAssetCoroutine(int threadIndex)
	{
		while (arrThreadProggress[threadIndex] < lstRes.Count)
		{
			// 载入资源
			arrThreadStates[threadIndex]=true;
			XmlElement assetElement = lstRes[arrThreadProggress[threadIndex]];
			ResourceManager.Instance.LoadAsync(assetElement);
			string assetPath=assetElement.GetAttribute("FilePath");
			while (ResourceManager.Instance.IsResLoaded(assetPath) == false)
			{
				yield return null;
			}
			Debug.Log(assetPath+" load over");
			arrThreadStates[threadIndex]=false;
			arrThreadProggress[threadIndex] += ThreadNum;
		}
		// 线程资源下载完毕，进行加载回掉
		if (IsLoadFinished())
		{
			if (OnLoadFinish != null)
			{
				OnLoadFinish();
				OnLoadFinish=null;
			}
		}
	}

	private bool IsLoadFinished()
	{
		foreach(bool b in arrThreadStates)
		{
			if(b)
			{
				return false;
			}
		}
		return true;
	}

	public void Destroy()
	{
		OnLoadFinish=null;
		lstRes.Clear();
	}
}
