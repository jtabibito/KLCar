using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class CampareMD5ToGenerateVersionNum
{
	public struct AssetXmlData
	{
		string filePath;
		string assetName;
		string assetType;
		string num;
	}

	public static void Execute(UnityEditor.BuildTarget target)
	{
		// 读取新旧MD5列表
		string newVersionMD5 = AssetBundleEditor.GetPlatformSavePath(target)+"/VersionNum/VersionMD5.xml";//System.IO.Path.Combine(Application.dataPath, AssetBundleEditor.GameResourcesPath + platform + "/VersionNum/VersionMD5.xml");
		string oldVersionMD5 = AssetBundleEditor.GetPlatformSavePath(target)+"/VersionNum/VersionMD5-old.xml";//System.IO.Path.Combine(Application.dataPath, AssetBundleEditor.GameResourcesPath + platform + "/VersionNum/VersionMD5-old.xml");
		
		Dictionary<string, XmlElement> dicNewMD5Info = ReadMD5File(newVersionMD5);
		Dictionary<string, XmlElement> dicOldMD5Info = ReadMD5File(oldVersionMD5);
		
		// 读取版本号记录文件VersinNum.xml
		string oldVersionNumPath = AssetBundleEditor.GetPlatformSavePath(target)+"/VersionNum/VersionNum.xml";//System.IO.Path.Combine(Application.dataPath, AssetBundleEditor.GameResourcesPath + platform + "/VersionNum/VersionNum.xml");
		Dictionary<string, XmlElement> dicVersionNumInfo = ReadVersionNumFile(oldVersionNumPath);
		
		// 对比新旧MD5信息，并更新版本号，即对比dicNewMD5Info&&dicOldMD5Info来更新dicVersionNumInfo
		foreach (KeyValuePair<string, XmlElement> newPair in dicNewMD5Info)
		{
			int num;
			// 旧版本中有
			if (dicOldMD5Info.ContainsKey(newPair.Key))
			{
				// MD5一样，则不变
				// MD5不一样，则+1
				// 容错：如果新旧MD5都有，但是还没有版本号记录的，则直接添加新纪录，并且将版本号设为1
				if (dicVersionNumInfo.ContainsKey(newPair.Key) == false)
				{
					newPair.Value.SetAttribute("Num","1");
					dicVersionNumInfo.Add(newPair.Key, newPair.Value);
				}
				else if (newPair.Value.GetAttribute("MD5") != dicOldMD5Info[newPair.Key].GetAttribute("MD5"))
				{
					num = int.Parse(dicVersionNumInfo[newPair.Key].GetAttribute("Num"))+1;
					dicVersionNumInfo[newPair.Key].SetAttribute("Num",num.ToString()) ;
				}
			}
			else // 旧版本中没有，则添加新纪录，并=1
			{
				newPair.Value.SetAttribute("Num","1");
				dicVersionNumInfo.Add(newPair.Key, newPair.Value);
			}
		}
		// 不可能出现旧版本中有，而新版本中没有的情况，原因见生成MD5List的处理逻辑
		
		// 存储最新的VersionNum.xml
		SaveVersionNumFile(dicVersionNumInfo, oldVersionNumPath);
		AssetDatabase.Refresh();

		CreateAssetBundleForXmlVersion.Execute(target);
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
		
		XmlRoot = null;
		XmlDoc = null;
		
		return DicMD5;
	}
	
	static Dictionary<string, XmlElement> ReadVersionNumFile(string fileName)
	{
		Dictionary<string, XmlElement> DicVersionNum = new Dictionary<string, XmlElement>();
		
		// 如果文件不存在，则直接返回
		if (System.IO.File.Exists(fileName) == false)
			return DicVersionNum;
		
		XmlDocument XmlDoc = new XmlDocument();
		XmlDoc.Load(fileName);
		XmlElement XmlRoot = XmlDoc.DocumentElement;
		
		foreach (XmlNode node in XmlRoot.ChildNodes)
		{
			if ((node is XmlElement) == false)
				continue;
			
			string file = (node as XmlElement).GetAttribute("FilePath");
			
			if (DicVersionNum.ContainsKey(file) == false)
			{
				DicVersionNum.Add(file, node as XmlElement);
			}
		}
		
		XmlRoot = null;
		XmlDoc = null;
		
		return DicVersionNum;
	}
	
	static void SaveVersionNumFile(Dictionary<string, XmlElement> data, string savePath)
	{
		XmlDocument XmlDoc = new XmlDocument();
		XmlElement XmlRoot = XmlDoc.CreateElement("VersionNum");
		XmlDoc.AppendChild(XmlRoot);
		
		foreach (KeyValuePair<string, XmlElement> pair in data)
		{
			XmlElement xmlElem = XmlDoc.CreateElement("File");
			XmlRoot.AppendChild(xmlElem);
			xmlElem.SetAttribute("FilePath", pair.Key);
			xmlElem.SetAttribute("AssetName",pair.Value.GetAttribute("AssetName"));
			xmlElem.SetAttribute("AssetType",pair.Value.GetAttribute("AssetType"));
			xmlElem.SetAttribute("Num", pair.Value.GetAttribute("Num"));
		}
		
		XmlDoc.Save(savePath);
		XmlRoot = null;
		XmlDoc = null;
	}
	
}
