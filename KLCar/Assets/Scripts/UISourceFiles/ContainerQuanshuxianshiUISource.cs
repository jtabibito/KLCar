using UnityEngine;
using System.Collections;

///UISource File Create Data: 7/10/2015 11:23:40 AM
public partial class ContainerQuanshuxianshiUIController : UIControllerBase 
{

	public GameObject LabelShuzi;
	public Vector3 UIOriginalPositionLabelShuzi;

	public GameObject SpriteZuihouyiquan;
	public Vector3 UIOriginalPositionSpriteZuihouyiquan;

	void Awake() {
		LabelShuzi=this.transform.FindChild ("LabelShuzi").gameObject;
		UIOriginalPositionLabelShuzi=this.LabelShuzi.transform.localPosition;

		SpriteZuihouyiquan=this.transform.FindChild ("SpriteZuihouyiquan").gameObject;
		UIOriginalPositionSpriteZuihouyiquan=this.SpriteZuihouyiquan.transform.localPosition;

	}

}
