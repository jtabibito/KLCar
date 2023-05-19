using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class SceneCreator
{

	private static SceneCreator instance;

	public static SceneCreator Instance {
		get {
			if (instance == null) {
				instance = new SceneCreator ();
			}
			return instance;
		}
	}

	string sceneName;

	public delegate void OnSceneCreatOver ();

	OnSceneCreatOver onSceneCreatOver;

	public void CreatScene (string sceneName, OnSceneCreatOver onSceneCreatOver)
	{
		this.sceneName = sceneName;
		this.onSceneCreatOver = onSceneCreatOver;
		GlobalSetting.StartCoroutine (LoadScene ());
	}


	private IEnumerator LoadScene ()
	{
		if(!GlobalSetting.UseLocalResources)
		{
		}

//		string sceneBundleName = GlobalSetting.GetSceneBundleNameBySceneName (sceneName);
//		string sceneXmlBundleName = GlobalSetting.GetSceneXmlBundleNameBySceneName (sceneName);
//		int sceneBundleVerNum = GlobalSetting.GetVersionNum (sceneBundleName);
//		int sceneXmlBundleVerNum = GlobalSetting.GetVersionNum (sceneXmlBundleName);
//		string sceneBundleUrl = GlobalSetting.ConverToFtpPathByBundlePath (sceneBundleName);
//		string sceneXmlBundleUrl = GlobalSetting.ConverToFtpPathByBundlePath (sceneXmlBundleName);
//
//
//		WWW wwwForSceneXml = WWW.LoadFromCacheOrDownload (sceneXmlBundleUrl, sceneXmlBundleVerNum);
//		while (wwwForSceneXml.isDone==false) {
//			yield return null;
//		}
//
//		AssetBundleRequest req = wwwForSceneXml.assetBundle.LoadAsync (sceneName + "Xml", typeof(Object));
//		while (req.isDone == false)
//			yield return null;
//
//		this.doc = new XmlDocument ();
//		doc.LoadXml (((TextAsset)req.asset).text);
//		wwwForSceneXml.assetBundle.Unload (false);
//		wwwForSceneXml.Dispose ();
//
//
//		WWW wwwForScene = WWW.LoadFromCacheOrDownload (sceneBundleUrl, sceneBundleVerNum);
//		while (wwwForScene.isDone == false) {
//			yield return null;
//		}
//		
//		AssetBundle sceneBundle = wwwForScene.assetBundle;
		yield return Application.LoadLevelAsync (sceneName);
//		wwwForScene.assetBundle.Unload (false);
//		wwwForScene.Dispose ();

		this.CreateSceneObjects ();
	}

	void CreateSceneObjects ()
	{
		XmlDocument doc=new XmlDocument();
		doc.LoadXml(GameResourcesManager.GetSceneXmlText(sceneName));

		GameObject root = new GameObject ("Env");
		
		GameObject rootFx = new GameObject ("Fx");
		rootFx.transform.parent = root.transform;
		
		GameObject rootModels = new GameObject ("Models");
		rootModels.transform.parent = root.transform;

		List<string> ef = new List<string> ();
		XmlElement elementsRoot = doc ["Root"];
		if (elementsRoot != null && elementsRoot.ChildNodes != null) {
			foreach (XmlElement e1 in elementsRoot.ChildNodes) {
				if (e1.ChildNodes != null) {
					if (e1.Name == "Fx") {
						foreach (XmlElement e2 in e1.ChildNodes) {
							if (e2.Name == "FxNode") {
								string name = e2.GetAttribute ("name");
								Debug.Log("FxName="+name);
								GameObject go = GameResourcesManager.GetSceneObject(name);
								GameObject creatObj = (GameObject)GameObject.Instantiate (go);
								creatObj.name = name;
								creatObj.transform.parent = rootFx.transform;
								creatObj.transform.position = new Vector3 (float.Parse (e2.GetAttribute ("posX")), float.Parse (e2.GetAttribute ("posY")), float.Parse (e2.GetAttribute ("posZ")));
								creatObj.transform.eulerAngles = new Vector3 (float.Parse (e2.GetAttribute ("rotX")), float.Parse (e2.GetAttribute ("rotY")), float.Parse (e2.GetAttribute ("rotZ")));
								creatObj.transform.localScale = new Vector3 (float.Parse (e2.GetAttribute ("scaleX")), float.Parse (e2.GetAttribute ("scaleY")), float.Parse (e2.GetAttribute ("scaleZ")));
								foreach (XmlElement e3 in e2.ChildNodes) {
									if (e3.Name == "LightMaps") {
										foreach (XmlElement e4 in e3.ChildNodes) {
											if (e4.Name == "LightMap") {
												string lmn = e4.GetAttribute ("Name");
 												GameObject target = creatObj.transform.FindChild (lmn).gameObject;
												target.isStatic = true;
												MeshRenderer mr = target.GetComponent<MeshRenderer> ();
												if (mr != null && mr.material != null) {
													mr.lightmapIndex = int.Parse (e4.GetAttribute ("LightmapIndex"));
													Vector4 v4 = new Vector4 ();
													v4.x = float.Parse (e4.GetAttribute ("OffsetX"));
													v4.y = float.Parse (e4.GetAttribute ("OffsetY"));
													v4.z = float.Parse (e4.GetAttribute ("OffsetZ"));
													v4.w = float.Parse (e4.GetAttribute ("OffsetW"));
													mr.lightmapTilingOffset = v4;
												}
											}
										}
									}
								}
							}
						}
					} else if (e1.Name == "Models") {
						foreach (XmlElement e2 in e1.ChildNodes) {
							if (e2.Name == "ModelsNode") {
								string name = e2.GetAttribute ("name");
//								Debug.Log ("ModleName=" + name);
								GameObject go = GameResourcesManager.GetSceneObject(name);
								GameObject creatObj = (GameObject)GameObject.Instantiate (go);
								creatObj.name = name;
								creatObj.transform.parent = rootModels.transform;
								creatObj.transform.position = new Vector3 (float.Parse (e2.GetAttribute ("posX")), float.Parse (e2.GetAttribute ("posY")), float.Parse (e2.GetAttribute ("posZ")));
								creatObj.transform.eulerAngles = new Vector3 (float.Parse (e2.GetAttribute ("rotX")), float.Parse (e2.GetAttribute ("rotY")), float.Parse (e2.GetAttribute ("rotZ")));
								creatObj.transform.localScale = new Vector3 (float.Parse (e2.GetAttribute ("scaleX")), float.Parse (e2.GetAttribute ("scaleY")), float.Parse (e2.GetAttribute ("scaleZ")));
								foreach (XmlElement e3 in e2.ChildNodes) {
									if (e3.Name == "LightMaps") {
										foreach (XmlElement e4 in e3.ChildNodes) {
											if (e4.Name == "LightMap") {
												string lmn = e4.GetAttribute ("Name");
												GameObject target = creatObj.transform.FindChild (lmn).gameObject;
												target.isStatic = true;
												MeshRenderer mr = target.GetComponent<MeshRenderer> ();
												if (mr != null && mr.material != null) {
													mr.lightmapIndex = int.Parse (e4.GetAttribute ("LightmapIndex"));
													Vector4 v4 = new Vector4 ();
													v4.x = float.Parse (e4.GetAttribute ("OffsetX"));
													v4.y = float.Parse (e4.GetAttribute ("OffsetY"));
													v4.z = float.Parse (e4.GetAttribute ("OffsetZ"));
													v4.w = float.Parse (e4.GetAttribute ("OffsetW"));
													mr.lightmapTilingOffset = v4;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		if (onSceneCreatOver != null) {
			onSceneCreatOver ();
		}
	}
}
