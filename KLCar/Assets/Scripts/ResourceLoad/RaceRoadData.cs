using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RaceRoadData :ScriptableObject {

	public string sceneName;
	public List<WaypointTF> wayPoints;
}

[Serializable]
public class WaypointTF{
	public Vector3 position;
	public Quaternion rotation;
}
