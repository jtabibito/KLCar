using UnityEngine;
using System.Collections;

public partial class ContainerOperationJiesuanFenshuUIController : UIControllerBase {

	public GameObject ButtonFenxiang;
	public GameObject ButtonChongxinkaishi;
	public GameObject ButtonFanhui;
	public GameObject ContainerBackground;
	public GameObject LabelGuoguan;
	public GameObject LabelShouji;
	public GameObject LabelFenshuwenzi;
	void Awake() {
		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
		LabelFenshuwenzi=this.transform.FindChild ("LabelFenshuwenzi").gameObject;
	}

}
