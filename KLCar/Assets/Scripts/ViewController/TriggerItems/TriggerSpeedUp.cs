using UnityEngine;
using System.Collections;

public class TriggerSpeedUp :TriggerItemBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTriggerCarHandler (CarEngine car)
	{
//		throw new System.NotImplementedException ();
		CarState carState = new CarState ();
//		carState.stateType = CarState.CarStateType.cst_speedUp;
//		carState.effectTime = 3f;
//		car.AddState (carState);
		car.playFire (3f);

	}
}
