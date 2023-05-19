using UnityEngine;
using System.Collections;

public partial class ContainerLoginUIController : UIControllerBase {

	public GameObject name;
	public GameObject Sprite1;
	public GameObject theBinding;
	public GameObject weiBo;
	public GameObject weiXin;
	public GameObject zhuCe;
	public GameObject login;
	public GameObject Sprite2;
	public GameObject into;
	void Awake() {
		name=this.transform.FindChild ("name").gameObject;
		Sprite1=this.transform.FindChild ("Sprite1").gameObject;
		theBinding=this.transform.FindChild ("theBinding").gameObject;
		weiBo=this.transform.FindChild ("weiBo").gameObject;
		weiXin=this.transform.FindChild ("weiXin").gameObject;
		zhuCe=this.transform.FindChild ("zhuCe").gameObject;
		login=this.transform.FindChild ("login").gameObject;
		Sprite2=this.transform.FindChild ("Sprite2").gameObject;
		into=this.transform.FindChild ("into").gameObject;
	}

}
