using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/8/2015 11:28:05 AM
public partial class ContainerBeforeLoginUIController : UIControllerBase {

	public GameObject background;
	public Vector3 UIOriginalPositionbackground;

	public GameObject logo;
	public Vector3 UIOriginalPositionlogo;

	void Awake() {
		background=this.transform.FindChild ("background").gameObject;
		UIOriginalPositionbackground=this.background.transform.localPosition;

		logo=this.transform.FindChild ("logo").gameObject;
		UIOriginalPositionlogo=this.logo.transform.localPosition;

	}

}
