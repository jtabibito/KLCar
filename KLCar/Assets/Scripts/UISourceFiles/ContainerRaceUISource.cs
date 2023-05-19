using UnityEngine;
using System.Collections;

public partial class ContainerRaceUIController : UIControllerBase {

	public GameObject ButtonLeftTurn;
	public GameObject ButtonRightTurn;
	void Awake() {
		ButtonLeftTurn=this.transform.FindChild ("ButtonLeftTurn").gameObject;
		ButtonRightTurn=this.transform.FindChild ("ButtonRightTurn").gameObject;
	}
}
