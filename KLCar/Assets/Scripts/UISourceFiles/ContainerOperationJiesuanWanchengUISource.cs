using UnityEngine;
using System.Collections;

public partial class ContainerOperationJiesuanWanchengUIController : UIControllerBase {

	public GameObject ButtonChongxinkaishi;
	public GameObject ButtonFenxiang;
	public GameObject ButtonXiayiguan;
	public GameObject ContainerBackground;
	public GameObject LabelGuoguan;
	public GameObject LabelShouji;
	void Awake() {
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		ButtonXiayiguan=this.transform.FindChild ("ButtonXiayiguan").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
	}

}
