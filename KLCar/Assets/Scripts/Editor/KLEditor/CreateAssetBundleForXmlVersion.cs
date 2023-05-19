using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CreateAssetBundleForXmlVersion
{
	public static void Execute(UnityEditor.BuildTarget target)
	{
		string SavePath = AssetBundleEditor.GetPlatformSavePath(target);
		Object obj = AssetDatabase.LoadAssetAtPath(SavePath + "VersionNum/VersionNum.xml", typeof(Object));
		BuildPipeline.BuildAssetBundle(obj, null, SavePath + "VersionNum/VersionNum.assetBundle", BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, target);
		
		AssetDatabase.Refresh();
	}
	
}
