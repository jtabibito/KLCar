using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/12/2015 12:48:41 PM
public partial class ContainerStoryduihuakuangUIController : UIControllerBase {

	public GameObject ButtonTiaoguojuqing;
	public Vector3 UIOriginalPositionButtonTiaoguojuqing;

	public GameObject ContainerBackground;
	public Vector3 UIOriginalPositionContainerBackground;

	public GameObject LabelJingqingqidai;
	public Vector3 UIOriginalPositionLabelJingqingqidai;

	public GameObject LabelXingming;
	public Vector3 UIOriginalPositionLabelXingming;

	public GameObject LabelWenzimiaoshu;
	public Vector3 UIOriginalPositionLabelWenzimiaoshu;

	public GameObject Sprite;
	public Vector3 UIOriginalPositionSprite;

	public GameObject SpriteJiantou;
	public Vector3 UIOriginalPositionSpriteJiantou;

	public GameObject SpriteBanshenxiangzuo;
	public Vector3 UIOriginalPositionSpriteBanshenxiangzuo;

	public GameObject SpriteBanshenxiangyou;
	public Vector3 UIOriginalPositionSpriteBanshenxiangyou;

	void Awake() {
		ButtonTiaoguojuqing=this.transform.FindChild ("ButtonTiaoguojuqing").gameObject;
		UIOriginalPositionButtonTiaoguojuqing=this.ButtonTiaoguojuqing.transform.localPosition;

		ContainerBackground=this.transform.FindChild ("ContainerBackground").gameObject;
		UIOriginalPositionContainerBackground=this.ContainerBackground.transform.localPosition;

		LabelJingqingqidai=this.transform.FindChild ("LabelJingqingqidai").gameObject;
		UIOriginalPositionLabelJingqingqidai=this.LabelJingqingqidai.transform.localPosition;

		LabelXingming=this.transform.FindChild ("LabelXingming").gameObject;
		UIOriginalPositionLabelXingming=this.LabelXingming.transform.localPosition;

		LabelWenzimiaoshu=this.transform.FindChild ("LabelWenzimiaoshu").gameObject;
		UIOriginalPositionLabelWenzimiaoshu=this.LabelWenzimiaoshu.transform.localPosition;

		Sprite=this.transform.FindChild ("Sprite").gameObject;
		UIOriginalPositionSprite=this.Sprite.transform.localPosition;

		SpriteJiantou=this.transform.FindChild ("SpriteJiantou").gameObject;
		UIOriginalPositionSpriteJiantou=this.SpriteJiantou.transform.localPosition;

		SpriteBanshenxiangzuo=this.transform.FindChild ("SpriteBanshenxiangzuo").gameObject;
		UIOriginalPositionSpriteBanshenxiangzuo=this.SpriteBanshenxiangzuo.transform.localPosition;

		SpriteBanshenxiangyou=this.transform.FindChild ("SpriteBanshenxiangyou").gameObject;
		UIOriginalPositionSpriteBanshenxiangyou=this.SpriteBanshenxiangyou.transform.localPosition;

	}

}
