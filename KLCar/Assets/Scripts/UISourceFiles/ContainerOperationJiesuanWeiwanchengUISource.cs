using UnityEngine;
using System.Collections;

public partial class ContainerOperationJiesuanWeiwanchengUIController : UIControllerBase {

	public GameObject ButtonChongxinkaishi;
	public GameObject ButtonFanhui;
	public GameObject ButtonFenxiang;
	public GameObject ContainerBackground;
	public GameObject LabelGuoguan;
	public GameObject LabelShouji;
	void Awake() {
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
	}

}
