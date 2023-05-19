using UnityEngine;
using System.Collections;

public partial class ContainerServerButtonUIController : UIControllerBase {

	public GameObject xuanqu;
	public GameObject zhuangtai;
	public GameObject into;
	public GameObject exit;
	void Awake() {
		xuanqu=this.transform.FindChild ("xuanqu").gameObject;
		zhuangtai=this.transform.FindChild ("zhuangtai").gameObject;
		into=this.transform.FindChild ("into").gameObject;
		exit=this.transform.FindChild ("exit").gameObject;
	}

}
