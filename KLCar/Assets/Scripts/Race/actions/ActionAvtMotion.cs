using UnityEngine;
using System.Collections;

/// <summary>
/// 播放动画. 支持2种播放形式.比如:直接指定需要播放的动作名称:"attack",或者播放指定的状态:"roleState,0",用","把名称和状态分开.
/// </summary>
public class ActionAvtMotion : ActionBase
{
		/// <summary>
		/// T要播放的动画名称.比如:直接指定需要播放的动作名称:"attack",或者播放指定的状态:"roleState,0",用","把名称和状态分开.
		/// </summary>
		public string motionName;
		/// <summary>
		/// 但这个动画播放结束后,接着需要播放的动画.比如:直接指定需要播放的动作名称:"attack",或者播放指定的状态:"roleState,0",用","把名称和状态分开.
		/// </summary>
		public string overmotionName;

		internal override void onCopyTo (ActionBase cloneTo)
		{
				ActionAvtMotion avt = (ActionAvtMotion)cloneTo;
//				avt.target = target;
				avt.motionName = motionName;
				avt.overmotionName = overmotionName;
		}

		private void setMotion (string motion)
		{
//				GameObject obj = GameObjectAgent.GetAgentGameObject (gameObject);
				Animator a = gameObject.GetComponent<Animator> ();
				string[] s = motion.Split (',');	
				if (s.Length == 1) {
						a.SetTrigger (s [0]);
				} else if (s.Length == 2) {
						a.SetInteger (s [0], int.Parse (s [1]));
				}
		}

		protected override void onStart ()
		{
				setMotion (motionName);
		}

		protected override void onOver ()
		{
				base.onOver ();
				if (overmotionName != null&&overmotionName!="") {
						setMotion (overmotionName);
				}
		}
}
