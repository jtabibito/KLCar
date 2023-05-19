using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/4/2015 11:25:10 AM
public partial class ContainerTouxiangUIController : UIControllerBase 
{

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject ScrollView;
	public Vector3 UIOriginalPositionScrollView;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		ScrollView=this.transform.FindChild ("ScrollView").gameObject;
		UIOriginalPositionScrollView=this.ScrollView.transform.localPosition;

	}

}
