using UnityEngine;
using System.Collections;

///UISource File Create Data: 4/29/2015 6:48:51 PM
public partial class ContainerDebugMsgUIController : UIControllerBase {

	public GameObject LabelDebugMsg;
	public Vector3 UIOriginalPositionLabelDebugMsg;

	void Awake() {
		LabelDebugMsg=this.transform.FindChild ("LabelDebugMsg").gameObject;
		UIOriginalPositionLabelDebugMsg=this.LabelDebugMsg.transform.localPosition;

	}

}
