using UnityEngine;
using System.Collections;

/// <summary>
/// 特效播放的工具类.用来快捷的播放各种特效.
/// </summary>
public class EffectManager
{
		public static GameObject playEffect (Transform target, GameObject effect)
		{
				return playEffect (target, effect, Vector3.zero);
		}
	 
		/// <summary>
		/// 在指定对象上播放特效.返回实际的播放对象.如果有PlayAble插件,则调用PlayAble功能.否则直接激活.
		/// </summary>
		/// <returns>实际生成的特效.</returns>
		/// <param name="target">要在什么地方生成特效.null表示场景上.</param>
		/// <param name="effect">提供的特效对象.如果不在当前对象内,则直接复制.</param>
		/// <param name="pos">位置.null表示默认位置.</param>
		public static GameObject playEffect (Transform target, GameObject effect, Vector3 pos)
		{
				if (effect == null)
						return null;
				if (target == null || target.root != effect.transform.root) {
						GameObject last = effect;
						effect = (GameObject)GameObject.Instantiate (effect);
						effect.transform.parent = target;
						effect.transform.localPosition = last.transform.localPosition;
						effect.transform.localRotation = last.transform.localRotation;
				}
				PlayAble p = effect.GetComponent <PlayAble> ();
				if (p == null) {
						effect.SetActive (true);
				} else {
						p.play ();
				}
				if (pos != Vector3.zero) {
						effect.transform.localPosition = pos;
				}
				return effect;
		}
		/**
		 * 关闭指定特效的播放.
		 */
		public static GameObject stopEffect (GameObject effect)
		{
			 	if (effect == null)
						return null;
				PlayAble p = effect.GetComponent<PlayAble> ();
				if (p != null) {
						p.stop ();	
				} else {
						effect.SetActive (false);
				}
				return effect; 
		}
}
