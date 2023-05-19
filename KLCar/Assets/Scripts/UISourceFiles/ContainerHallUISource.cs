using UnityEngine;
using System.Collections;

public partial class ContainerHallUIController : UIControllerBase {

	public GameObject ButtoncarList;
	public GameObject Buttonemil;
	public GameObject Buttonfriend;
	public GameObject Buttongame;
	public GameObject LabelDiamond;
	public GameObject LabelGold;
	public GameObject LabelGonggao;
	public GameObject LabelLv;
	public GameObject LabelPower;
	public GameObject LabelVipNumber;
	public GameObject Buttonmission;
	public GameObject Buttonshop;
	public GameObject SpriteCharacterFace;
	public GameObject ContainerBackground;
	public GameObject Buttonstyle;
	public GameObject Buttonworld;
	void Awake() {
		ButtoncarList=this.transform.FindChild ("ButtoncarList").gameObject;
		Buttonemil=this.transform.FindChild ("Buttonemil").gameObject;
		Buttonfriend=this.transform.FindChild ("Buttonfriend").gameObject;
		Buttongame=this.transform.FindChild ("Buttongame").gameObject;
		LabelDiamond=this.transform.FindChild ("LabelDiamond").gameObject;
		LabelGold=this.transform.FindChild ("LabelGold").gameObject;
		LabelGonggao=this.transform.FindChild ("LabelGonggao").gameObject;
		LabelLv=this.transform.FindChild ("LabelLv").gameObject;
		LabelPower=this.transform.FindChild ("LabelPower").gameObject;
		LabelVipNumber=this.transform.FindChild ("LabelVipNumber").gameObject;
		Buttonmission=this.transform.FindChild ("Buttonmission").gameObject;
		Buttonshop=this.transform.FindChild ("Buttonshop").gameObject;
		SpriteCharacterFace=this.transform.FindChild ("SpriteCharacterFace").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		Buttonstyle=this.transform.FindChild ("Buttonstyle").gameObject;
		Buttonworld=this.transform.FindChild ("Buttonworld").gameObject;
	}

}
