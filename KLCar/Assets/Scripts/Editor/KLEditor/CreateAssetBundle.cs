using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;

public class CreateAssetBundle
{
	public static string GameResourcesPath="Assets/Resources/";

	public class AssetData
	{
		public string path;
		public string name;
		public string type;
		public Object o;
		public string strMD5;
	}

	public struct AssetNameData
	{
		public string relativeDirPath;//相对路径
		public string assetName;//资源名
		public string backName;//资源后缀名
	}

	static List<AssetData> assetsList;
	static BuildTarget bt;

	public static void Execute(UnityEditor.BuildTarget target)
	{
		bt=target;
		assetsList=new List<AssetData>();
		List<AssetData> uiCommonList=new List<AssetData>();
		List<AssetData> uiAtlasList=new List<AssetData>();
		List<AssetData> uiFontList=new List<AssetData>();
		List<AssetData> uiPrefabList=new List<AssetData>();
		//sceneList=new List<AssetData>();


		List<Object> assets=new List<Object>();
		string assetsPath=GameResourcesPath;
		GetAllAssetsUnderDir(assetsPath,assets);

		// 当前选中的资源列表
		foreach (Object o in assets)
		{	
			string assetPath = AssetDatabase.GetAssetPath(o);
			Debug.Log("wholePath="+assetPath);
			AssetNameData and=GetAssetNameData(assetPath);

			string type="";
			if(and.relativeDirPath.Contains("UIResources/UIAtlas") && and.backName=="prefab")
			{
				//相对路径中包括UIAtlas并且后缀为prefab,定义为UIAtlas-图集预制体
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type="UIAtlas";
				ad.o=o;
				uiAtlasList.Add(ad);
			}
			else if(and.relativeDirPath.Contains("UIResources/UIPrefab") && and.backName=="prefab")
			{
				//相对路径中包括UIPrefab并且后缀为prefab,定义为UIPrefab-界面预制体
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type="UIPrefab";
				ad.o=o;
				uiPrefabList.Add(ad);
			}
			else if(and.relativeDirPath.Contains("UIResources/UICommon") && and.backName=="prefab")
			{
				//相对路径中包括UICommon并且后缀为prefab,定义为UICommon-界面基本预制体
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type="UICommon";
				ad.o=o;
				uiCommonList.Add(ad);
			}
			else if(and.relativeDirPath.Contains("UIResources/UIFont") && and.backName=="prefab")
			{
				//相对路径中包括UIFont并且后缀为prefab,定义为UIFont-字体预制体
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type="UIFont";
				ad.o=o;
				uiFontList.Add(ad);
			}
			else if(and.relativeDirPath.Contains("SoundResources"))
			{
				if(and.backName=="mp3")
				{
					//mp3声音文件
					AssetData ad=new AssetData();
					ad.path=and.relativeDirPath;
					ad.name=and.assetName;
					ad.type="mp3";
					ad.o=o;
					assetsList.Add(ad);

				}
				else if(and.backName=="wav")
				{
					//wav声音文件
					AssetData ad=new AssetData();
					ad.path=and.relativeDirPath;
					ad.name=and.assetName;
					ad.type="wav";
					ad.o=o;
					assetsList.Add(ad);
				}
			}
			else if(and.backName=="csv" || and.backName=="xml" || and.backName=="txt")
			{
				//文本文件
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type=and.backName;
				ad.o=o;
				assetsList.Add(ad);
			}
			else if(and.backName=="prefab")
			{
				//预制体
				AssetData ad=new AssetData();
				ad.path=and.relativeDirPath;
				ad.name=and.assetName;
				ad.type=and.backName;
				ad.o=o;
				assetsList.Add(ad);
			}
		}

		string platFormPath=AssetBundleEditor.GetPlatformSavePath(bt);

		//打包除UI以外的预制体
		foreach(AssetData ad in assetsList)
		{
			string dirPath = platFormPath + ad.path;
			if(Directory.Exists(dirPath)==false)
			{
				Directory.CreateDirectory(dirPath);
			}
			string savePath=dirPath+ad.name+".assetBundle";
			BuildPipeline.BuildAssetBundle(ad.o, null, savePath, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies, bt);
		}

		//依赖打包UI资源
		BuildPipeline.PushAssetDependencies();
		//打包基础脚本
		foreach(AssetData ad in uiCommonList)
		{
			string dirPath = platFormPath + ad.path;
			if(Directory.Exists(dirPath)==false)
			{
				Directory.CreateDirectory(dirPath);
			}
			string savePath=dirPath+ad.name+".assetBundle";
			assetsList.Add(ad);
			BuildPipeline.BuildAssetBundle(ad.o, null, savePath, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, bt);
		}

		BuildPipeline.PushAssetDependencies();
		//打包字体
		foreach(AssetData ad in uiFontList)
		{
			string dirPath = platFormPath + ad.path;
			if(Directory.Exists(dirPath)==false)
			{
				Directory.CreateDirectory(dirPath);
			}
			string savePath=dirPath+ad.name+".assetBundle";
			assetsList.Add(ad);
			BuildPipeline.BuildAssetBundle(ad.o, null, savePath, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, bt);
		}

		//打包图集
		foreach(AssetData ad in uiAtlasList)
		{
			string dirPath = platFormPath + ad.path;
			if(Directory.Exists(dirPath)==false)
			{
				Directory.CreateDirectory(dirPath);
			}
			string savePath=dirPath+ad.name+".assetBundle";
			assetsList.Add(ad);
			BuildPipeline.BuildAssetBundle(ad.o, null, savePath, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, bt);
		}

		//打包UI预制体
		foreach(AssetData ad in uiPrefabList)
		{
			BuildPipeline.PushAssetDependencies();

			string dirPath = platFormPath + ad.path;
			if(Directory.Exists(dirPath)==false)
			{
				Directory.CreateDirectory(dirPath);
			}
			string savePath=dirPath+ad.name+".assetBundle";
			assetsList.Add(ad);
			BuildPipeline.BuildAssetBundle(ad.o, null, savePath, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, bt);

			BuildPipeline.PopAssetDependencies();
		}

		BuildPipeline.PopAssetDependencies();
		
		BuildPipeline.PopAssetDependencies();

		AssetDatabase.Refresh();

		CreatMD5List();
	}

	public static AssetNameData GetAssetNameData(string path)
	{
		AssetNameData and;
		path=path.Replace(GameResourcesPath,"");
		and.relativeDirPath=path.Substring(0,path.LastIndexOf("/")+1);
		and.assetName=path.Substring(path.LastIndexOf('/') + 1, path.LastIndexOf('.') - path.LastIndexOf('/') - 1);
		and.backName=path.Substring(path.LastIndexOf('.')+1);
		return and;
	}

	static void GetAllAssetsUnderDir(string dir,List<Object> ol)
	{
		Debug.Log("search dir--"+dir);
		string[] files=Directory.GetFiles(dir);
		foreach (string filePath in files)
		{
			//临时文件
			if (filePath.Contains(".meta"))
			{
				continue;
			}
			Debug.Log("create assetbundle--"+filePath);
			Object getObj=AssetDatabase.LoadAssetAtPath(filePath,typeof(Object));
			if(getObj==null)
			{
				throw new UnityException("null obj-----------------"+filePath);
			}
			ol.Add(AssetDatabase.LoadAssetAtPath(filePath,typeof(Object)));
		}
		string[] dirs=Directory.GetDirectories(dir);
		foreach(string dirPath in dirs)
		{
			GetAllAssetsUnderDir(dirPath,ol);
		}
	}

	public static void CreatMD5List()
	{
		Dictionary<string, AssetData> DicFileMD5 = new Dictionary<string,AssetData>();
		MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
		string platFormPath=AssetBundleEditor.GetPlatformSavePath(bt);

		foreach(AssetData ad in assetsList)
		{
			string filePath=platFormPath+ad.path+ad.name+".assetBundle";
			FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] hash = md5Generator.ComputeHash(file);
			ad.strMD5=System.BitConverter.ToString(hash);
			file.Close();
			
			string key = ad.path+ad.name;
			
			if (DicFileMD5.ContainsKey(key) == false)
				DicFileMD5.Add(key, ad);
			else
				Debug.LogWarning("<Two File has the same name> name = " + filePath);
		}

		string scenesDir=platFormPath+"Scenes/";
		string[] files=Directory.GetFiles(scenesDir);
		foreach (string filePath in files)
		{
			if (!filePath.Contains(".meta") && filePath.Contains(".unity3d"))
			{
				AssetNameData and=GetAssetNameData(filePath);
				AssetData ad=new AssetData();
				ad.path="Scenes/";
				ad.name=filePath.Substring(filePath.LastIndexOf('/') + 1, filePath.LastIndexOf('.') - filePath.LastIndexOf('/') - 1);;
				ad.type="unity3d";
				
				FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
				byte[] hash=md5Generator.ComputeHash(file);
				ad.strMD5=System.BitConverter.ToString(hash);
				file.Close();

				string key=ad.path+ad.name;

				if (DicFileMD5.ContainsKey(key) == false)
					DicFileMD5.Add(key, ad);
				else
					Debug.LogWarning("<Two File has the same name> name = " + filePath);
			}
		}
		
		string savePath = AssetBundleEditor.GetPlatformSavePath(bt) + "/VersionNum";
		if (Directory.Exists(savePath) == false)
			Directory.CreateDirectory(savePath);
		
		// 删除前一版的old数据
		if (File.Exists(savePath + "/VersionMD5-old.xml"))
		{
			System.IO.File.Delete(savePath + "/VersionMD5-old.xml");
		}
		
		// 如果之前的版本存在，则将其名字改为VersionMD5-old.xml
		if (File.Exists(savePath + "/VersionMD5.xml"))
		{
			System.IO.File.Move(savePath + "/VersionMD5.xml", savePath + "/VersionMD5-old.xml");
		}
		
		XmlDocument XmlDoc = new XmlDocument();
		XmlElement XmlRoot = XmlDoc.CreateElement("Files");
		XmlDoc.AppendChild(XmlRoot);
		foreach (KeyValuePair<string, AssetData> pair in DicFileMD5)
		{
			XmlElement xmlElem = XmlDoc.CreateElement("File");
			XmlRoot.AppendChild(xmlElem);

			AssetData ad=pair.Value;
			xmlElem.SetAttribute("FilePath", ad.path+ad.name);
			xmlElem.SetAttribute("AssetName",ad.name);
			xmlElem.SetAttribute("AssetType",ad.type);
			xmlElem.SetAttribute("MD5", ad.strMD5);
		}
		
		// 读取旧版本的MD5
		Dictionary<string, XmlElement> dicOldMD5 = ReadMD5File(savePath + "/VersionMD5-old.xml");
		// VersionMD5-old中有，而VersionMD5中没有的信息，手动添加到VersionMD5
		foreach (KeyValuePair<string, XmlElement> pair in dicOldMD5)
		{
			if (DicFileMD5.ContainsKey(pair.Key) == false)
			{
				XmlElement xmlElem=XmlDoc.CreateElement("File");
				XmlRoot.AppendChild(xmlElem);

				xmlElem.SetAttribute("FilePath", pair.Value.GetAttribute("FilePath"));
				xmlElem.SetAttribute("AssetName",pair.Value.GetAttribute("AssetName"));
				xmlElem.SetAttribute("AssetType",pair.Value.GetAttribute("AssetType"));
				xmlElem.SetAttribute("MD5", pair.Value.GetAttribute("MD5"));
			}
		}
		
		XmlDoc.Save(savePath + "/VersionMD5.xml");
		AssetDatabase.Refresh();

		CampareMD5ToGenerateVersionNum.Execute(bt);
	}
	
	static Dictionary<string, XmlElement> ReadMD5File(string fileName)
	{
		Dictionary<string, XmlElement> DicMD5 = new Dictionary<string, XmlElement>();
		
		// 如果文件不存在，则直接返回
		if (System.IO.File.Exists(fileName) == false)
			return DicMD5;
		
		XmlDocument XmlDoc = new XmlDocument();
		XmlDoc.Load(fileName);
		XmlElement XmlRoot = XmlDoc.DocumentElement;
		
		foreach (XmlNode node in XmlRoot.ChildNodes)
		{
			if ((node is XmlElement) == false)
				continue;
			
			string file = (node as XmlElement).GetAttribute("FilePath");
			
			if (DicMD5.ContainsKey(file) == false)
			{
				DicMD5.Add(file, node as XmlElement);
			}
		}
		return DicMD5;
	}

}
