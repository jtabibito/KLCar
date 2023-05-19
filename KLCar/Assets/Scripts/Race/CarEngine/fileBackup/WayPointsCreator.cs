using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsCreator : MonoBehaviour {

	private List<Transform> wayPoints=new List<Transform>();
	void Start () {
	
	}
	void Awake()
	{
		foreach(Transform tf in transform)
		{
			this.wayPoints.Add(tf);
		}
	}

	void Update () {
	
	}

	
	public List<Transform> WayPoints {
		get {
			return wayPoints;
		}
	}
	public Transform getNearWayPort(Transform tf)
	{
		return wayPoints[getNearWayPortIndex(tf)];
	}
	/**
	 * 取得最近的路点.
	 */
	public int getNearWayPortIndex(Transform tf)
	{
		float max = float.MaxValue;
		Transform maxObj =wayPoints[0];
		int index = 0;
		for(int i=1;i<wayPoints.Count;i++)
		{
			Transform p=WayPoints[i];
			float d=Vector3.Distance(p.position,tf.position);
			if(d<max)
			{
				index=i; 
				max=d;
			}
		}
		 
		return index;		
	}
	/**
	 * 取得指定位置的路点.
	 * 
	 */
	public Transform getWayPort(int index)
	{
		if (index >= wayPoints.Count) {
						index = index % wayPoints.Count;
				} else if (index < 0) {
			index=wayPoints.Count+index%wayPoints.Count;		
		}
		return wayPoints[index];
	}
}
