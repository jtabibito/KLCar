using UnityEngine;
using System.Collections;

public partial class NickNameConfigData : GameConfigDataBase
{
	public string id;
	public string sex;
	public string symbol;
	public string name1;
	public string name2;
	public string name3;
	protected override string getFilePath ()
	{
		return "NickNameConfigData";
	}
}
