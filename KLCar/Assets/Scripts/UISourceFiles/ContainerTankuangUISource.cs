using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/6/2015 5:35:56 PM
public partial class ContainerTankuangUIController : UIControllerBase {

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelJingqingqidai;
	public Vector3 UIOriginalPositionLabelJingqingqidai;

	public GameObject ButtonBg;
	public Vector3 UIOriginalPositionButtonBg;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelJingqingqidai=this.transform.FindChild ("LabelJingqingqidai").gameObject;
		UIOriginalPositionLabelJingqingqidai=this.LabelJingqingqidai.transform.localPosition;

		ButtonBg=this.transform.FindChild ("ButtonBg").gameObject;
		UIOriginalPositionButtonBg=this.ButtonBg.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

	}

}
