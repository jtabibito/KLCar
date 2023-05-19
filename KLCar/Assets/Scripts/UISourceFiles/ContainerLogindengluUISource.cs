using UnityEngine;
using System.Collections;

public partial class ContainerLogindengluUIController : UIControllerBase {

	public GameObject ButtonKaishiyouxi;
	public GameObject ButtonQiehuanzhanghao;
	public GameObject ButtonPingtaidenglu;
	public GameObject ContainerBackground;
	void Awake() {
		ButtonKaishiyouxi=this.transform.FindChild ("ButtonKaishiyouxi").gameObject;
		ButtonQiehuanzhanghao=this.transform.FindChild ("ButtonQiehuanzhanghao").gameObject;
		ButtonPingtaidenglu=this.transform.FindChild ("ButtonPingtaidenglu").gameObject;
		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
	}

}
