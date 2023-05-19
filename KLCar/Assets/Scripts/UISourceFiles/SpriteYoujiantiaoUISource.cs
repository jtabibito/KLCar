using UnityEngine;
using System.Collections;

///UISource File Create Data: 6/3/2015 9:56:13 AM
public partial class SpriteYoujiantiaoUIController : UIControllerBase 
{

	public GameObject LabelRenwumiaoshu;
	public Vector3 UIOriginalPositionLabelRenwumiaoshu;

	public GameObject LabelRenwumingzi;
	public Vector3 UIOriginalPositionLabelRenwumingzi;

	public GameObject SpriteTouxiang1;
	public Vector3 UIOriginalPositionSpriteTouxiang1;

	public GameObject SpriteMingzidi;
	public Vector3 UIOriginalPositionSpriteMingzidi;

	public GameObject SpriteJiangpin;
	public Vector3 UIOriginalPositionSpriteJiangpin;

	public GameObject Sprite1;
	public Vector3 UIOriginalPositionSprite1;

	public GameObject SpriteSongxin;
	public Vector3 UIOriginalPositionSpriteSongxin;

	void Awake() {
		LabelRenwumiaoshu=this.transform.FindChild ("LabelRenwumiaoshu").gameObject;
		UIOriginalPositionLabelRenwumiaoshu=this.LabelRenwumiaoshu.transform.localPosition;

		LabelRenwumingzi=this.transform.FindChild ("LabelRenwumingzi").gameObject;
		UIOriginalPositionLabelRenwumingzi=this.LabelRenwumingzi.transform.localPosition;

		SpriteTouxiang1=this.transform.FindChild ("SpriteTouxiang1").gameObject;
		UIOriginalPositionSpriteTouxiang1=this.SpriteTouxiang1.transform.localPosition;

		SpriteMingzidi=this.transform.FindChild ("SpriteMingzidi").gameObject;
		UIOriginalPositionSpriteMingzidi=this.SpriteMingzidi.transform.localPosition;

		SpriteJiangpin=this.transform.FindChild ("SpriteJiangpin").gameObject;
		UIOriginalPositionSpriteJiangpin=this.SpriteJiangpin.transform.localPosition;

		Sprite1=this.transform.FindChild ("Sprite1").gameObject;
		UIOriginalPositionSprite1=this.Sprite1.transform.localPosition;

		SpriteSongxin=this.transform.FindChild ("SpriteSongxin").gameObject;
		UIOriginalPositionSpriteSongxin=this.SpriteSongxin.transform.localPosition;

	}

}
