using UnityEngine;
using System.Collections;

///UISource File Create Data: 4/27/2015 3:38:22 PM
public partial class ui_show_3DUIController : UIControllerBase {

	public GameObject Car;
	public Vector3 UIOriginalPositionCar;

	public GameObject Car_role;
	public Vector3 UIOriginalPositionCar_role;

	public GameObject anim_ui;
	public Vector3 UIOriginalPositionanim_ui;

	public GameObject Pet;
	public Vector3 UIOriginalPositionPet;

	public GameObject Role;
	public Vector3 UIOriginalPositionRole;

	public GameObject Tai;
	public Vector3 UIOriginalPositionTai;

	public GameObject camera;
	public Vector3 UIOriginalPositioncamera;

	void Awake() {
		Car=this.transform.FindChild ("Car").gameObject;
		UIOriginalPositionCar=this.Car.transform.localPosition;

		Car_role=this.transform.FindChild ("Car_role").gameObject;
		UIOriginalPositionCar_role=this.Car_role.transform.localPosition;

		anim_ui=this.transform.FindChild ("anim_ui").gameObject;
		UIOriginalPositionanim_ui=this.anim_ui.transform.localPosition;

		Pet=this.transform.FindChild ("Pet").gameObject;
		UIOriginalPositionPet=this.Pet.transform.localPosition;

		Role=this.transform.FindChild ("Role").gameObject;
		UIOriginalPositionRole=this.Role.transform.localPosition;

		Tai=this.transform.FindChild ("Tai").gameObject;
		UIOriginalPositionTai=this.Tai.transform.localPosition;

		camera=this.transform.FindChild ("camera").gameObject;
		UIOriginalPositioncamera=this.camera.transform.localPosition;

	}

}
