using UnityEngine;
using System.Collections;

/// <summary>
/// Race data.
/// 比赛数据
/// </summary>
public class RaceData {
	public string carId;
	CarConfigData carConfig;

	public CarConfigData CarConfig {
		get {
			if(carConfig==null)
			{
				carConfig=CarConfigData.GetConfigData<CarConfigData>(carId);
			}

		
			return carConfig;
		}
	}

	public string roleId;
	RoleConfigData roleConfig;

	public RoleConfigData RoleConfig {
		get {
			if(roleConfig==null)
			{
				roleConfig=RoleConfigData.GetConfigData<RoleConfigData>(roleId);
			}
			return roleConfig;
		}
	}

	public string petId;
	PetConfigData petConfig;

	public PetConfigData PetConfig {
		get {
			if(petConfig==null)
			{
				petConfig=PetConfigData.GetConfigData<PetConfigData>(petId);
			}
			return petConfig;
		}
	}

	public string storyId;

	public int Feidanitem1Num;
	public int Hudunitem2Num;
	public int Yinshenitem3Num;
	public int Jiasuitem4Num;

	public string raceId;
}
