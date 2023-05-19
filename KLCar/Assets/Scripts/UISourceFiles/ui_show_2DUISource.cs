using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/14/2015 3:54:41 PM
public partial class ui_show_2DUIController : UIControllerBase {

	public GameObject Camera3D;
	public Vector3 UIOriginalPositionCamera3D;

	public GameObject Car;
	public Vector3 UIOriginalPositionCar;

	public GameObject Car_role;
	public Vector3 UIOriginalPositionCar_role;

	public GameObject Pet;
	public Vector3 UIOriginalPositionPet;

	public GameObject Role;
	public Vector3 UIOriginalPositionRole;

	public GameObject Tai;
	public Vector3 UIOriginalPositionTai;

	public GameObject uiFx;
	public Vector3 UIOriginalPositionuiFx;

	public GameObject uiLight;
	public Vector3 UIOriginalPositionuiLight;

	public GameObject Anim;
	public Vector3 UIOriginalPositionAnim;

	void Awake() {
		Camera3D=this.transform.FindChild ("Camera3D").gameObject;
		UIOriginalPositionCamera3D=this.Camera3D.transform.localPosition;

		Car=this.transform.FindChild ("Car").gameObject;
		UIOriginalPositionCar=this.Car.transform.localPosition;

		Car_role=this.transform.FindChild ("Car_role").gameObject;
		UIOriginalPositionCar_role=this.Car_role.transform.localPosition;

		Pet=this.transform.FindChild ("Pet").gameObject;
		UIOriginalPositionPet=this.Pet.transform.localPosition;

		Role=this.transform.FindChild ("Role").gameObject;
		UIOriginalPositionRole=this.Role.transform.localPosition;

		Tai=this.transform.FindChild ("Tai").gameObject;
		UIOriginalPositionTai=this.Tai.transform.localPosition;

		uiFx=this.transform.FindChild ("uiFx").gameObject;
		UIOriginalPositionuiFx=this.uiFx.transform.localPosition;

		uiLight=this.transform.FindChild ("uiLight").gameObject;
		UIOriginalPositionuiLight=this.uiLight.transform.localPosition;

		Anim=this.transform.FindChild ("Anim").gameObject;
		UIOriginalPositionAnim=this.Anim.transform.localPosition;

	}

}
