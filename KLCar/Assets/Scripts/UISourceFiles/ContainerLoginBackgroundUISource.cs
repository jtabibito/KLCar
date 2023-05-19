using UnityEngine;
using System.Collections;

public partial class ContainerLoginBackgroundUIController : UIControllerBase {

	public GameObject Sprite1;
	public GameObject Sprite2;
	void Awake() {
		Sprite1=this.transform.FindChild ("Sprite1").gameObject;
		Sprite2=this.transform.FindChild ("Sprite2").gameObject;
	}

}
