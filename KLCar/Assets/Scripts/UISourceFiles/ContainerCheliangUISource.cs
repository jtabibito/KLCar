using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/11/2015 4:32:43 PM
public partial class ContainerCheliangUIController : UIControllerBase {

	public GameObject ButtonMax1;
	public Vector3 UIOriginalPositionButtonMax1;

	public GameObject ButtonMax2;
	public Vector3 UIOriginalPositionButtonMax2;

	public GameObject ButtonMax3;
	public Vector3 UIOriginalPositionButtonMax3;

	public GameObject ButtonShengji1;
	public Vector3 UIOriginalPositionButtonShengji1;

	public GameObject ButtonShengji2;
	public Vector3 UIOriginalPositionButtonShengji2;

	public GameObject ButtonShengji3;
	public Vector3 UIOriginalPositionButtonShengji3;

	public GameObject ButtonXuanzhong;
	public Vector3 UIOriginalPositionButtonXuanzhong;

	public GameObject ButtonJiesuo;
	public Vector3 UIOriginalPositionButtonJiesuo;

	public GameObject ContainerWeijiesuorongqi;
	public Vector3 UIOriginalPositionContainerWeijiesuorongqi;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelZhihoushu1;
	public Vector3 UIOriginalPositionLabelZhihoushu1;

	public GameObject LabelCheliangmingcheng;
	public Vector3 UIOriginalPositionLabelCheliangmingcheng;

	public GameObject LabelDangqianshu1;
	public Vector3 UIOriginalPositionLabelDangqianshu1;

	public GameObject LabelDangqianshu2;
	public Vector3 UIOriginalPositionLabelDangqianshu2;

	public GameObject LabelDangqianshu3;
	public Vector3 UIOriginalPositionLabelDangqianshu3;

	public GameObject LabelShengji1;
	public Vector3 UIOriginalPositionLabelShengji1;

	public GameObject LabelShengji2;
	public Vector3 UIOriginalPositionLabelShengji2;

	public GameObject LabelShengji3;
	public Vector3 UIOriginalPositionLabelShengji3;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelJiesuoshuzi;
	public Vector3 UIOriginalPositionLabelJiesuoshuzi;

	public GameObject LabelZhihoushu2;
	public Vector3 UIOriginalPositionLabelZhihoushu2;

	public GameObject LabelZhihoushu3;
	public Vector3 UIOriginalPositionLabelZhihoushu3;

	public GameObject LabelNpcduihuakuang;
	public Vector3 UIOriginalPositionLabelNpcduihuakuang;

	public GameObject ContainerJiesuoBackground;
	public Vector3 UIOriginalPositionContainerJiesuoBackground;

	public GameObject SpriteBeijingkuang;
	public Vector3 UIOriginalPositionSpriteBeijingkuang;

	public GameObject SpriteJindutiaodi1;
	public Vector3 UIOriginalPositionSpriteJindutiaodi1;

	public GameObject SpriteJindutiaodi2;
	public Vector3 UIOriginalPositionSpriteJindutiaodi2;

	public GameObject SpriteJindutiaodi3;
	public Vector3 UIOriginalPositionSpriteJindutiaodi3;

	public GameObject SpriteNpc1;
	public Vector3 UIOriginalPositionSpriteNpc1;

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

	public GameObject SpriteXiaojinbi;
	public Vector3 UIOriginalPositionSpriteXiaojinbi;

	public GameObject SpriteLoginbeijing;
	public Vector3 UIOriginalPositionSpriteLoginbeijing;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	public GameObject ButtonZuo;
	public Vector3 UIOriginalPositionButtonZuo;

	public GameObject ButtonYou;
	public Vector3 UIOriginalPositionButtonYou;

	public GameObject ButtonFanhui;
	public Vector3 UIOriginalPositionButtonFanhui;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject ContainerHuadong;
	public Vector3 UIOriginalPositionContainerHuadong;

	public GameObject SpriteXiaojuan;
	public Vector3 UIOriginalPositionSpriteXiaojuan;

	public GameObject SpriteXiaozuan;
	public Vector3 UIOriginalPositionSpriteXiaozuan;

	void Awake() {
		ButtonMax1=this.transform.FindChild ("ButtonMax1").gameObject;
		UIOriginalPositionButtonMax1=this.ButtonMax1.transform.localPosition;

		ButtonMax2=this.transform.FindChild ("ButtonMax2").gameObject;
		UIOriginalPositionButtonMax2=this.ButtonMax2.transform.localPosition;

		ButtonMax3=this.transform.FindChild ("ButtonMax3").gameObject;
		UIOriginalPositionButtonMax3=this.ButtonMax3.transform.localPosition;

		ButtonShengji1=this.transform.FindChild ("ButtonShengji1").gameObject;
		UIOriginalPositionButtonShengji1=this.ButtonShengji1.transform.localPosition;

		ButtonShengji2=this.transform.FindChild ("ButtonShengji2").gameObject;
		UIOriginalPositionButtonShengji2=this.ButtonShengji2.transform.localPosition;

		ButtonShengji3=this.transform.FindChild ("ButtonShengji3").gameObject;
		UIOriginalPositionButtonShengji3=this.ButtonShengji3.transform.localPosition;

		ButtonXuanzhong=this.transform.FindChild ("ButtonXuanzhong").gameObject;
		UIOriginalPositionButtonXuanzhong=this.ButtonXuanzhong.transform.localPosition;

		ButtonJiesuo=this.transform.FindChild ("ButtonJiesuo").gameObject;
		UIOriginalPositionButtonJiesuo=this.ButtonJiesuo.transform.localPosition;

		ContainerWeijiesuorongqi=this.transform.FindChild ("ContainerWeijiesuorongqi").gameObject;
		UIOriginalPositionContainerWeijiesuorongqi=this.ContainerWeijiesuorongqi.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelZhihoushu1=this.transform.FindChild ("LabelZhihoushu1").gameObject;
		UIOriginalPositionLabelZhihoushu1=this.LabelZhihoushu1.transform.localPosition;

		LabelCheliangmingcheng=this.transform.FindChild ("LabelCheliangmingcheng").gameObject;
		UIOriginalPositionLabelCheliangmingcheng=this.LabelCheliangmingcheng.transform.localPosition;

		LabelDangqianshu1=this.transform.FindChild ("LabelDangqianshu1").gameObject;
		UIOriginalPositionLabelDangqianshu1=this.LabelDangqianshu1.transform.localPosition;

		LabelDangqianshu2=this.transform.FindChild ("LabelDangqianshu2").gameObject;
		UIOriginalPositionLabelDangqianshu2=this.LabelDangqianshu2.transform.localPosition;

		LabelDangqianshu3=this.transform.FindChild ("LabelDangqianshu3").gameObject;
		UIOriginalPositionLabelDangqianshu3=this.LabelDangqianshu3.transform.localPosition;

		LabelShengji1=this.transform.FindChild ("LabelShengji1").gameObject;
		UIOriginalPositionLabelShengji1=this.LabelShengji1.transform.localPosition;

		LabelShengji2=this.transform.FindChild ("LabelShengji2").gameObject;
		UIOriginalPositionLabelShengji2=this.LabelShengji2.transform.localPosition;

		LabelShengji3=this.transform.FindChild ("LabelShengji3").gameObject;
		UIOriginalPositionLabelShengji3=this.LabelShengji3.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelJiesuoshuzi=this.transform.FindChild ("LabelJiesuoshuzi").gameObject;
		UIOriginalPositionLabelJiesuoshuzi=this.LabelJiesuoshuzi.transform.localPosition;

		LabelZhihoushu2=this.transform.FindChild ("LabelZhihoushu2").gameObject;
		UIOriginalPositionLabelZhihoushu2=this.LabelZhihoushu2.transform.localPosition;

		LabelZhihoushu3=this.transform.FindChild ("LabelZhihoushu3").gameObject;
		UIOriginalPositionLabelZhihoushu3=this.LabelZhihoushu3.transform.localPosition;

		LabelNpcduihuakuang=this.transform.FindChild ("LabelNpcduihuakuang").gameObject;
		UIOriginalPositionLabelNpcduihuakuang=this.LabelNpcduihuakuang.transform.localPosition;

		ContainerJiesuoBackground=this.transform.FindChild ("ContainerJiesuoBackground").gameObject;
		UIOriginalPositionContainerJiesuoBackground=this.ContainerJiesuoBackground.transform.localPosition;

		SpriteBeijingkuang=this.transform.FindChild ("SpriteBeijingkuang").gameObject;
		UIOriginalPositionSpriteBeijingkuang=this.SpriteBeijingkuang.transform.localPosition;

		SpriteJindutiaodi1=this.transform.FindChild ("SpriteJindutiaodi1").gameObject;
		UIOriginalPositionSpriteJindutiaodi1=this.SpriteJindutiaodi1.transform.localPosition;

		SpriteJindutiaodi2=this.transform.FindChild ("SpriteJindutiaodi2").gameObject;
		UIOriginalPositionSpriteJindutiaodi2=this.SpriteJindutiaodi2.transform.localPosition;

		SpriteJindutiaodi3=this.transform.FindChild ("SpriteJindutiaodi3").gameObject;
		UIOriginalPositionSpriteJindutiaodi3=this.SpriteJindutiaodi3.transform.localPosition;

		SpriteNpc1=this.transform.FindChild ("SpriteNpc1").gameObject;
		UIOriginalPositionSpriteNpc1=this.SpriteNpc1.transform.localPosition;

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

		SpriteXiaojinbi=this.transform.FindChild ("SpriteXiaojinbi").gameObject;
		UIOriginalPositionSpriteXiaojinbi=this.SpriteXiaojinbi.transform.localPosition;

		SpriteLoginbeijing=this.transform.FindChild ("SpriteLoginbeijing").gameObject;
		UIOriginalPositionSpriteLoginbeijing=this.SpriteLoginbeijing.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

		ButtonZuo=this.transform.FindChild ("ButtonZuo").gameObject;
		UIOriginalPositionButtonZuo=this.ButtonZuo.transform.localPosition;

		ButtonYou=this.transform.FindChild ("ButtonYou").gameObject;
		UIOriginalPositionButtonYou=this.ButtonYou.transform.localPosition;

		ButtonFanhui=this.transform.FindChild ("ButtonFanhui").gameObject;
		UIOriginalPositionButtonFanhui=this.ButtonFanhui.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		ContainerHuadong=this.transform.FindChild ("ContainerHuadong").gameObject;
		UIOriginalPositionContainerHuadong=this.ContainerHuadong.transform.localPosition;

		SpriteXiaojuan=this.transform.FindChild ("SpriteXiaojuan").gameObject;
		UIOriginalPositionSpriteXiaojuan=this.SpriteXiaojuan.transform.localPosition;

		SpriteXiaozuan=this.transform.FindChild ("SpriteXiaozuan").gameObject;
		UIOriginalPositionSpriteXiaozuan=this.SpriteXiaozuan.transform.localPosition;

	}

}
