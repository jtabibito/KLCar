using UnityEngine;
using System.Collections;

///UISource File Create Data: 7/1/2015 6:07:19 PM
public partial class ContainerOperationzantingshezhiUIController : UIControllerBase {

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJixuyouxi;
	public Vector3 UIOriginalPositionButtonJixuyouxi;

	public GameObject ButtonChongxinkaishi;
	public Vector3 UIOriginalPositionButtonChongxinkaishi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	void Awake() {
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJixuyouxi=this.transform.FindChild ("ButtonJixuyouxi").gameObject;
		UIOriginalPositionButtonJixuyouxi=this.ButtonJixuyouxi.transform.localPosition;

		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		UIOriginalPositionButtonChongxinkaishi=this.ButtonChongxinkaishi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

	}

}
