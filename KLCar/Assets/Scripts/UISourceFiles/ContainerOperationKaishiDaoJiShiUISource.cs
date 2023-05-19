using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/12/2015 3:27:38 PM
public partial class ContainerOperationKaishiDaoJiShiUIController : UIControllerBase {

	public GameObject SpriteNum3;
	public Vector3 UIOriginalPositionSpriteNum3;

	public GameObject SpriteNum2;
	public Vector3 UIOriginalPositionSpriteNum2;

	public GameObject SpriteNum1;
	public Vector3 UIOriginalPositionSpriteNum1;

	public GameObject SpriteNumgo;
	public Vector3 UIOriginalPositionSpriteNumgo;

	void Awake() {
		SpriteNum3=this.transform.FindChild ("SpriteNum3").gameObject;
		UIOriginalPositionSpriteNum3=this.SpriteNum3.transform.localPosition;

		SpriteNum2=this.transform.FindChild ("SpriteNum2").gameObject;
		UIOriginalPositionSpriteNum2=this.SpriteNum2.transform.localPosition;

		SpriteNum1=this.transform.FindChild ("SpriteNum1").gameObject;
		UIOriginalPositionSpriteNum1=this.SpriteNum1.transform.localPosition;

		SpriteNumgo=this.transform.FindChild ("SpriteNumgo").gameObject;
		UIOriginalPositionSpriteNumgo=this.SpriteNumgo.transform.localPosition;

	}

}
