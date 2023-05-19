using UnityEngine;
using System.Collections;

public partial class StoryConfigData : GameConfigDataBase
{
	public string id;
	public string chapterIndex;
	public int chapterIndexValue;
	public string chapterName;
	public string stageIndex;
	public int stageIndexValue;
	public string stageName;
	public string stageDescription;
	public string raceId;
	public int costPower;
	public int rewardType;
	public int rewardValue;
	public int weight;
	public string nextId;
	protected override string getFilePath ()
	{
		return "StoryConfigData";
	}
}
