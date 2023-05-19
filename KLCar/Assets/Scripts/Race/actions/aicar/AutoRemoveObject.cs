using UnityEngine;
using System.Collections;

public class AutoRemoveObject : MonoBehaviour {
	public int index;
	private CarEngine user;
	void Start () {
		user = RaceManager.Instance.userCar;
	}
	
	void Update () {
		int wnumber = RaceManager.Instance.wayPointNumber;
		int carIndex = index;
		int userIndex = user.fowardWaypointIndex; 
		float offset=MathUtils.getRoundDiff (userIndex,carIndex,wnumber);
		if (offset <= -10||offset>=100)
		{
			GameObject.DestroyObject(gameObject);
		}
	}
}
