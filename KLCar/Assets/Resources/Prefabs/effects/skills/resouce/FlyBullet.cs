using UnityEngine;
using System.Collections;

public class FlyBullet : MonoBehaviour {

	public float force=1500;
	public float aliveTime=2;
	float nowTime=0;
	void Start ()
	{
		this.rigidbody.AddForce(Vector3.forward*force);
	}
	
	// Update is called once per frame
	void Update () {
		if(nowTime<aliveTime)
		{
			nowTime+=Time.deltaTime;
		}
		else
		{
			FlyOver();
		}
	}

	void FlyOver()
	{
		Destroy(this.gameObject);
	}
}
