using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/6/2015 5:29:43 PM
public partial class ContainerToastUIController : UIControllerBase {

	public GameObject LabelMesage;
	public Vector3 UIOriginalPositionLabelMesage;

	void Awake() {
		LabelMesage=this.transform.FindChild ("LabelMesage").gameObject;
		UIOriginalPositionLabelMesage=this.LabelMesage.transform.localPosition;

	}

}
