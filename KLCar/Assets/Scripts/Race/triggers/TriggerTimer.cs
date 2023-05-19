using UnityEngine;
using System.Collections;

public class TriggerTimer : TriggerObjectBase {

	public float triggerTime;

	public float nowTime=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		nowTime+=Time.deltaTime;
		if(nowTime>triggerTime)
		{
			onTrigger (this.gameObject);
			nowTime=0;
		}
	}
}
