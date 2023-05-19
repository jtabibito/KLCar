using UnityEngine;
using System.Collections;

///UISource File Create Data: 7/11/2015 4:54:07 PM
public partial class ContainerOperationJiesuanXingchengUIController : UIControllerBase {

	public GameObject ButtonChongxinkaishi;
	public Vector3 UIOriginalPositionButtonChongxinkaishi;

	public GameObject ButtonFenxiang;
	public Vector3 UIOriginalPositionButtonFenxiang;

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelGuoguan;
	public Vector3 UIOriginalPositionLabelGuoguan;

	public GameObject LabelLishichengji;
	public Vector3 UIOriginalPositionLabelLishichengji;

	public GameObject LabelShouji;
	public Vector3 UIOriginalPositionLabelShouji;

	public GameObject LabelXingchengwenzi;
	public Vector3 UIOriginalPositionLabelXingchengwenzi;

	void Awake() {
		ButtonChongxinkaishi=this.transform.FindChild ("ButtonChongxinkaishi").gameObject;
		UIOriginalPositionButtonChongxinkaishi=this.ButtonChongxinkaishi.transform.localPosition;

		ButtonFenxiang=this.transform.FindChild ("ButtonFenxiang").gameObject;
		UIOriginalPositionButtonFenxiang=this.ButtonFenxiang.transform.localPosition;

		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelGuoguan=this.transform.FindChild ("LabelGuoguan").gameObject;
		UIOriginalPositionLabelGuoguan=this.LabelGuoguan.transform.localPosition;

		LabelLishichengji=this.transform.FindChild ("LabelLishichengji").gameObject;
		UIOriginalPositionLabelLishichengji=this.LabelLishichengji.transform.localPosition;

		LabelShouji=this.transform.FindChild ("LabelShouji").gameObject;
		UIOriginalPositionLabelShouji=this.LabelShouji.transform.localPosition;

		LabelXingchengwenzi=this.transform.FindChild ("LabelXingchengwenzi").gameObject;
		UIOriginalPositionLabelXingchengwenzi=this.LabelXingchengwenzi.transform.localPosition;

	}

}
