using UnityEngine;
using System.Collections;

/// <summary>
/// 摄像机黑屏(默认).可以指定黑屏的图片..
/// </summary>
public class ActionCameraFade : ActionBase
{
		/// <summary>
		/// 摄像机要变成的黑屏深度.fade=true表示黑屏.false表示恢复屏幕.
		/// </summary>
		public bool fade = true;
		/// <summary>
		/// 是否黑屏之后,还会恢复.如果为true,那么会先用一半的时间变黑,然后再变白.
		/// </summary>
		public bool recover = false;
		/// <summary>
		/// 缓动的类型.
		/// </summary>
		public Easetype easetype;
		private static bool isCreate = false;
		private bool isRecovering = false;
//		public Color color=Color.black;
//		private static Color lastcolor;
		protected override void onStart ()
		{
				base.onStart ();
//				if (!isCreate) {
//						iTween.CameraFadeAdd ();
//						isCreate = true;
//				}
				TweenUtils.validateCameraFade ();
//				if (color != lastcolor) {
//						iTween.CameraFadeDepth (color);
//						lastcolor = color;
//				}
//				 if (time == 0) {
//					iTween.CameraFadeValue( fade ? 1 : 0);
//				} else {
					Hashtable t = iTween.Hash ("amount", fade ? 1 : 0, "easetype", easetype.ToString());
					if (recover) {
						t.Add ("time", time / 2);
						isRecovering = false;
					} else {
						t.Add ("time", time);
					}
					iTween.CameraFadeTo (t);
//				}
				 
		}

		string getEasetype ()
		{
				string last = easetype.ToString ();
				string s = last.Replace ("easeOut", "easeIn");
				if (last == s) {
						s = s.Replace ("easeIn", "easeOut");
				}
				return s;
		}

		void Update ()
		{
				if (isStart) {
						if (recover && !isRecovering && duration >= time / 2) {
								Hashtable t = iTween.Hash ("amount", fade ? 0 : 1, "easetype", getEasetype (), "time", time / 2);
								iTween.CameraFadeTo (t);
								isRecovering = true;
						}
				}
		}

		internal override void onCopyTo (ActionBase cloneTo)
		{
				ActionCameraFade f = (ActionCameraFade)cloneTo;
				f.fade = fade;
				f.recover = recover;
		}
}
