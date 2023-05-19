using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/29/2015 11:27:11 AM
public partial class SpriteRichangrenwutiaoUIController : UIControllerBase {

	public GameObject LabelRenwumiaoshu;
	public Vector3 UIOriginalPositionLabelRenwumiaoshu;

	public GameObject LabelRenwumingzi;
	public Vector3 UIOriginalPositionLabelRenwumingzi;

	public GameObject LabelJindu;
	public Vector3 UIOriginalPositionLabelJindu;

	public GameObject LabelShuzi;
	public Vector3 UIOriginalPositionLabelShuzi;

	public GameObject SpriteDianjilingqu;
	public Vector3 UIOriginalPositionSpriteDianjilingqu;

	public GameObject SpriteJiangli;
	public Vector3 UIOriginalPositionSpriteJiangli;

	public GameObject SpriteJindutiao;
	public Vector3 UIOriginalPositionSpriteJindutiao;

	public GameObject SpriteJiangpin;
	public Vector3 UIOriginalPositionSpriteJiangpin;

	public GameObject SpriteJindudi;
	public Vector3 UIOriginalPositionSpriteJindudi;

	public GameObject SpriteJindu;
	public Vector3 UIOriginalPositionSpriteJindu;

	public GameObject Sprite1;
	public Vector3 UIOriginalPositionSprite1;

	public GameObject SpriteJinxingzhong;
	public Vector3 UIOriginalPositionSpriteJinxingzhong;

	public GameObject SpriteYiwancheng;
	public Vector3 UIOriginalPositionSpriteYiwancheng;

	public GameObject Sprite;
	public Vector3 UIOriginalPositionSprite;

	public GameObject SpriteJinbi;
	public Vector3 UIOriginalPositionSpriteJinbi;

	public GameObject SpriteZhuanpanshu;
	public Vector3 UIOriginalPositionSpriteZhuanpanshu;

	public GameObject SpriteZuanshi;
	public Vector3 UIOriginalPositionSpriteZuanshi;

	void Awake() {
		LabelRenwumiaoshu=this.transform.FindChild ("LabelRenwumiaoshu").gameObject;
		UIOriginalPositionLabelRenwumiaoshu=this.LabelRenwumiaoshu.transform.localPosition;

		LabelRenwumingzi=this.transform.FindChild ("LabelRenwumingzi").gameObject;
		UIOriginalPositionLabelRenwumingzi=this.LabelRenwumingzi.transform.localPosition;

		LabelJindu=this.transform.FindChild ("LabelJindu").gameObject;
		UIOriginalPositionLabelJindu=this.LabelJindu.transform.localPosition;

		LabelShuzi=this.transform.FindChild ("LabelShuzi").gameObject;
		UIOriginalPositionLabelShuzi=this.LabelShuzi.transform.localPosition;

		SpriteDianjilingqu=this.transform.FindChild ("SpriteDianjilingqu").gameObject;
		UIOriginalPositionSpriteDianjilingqu=this.SpriteDianjilingqu.transform.localPosition;

		SpriteJiangli=this.transform.FindChild ("SpriteJiangli").gameObject;
		UIOriginalPositionSpriteJiangli=this.SpriteJiangli.transform.localPosition;

		SpriteJindutiao=this.transform.FindChild ("SpriteJindutiao").gameObject;
		UIOriginalPositionSpriteJindutiao=this.SpriteJindutiao.transform.localPosition;

		SpriteJiangpin=this.transform.FindChild ("SpriteJiangpin").gameObject;
		UIOriginalPositionSpriteJiangpin=this.SpriteJiangpin.transform.localPosition;

		SpriteJindudi=this.transform.FindChild ("SpriteJindudi").gameObject;
		UIOriginalPositionSpriteJindudi=this.SpriteJindudi.transform.localPosition;

		SpriteJindu=this.transform.FindChild ("SpriteJindu").gameObject;
		UIOriginalPositionSpriteJindu=this.SpriteJindu.transform.localPosition;

		Sprite1=this.transform.FindChild ("Sprite1").gameObject;
		UIOriginalPositionSprite1=this.Sprite1.transform.localPosition;

		SpriteJinxingzhong=this.transform.FindChild ("SpriteJinxingzhong").gameObject;
		UIOriginalPositionSpriteJinxingzhong=this.SpriteJinxingzhong.transform.localPosition;

		SpriteYiwancheng=this.transform.FindChild ("SpriteYiwancheng").gameObject;
		UIOriginalPositionSpriteYiwancheng=this.SpriteYiwancheng.transform.localPosition;

		Sprite=this.transform.FindChild ("Sprite").gameObject;
		UIOriginalPositionSprite=this.Sprite.transform.localPosition;

		SpriteJinbi=this.transform.FindChild ("SpriteJinbi").gameObject;
		UIOriginalPositionSpriteJinbi=this.SpriteJinbi.transform.localPosition;

		SpriteZhuanpanshu=this.transform.FindChild ("SpriteZhuanpanshu").gameObject;
		UIOriginalPositionSpriteZhuanpanshu=this.SpriteZhuanpanshu.transform.localPosition;

		SpriteZuanshi=this.transform.FindChild ("SpriteZuanshi").gameObject;
		UIOriginalPositionSpriteZuanshi=this.SpriteZuanshi.transform.localPosition;

	}

}
