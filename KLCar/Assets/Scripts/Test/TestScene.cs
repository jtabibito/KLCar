using ProtoBuf;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class TestScene : MonoBehaviour {
	/// <summary>
	/// 是否自动开始.
	/// </summary>
	public bool autoStart;
	public string carId="3";
	public string roleId="1";
	public string raceId="1";
	void Start () {

		RaceData rd = new RaceData ();
		rd.carId=carId;
		rd.roleId=roleId;
		rd.raceId=raceId;
		RaceConfigData c = RaceConfigData.GetConfigData<RaceConfigData> (rd.raceId);
//		RaceManager.Instance.InitRace( rd,c);

	}
	
 
	void Update () {
	
	}

}
