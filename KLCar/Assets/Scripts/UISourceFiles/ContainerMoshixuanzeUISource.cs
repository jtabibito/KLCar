using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/25/2015 9:58:55 AM
public partial class ContainerMoshixuanzeUIController : UIControllerBase {

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject ButtonKaishi;
	public Vector3 UIOriginalPositionButtonKaishi;

	public GameObject ButtonJingsusai;
	public Vector3 UIOriginalPositionButtonJingsusai;

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelMoshixuanze;
	public Vector3 UIOriginalPositionLabelMoshixuanze;

	public GameObject LabelJikexing;
	public Vector3 UIOriginalPositionLabelJikexing;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject ScrollDituxuanze;
	public Vector3 UIOriginalPositionScrollDituxuanze;

	public GameObject SpriteFeichuan;
	public Vector3 UIOriginalPositionSpriteFeichuan;

	public GameObject SpriteLoginbeijing;
	public Vector3 UIOriginalPositionSpriteLoginbeijing;

	public GameObject ButtonZuo;
	public Vector3 UIOriginalPositionButtonZuo;

	public GameObject ButtonYou;
	public Vector3 UIOriginalPositionButtonYou;

	public GameObject ButtonJixiansai;
	public Vector3 UIOriginalPositionButtonJixiansai;

	public GameObject ButtonPohuaisai;
	public Vector3 UIOriginalPositionButtonPohuaisai;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	public GameObject ContainerHuadong;
	public Vector3 UIOriginalPositionContainerHuadong;

	void Awake() {
		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		ButtonKaishi=this.transform.FindChild ("ButtonKaishi").gameObject;
		UIOriginalPositionButtonKaishi=this.ButtonKaishi.transform.localPosition;

		ButtonJingsusai=this.transform.FindChild ("ButtonJingsusai").gameObject;
		UIOriginalPositionButtonJingsusai=this.ButtonJingsusai.transform.localPosition;

		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelMoshixuanze=this.transform.FindChild ("LabelMoshixuanze").gameObject;
		UIOriginalPositionLabelMoshixuanze=this.LabelMoshixuanze.transform.localPosition;

		LabelJikexing=this.transform.FindChild ("LabelJikexing").gameObject;
		UIOriginalPositionLabelJikexing=this.LabelJikexing.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		ScrollDituxuanze=this.transform.FindChild ("ScrollDituxuanze").gameObject;
		UIOriginalPositionScrollDituxuanze=this.ScrollDituxuanze.transform.localPosition;

		SpriteFeichuan=this.transform.FindChild ("SpriteFeichuan").gameObject;
		UIOriginalPositionSpriteFeichuan=this.SpriteFeichuan.transform.localPosition;

		SpriteLoginbeijing=this.transform.FindChild ("SpriteLoginbeijing").gameObject;
		UIOriginalPositionSpriteLoginbeijing=this.SpriteLoginbeijing.transform.localPosition;

		ButtonZuo=this.transform.FindChild ("ButtonZuo").gameObject;
		UIOriginalPositionButtonZuo=this.ButtonZuo.transform.localPosition;

		ButtonYou=this.transform.FindChild ("ButtonYou").gameObject;
		UIOriginalPositionButtonYou=this.ButtonYou.transform.localPosition;

		ButtonJixiansai=this.transform.FindChild ("ButtonJixiansai").gameObject;
		UIOriginalPositionButtonJixiansai=this.ButtonJixiansai.transform.localPosition;

		ButtonPohuaisai=this.transform.FindChild ("ButtonPohuaisai").gameObject;
		UIOriginalPositionButtonPohuaisai=this.ButtonPohuaisai.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

		ContainerHuadong=this.transform.FindChild ("ContainerHuadong").gameObject;
		UIOriginalPositionContainerHuadong=this.ContainerHuadong.transform.localPosition;

	}

}
