using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/1/2015 10:01:51 AM
public partial class ContainerYoujianUIController : UIControllerBase
{

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject Button2Xitong;
	public Vector3 UIOriginalPositionButton2Xitong;

	public GameObject Container1Lingqu;
	public Vector3 UIOriginalPositionContainer1Lingqu;

	public GameObject Container2Xitong;
	public Vector3 UIOriginalPositionContainer2Xitong;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject Button1Lingqu;
	public Vector3 UIOriginalPositionButton1Lingqu;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		Button2Xitong=this.transform.FindChild ("Button2Xitong").gameObject;
		UIOriginalPositionButton2Xitong=this.Button2Xitong.transform.localPosition;

		Container1Lingqu=this.transform.FindChild ("Container1Lingqu").gameObject;
		UIOriginalPositionContainer1Lingqu=this.Container1Lingqu.transform.localPosition;

		Container2Xitong=this.transform.FindChild ("Container2Xitong").gameObject;
		UIOriginalPositionContainer2Xitong=this.Container2Xitong.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		Button1Lingqu=this.transform.FindChild ("Button1Lingqu").gameObject;
		UIOriginalPositionButton1Lingqu=this.Button1Lingqu.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

	}

}
