using UnityEngine;
using System.Collections;

public partial class MissionConfigData : GameConfigDataBase
{
	public string id;
	public int  missionType;
	public string missionTypePar1;
	public string missionTypePar2;
	public string missionName;
	public string missionDescription;
	public string determinePoint;
	public int rewardType;
	public int rewardValue;
	public int showPar;
	public string condition1;
	public string condition1par1;
	public string condition1par2;
	public string condition1par3;
	public string condition2;
	public string condition2par1;
	public string condition2par2;
	public string condition2par3;
	public string condition3;
	public string condition3par1;
	public string condition3par2;
	public string condition3par3;
	public string condition4;
	public string condition4par1;
	public string condition4par2;
	public string condition4par3;
	public string condition5;
	public string condition5par1;
	public string condition5par2;
	public string condition5par3;
	protected override string getFilePath ()
	{
		return "MissionConfigData";
	}
}
