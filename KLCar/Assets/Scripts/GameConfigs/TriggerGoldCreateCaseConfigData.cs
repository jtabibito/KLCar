using UnityEngine;
using System.Collections;

public partial class TriggerGoldCreateCaseConfigData : GameConfigDataBase
{
	public string id;
	public int createSpace;
	public string createPositions;
	protected override string getFilePath ()
	{
		return "TriggerGoldCreateCaseConfigData";
	}
}
