using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/11/2015 7:29:52 PM
public partial class ContainerLoginxuanrenUIController : UIControllerBase {

	public GameObject ButtonHejiongyi;
	public Vector3 UIOriginalPositionButtonHejiongyi;

	public GameObject ButtonDianjijinru;
	public Vector3 UIOriginalPositionButtonDianjijinru;

	public GameObject ButtonWeijiasi;
	public Vector3 UIOriginalPositionButtonWeijiasi;

	public GameObject ButtonDuhaitaowu;
	public Vector3 UIOriginalPositionButtonDuhaitaowu;

	public GameObject ButtonXienaer;
	public Vector3 UIOriginalPositionButtonXienaer;

	public GameObject ButtonWuxinsan;
	public Vector3 UIOriginalPositionButtonWuxinsan;

	public GameObject ButtonShaizi;
	public Vector3 UIOriginalPositionButtonShaizi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerXuanzenv;
	public Vector3 UIOriginalPositionContainerXuanzenv;

	public GameObject LabelWanjiaming;
	public Vector3 UIOriginalPositionLabelWanjiaming;

	public GameObject ContainerXuanzenan;
	public Vector3 UIOriginalPositionContainerXuanzenan;

	public Vector3 UIOriginalPositionLabelTiShi;

	void Awake() {
		ButtonHejiongyi=this.transform.FindChild ("ButtonHejiongyi").gameObject;
		UIOriginalPositionButtonHejiongyi=this.ButtonHejiongyi.transform.localPosition;

		ButtonDianjijinru=this.transform.FindChild ("ButtonDianjijinru").gameObject;
		UIOriginalPositionButtonDianjijinru=this.ButtonDianjijinru.transform.localPosition;

		ButtonWeijiasi=this.transform.FindChild ("ButtonWeijiasi").gameObject;
		UIOriginalPositionButtonWeijiasi=this.ButtonWeijiasi.transform.localPosition;

		ButtonDuhaitaowu=this.transform.FindChild ("ButtonDuhaitaowu").gameObject;
		UIOriginalPositionButtonDuhaitaowu=this.ButtonDuhaitaowu.transform.localPosition;

		ButtonXienaer=this.transform.FindChild ("ButtonXienaer").gameObject;
		UIOriginalPositionButtonXienaer=this.ButtonXienaer.transform.localPosition;

		ButtonWuxinsan=this.transform.FindChild ("ButtonWuxinsan").gameObject;
		UIOriginalPositionButtonWuxinsan=this.ButtonWuxinsan.transform.localPosition;

		ButtonShaizi=this.transform.FindChild ("ButtonShaizi").gameObject;
		UIOriginalPositionButtonShaizi=this.ButtonShaizi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerXuanzenv=this.transform.FindChild ("ContainerXuanzenv").gameObject;
		UIOriginalPositionContainerXuanzenv=this.ContainerXuanzenv.transform.localPosition;

		LabelWanjiaming=this.transform.FindChild ("LabelWanjiaming").gameObject;
		UIOriginalPositionLabelWanjiaming=this.LabelWanjiaming.transform.localPosition;

		ContainerXuanzenan=this.transform.FindChild ("ContainerXuanzenan").gameObject;
		UIOriginalPositionContainerXuanzenan=this.ContainerXuanzenan.transform.localPosition;

		LabelTiShi=this.transform.FindChild ("LabelTiShi").gameObject;
		UIOriginalPositionLabelTiShi=this.LabelTiShi.transform.localPosition;

	}

}
