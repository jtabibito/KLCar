using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/2/2015 3:32:36 PM
public partial class ContainerShezhiUIController : UIControllerBase 
{

	public GameObject ButtonGuanbi;
	public Vector3 UIOriginalPositionButtonGuanbi;

	public GameObject ButtonShengyinguan;
	public Vector3 UIOriginalPositionButtonShengyinguan;

	public GameObject ButtonYinyuekai;
	public Vector3 UIOriginalPositionButtonYinyuekai;

	public GameObject ButtonShengyinkai;
	public Vector3 UIOriginalPositionButtonShengyinkai;

	public GameObject ButtonYinyueguan;
	public Vector3 UIOriginalPositionButtonYinyueguan;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject LabelGuanyu;
	public Vector3 UIOriginalPositionLabelGuanyu;

	void Awake() {
		ButtonGuanbi=this.transform.FindChild ("ButtonGuanbi").gameObject;
		UIOriginalPositionButtonGuanbi=this.ButtonGuanbi.transform.localPosition;

		ButtonShengyinguan=this.transform.FindChild ("ButtonShengyinguan").gameObject;
		UIOriginalPositionButtonShengyinguan=this.ButtonShengyinguan.transform.localPosition;

		ButtonYinyuekai=this.transform.FindChild ("ButtonYinyuekai").gameObject;
		UIOriginalPositionButtonYinyuekai=this.ButtonYinyuekai.transform.localPosition;

		ButtonShengyinkai=this.transform.FindChild ("ButtonShengyinkai").gameObject;
		UIOriginalPositionButtonShengyinkai=this.ButtonShengyinkai.transform.localPosition;

		ButtonYinyueguan=this.transform.FindChild ("ButtonYinyueguan").gameObject;
		UIOriginalPositionButtonYinyueguan=this.ButtonYinyueguan.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		LabelGuanyu=this.transform.FindChild ("LabelGuanyu").gameObject;
		UIOriginalPositionLabelGuanyu=this.LabelGuanyu.transform.localPosition;

	}

}
