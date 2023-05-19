using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class LogicLoadResources :LogicBase {

	AssetBundleResourceLoader asrl;
	List<XmlElement> baseAssets=new List<XmlElement>();
	List<XmlElement> uiCommonAssets=new List<XmlElement>();
	List<XmlElement> uiFontAssets=new List<XmlElement>();
	List<XmlElement> uiAtlasAssets=new List<XmlElement>();
	List<XmlElement> uiPrefabAssets=new List<XmlElement>();


	public override void ActLogic (Hashtable logicPar)
	{
//		throw new System.NotImplementedException ();

		if(GlobalSetting.UseLocalResources)
		{
			FinishLogic(null);
		}
		else
		{
			GlobalSetting.StartCoroutine(LoadResources());
		}
	}
	
	IEnumerator LoadResources()
	{

		Debug.Log("platform="+Application.platform.ToString());
		string versionNumUrl=GlobalSetting.PlatformResourcesUrl+"VersionNum/VersionNum.assetBundle";
		Debug.Log("versionNumUrl="+versionNumUrl);
		WWW versionWWW=new WWW(versionNumUrl);
		while (versionWWW.isDone == false)
			yield return null;
		
		AssetBundleRequest req = versionWWW.assetBundle.LoadAsync("VersionNum", typeof(Object));
		while (req.isDone == false)
			yield return null;

		XmlDocument doc=new XmlDocument();
		doc.LoadXml(((TextAsset)req.asset).text);
		versionWWW.assetBundle.Unload(false);
		versionWWW.Dispose();

		foreach(XmlNode node in doc.DocumentElement.ChildNodes)
		{
			if((node is XmlElement) && node.Name=="File")
			{
				XmlElement xe=node as XmlElement;
				switch(xe.GetAttribute("AssetType"))
				{
				case "UICommon":
					uiCommonAssets.Add(xe);
					break;
				case "UIFont":
					uiFontAssets.Add(xe);
					break;
				case "UIAtlas":
					uiAtlasAssets.Add(xe);
					break;
				case "UIPrefab":
					uiPrefabAssets.Add(xe);
					break;
				default:
					baseAssets.Add(xe);
					break;
				}
			}
		}
		asrl=new AssetBundleResourceLoader(uiCommonAssets,OnUICommonAssetsLoadOver);
	}

	void OnUICommonAssetsLoadOver()
	{
		asrl.Destroy();
		asrl=new AssetBundleResourceLoader(uiFontAssets,OnUIFontAssetsLoadOver);
	}

	void OnUIFontAssetsLoadOver()
	{
		asrl.Destroy();
		asrl=new AssetBundleResourceLoader(uiAtlasAssets,OnUIAtlasAssetsLoadOver);
	}

	void OnUIAtlasAssetsLoadOver()
	{
		asrl.Destroy();
		asrl=new AssetBundleResourceLoader(uiPrefabAssets,OnUIPrefabAssetsLoadOver);
	}

	void OnUIPrefabAssetsLoadOver()
	{
		asrl.Destroy();
		asrl=new AssetBundleResourceLoader(baseAssets,OnAllAssetsLoadOver);
	}

	void OnAllAssetsLoadOver()
	{
		asrl.Destroy();
		this.FinishLogic(null);
	}

	public override void Destroy ()
	{
		asrl=null;
		base.Destroy ();
	}
}
