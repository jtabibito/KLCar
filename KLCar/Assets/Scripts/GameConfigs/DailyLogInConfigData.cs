using UnityEngine;
using System.Collections;

public partial class DailyLogInConfigData : GameConfigDataBase
{
	public string id;
	public int day1type;
	public int day1value;
	public int day2type;
	public int day2value;
	public int day3type;
	public int day3value;
	public int day4type;
	public int day4value;
	public int day5type;
	public int day5value;
	public int day6type;
	public int day6value;
	public int day7type;
	public int day7value;
	protected override string getFilePath ()
	{
		return "DailyLogInConfigData";
	}
}
