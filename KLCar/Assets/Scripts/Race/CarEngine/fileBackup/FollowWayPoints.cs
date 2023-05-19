using UnityEngine;
using System.Collections;
/**
 * 跟谁路径路点运动的对象.
 */
public class FollowWayPoints : MonoBehaviour {
	private int currentIndex = -1;
	private Transform currentWayPoint;
	private WayPointsCreator wayPointsCreator;
	public bool enabel;
	private CarInfo carInfo;
	void Start () {
		carInfo=GetComponent <CarInfo>();
	}
	
	 
	void Update () {
		if (!enabel) {
			return;		
		}
		if (wayPointsCreator == null) {
			GameObject obj = GameObject.Find("WayPoints_create");
			wayPointsCreator=obj.GetComponent<WayPointsCreator>();
			//currentIndex = wayPointsCreator.getNearWayPortIndex (transform)-1;
			 
		}
		 
		currentIndex = wayPointsCreator.getNearWayPortIndex (transform);
		currentWayPoint = wayPointsCreator.getWayPort (currentIndex+1);
		//rigidbody.position = currentWayPoint.position+new Vector3(carInfo.offsetX,0,0);
		 
		//Vector3 v = currentWayPoint.rotation;
 		//rigidbody.velocity =v.normalized*carInfo.speed;

	}
}
