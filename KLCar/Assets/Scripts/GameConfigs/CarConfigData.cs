using UnityEngine;
using System.Collections;

public partial class CarConfigData : GameConfigDataBase
{
	public string id;
	public string carName;
	public string carAvt;
	public string description;
	public int beginAcc;
	public int beginSpeed;
	public int beginHandler;
	public int costTypeOfGain;
	public int costValueOfGain;
	public int accMaxLv;
	public string accLvupCostGold;
	public string accLvupValue;
	public int accLvupMaxValue;
	public int speedMaxLv;
	public string speedLvupCostGold;
	public string speedLvupValue;
	public int speedLvupMaxValue;
	public int handlerMaxLv;
	public string handlerLvupCostGold;
	public string handlerLvupValue;
	public int handlerLvupMaxValue;
	public string unlockFailTip;
	public string levelupFailTip;
	protected override string getFilePath ()
	{
		return "CarConfigData";
	}
}
