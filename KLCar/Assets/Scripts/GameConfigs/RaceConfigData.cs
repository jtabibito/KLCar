using UnityEngine;
using System.Collections;

public partial class RaceConfigData : GameConfigDataBase
{
	public string id;
	public string sceneName;
	public int minOffset;
	public int maxOffset;
	public int obstacleCarCreateDistance;
	public int obstacleCarDestroyDistance;
	public int obstacleCarCreatePro;
	public int obstacleCarNum;
	public int obstacleCarCreateSpace;
	public int triggerGoldCreatDistance;
	public int triggerGoldDestroyDistance;
	public int triggerGoldCaseCreatPro;
	public int triggerGoldCaseCreatSpace;
	public string triggerGoldCases;
	public int specialObstacleCar1Pro;
	public int specialObstacleCar2Pro;
	public int raceMode;
	public int racePar1;
	public int racePar2;
	public int racePar3;
	public string aiCars;
	public int storyIndex;
	protected override string getFilePath ()
	{
		return "RaceConfigData";
	}
}
