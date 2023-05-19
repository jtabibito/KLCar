using UnityEngine;
using System.Collections;

public class CarForce : MonoBehaviour {

	public Vector3 force = new Vector3 (0,0,0);
	public Vector3 startSeed=new Vector3(0,0,0);
	void Start () {
		rigidbody.velocity = startSeed;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.AddForce (force);
		//rigidbody.velocity = force;
	}
}
