using UnityEngine;
using System.Collections;

///UISource File Create Data: 7/9/2015 5:34:24 PM
public partial class ContainerLianjiUIController : UIControllerBase {

	public GameObject LabelShuzi;
	public Vector3 UIOriginalPositionLabelShuzi;

	public GameObject SpriteLianjiditu;
	public Vector3 UIOriginalPositionSpriteLianjiditu;

	void Awake() {
		LabelShuzi=this.transform.FindChild ("LabelShuzi").gameObject;
		UIOriginalPositionLabelShuzi=this.LabelShuzi.transform.localPosition;

		SpriteLianjiditu=this.transform.FindChild ("SpriteLianjiditu").gameObject;
		UIOriginalPositionSpriteLianjiditu=this.SpriteLianjiditu.transform.localPosition;

	}

}
