using UnityEngine;
using System.Collections;

public class ViewHallController : MonoBehaviour {
	GameObject HallModel;

	void Awake(){
		HallModel = this.transform.FindChild ("HallModel").gameObject;
	}

	// Use this for initialization
	void Start () {
		HallModelController hmc = this.HallModel.GetComponent<HallModelController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
