using UnityEngine;
using System.Collections;

public partial class ContainerOperationJiesuanHaoshiUIController : UIControllerBase {

	public GameObject ButtonChongxinkaishi;
	public GameObject ButtonFanhui;
	public GameObject ButtonFenxiang;
	public GameObject ContainerBackground;
	public GameObject LabelMiao;
	public GameObject LabelHaomiao;
	public GameObject LabelGuoguan;
	public GameObject LabelShouji;
	public GameObject LabelFen;
	void Awake() {
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		LabelMiao=this.transform.FindChild ("LabelMiao").gameObject;
		LabelHaomiao=this.transform.FindChild ("LabelHaomiao").gameObject;
		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
		LabelFen=this.transform.FindChild ("LabelFen").gameObject;
	}

}
