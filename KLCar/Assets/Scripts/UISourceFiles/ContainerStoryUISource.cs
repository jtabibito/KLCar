using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/19/2015 2:27:00 PM
public partial class ContainerStoryUIController : UIControllerBase {

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject ButtonJixu;
	public Vector3 UIOriginalPositionButtonJixu;

	public GameObject ButtonYou;
	public Vector3 UIOriginalPositionButtonYou;

	public GameObject ButtonZuo;
	public Vector3 UIOriginalPositionButtonZuo;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject LabelGuankamiaoshu;
	public Vector3 UIOriginalPositionLabelGuankamiaoshu;

	public GameObject LabelJikexing;
	public Vector3 UIOriginalPositionLabelJikexing;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelMoshixuanze;
	public Vector3 UIOriginalPositionLabelMoshixuanze;

	public GameObject LabelBisaimoshi;
	public Vector3 UIOriginalPositionLabelBisaimoshi;

	public GameObject LabelZhangjieming;
	public Vector3 UIOriginalPositionLabelZhangjieming;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelZhangjie;
	public Vector3 UIOriginalPositionLabelZhangjie;

	public GameObject LabelJianglishu;
	public Vector3 UIOriginalPositionLabelJianglishu;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject ScrollView;
	public Vector3 UIOriginalPositionScrollView;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	void Awake() {
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		ButtonJixu=this.transform.FindChild ("ButtonJixu").gameObject;
		UIOriginalPositionButtonJixu=this.ButtonJixu.transform.localPosition;

		ButtonYou=this.transform.FindChild ("ButtonYou").gameObject;
		UIOriginalPositionButtonYou=this.ButtonYou.transform.localPosition;

		ButtonZuo=this.transform.FindChild ("ButtonZuo").gameObject;
		UIOriginalPositionButtonZuo=this.ButtonZuo.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		LabelGuankamiaoshu=this.transform.FindChild ("LabelGuankamiaoshu").gameObject;
		UIOriginalPositionLabelGuankamiaoshu=this.LabelGuankamiaoshu.transform.localPosition;

		LabelJikexing=this.transform.FindChild ("LabelJikexing").gameObject;
		UIOriginalPositionLabelJikexing=this.LabelJikexing.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelMoshixuanze=this.transform.FindChild ("LabelMoshixuanze").gameObject;
		UIOriginalPositionLabelMoshixuanze=this.LabelMoshixuanze.transform.localPosition;

		LabelBisaimoshi=this.transform.FindChild ("LabelBisaimoshi").gameObject;
		UIOriginalPositionLabelBisaimoshi=this.LabelBisaimoshi.transform.localPosition;

		LabelZhangjieming=this.transform.FindChild ("LabelZhangjieming").gameObject;
		UIOriginalPositionLabelZhangjieming=this.LabelZhangjieming.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelZhangjie=this.transform.FindChild ("LabelZhangjie").gameObject;
		UIOriginalPositionLabelZhangjie=this.LabelZhangjie.transform.localPosition;

		LabelJianglishu=this.transform.FindChild ("LabelJianglishu").gameObject;
		UIOriginalPositionLabelJianglishu=this.LabelJianglishu.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		ScrollView=this.transform.FindChild ("ScrollView").gameObject;
		UIOriginalPositionScrollView=this.ScrollView.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

	}

}
