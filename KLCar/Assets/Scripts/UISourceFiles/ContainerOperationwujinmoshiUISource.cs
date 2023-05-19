using UnityEngine;
using System.Collections;

///UISource File Create Data: 5/6/2015 2:59:52 PM
public partial class ContainerOperationwujinmoshiUIController : UIControllerBase {

	public GameObject ButtonFeidan;
	public Vector3 UIOriginalPositionButtonFeidan;

	public GameObject ButtonLeft;
	public Vector3 UIOriginalPositionButtonLeft;

	public GameObject ButtonHudun;
	public Vector3 UIOriginalPositionButtonHudun;

	public GameObject ButtonRight;
	public Vector3 UIOriginalPositionButtonRight;

	public GameObject ButtonZanting;
	public Vector3 UIOriginalPositionButtonZanting;

	public GameObject ButtonYinshen;
	public Vector3 UIOriginalPositionButtonYinshen;

	public GameObject ButtonJiasu;
	public Vector3 UIOriginalPositionButtonJiasu;

	public GameObject ButtonChuping;
	public Vector3 UIOriginalPositionButtonChuping;

	public GameObject ButtonZhanTingWenzhi;
	public Vector3 UIOriginalPositionButtonZhanTingWenzhi;

	public GameObject LabelQuanSudu;
	public Vector3 UIOriginalPositionLabelQuanSudu;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelJinbishuzi;
	public Vector3 UIOriginalPositionLabelJinbishuzi;

	public GameObject ContainerChongwu;
	public Vector3 UIOriginalPositionContainerChongwu;

	public GameObject LabelTishifeidan;
	public Vector3 UIOriginalPositionLabelTishifeidan;

	public GameObject LabelTishihudun;
	public Vector3 UIOriginalPositionLabelTishihudun;

	public GameObject LabelTishijiasu;
	public Vector3 UIOriginalPositionLabelTishijiasu;

	public GameObject LabelTishiyinshen;
	public Vector3 UIOriginalPositionLabelTishiyinshen;

	public GameObject SpriteHudunmengzhu;
	public Vector3 UIOriginalPositionSpriteHudunmengzhu;

	public GameObject SpriteFeidanmengzhu;
	public Vector3 UIOriginalPositionSpriteFeidanmengzhu;

	public GameObject SpriteYinshenmengzhu;
	public Vector3 UIOriginalPositionSpriteYinshenmengzhu;

	public GameObject SpriteJiayout;
	public Vector3 UIOriginalPositionSpriteJiayout;

	public GameObject SpriteXiaoxiong;
	public Vector3 UIOriginalPositionSpriteXiaoxiong;

	public GameObject SpriteJiasumengzhu;
	public Vector3 UIOriginalPositionSpriteJiasumengzhu;

	public GameObject SpriteZhizhen;
	public Vector3 UIOriginalPositionSpriteZhizhen;

	public GameObject ButtonZhongli;
	public Vector3 UIOriginalPositionButtonZhongli;

	void Awake() {
		ButtonFeidan=this.transform.FindChild ("ButtonFeidan").gameObject;
		UIOriginalPositionButtonFeidan=this.ButtonFeidan.transform.localPosition;

		ButtonLeft=this.transform.FindChild ("ButtonLeft").gameObject;
		UIOriginalPositionButtonLeft=this.ButtonLeft.transform.localPosition;

		ButtonHudun=this.transform.FindChild ("ButtonHudun").gameObject;
		UIOriginalPositionButtonHudun=this.ButtonHudun.transform.localPosition;

		ButtonRight=this.transform.FindChild ("ButtonRight").gameObject;
		UIOriginalPositionButtonRight=this.ButtonRight.transform.localPosition;

		ButtonZanting=this.transform.FindChild ("ButtonZanting").gameObject;
		UIOriginalPositionButtonZanting=this.ButtonZanting.transform.localPosition;

		ButtonYinshen=this.transform.FindChild ("ButtonYinshen").gameObject;
		UIOriginalPositionButtonYinshen=this.ButtonYinshen.transform.localPosition;

		ButtonJiasu=this.transform.FindChild ("ButtonJiasu").gameObject;
		UIOriginalPositionButtonJiasu=this.ButtonJiasu.transform.localPosition;

		ButtonChuping=this.transform.FindChild ("ButtonChuping").gameObject;
		UIOriginalPositionButtonChuping=this.ButtonChuping.transform.localPosition;

		ButtonZhanTingWenzhi=this.transform.FindChild ("ButtonZhanTingWenzhi").gameObject;
		UIOriginalPositionButtonZhanTingWenzhi=this.ButtonZhanTingWenzhi.transform.localPosition;

		LabelQuanSudu=this.transform.FindChild ("LabelQuanSudu").gameObject;
		UIOriginalPositionLabelQuanSudu=this.LabelQuanSudu.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelJinbishuzi=this.transform.FindChild ("LabelJinbishuzi").gameObject;
		UIOriginalPositionLabelJinbishuzi=this.LabelJinbishuzi.transform.localPosition;

		ContainerChongwu=this.transform.FindChild ("ContainerChongwu").gameObject;
		UIOriginalPositionContainerChongwu=this.ContainerChongwu.transform.localPosition;

		LabelTishifeidan=this.transform.FindChild ("LabelTishifeidan").gameObject;
		UIOriginalPositionLabelTishifeidan=this.LabelTishifeidan.transform.localPosition;

		LabelTishihudun=this.transform.FindChild ("LabelTishihudun").gameObject;
		UIOriginalPositionLabelTishihudun=this.LabelTishihudun.transform.localPosition;

		LabelTishijiasu=this.transform.FindChild ("LabelTishijiasu").gameObject;
		UIOriginalPositionLabelTishijiasu=this.LabelTishijiasu.transform.localPosition;

		LabelTishiyinshen=this.transform.FindChild ("LabelTishiyinshen").gameObject;
		UIOriginalPositionLabelTishiyinshen=this.LabelTishiyinshen.transform.localPosition;

		SpriteHudunmengzhu=this.transform.FindChild ("SpriteHudunmengzhu").gameObject;
		UIOriginalPositionSpriteHudunmengzhu=this.SpriteHudunmengzhu.transform.localPosition;

		SpriteFeidanmengzhu=this.transform.FindChild ("SpriteFeidanmengzhu").gameObject;
		UIOriginalPositionSpriteFeidanmengzhu=this.SpriteFeidanmengzhu.transform.localPosition;

		SpriteYinshenmengzhu=this.transform.FindChild ("SpriteYinshenmengzhu").gameObject;
		UIOriginalPositionSpriteYinshenmengzhu=this.SpriteYinshenmengzhu.transform.localPosition;

		SpriteJiayout=this.transform.FindChild ("SpriteJiayout").gameObject;
		UIOriginalPositionSpriteJiayout=this.SpriteJiayout.transform.localPosition;

		SpriteXiaoxiong=this.transform.FindChild ("SpriteXiaoxiong").gameObject;
		UIOriginalPositionSpriteXiaoxiong=this.SpriteXiaoxiong.transform.localPosition;

		SpriteJiasumengzhu=this.transform.FindChild ("SpriteJiasumengzhu").gameObject;
		UIOriginalPositionSpriteJiasumengzhu=this.SpriteJiasumengzhu.transform.localPosition;

		SpriteZhizhen=this.transform.FindChild ("SpriteZhizhen").gameObject;
		UIOriginalPositionSpriteZhizhen=this.SpriteZhizhen.transform.localPosition;

		ButtonZhongli=this.transform.FindChild ("ButtonZhongli").gameObject;
		UIOriginalPositionButtonZhongli=this.ButtonZhongli.transform.localPosition;

	}

}