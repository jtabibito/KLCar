using UnityEngine;
using System.Collections;

public class SceneLoader{
	
	static string sceneName="";

	public static string SceneName {
		get {
			return sceneName;
		}
	}

	static AsyncOperation async;

	public static AsyncOperation Async {
		get {
			return async;
		}
	}

	public delegate void OnSceenLoadOver ();
	static OnSceenLoadOver onSceenLoadOver;

	public static void BeginLoadScene(string SceneName,OnSceenLoadOver oslo)
	{
		if(sceneName!="")
		{
			throw new UnityException("not load over");
		}
		sceneName = SceneName;
		onSceenLoadOver = oslo;

		PanelMainUIController.Instance.AddUI (PanelMainUIController.UILayer.L_Top, "ContainerLoading");
		GlobalSetting.StartCoroutine(LoadScene());
//		GameObject loader=(GameObject)GameObject.Instantiate(ResourceManager.Load<GameObject>("Prefabs/SceneView/SceneLoader"));
	}

	static IEnumerator LoadScene()
	{
		yield return new WaitForEndOfFrame ();
		async = Application.LoadLevelAsync (SceneName);
		yield return async;
		sceneName = "";
		if(onSceenLoadOver!=null)
		{
			onSceenLoadOver();
			onSceenLoadOver=null;
		}
	}

}
