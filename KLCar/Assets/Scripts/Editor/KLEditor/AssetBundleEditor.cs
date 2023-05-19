using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleEditor : EditorWindow
{
		public static AssetBundleEditor window;
		public static UnityEditor.BuildTarget buildTarget = BuildTarget.StandaloneWindows;

		[MenuItem("KLEditor/AssetBundle/AssetBundle For Windows32", false, 1)]
		public static void ExecuteWindows32 ()
		{
				buildTarget = UnityEditor.BuildTarget.StandaloneWindows;
				CreateAssetBundle.Execute (buildTarget);
		}
	
		[MenuItem("KLEditor/AssetBundle/AssetBundle For IPhone", false, 2)]
		public static void ExecuteIPhone ()
		{
				buildTarget = UnityEditor.BuildTarget.iPhone;
				CreateAssetBundle.Execute (buildTarget);
		}
	
		[MenuItem("KLEditor/AssetBundle/AssetBundle For Mac", false, 3)]
		public static void ExecuteMac ()
		{
				buildTarget = UnityEditor.BuildTarget.StandaloneOSXUniversal;
				CreateAssetBundle.Execute (buildTarget);
		}
	
		[MenuItem("KLEditor/AssetBundle/AssetBundle For Android", false, 4)]
		public static void ExecuteAndroid ()
		{
				buildTarget = UnityEditor.BuildTarget.Android;
				CreateAssetBundle.Execute (buildTarget);
		}
	
		[MenuItem("KLEditor/AssetBundle/AssetBundle For WebPlayer", false, 5)]
		public static void ExecuteWebPlayer ()
		{
				buildTarget = UnityEditor.BuildTarget.WebPlayer;
				CreateAssetBundle.Execute (buildTarget);
		}
	
//		void OnGUI ()
//		{
//				if (GUI.Button (new Rect (10f, 10f, 200f, 50f), "(1)CreateAssetBundle")) {
//						CreateAssetBundle.Execute (buildTarget);
//						EditorUtility.DisplayDialog ("", "Step (1) Completed", "OK");
//				}
//		
//				if (GUI.Button (new Rect (10f, 80f, 200f, 50f), "(2)Generate MD5")) {
//						CreateMD5List.Execute (buildTarget);
//						EditorUtility.DisplayDialog ("", "Step (2) Completed", "OK");
//				}
//		
//				if (GUI.Button (new Rect (10f, 150f, 200f, 50f), "(3)Compare MD5")) {
//						CampareMD5ToGenerateVersionNum.Execute (buildTarget);
//						EditorUtility.DisplayDialog ("", "Step (3) Completed", "OK");
//				}
//		
//				if (GUI.Button (new Rect (10f, 220f, 200f, 50f), "(4)Build VersionNum.xml")) {
//						CreateAssetBundleForXmlVersion.Execute (buildTarget);
//						EditorUtility.DisplayDialog ("", "Step (4) Completed", "OK");
//				}
//		}
	
		public static string GetPlatformSavePath (UnityEditor.BuildTarget target)
		{
				string SavePath = "";
				switch (target) {
				case BuildTarget.StandaloneWindows:
						SavePath = "Assets/NetAssets/Windows32/";
						break;
				case BuildTarget.StandaloneWindows64:
						SavePath = "Assets/NetAssets/Windows64/";
						break;
				case BuildTarget.iPhone:
						SavePath = "Assets/NetAssets/IOS/";
						break;
				case BuildTarget.StandaloneOSXUniversal:
						SavePath = "Assets/NetAssets/Mac/";
						break;
				case BuildTarget.Android:
						SavePath = "Assets/NetAssets/Android/";
						break;
				case BuildTarget.WebPlayer:
						SavePath = "Assets/NetAssets/WebPlayer/";
						break;
				default:
						SavePath = "Assets/NetAssets/Other/";
						break;
				}
		
				if (Directory.Exists (SavePath) == false)
						Directory.CreateDirectory (SavePath);
		
				return SavePath;
		}
	
		public static string GetPlatformName (UnityEditor.BuildTarget target)
		{
				string platform = "Windows32";
				switch (target) {
				case BuildTarget.StandaloneWindows:
						platform = "Windows32";
						break;
				case BuildTarget.StandaloneWindows64:
						platform = "Windows64";
						break;
				case BuildTarget.iPhone:
						platform = "IOS";
						break;
				case BuildTarget.StandaloneOSXUniversal:
						platform = "Mac";
						break;
				case BuildTarget.Android:
						platform = "Android";
						break;
				case BuildTarget.WebPlayer:
						platform = "WebPlayer";
						break;
				default:
						break;
				}
				return platform;
		}
	
}
