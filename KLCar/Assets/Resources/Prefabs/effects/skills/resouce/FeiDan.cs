using UnityEngine;
using System.Collections;
/// <summary>
/// 可以发射对象的技能效果.
/// duration表示冲刺的时长.
/// </summary>
public class FeiDan : SkillBase {

	/// <summary>
	/// 发射的对象.
	/// </summary>
	public GameObject emitItem;
	public Vector3 posOffset=Vector3.zero;

	protected override void onPlay()
	{ 
		base.onPlay ();
		if (emitItem != null)
		{
				GameObject obj=(GameObject)GameObject.Instantiate(emitItem);
				obj.transform.position=carEngine.transform.position+posOffset;
				obj.transform.rotation=carEngine.transform.rotation;
				JianTou j=obj.GetComponent<JianTou>();
				if(j!=null)
				{
					j.parent=carEngine.gameObject;
				}
		}
	}
}
