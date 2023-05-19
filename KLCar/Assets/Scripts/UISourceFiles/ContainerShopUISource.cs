using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/21/2015 6:51:35 PM
public partial class ContainerShopUIController : UIControllerBase {

	public GameObject Button2zuan;
	public Vector3 UIOriginalPositionButton2zuan;

	public GameObject Button3bi;
	public Vector3 UIOriginalPositionButton3bi;

	public GameObject Button4daoju;
	public Vector3 UIOriginalPositionButton4daoju;

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject Container2zuan;
	public Vector3 UIOriginalPositionContainer2zuan;

	public GameObject Container4daoju;
	public Vector3 UIOriginalPositionContainer4daoju;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject Container3bi;
	public Vector3 UIOriginalPositionContainer3bi;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject Button1xin;
	public Vector3 UIOriginalPositionButton1xin;

	public GameObject Container1Xin;
	public Vector3 UIOriginalPositionContainer1Xin;

	public GameObject ContainerBj;
	public Vector3 UIOriginalPositionContainerBj;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	void Awake() {
		Button2zuan=this.transform.FindChild ("Button2zuan").gameObject;
		UIOriginalPositionButton2zuan=this.Button2zuan.transform.localPosition;

		Button3bi=this.transform.FindChild ("Button3bi").gameObject;
		UIOriginalPositionButton3bi=this.Button3bi.transform.localPosition;

		Button4daoju=this.transform.FindChild ("Button4daoju").gameObject;
		UIOriginalPositionButton4daoju=this.Button4daoju.transform.localPosition;

		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		Container2zuan=this.transform.FindChild ("Container2zuan").gameObject;
		UIOriginalPositionContainer2zuan=this.Container2zuan.transform.localPosition;

		Container4daoju=this.transform.FindChild ("Container4daoju").gameObject;
		UIOriginalPositionContainer4daoju=this.Container4daoju.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		Container3bi=this.transform.FindChild ("Container3bi").gameObject;
		UIOriginalPositionContainer3bi=this.Container3bi.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		Button1xin=this.transform.FindChild ("Button1xin").gameObject;
		UIOriginalPositionButton1xin=this.Button1xin.transform.localPosition;

		Container1Xin=this.transform.FindChild ("Container1Xin").gameObject;
		UIOriginalPositionContainer1Xin=this.Container1Xin.transform.localPosition;

		ContainerBj=this.transform.FindChild ("ContainerBj").gameObject;
		UIOriginalPositionContainerBj=this.ContainerBj.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

	}

}
