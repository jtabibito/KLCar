using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/8/2015 4:37:23 PM
public partial class ContainerMeiridengluUIController : UIControllerBase 
{

	public GameObject ButtonDiyitian2;
	public Vector3 UIOriginalPositionButtonDiyitian2;

	public GameObject ButtonDiyitian3;
	public Vector3 UIOriginalPositionButtonDiyitian3;

	public GameObject ButtonDiyitian4;
	public Vector3 UIOriginalPositionButtonDiyitian4;

	public GameObject ButtonDiyitian5;
	public Vector3 UIOriginalPositionButtonDiyitian5;

	public GameObject ButtonDiyitian6;
	public Vector3 UIOriginalPositionButtonDiyitian6;

	public GameObject ButtonDiyitian7;
	public Vector3 UIOriginalPositionButtonDiyitian7;

	public GameObject ButtonDiyitian1;
	public Vector3 UIOriginalPositionButtonDiyitian1;

	public GameObject ButtonYijianlingqu;
	public Vector3 UIOriginalPositionButtonYijianlingqu;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	void Awake() {
		ButtonDiyitian2=this.transform.FindChild ("ButtonDiyitian2").gameObject;
		UIOriginalPositionButtonDiyitian2=this.ButtonDiyitian2.transform.localPosition;

		ButtonDiyitian3=this.transform.FindChild ("ButtonDiyitian3").gameObject;
		UIOriginalPositionButtonDiyitian3=this.ButtonDiyitian3.transform.localPosition;

		ButtonDiyitian4=this.transform.FindChild ("ButtonDiyitian4").gameObject;
		UIOriginalPositionButtonDiyitian4=this.ButtonDiyitian4.transform.localPosition;

		ButtonDiyitian5=this.transform.FindChild ("ButtonDiyitian5").gameObject;
		UIOriginalPositionButtonDiyitian5=this.ButtonDiyitian5.transform.localPosition;

		ButtonDiyitian6=this.transform.FindChild ("ButtonDiyitian6").gameObject;
		UIOriginalPositionButtonDiyitian6=this.ButtonDiyitian6.transform.localPosition;

		ButtonDiyitian7=this.transform.FindChild ("ButtonDiyitian7").gameObject;
		UIOriginalPositionButtonDiyitian7=this.ButtonDiyitian7.transform.localPosition;

		ButtonDiyitian1=this.transform.FindChild ("ButtonDiyitian1").gameObject;
		UIOriginalPositionButtonDiyitian1=this.ButtonDiyitian1.transform.localPosition;

		ButtonYijianlingqu=this.transform.FindChild ("ButtonYijianlingqu").gameObject;
		UIOriginalPositionButtonYijianlingqu=this.ButtonYijianlingqu.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

	}

}
