using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/17/2015 6:15:19 PM
public partial class ContainerMainzhujiemianUIController : UIControllerBase {

	public GameObject ButtonCheku;
	public Vector3 UIOriginalPositionButtonCheku;

	public GameObject ButtonChoujiang;
	public Vector3 UIOriginalPositionButtonChoujiang;

	public GameObject ButtonJiahaobi;
	public Vector3 UIOriginalPositionButtonJiahaobi;

	public GameObject ButtonJiahaoxin;
	public Vector3 UIOriginalPositionButtonJiahaoxin;

	public GameObject ButtonShezhi;
	public Vector3 UIOriginalPositionButtonShezhi;

	public GameObject ButtonJingcaigonggao;
	public Vector3 UIOriginalPositionButtonJingcaigonggao;

	public GameObject ButtonHaoyou;
	public Vector3 UIOriginalPositionButtonHaoyou;

	public GameObject ButtonRenwu;
	public Vector3 UIOriginalPositionButtonRenwu;

	public GameObject ButtonYoujian;
	public Vector3 UIOriginalPositionButtonYoujian;

	public GameObject ButtonTiaozhansai;
	public Vector3 UIOriginalPositionButtonTiaozhansai;

	public GameObject ButtonJuese;
	public Vector3 UIOriginalPositionButtonJuese;

	public GameObject ButtonChongwu;
	public Vector3 UIOriginalPositionButtonChongwu;

	public GameObject ButtonXiuxianmoshi;
	public Vector3 UIOriginalPositionButtonXiuxianmoshi;

	public GameObject ButtonGushimoshi;
	public Vector3 UIOriginalPositionButtonGushimoshi;

	public GameObject ButtonJiahaozuan;
	public Vector3 UIOriginalPositionButtonJiahaozuan;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelXinshuzi;
	public Vector3 UIOriginalPositionLabelXinshuzi;

	public GameObject LabelMingzi;
	public Vector3 UIOriginalPositionLabelMingzi;

	public GameObject LabelZuanshuzi;
	public Vector3 UIOriginalPositionLabelZuanshuzi;

	public GameObject LabelBishuzi;
	public Vector3 UIOriginalPositionLabelBishuzi;

	public GameObject SpriteTishixiaodiandian1;
	public Vector3 UIOriginalPositionSpriteTishixiaodiandian1;

	public GameObject SpriteLoginbeijing;
	public Vector3 UIOriginalPositionSpriteLoginbeijing;

	public GameObject ContainerHuadong;
	public Vector3 UIOriginalPositionContainerHuadong;

	public GameObject SpriteTishixiaodiandian2;
	public Vector3 UIOriginalPositionSpriteTishixiaodiandian2;

	public GameObject SpriteTouxiang;
	public Vector3 UIOriginalPositionSpriteTouxiang;

	void Awake() {
		ButtonCheku=this.transform.FindChild ("ButtonCheku").gameObject;
		UIOriginalPositionButtonCheku=this.ButtonCheku.transform.localPosition;

		ButtonChoujiang=this.transform.FindChild ("ButtonChoujiang").gameObject;
		UIOriginalPositionButtonChoujiang=this.ButtonChoujiang.transform.localPosition;

		ButtonJiahaobi=this.transform.FindChild ("ButtonJiahaobi").gameObject;
		UIOriginalPositionButtonJiahaobi=this.ButtonJiahaobi.transform.localPosition;

		ButtonJiahaoxin=this.transform.FindChild ("ButtonJiahaoxin").gameObject;
		UIOriginalPositionButtonJiahaoxin=this.ButtonJiahaoxin.transform.localPosition;

		ButtonShezhi=this.transform.FindChild ("ButtonShezhi").gameObject;
		UIOriginalPositionButtonShezhi=this.ButtonShezhi.transform.localPosition;

		ButtonJingcaigonggao=this.transform.FindChild ("ButtonJingcaigonggao").gameObject;
		UIOriginalPositionButtonJingcaigonggao=this.ButtonJingcaigonggao.transform.localPosition;

		ButtonHaoyou=this.transform.FindChild ("ButtonHaoyou").gameObject;
		UIOriginalPositionButtonHaoyou=this.ButtonHaoyou.transform.localPosition;

		ButtonRenwu=this.transform.FindChild ("ButtonRenwu").gameObject;
		UIOriginalPositionButtonRenwu=this.ButtonRenwu.transform.localPosition;

		ButtonYoujian=this.transform.FindChild ("ButtonYoujian").gameObject;
		UIOriginalPositionButtonYoujian=this.ButtonYoujian.transform.localPosition;

		ButtonTiaozhansai=this.transform.FindChild ("ButtonTiaozhansai").gameObject;
		UIOriginalPositionButtonTiaozhansai=this.ButtonTiaozhansai.transform.localPosition;

		ButtonJuese=this.transform.FindChild ("ButtonJuese").gameObject;
		UIOriginalPositionButtonJuese=this.ButtonJuese.transform.localPosition;

		ButtonChongwu=this.transform.FindChild ("ButtonChongwu").gameObject;
		UIOriginalPositionButtonChongwu=this.ButtonChongwu.transform.localPosition;

		ButtonXiuxianmoshi=this.transform.FindChild ("ButtonXiuxianmoshi").gameObject;
		UIOriginalPositionButtonXiuxianmoshi=this.ButtonXiuxianmoshi.transform.localPosition;

		ButtonGushimoshi=this.transform.FindChild ("ButtonGushimoshi").gameObject;
		UIOriginalPositionButtonGushimoshi=this.ButtonGushimoshi.transform.localPosition;

		ButtonJiahaozuan=this.transform.FindChild ("ButtonJiahaozuan").gameObject;
		UIOriginalPositionButtonJiahaozuan=this.ButtonJiahaozuan.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelXinshuzi=this.transform.FindChild ("LabelXinshuzi").gameObject;
		UIOriginalPositionLabelXinshuzi=this.LabelXinshuzi.transform.localPosition;

		LabelMingzi=this.transform.FindChild ("LabelMingzi").gameObject;
		UIOriginalPositionLabelMingzi=this.LabelMingzi.transform.localPosition;

		LabelZuanshuzi=this.transform.FindChild ("LabelZuanshuzi").gameObject;
		UIOriginalPositionLabelZuanshuzi=this.LabelZuanshuzi.transform.localPosition;

		LabelBishuzi=this.transform.FindChild ("LabelBishuzi").gameObject;
		UIOriginalPositionLabelBishuzi=this.LabelBishuzi.transform.localPosition;

		SpriteTishixiaodiandian1=this.transform.FindChild ("SpriteTishixiaodiandian1").gameObject;
		UIOriginalPositionSpriteTishixiaodiandian1=this.SpriteTishixiaodiandian1.transform.localPosition;

		SpriteLoginbeijing=this.transform.FindChild ("SpriteLoginbeijing").gameObject;
		UIOriginalPositionSpriteLoginbeijing=this.SpriteLoginbeijing.transform.localPosition;

		ContainerHuadong=this.transform.FindChild ("ContainerHuadong").gameObject;
		UIOriginalPositionContainerHuadong=this.ContainerHuadong.transform.localPosition;

		SpriteTishixiaodiandian2=this.transform.FindChild ("SpriteTishixiaodiandian2").gameObject;
		UIOriginalPositionSpriteTishixiaodiandian2=this.SpriteTishixiaodiandian2.transform.localPosition;

		SpriteTouxiang=this.transform.FindChild ("SpriteTouxiang").gameObject;
		UIOriginalPositionSpriteTouxiang=this.SpriteTouxiang.transform.localPosition;

	}

}
