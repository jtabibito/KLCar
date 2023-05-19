using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class CreatConfigDataFile :EditorWindow {

	static string writePath="/Scripts/GameConfigs/";
	static Object selectObj;

	[MenuItem("KLEditor/Window/Config Creator Window")]
	static void ByWindow () 
	{
		CreatConfigDataFile window  = EditorWindow.GetWindow<CreatConfigDataFile>();
	}

	[MenuItem ("Assets/生成静态配置文件")]
	static void BySelection()
	{
		Object nowObj=Selection.activeObject;
		string path=AssetDatabase.GetAssetPath(nowObj);
		if(path.Substring(path.Length-4,4)==".csv")
		{
			selectObj=nowObj;
			CreatConfigFile();
		}
	}

	void OnGUI()
	{
		GUILayout.Label("设定配置数据文件生成路径");
		writePath=GUILayout.TextField (writePath);
		GUILayout.Label ("选择需要生成配置数据的CSV文件");
		if(GUILayout.Button("生成"))
		{
			if(selectObj!=null)
			{
				CreatConfigFile();
			}
		}

		if(Selection.activeObject!=null)
		{
			string path=AssetDatabase.GetAssetPath(Selection.activeObject);
			if(path.Substring(path.Length-4,4)==".csv")
			{
				selectObj=Selection.activeObject;
				GUILayout.Label (path);
			}
		}
	}

	void OnSelectionChange()
	{
		Repaint ();
	}

	static void CreatConfigFile()
	{
		string fileName=selectObj.name;
		string className = fileName;
		StreamWriter sw = new StreamWriter (Application.dataPath + writePath +className+".cs");
		
		sw.WriteLine ("using UnityEngine;\nusing System.Collections;\n");
		sw.WriteLine ("public partial class " + className + " : GameConfigDataBase");
		sw.WriteLine ("{");
		
		string filePath = AssetDatabase.GetAssetPath (selectObj);
		CsvStreamReader csr=new CsvStreamReader(filePath);
		for(int colNum=1;colNum<csr.ColCount+1;colNum++)
		{
			string fieldName=csr[1,colNum];
			string fieldType=csr[2,colNum];
			sw.WriteLine ("\t" + "public " + fieldType + " " + fieldName + ";" + "");
		}
		sw.WriteLine ("\t" + "protected override string getFilePath ()");
		sw.WriteLine ("\t" + "{");
//		filePath=filePath.Replace("Assets/Resources/","");
//		filePath=filePath.Substring(0,filePath.LastIndexOf('.'));
		sw.WriteLine ("\t\t" + "return " + "\"" + fileName + "\";");
		sw.WriteLine ("\t" + "}");
		sw.WriteLine ("}");
		
		sw.Flush ();
		sw.Close ();
		AssetDatabase.Refresh();
	}
}
