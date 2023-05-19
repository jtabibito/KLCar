using UnityEngine;
using System.Collections;

public partial class ContainerLoadingUIController : UIControllerBase {

	public GameObject LabelLaoding;
	public GameObject ProgressLoading;
	void Awake() {
		LabelLaoding=this.transform.FindChild ("LabelLaoding").gameObject;
		ProgressLoading=this.transform.FindChild ("ProgressLoading").gameObject;
	}

}
