using UnityEngine;
using System.Collections;

public class GameResourcesManager
{
	public static string uiPath = "UIResources/UIPrefab/";
	public static string skillPath = "Prefabs/effects/skills/";				//道具技能
	public static string petSkillPath = "Prefabs/effects/pets/";			//宠物技能
	public static string carAvtPath = "Prefabs/GameObjs/CarAvt/";
	public static string petAvtPath = "Prefabs/GameObjs/PetAvt/";
	public static string roleAvtPath = "Prefabs/GameObjs/RoleAvt/";
	public static string lightAvtPath = "Prefabs/GameObjs/LightAvt/";
	public static string gameConfigPath = "GameConfigResources/";
	public static string sceneObjectPath = "Prefabs/SceneObjs/";
	public static string raceObjectPath="Prefabs/GameObjs/Race/";
	public static string sceneXmlPath="SceneResources/SceneXmls/";
	public static string actionPrefab="Prefabs/GameObjs/actions/";

	public static GameObject GetSceneObject (string pName)
	{
		return ResourceManager.Load<GameObject> (sceneObjectPath + pName);
	}

	public static string GetGameConfigText (string configName)
	{
		return ResourceManager.Load<TextAsset> (gameConfigPath + configName).text;
	}

	public static string GetSceneXmlText(string sceneName)
	{
		return ResourceManager.Load<TextAsset>(sceneXmlPath+sceneName+"Xml").text;
	}

	public static GameObject GetUIPrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (uiPath + pName);
	}

	/// <summary>
	/// 得到技能 Prefeb 预制件
	/// Add by maojudong
	/// 2015年4月17日15:48:13
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetSkillPrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (skillPath + pName);
	}

	/// <summary>
	/// 得到宠物技能 Prefeb 预制件
	/// Add by maojudong
	/// 2015年4月17日15:48:13
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetPetSkillPrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (petSkillPath + pName);
	}

	/// <summary>
	/// 得到车 Prefeb 预制件
	/// Add by maojudong
	/// 2015年4月27日15:58:50
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetCarAvtPrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (carAvtPath+pName);
	}

	/// <summary>
	/// 得到宠物 Prefeb 预制件---界面上展示
	/// Add by maojudong
	/// 2015年4月27日15:58:57
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetPetAvtPrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (petAvtPath+pName);
	}

	/// <summary>
	/// 得到赛程对象 Prefeb 预制件
	/// Add by maojudong
	/// 2015年5月13日19:28:26
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetRaceObject (string pName)
	{
		return ResourceManager.Load<GameObject> (raceObjectPath+pName);
	}

	/// <summary>
	/// 得到角色 Prefeb 预制件
	/// Add by maojudong
	/// 2015年4月27日15:59:02
	/// </summary>
	/// <returns> 根据Prefeb创建好的GameObject对象 </returns>
	/// <param name="pName">预制件的名称</param>
	public static GameObject GetRolePrefab (string pName)
	{
		return ResourceManager.Load<GameObject> (roleAvtPath+pName);
	}
	/// <summary>
	/// 获得用来执行动作脚本的prefab.
	/// </summary>
	/// <returns>The action prefab.</returns>
	/// <param name="name">Name.</param>
	public static GameObject getActionPrefab(string name)
	{
		return ResourceManager.Load<GameObject> (actionPrefab+name);
	}
}
