using UnityEngine;
using System.Collections;

/// <summary>
/// 关闭或者开启触发器.
/// </summary>
public class ActionTrigger : ActionBase
{
		/// <summary>
		/// T是否开启触发器.
		/// </summary>
		public bool open;
		/// <summary>
		/// T是否在运行结束后再恢复原来的触发状态.
		/// </summary>
		public bool recoverOnOver;

		protected override void onStart ()
		{
				base.onStart ();
				TriggerGameObject t = gameObject.GetComponent <TriggerGameObject> ();
				if (t != null) {
						t.enabled = open;
				}
		}

		protected override void onOver ()
		{
				base.onOver ();
				if (recoverOnOver) {
						TriggerGameObject t = gameObject.GetComponent <TriggerGameObject> ();
						if (t != null) {
								t.enabled = !open;
						}
				}
		}

		internal override void onCopyTo (ActionBase cloneTo)
		{
				ActionTrigger t = (ActionTrigger)cloneTo;
				t.open = open;
				t.recoverOnOver = recoverOnOver;
		}
}
