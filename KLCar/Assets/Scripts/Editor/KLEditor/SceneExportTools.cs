using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SceneExportTools
{

	static string txtPath = "Assets/Resources/SceneResources/SceneXmls/";

	[MenuItem("KLEditor/SceneExport/Scene For Windows32", false, 1)]
	public static void ExecuteWindows32 ()
	{
		PackageScene (UnityEditor.BuildTarget.StandaloneWindows);
	}
	
	[MenuItem("KLEditor/SceneExport/Scene For IPhone", false, 2)]
	public static void ExecuteIPhone ()
	{
		PackageScene (UnityEditor.BuildTarget.iPhone);
	}
	
	[MenuItem("KLEditor/SceneExport/Scene For Mac", false, 3)]
	public static void ExecuteMac ()
	{
		PackageScene (UnityEditor.BuildTarget.StandaloneOSXUniversal);
	}
	
	[MenuItem("KLEditor/SceneExport/Scene For Android", false, 4)]
	public static void ExecuteAndroid ()
	{
		PackageScene (UnityEditor.BuildTarget.Android);
	}
	
	[MenuItem("KLEditor/SceneExport/Scene For WebPlayer", false, 5)]
	public static void ExecuteWebPlayer ()
	{
		PackageScene (UnityEditor.BuildTarget.WebPlayer);
	}

	public static void PackageScene (UnityEditor.BuildTarget bt)
	{
		string levelPath = EditorApplication.currentScene;
		string levelName = levelPath.Substring (levelPath.LastIndexOf ('/') + 1, levelPath.LastIndexOf ('.') - levelPath.LastIndexOf ('/') - 1);
		string platformPath = AssetBundleEditor.GetPlatformSavePath (bt);

		string scenePath = platformPath + "Scenes/";
		if (Directory.Exists (scenePath) == false)
			Directory.CreateDirectory (scenePath);
		string filePath = scenePath + levelName + ".unity3d";

		BuildPipeline.BuildStreamedSceneAssetBundle (new string[1] { EditorApplication.currentScene }, filePath, bt);	
	}

	[MenuItem("KLEditor/SceneExport/Export Scene XML")]
	public static void ExportSceneXML ()
	{
		// 所有的动态加载的物体都挂在Env下面
		GameObject parent = GameObject.Find ("Env");
		if (parent == null) {
			Debug.LogError ("No ActiveObjectRoot Node!");
			return;
		}

		string levelPath = EditorApplication.currentScene;
		string levelName = levelPath.Substring (levelPath.LastIndexOf ('/') + 1, levelPath.LastIndexOf ('.') - levelPath.LastIndexOf ('/') - 1);

		XmlDocument XmlDoc = new XmlDocument ();
		XmlElement XmlRoot = XmlDoc.CreateElement ("Root");
		XmlRoot.SetAttribute ("level", levelName);
		XmlDoc.AppendChild (XmlRoot);

		XmlElement XmlFx = XmlDoc.CreateElement ("Fx");
		XmlRoot.AppendChild (XmlFx);
		foreach (Transform tf in parent.transform.FindChild("Fx")) {
			XmlElement node = XmlDoc.CreateElement ("FxNode");
			XmlFx.AppendChild (node);
			CreateTransformNode (XmlDoc, node, tf);
			CreatLightMapNodes (XmlDoc, node, tf);
		}

		XmlElement XmlModels = XmlDoc.CreateElement ("Models");
		XmlRoot.AppendChild (XmlModels);
		foreach (Transform tf in parent.transform.FindChild("Models")) {
			XmlElement node = XmlDoc.CreateElement ("ModelsNode");
			XmlModels.AppendChild (node);
			CreateTransformNode (XmlDoc, node, tf);
			CreatLightMapNodes (XmlDoc, node, tf);
		}

		if (Directory.Exists (txtPath) == false)
			Directory.CreateDirectory (txtPath);

		XmlDoc.Save (txtPath + levelName + "Xml" + ".xml");
		XmlDoc = null;

		AssetDatabase.Refresh ();
	}

	private static void CreateTransformNode (XmlDocument XmlDoc, XmlElement xmlNode, Transform tran)
	{
		if (XmlDoc == null || xmlNode == null || tran == null)
			return;
		
		xmlNode.SetAttribute ("name", tran.name);
		xmlNode.SetAttribute ("posX", tran.position.x.ToString ());
		xmlNode.SetAttribute ("posY", tran.position.y.ToString ());
		xmlNode.SetAttribute ("posZ", tran.position.z.ToString ());
		xmlNode.SetAttribute ("rotX", tran.eulerAngles.x.ToString ());
		xmlNode.SetAttribute ("rotY", tran.eulerAngles.y.ToString ());
		xmlNode.SetAttribute ("rotZ", tran.eulerAngles.z.ToString ());
		xmlNode.SetAttribute ("scaleX", tran.localScale.x.ToString ());
		xmlNode.SetAttribute ("scaleY", tran.localScale.y.ToString ());
		xmlNode.SetAttribute ("scaleZ", tran.localScale.z.ToString ());
	}

	private static void CreatLightMapNodes (XmlDocument xmlDoc, XmlElement xmlNode, Transform tran)
	{
		if (xmlDoc == null || xmlNode == null || tran == null)
			return;
		
		XmlElement xmlProp = xmlDoc.CreateElement ("LightMaps");
		xmlNode.AppendChild (xmlProp);
		CreateLightMapNode (xmlDoc, xmlProp, tran, "myself");
	}

	private static void CreateLightMapNode (XmlDocument xmlDoc, XmlElement xmlNode, Transform tran, string parentName)
	{
		MeshRenderer mr = tran.gameObject.GetComponent<MeshRenderer> ();
		if (mr != null && mr.sharedMaterial != null) {
			XmlElement xmlLightmap = xmlDoc.CreateElement ("LightMap");
			xmlNode.AppendChild (xmlLightmap);
			string name = "";
			if (parentName != "myself") {
				name = parentName + tran.name;
			}
			xmlLightmap.SetAttribute ("Name", name);
			xmlLightmap.SetAttribute ("LightmapIndex", mr.lightmapIndex.ToString ());
			xmlLightmap.SetAttribute ("OffsetX", mr.lightmapTilingOffset.x.ToString ());
			xmlLightmap.SetAttribute ("OffsetY", mr.lightmapTilingOffset.y.ToString ());
			xmlLightmap.SetAttribute ("OffsetZ", mr.lightmapTilingOffset.z.ToString ());
			xmlLightmap.SetAttribute ("OffsetW", mr.lightmapTilingOffset.w.ToString ());
		}

		if (parentName == "myself") {
			parentName = "";
		} else {
			parentName += tran.name + "/";
		}
		foreach (Transform tf in tran) {
			CreateLightMapNode (xmlDoc, xmlNode, tf, parentName);
		}
	}
	
	private static void CreateMeshNode (XmlDocument XmlDoc, XmlElement xmlNode, Transform tran)
	{
		if (XmlDoc == null || xmlNode == null || tran == null)
			return;
		
		XmlElement xmlProp = XmlDoc.CreateElement ("MeshRenderer");
		xmlNode.AppendChild (xmlProp);
		
		foreach (MeshRenderer mr in tran.gameObject.GetComponentsInChildren<MeshRenderer>(true)) {
			if (mr.material != null) {	
				XmlElement xmlMesh = XmlDoc.CreateElement ("Mesh");
				xmlProp.AppendChild (xmlMesh);
				
				// 记录Mesh名字和Shader
				xmlMesh.SetAttribute ("Mesh", mr.name);
				xmlMesh.SetAttribute ("Shader", mr.material.shader.name);
				
				// 记录主颜色
				XmlElement xmlColor = XmlDoc.CreateElement ("Color");
				xmlMesh.AppendChild (xmlColor);
				bool hasColor = mr.material.HasProperty ("_Color");
				xmlColor.SetAttribute ("hasColor", hasColor.ToString ());
				if (hasColor) {
					xmlColor.SetAttribute ("r", mr.material.color.r.ToString ());
					xmlColor.SetAttribute ("g", mr.material.color.g.ToString ());
					xmlColor.SetAttribute ("b", mr.material.color.b.ToString ());
					xmlColor.SetAttribute ("a", mr.material.color.a.ToString ());
				}
				
				// 光照贴图信息
				XmlElement xmlLightmap = XmlDoc.CreateElement ("Lightmap");
				xmlMesh.AppendChild (xmlLightmap);
				// 是否为static，static的对象才有lightmap信息
				xmlLightmap.SetAttribute ("IsStatic", mr.gameObject.isStatic.ToString ());
				xmlLightmap.SetAttribute ("LightmapIndex", mr.lightmapIndex.ToString ());
				xmlLightmap.SetAttribute ("OffsetX", mr.lightmapTilingOffset.x.ToString ());
				xmlLightmap.SetAttribute ("OffsetY", mr.lightmapTilingOffset.y.ToString ());
				xmlLightmap.SetAttribute ("OffsetZ", mr.lightmapTilingOffset.z.ToString ());
				xmlLightmap.SetAttribute ("OffsetW", mr.lightmapTilingOffset.w.ToString ());
			}
		}
	}
}
