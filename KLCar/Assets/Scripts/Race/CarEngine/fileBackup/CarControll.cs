using UnityEngine;
using System.Collections;
/**
 * 能够手动控制车的控制器.
 * 
 */
public class CarControll : MonoBehaviour {
	//public bool speed=10;
	/**
	 * 可以控制的方向及速度.{前,后,左,右}
	 */
	public float[] controllDirect={0,0,10,10};
	private CarInfo carInfo;
	void Start () {
		carInfo = GetComponent <CarInfo>();
	}

	void Update () {
		if (carInfo != null && !carInfo.isCanControll) {
			return ;
				}
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Debug.Log (h+" "+v);
		if (h > 0 && controllDirect [3] == 0)
						h = 0;
		else if (h < 0 && controllDirect [2] == 0)
			h = 0;
		if (v > 0 && controllDirect [0] == 0)
			v = 0;
		else if (v < 0 && controllDirect [1] == 0)
			v = 0;
		h*=h>0?controllDirect[3]:controllDirect[2];
		v*=v>0?controllDirect[0]:controllDirect[1];
		Vector3 vct = new Vector3 (0, 0, v);
		rigidbody.AddForce (vct);
		rigidbody.AddRelativeTorque (0,h,0);
		//rigidbody.MoveRotation (rigidbody.rotation+Quaternion.AngleAxis(30,rigidbody.ro));

	}
}
