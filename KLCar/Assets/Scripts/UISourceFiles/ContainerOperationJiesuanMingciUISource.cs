using UnityEngine;
using System.Collections;

public partial class ContainerOperationJiesuanMingciUIController : UIControllerBase {

	public GameObject ButtonChongxinkaishi;
	public GameObject ButtonFenxiang;
	public GameObject ButtonFanhui;
	public GameObject ContainerBackground;
	public GameObject LabelGuoguan;
	public GameObject LabelShouji;
	public GameObject LabelDijiming;
	void Awake() {
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
		LabelDijiming=this.transform.FindChild ("LabelDijiming").gameObject;
	}

}
