using UnityEngine;
using System.Collections;
using UnityEditor;

public class CleanTools{
	[MenuItem("KLEditor/CleanTools/Clean Player Datas")]
	static void ExcuteCleanPlayerDatas() 
	{
		PlayerPrefs.DeleteKey ("playerInfo");
	}

	[MenuItem("KLEditor/CleanTools/Clean Resources Cache")]
	static void ExcuteCleanResourcesCache()
	{
		Caching.CleanCache();
	}
}
