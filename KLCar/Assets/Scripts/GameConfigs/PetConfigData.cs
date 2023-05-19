using UnityEngine;
using System.Collections;

public partial class PetConfigData : GameConfigDataBase
{
	public string id;
	public string petName;
	public string petAvt;
	public string description;
	public int costTypeOfGain;
	public int costValueOfGain;
	public string skillPrefab;
	public string skillName;
	public string skillDescription;
	public int skillEffect1;
	public int skillEffect2;
	public int skillBelongTime;
	public int skillCooldown;
	public int cooldownAtBegin;
	public string unlockFailTip;
	protected override string getFilePath ()
	{
		return "PetConfigData";
	}
}
