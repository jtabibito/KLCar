using UnityEngine;
using System.Collections;

public class AutoRemoveAI : MonoBehaviour {
	private CarEngine car;
	private CarEngine user;
	void Start () {
		Transform t= transform.root.FindChild ("Engine");
		if (t != null)
		{
			car=t.GetComponent<CarEngine>();
		}
		user = RaceManager.Instance.userCar;
	}
	
	void Update () {
		int wnumber = RaceManager.Instance.wayPointNumber;
		int carIndex = car.fowardWaypointIndex;
		int userIndex = user.fowardWaypointIndex; 
		float offset=MathUtils.getRoundDiff (userIndex,carIndex,wnumber);
		if (offset <= -10||offset>=100)
		{
			RaceManager.Instance.destoryCar(car);
		}
	}
}
