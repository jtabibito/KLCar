using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/12/2015 5:41:56 PM
public partial class ContainerJueseUIController : UIControllerBase {

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject ButtonShengjizuo;
	public Vector3 UIOriginalPositionButtonShengjizuo;

	public GameObject ButtonXuanzhong;
	public Vector3 UIOriginalPositionButtonXuanzhong;

	public GameObject ButtonJiesuo;
	public Vector3 UIOriginalPositionButtonJiesuo;

	public GameObject ButtonMaxzuo;
	public Vector3 UIOriginalPositionButtonMaxzuo;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject ContainerHuadong;
	public Vector3 UIOriginalPositionContainerHuadong;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject LabelRenwushuxing1;
	public Vector3 UIOriginalPositionLabelRenwushuxing1;

	public GameObject LabelRenwushuxing2;
	public Vector3 UIOriginalPositionLabelRenwushuxing2;

	public GameObject LabelRenwushuxing3;
	public Vector3 UIOriginalPositionLabelRenwushuxing3;

	public GameObject LabelShengji;
	public Vector3 UIOriginalPositionLabelShengji;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelZhihoushu1;
	public Vector3 UIOriginalPositionLabelZhihoushu1;

	public GameObject LabelJuesemingcheng;
	public Vector3 UIOriginalPositionLabelJuesemingcheng;

	public GameObject LabelDangqianshu1;
	public Vector3 UIOriginalPositionLabelDangqianshu1;

	public GameObject LabelDangqianshu2;
	public Vector3 UIOriginalPositionLabelDangqianshu2;

	public GameObject LabelDangqianshu3;
	public Vector3 UIOriginalPositionLabelDangqianshu3;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelJiesuoshuzi;
	public Vector3 UIOriginalPositionLabelJiesuoshuzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject LabelZuigaoshu1;
	public Vector3 UIOriginalPositionLabelZuigaoshu1;

	public GameObject LabelZuigaoshu2;
	public Vector3 UIOriginalPositionLabelZuigaoshu2;

	public GameObject LabelZhihoushu2;
	public Vector3 UIOriginalPositionLabelZhihoushu2;

	public GameObject LabelZuigaoshu3;
	public Vector3 UIOriginalPositionLabelZuigaoshu3;

	public GameObject LabelZhihoushu3;
	public Vector3 UIOriginalPositionLabelZhihoushu3;

	public GameObject LabelNpcduihuakuang;
	public Vector3 UIOriginalPositionLabelNpcduihuakuang;

	public GameObject LabelDengji;
	public Vector3 UIOriginalPositionLabelDengji;

	public GameObject SpriteNpc;
	public Vector3 UIOriginalPositionSpriteNpc;

	public GameObject SpriteShengjijiantou1;
	public Vector3 UIOriginalPositionSpriteShengjijiantou1;

	public GameObject SpriteShengjijiantou2;
	public Vector3 UIOriginalPositionSpriteShengjijiantou2;

	public GameObject SpriteShengjijiantou3;
	public Vector3 UIOriginalPositionSpriteShengjijiantou3;

	public GameObject SpriteSuo;
	public Vector3 UIOriginalPositionSpriteSuo;

	public GameObject SpriteDangqianjindu1;
	public Vector3 UIOriginalPositionSpriteDangqianjindu1;

	public GameObject SpriteShengjijindu1;
	public Vector3 UIOriginalPositionSpriteShengjijindu1;

	public GameObject SpriteDangqianjindu2;
	public Vector3 UIOriginalPositionSpriteDangqianjindu2;

	public GameObject SpriteDangqianjindu3;
	public Vector3 UIOriginalPositionSpriteDangqianjindu3;

	public GameObject SpriteShengjijindu2;
	public Vector3 UIOriginalPositionSpriteShengjijindu2;

	public GameObject SpriteShengjijindu3;
	public Vector3 UIOriginalPositionSpriteShengjijindu3;

	public GameObject SpriteGou;
	public Vector3 UIOriginalPositionSpriteGou;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	public GameObject SpriteXiaojinbi;
	public Vector3 UIOriginalPositionSpriteXiaojinbi;

	public GameObject ButtonZuo;
	public Vector3 UIOriginalPositionButtonZuo;

	public GameObject ButtonYou;
	public Vector3 UIOriginalPositionButtonYou;

	public GameObject SpriteXiaojuan;
	public Vector3 UIOriginalPositionSpriteXiaojuan;

	public GameObject SpriteXiaojinbiShengji;
	public Vector3 UIOriginalPositionSpriteXiaojinbiShengji;

	public GameObject SpriteXiaozuan;
	public Vector3 UIOriginalPositionSpriteXiaozuan;

	void Awake() {
		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		ButtonShengjizuo=this.transform.FindChild ("ButtonShengjizuo").gameObject;
		UIOriginalPositionButtonShengjizuo=this.ButtonShengjizuo.transform.localPosition;

		ButtonXuanzhong=this.transform.FindChild ("ButtonXuanzhong").gameObject;
		UIOriginalPositionButtonXuanzhong=this.ButtonXuanzhong.transform.localPosition;

		ButtonJiesuo=this.transform.FindChild ("ButtonJiesuo").gameObject;
		UIOriginalPositionButtonJiesuo=this.ButtonJiesuo.transform.localPosition;

		ButtonMaxzuo=this.transform.FindChild ("ButtonMaxzuo").gameObject;
		UIOriginalPositionButtonMaxzuo=this.ButtonMaxzuo.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		ContainerHuadong=this.transform.FindChild ("ContainerHuadong").gameObject;
		UIOriginalPositionContainerHuadong=this.ContainerHuadong.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		LabelRenwushuxing1=this.transform.FindChild ("LabelRenwushuxing1").gameObject;
		UIOriginalPositionLabelRenwushuxing1=this.LabelRenwushuxing1.transform.localPosition;

		LabelRenwushuxing2=this.transform.FindChild ("LabelRenwushuxing2").gameObject;
		UIOriginalPositionLabelRenwushuxing2=this.LabelRenwushuxing2.transform.localPosition;

		LabelRenwushuxing3=this.transform.FindChild ("LabelRenwushuxing3").gameObject;
		UIOriginalPositionLabelRenwushuxing3=this.LabelRenwushuxing3.transform.localPosition;

		LabelShengji=this.transform.FindChild ("LabelShengji").gameObject;
		UIOriginalPositionLabelShengji=this.LabelShengji.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelZhihoushu1=this.transform.FindChild ("LabelZhihoushu1").gameObject;
		UIOriginalPositionLabelZhihoushu1=this.LabelZhihoushu1.transform.localPosition;

		LabelJuesemingcheng=this.transform.FindChild ("LabelJuesemingcheng").gameObject;
		UIOriginalPositionLabelJuesemingcheng=this.LabelJuesemingcheng.transform.localPosition;

		LabelDangqianshu1=this.transform.FindChild ("LabelDangqianshu1").gameObject;
		UIOriginalPositionLabelDangqianshu1=this.LabelDangqianshu1.transform.localPosition;

		LabelDangqianshu2=this.transform.FindChild ("LabelDangqianshu2").gameObject;
		UIOriginalPositionLabelDangqianshu2=this.LabelDangqianshu2.transform.localPosition;

		LabelDangqianshu3=this.transform.FindChild ("LabelDangqianshu3").gameObject;
		UIOriginalPositionLabelDangqianshu3=this.LabelDangqianshu3.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelJiesuoshuzi=this.transform.FindChild ("LabelJiesuoshuzi").gameObject;
		UIOriginalPositionLabelJiesuoshuzi=this.LabelJiesuoshuzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		LabelZuigaoshu1=this.transform.FindChild ("LabelZuigaoshu1").gameObject;
		UIOriginalPositionLabelZuigaoshu1=this.LabelZuigaoshu1.transform.localPosition;

		LabelZuigaoshu2=this.transform.FindChild ("LabelZuigaoshu2").gameObject;
		UIOriginalPositionLabelZuigaoshu2=this.LabelZuigaoshu2.transform.localPosition;

		LabelZhihoushu2=this.transform.FindChild ("LabelZhihoushu2").gameObject;
		UIOriginalPositionLabelZhihoushu2=this.LabelZhihoushu2.transform.localPosition;

		LabelZuigaoshu3=this.transform.FindChild ("LabelZuigaoshu3").gameObject;
		UIOriginalPositionLabelZuigaoshu3=this.LabelZuigaoshu3.transform.localPosition;

		LabelZhihoushu3=this.transform.FindChild ("LabelZhihoushu3").gameObject;
		UIOriginalPositionLabelZhihoushu3=this.LabelZhihoushu3.transform.localPosition;

		LabelNpcduihuakuang=this.transform.FindChild ("LabelNpcduihuakuang").gameObject;
		UIOriginalPositionLabelNpcduihuakuang=this.LabelNpcduihuakuang.transform.localPosition;

		LabelDengji=this.transform.FindChild ("LabelDengji").gameObject;
		UIOriginalPositionLabelDengji=this.LabelDengji.transform.localPosition;

		SpriteNpc=this.transform.FindChild ("SpriteNpc").gameObject;
		UIOriginalPositionSpriteNpc=this.SpriteNpc.transform.localPosition;

		SpriteShengjijiantou1=this.transform.FindChild ("SpriteShengjijiantou1").gameObject;
		UIOriginalPositionSpriteShengjijiantou1=this.SpriteShengjijiantou1.transform.localPosition;

		SpriteShengjijiantou2=this.transform.FindChild ("SpriteShengjijiantou2").gameObject;
		UIOriginalPositionSpriteShengjijiantou2=this.SpriteShengjijiantou2.transform.localPosition;

		SpriteShengjijiantou3=this.transform.FindChild ("SpriteShengjijiantou3").gameObject;
		UIOriginalPositionSpriteShengjijiantou3=this.SpriteShengjijiantou3.transform.localPosition;

		SpriteSuo=this.transform.FindChild ("SpriteSuo").gameObject;
		UIOriginalPositionSpriteSuo=this.SpriteSuo.transform.localPosition;

		SpriteDangqianjindu1=this.transform.FindChild ("SpriteDangqianjindu1").gameObject;
		UIOriginalPositionSpriteDangqianjindu1=this.SpriteDangqianjindu1.transform.localPosition;

		SpriteShengjijindu1=this.transform.FindChild ("SpriteShengjijindu1").gameObject;
		UIOriginalPositionSpriteShengjijindu1=this.SpriteShengjijindu1.transform.localPosition;

		SpriteDangqianjindu2=this.transform.FindChild ("SpriteDangqianjindu2").gameObject;
		UIOriginalPositionSpriteDangqianjindu2=this.SpriteDangqianjindu2.transform.localPosition;

		SpriteDangqianjindu3=this.transform.FindChild ("SpriteDangqianjindu3").gameObject;
		UIOriginalPositionSpriteDangqianjindu3=this.SpriteDangqianjindu3.transform.localPosition;

		SpriteShengjijindu2=this.transform.FindChild ("SpriteShengjijindu2").gameObject;
		UIOriginalPositionSpriteShengjijindu2=this.SpriteShengjijindu2.transform.localPosition;

		SpriteShengjijindu3=this.transform.FindChild ("SpriteShengjijindu3").gameObject;
		UIOriginalPositionSpriteShengjijindu3=this.SpriteShengjijindu3.transform.localPosition;

		SpriteGou=this.transform.FindChild ("SpriteGou").gameObject;
		UIOriginalPositionSpriteGou=this.SpriteGou.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

		SpriteXiaojinbi=this.transform.FindChild ("SpriteXiaojinbi").gameObject;
		UIOriginalPositionSpriteXiaojinbi=this.SpriteXiaojinbi.transform.localPosition;

		ButtonZuo=this.transform.FindChild ("ButtonZuo").gameObject;
		UIOriginalPositionButtonZuo=this.ButtonZuo.transform.localPosition;

		ButtonYou=this.transform.FindChild ("ButtonYou").gameObject;
		UIOriginalPositionButtonYou=this.ButtonYou.transform.localPosition;

		SpriteXiaojuan=this.transform.FindChild ("SpriteXiaojuan").gameObject;
		UIOriginalPositionSpriteXiaojuan=this.SpriteXiaojuan.transform.localPosition;

		SpriteXiaojinbiShengji=this.transform.FindChild ("SpriteXiaojinbiShengji").gameObject;
		UIOriginalPositionSpriteXiaojinbiShengji=this.SpriteXiaojinbiShengji.transform.localPosition;

		SpriteXiaozuan=this.transform.FindChild ("SpriteXiaozuan").gameObject;
		UIOriginalPositionSpriteXiaozuan=this.SpriteXiaozuan.transform.localPosition;

	}

}
