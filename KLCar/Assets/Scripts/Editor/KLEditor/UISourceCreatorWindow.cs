using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class UISourceCreatorWindow : EditorWindow {

	[MenuItem("KLEditor/Window/UI Source Creator Window")]
	static void Init () 
	{
		UISourceCreatorWindow window  = EditorWindow.GetWindow<UISourceCreatorWindow>();
	}


	GameObject selectGameObject;
	List<GameObject> UIPrefabList;
	void OnGUI()
	{
		GUILayout.Label ("选择需要生成界面源文件的对象");
		if(GUILayout.Button("生成界面源文件"))
		{
			if(this.selectGameObject!=null)
			{
				CreatUISourceFile();
			}
		}

		this.selectGameObject = GetSelectedPrefab ();
		if(this.selectGameObject!=null)
		{
			GUILayout.Label (this.selectGameObject.name);
		}
	}

	void OnSelectionChange()
	{
		Repaint ();
	}

	void CreatUISourceFile()
	{
		string gameObjectName= this.selectGameObject.name;
		string fileName = gameObjectName + "UISource";
		string className = gameObjectName + "UIController";
		StreamWriter sw = new StreamWriter (Application.dataPath + "/Scripts/UISourceFiles/"+fileName+".cs");
		sw.WriteLine(
			"using UnityEngine;\nusing System.Collections;\n");

		sw.WriteLine ("///UISource File Create Data: "+ System.DateTime.Now.ToString());
		sw.WriteLine ("public partial class "+className+" : UIControllerBase {"+"\n");
		foreach(Transform tf in this.selectGameObject.transform)
		{
			string childName=tf.gameObject.name;
			sw.WriteLine("\t"+"public GameObject "+childName+";");
			sw.WriteLine("\t"+"public Vector3 UIOriginalPosition"+childName+";\n");
		}
		sw.WriteLine ("\t" + "void Awake() {");
		foreach(Transform tf in this.selectGameObject.transform)
		{
			string childName=tf.gameObject.name;
			sw.WriteLine("\t\t"+childName+"=this.transform.FindChild (\""+childName+"\").gameObject;");
			sw.WriteLine("\t\tUIOriginalPosition"+childName+"=this."+childName+".transform.localPosition;\n");
		}
		sw.WriteLine ("\t" + "}" + "\n");
		sw.WriteLine ("}");
		sw.Flush ();
		sw.Close ();

//		this.selectGameObject.AddComponent (className);
	}

	GameObject GetSelectedPrefab ()
	{
		List<GameObject> gos = new List<GameObject> ();
		
		if (Selection.activeGameObject!=null)
		{
			return Selection.activeGameObject;
		}
		return null;
	}
}
