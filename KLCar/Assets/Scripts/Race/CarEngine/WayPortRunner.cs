using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WayPortRunner : MonoBehaviour {

	private List<Transform> _waypoints ;
	private int _currentIndex;
	public bool isFaword=true;
	private float step=0;
	void Start () {
		 
	}

	void Update () {
	
	}
	void LateUpdate()
	{
		int curr = currentIndex;
		int index = currentIndex;
		float min = getDistance (index);
		int le=(int)Mathf.Ceil(waypoints.Count/2);
		float value = 0;
		for (int i=1; i<le; i++) {
			value=getDistance(curr+i);
			if(min>value)
			{
				index=curr+i;
				min=value;
			}
			value=getDistance(curr-i);
			if(min>value)
			{
				index=curr+i;
				min=value;
			}
			if(min<=step)
			{
				break;
			}
		}
		_currentIndex = getIndex(index);
	}
	public Transform currentWayPoint 
	{
		get
		{
			return waypoints[_currentIndex];
		}
	}
	public List<Transform> waypoints 
	{
		get
		{
			if(_waypoints==null)
			{
				_waypoints=RaceManager.Instance.WayPoints;
				step = Vector3.Distance (getWayPoint (0).position, getWayPoint (1).position);
				LateUpdate();
			}
			return _waypoints;
		}
	}
	public Transform getWayPoint(int index)
	{
		return waypoints[getIndex(index)];
	}
	public float getDistance(int index)
	{
		return Vector3.Distance (transform.position,getWayPoint(index).position);
	}
	public int getIndex(int index)
	{
		return (index + waypoints.Count) % waypoints.Count;
	}
	public int currentIndex 
	{
		get
		{
			return _currentIndex;
		}
	}

}
