using UnityEngine;
using System.Collections;

public partial class NickNameFilterConfigDa : GameConfigDataBase
{
	public string id;
	public string illegalName;
	protected override string getFilePath ()
	{
		return "NickNameFilterConfigDa";
	}
}
