using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/9/2015 9:22:16 AM
public partial class ContainerGonggaoUIController : UIControllerBase 
{

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject LabelGuanyu;
	public Vector3 UIOriginalPositionLabelGuanyu;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		LabelGuanyu=this.transform.FindChild ("LabelGuanyu").gameObject;
		UIOriginalPositionLabelGuanyu=this.LabelGuanyu.transform.localPosition;

	}

}
