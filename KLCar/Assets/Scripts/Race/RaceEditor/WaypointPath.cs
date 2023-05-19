using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class WaypointPath : MonoBehaviour
{

	public string folderName = "WayPointsEditor";
    public string preName = "Waypoint";
    public Material waypointMaterial;
    public bool batchCreating = false;
	private List<Transform> wayPoints;

	public List<Transform> WayPoints {
		get {
			if(wayPoints==null)
			{
				GameObject wayPointsParentObj=GameObject.Find (folderName + "_create");
				if(wayPointsParentObj==null)
				{
					throw new UnityException("no way points parent");
				}
				this.wayPoints=new List<Transform>();
				foreach(Transform tf in wayPointsParentObj.transform)
				{
					this.wayPoints.Add(tf);
				}
			}
			return wayPoints;
		}
	}

	private static WaypointPath instance;

	public static WaypointPath Instance {
		get {
			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}
    
}
