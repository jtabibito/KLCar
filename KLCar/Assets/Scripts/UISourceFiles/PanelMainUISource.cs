using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/30/2015 11:30:56 AM
public partial class PanelMainUIController : UIControllerBase {

	public GameObject PanelBackground;
	public Vector3 UIOriginalPositionPanelBackground;

	public GameObject PanelButtom;
	public Vector3 UIOriginalPositionPanelButtom;

	public GameObject PanelPopupTip;
	public Vector3 UIOriginalPositionPanelPopupTip;

	public GameObject PanelPopupWindow;
	public Vector3 UIOriginalPositionPanelPopupWindow;

	public GameObject PanelTop;
	public Vector3 UIOriginalPositionPanelTop;

	public GameObject PanelDebug;
	public Vector3 UIOriginalPositionPanelDebug;

	void Awake() {
		PanelBackground=this.transform.FindChild ("PanelBackground").gameObject;
		UIOriginalPositionPanelBackground=this.PanelBackground.transform.localPosition;

		PanelButtom=this.transform.FindChild ("PanelButtom").gameObject;
		UIOriginalPositionPanelButtom=this.PanelButtom.transform.localPosition;

		PanelPopupTip=this.transform.FindChild ("PanelPopupTip").gameObject;
		UIOriginalPositionPanelPopupTip=this.PanelPopupTip.transform.localPosition;

		PanelPopupWindow=this.transform.FindChild ("PanelPopupWindow").gameObject;
		UIOriginalPositionPanelPopupWindow=this.PanelPopupWindow.transform.localPosition;

		PanelTop=this.transform.FindChild ("PanelTop").gameObject;
		UIOriginalPositionPanelTop=this.PanelTop.transform.localPosition;

		PanelDebug=this.transform.FindChild ("PanelDebug").gameObject;
		UIOriginalPositionPanelDebug=this.PanelDebug.transform.localPosition;

	}

}
