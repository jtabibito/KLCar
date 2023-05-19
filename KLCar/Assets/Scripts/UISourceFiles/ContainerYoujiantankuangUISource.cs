using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/3/2015 9:56:20 AM
public partial class ContainerYoujiantankuangUIController : UIControllerBase {

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject LabelFajianren;
	public Vector3 UIOriginalPositionLabelFajianren;

	public GameObject LabelXitongwenzi;
	public Vector3 UIOriginalPositionLabelXitongwenzi;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		LabelFajianren=this.transform.FindChild ("LabelFajianren").gameObject;
		UIOriginalPositionLabelFajianren=this.LabelFajianren.transform.localPosition;

		LabelXitongwenzi=this.transform.FindChild ("LabelXitongwenzi").gameObject;
		UIOriginalPositionLabelXitongwenzi=this.LabelXitongwenzi.transform.localPosition;

	}

}
