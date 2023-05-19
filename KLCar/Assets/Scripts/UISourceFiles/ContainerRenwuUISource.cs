using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/18/2015 2:48:07 PM
public partial class ContainerRenwuUIController : UIControllerBase {

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject Button2Shengyarenwu;
	public Vector3 UIOriginalPositionButton2Shengyarenwu;

	public GameObject Container1Richangrenwu;
	public Vector3 UIOriginalPositionContainer1Richangrenwu;

	public GameObject Container2Shengyarenwu;
	public Vector3 UIOriginalPositionContainer2Shengyarenwu;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject Button1Richangrenwu;
	public Vector3 UIOriginalPositionButton1Richangrenwu;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject SpriteTishidiandian2;
	public Vector3 UIOriginalPositionSpriteTishidiandian2;

	public GameObject SpriteTishidiandian1;
	public Vector3 UIOriginalPositionSpriteTishidiandian1;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		Button2Shengyarenwu=this.transform.FindChild ("Button2Shengyarenwu").gameObject;
		UIOriginalPositionButton2Shengyarenwu=this.Button2Shengyarenwu.transform.localPosition;

		Container1Richangrenwu=this.transform.FindChild ("Container1Richangrenwu").gameObject;
		UIOriginalPositionContainer1Richangrenwu=this.Container1Richangrenwu.transform.localPosition;

		Container2Shengyarenwu=this.transform.FindChild ("Container2Shengyarenwu").gameObject;
		UIOriginalPositionContainer2Shengyarenwu=this.Container2Shengyarenwu.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		Button1Richangrenwu=this.transform.FindChild ("Button1Richangrenwu").gameObject;
		UIOriginalPositionButton1Richangrenwu=this.Button1Richangrenwu.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		SpriteTishidiandian2=this.transform.FindChild ("SpriteTishidiandian2").gameObject;
		UIOriginalPositionSpriteTishidiandian2=this.SpriteTishidiandian2.transform.localPosition;

		SpriteTishidiandian1=this.transform.FindChild ("SpriteTishidiandian1").gameObject;
		UIOriginalPositionSpriteTishidiandian1=this.SpriteTishidiandian1.transform.localPosition;

	}

}
