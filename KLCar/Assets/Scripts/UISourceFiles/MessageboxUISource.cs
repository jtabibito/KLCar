using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/21/2015 8:11:40 PM
public partial class MessageboxUIController : UIControllerBase {

	public GameObject Label;
	public Vector3 UIOriginalPositionLabel;

	public GameObject SpriteDi;
	public Vector3 UIOriginalPositionSpriteDi;

	void Awake() {
		Label=this.transform.FindChild ("Label").gameObject;
		UIOriginalPositionLabel=this.Label.transform.localPosition;

		SpriteDi=this.transform.FindChild ("SpriteDi").gameObject;
		UIOriginalPositionSpriteDi=this.SpriteDi.transform.localPosition;

	}

}
