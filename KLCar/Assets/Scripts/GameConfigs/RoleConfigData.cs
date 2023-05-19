using UnityEngine;
using System.Collections;

public partial class RoleConfigData : GameConfigDataBase
{
	public string id;
	public string roleName;
	public string roleAvt;
	public string description;
	public int costTypeOfGain;
	public int costValueOfGain;
	public int maxLv;
	public string costGoldOfLvup;
	public int atr1;
	public string atr1AddValue;
	public int atr2;
	public string atr2AddValue;
	public int atr3;
	public string atr3AddValue;
	public string unlockFailTip;
	public string levelupFailTip;
	protected override string getFilePath ()
	{
		return "RoleConfigData";
	}
}
