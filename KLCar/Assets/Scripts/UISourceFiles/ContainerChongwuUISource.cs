using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/12/2015 2:34:14 PM
public partial class ContainerChongwuUIController : UIControllerBase {

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject ButtonJiesuo;
	public Vector3 UIOriginalPositionButtonJiesuo;

	public GameObject ButtonXuanzhong;
	public Vector3 UIOriginalPositionButtonXuanzhong;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerHuadong;
	public Vector3 UIOriginalPositionContainerHuadong;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject LabelCheliangmingcheng;
	public Vector3 UIOriginalPositionLabelCheliangmingcheng;

	public GameObject LabelJinengming;
	public Vector3 UIOriginalPositionLabelJinengming;

	public GameObject LabelJiesuoshuzi;
	public Vector3 UIOriginalPositionLabelJiesuoshuzi;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelNpcduihuakuang;
	public Vector3 UIOriginalPositionLabelNpcduihuakuang;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject SpriteNpc;
	public Vector3 UIOriginalPositionSpriteNpc;

	public GameObject SpriteSuo;
	public Vector3 UIOriginalPositionSpriteSuo;

	public GameObject SpriteGou;
	public Vector3 UIOriginalPositionSpriteGou;

	public GameObject SpriteLoginbeijing;
	public Vector3 UIOriginalPositionSpriteLoginbeijing;

	public GameObject ButtonZuo;
	public Vector3 UIOriginalPositionButtonZuo;

	public GameObject ButtonYou;
	public Vector3 UIOriginalPositionButtonYou;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	public GameObject SpriteXiaojinbi;
	public Vector3 UIOriginalPositionSpriteXiaojinbi;

	public GameObject SpriteXiaojuan;
	public Vector3 UIOriginalPositionSpriteXiaojuan;

	public GameObject SpriteXiaozuan;
	public Vector3 UIOriginalPositionSpriteXiaozuan;

	public GameObject LabelMiaoshu;
	public Vector3 UIOriginalPositionLabelMiaoshu;

	void Awake() {
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		ButtonJiesuo=this.transform.FindChild ("ButtonJiesuo").gameObject;
		UIOriginalPositionButtonJiesuo=this.ButtonJiesuo.transform.localPosition;

		ButtonXuanzhong=this.transform.FindChild ("ButtonXuanzhong").gameObject;
		UIOriginalPositionButtonXuanzhong=this.ButtonXuanzhong.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerHuadong=this.transform.FindChild ("ContainerHuadong").gameObject;
		UIOriginalPositionContainerHuadong=this.ContainerHuadong.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		LabelCheliangmingcheng=this.transform.FindChild ("LabelCheliangmingcheng").gameObject;
		UIOriginalPositionLabelCheliangmingcheng=this.LabelCheliangmingcheng.transform.localPosition;

		LabelJinengming=this.transform.FindChild ("LabelJinengming").gameObject;
		UIOriginalPositionLabelJinengming=this.LabelJinengming.transform.localPosition;

		LabelJiesuoshuzi=this.transform.FindChild ("LabelJiesuoshuzi").gameObject;
		UIOriginalPositionLabelJiesuoshuzi=this.LabelJiesuoshuzi.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelNpcduihuakuang=this.transform.FindChild ("LabelNpcduihuakuang").gameObject;
		UIOriginalPositionLabelNpcduihuakuang=this.LabelNpcduihuakuang.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		SpriteNpc=this.transform.FindChild ("SpriteNpc").gameObject;
		UIOriginalPositionSpriteNpc=this.SpriteNpc.transform.localPosition;

		SpriteSuo=this.transform.FindChild ("SpriteSuo").gameObject;
		UIOriginalPositionSpriteSuo=this.SpriteSuo.transform.localPosition;

		SpriteGou=this.transform.FindChild ("SpriteGou").gameObject;
		UIOriginalPositionSpriteGou=this.SpriteGou.transform.localPosition;

		SpriteLoginbeijing=this.transform.FindChild ("SpriteLoginbeijing").gameObject;
		UIOriginalPositionSpriteLoginbeijing=this.SpriteLoginbeijing.transform.localPosition;

		ButtonZuo=this.transform.FindChild ("ButtonZuo").gameObject;
		UIOriginalPositionButtonZuo=this.ButtonZuo.transform.localPosition;

		ButtonYou=this.transform.FindChild ("ButtonYou").gameObject;
		UIOriginalPositionButtonYou=this.ButtonYou.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

		SpriteXiaojinbi=this.transform.FindChild ("SpriteXiaojinbi").gameObject;
		UIOriginalPositionSpriteXiaojinbi=this.SpriteXiaojinbi.transform.localPosition;

		SpriteXiaojuan=this.transform.FindChild ("SpriteXiaojuan").gameObject;
		UIOriginalPositionSpriteXiaojuan=this.SpriteXiaojuan.transform.localPosition;

		SpriteXiaozuan=this.transform.FindChild ("SpriteXiaozuan").gameObject;
		UIOriginalPositionSpriteXiaozuan=this.SpriteXiaozuan.transform.localPosition;

		LabelMiaoshu=this.transform.FindChild ("LabelMiaoshu").gameObject;
		UIOriginalPositionLabelMiaoshu=this.LabelMiaoshu.transform.localPosition;

	}

}
