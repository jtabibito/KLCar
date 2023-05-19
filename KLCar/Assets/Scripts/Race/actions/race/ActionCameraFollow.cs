using UnityEngine;
using System.Collections;

/// <summary>
/// 控制摄像机是否继续使用跟随代码.
/// </summary>
public class ActionCameraFollow : ActionBase
{
//		/// <summary>
//		/// 是否使用跟随代码.
//		/// </summary>
//		public bool follow;
		/// <summary>
		/// 运行结束后,是否恢复成原来状态.
		/// </summary>
		public bool reCover;
		/// <summary>
		/// 设置摄像机的跟随目标.null表示放弃跟随.
		/// </summary>
		public GameObject target;
		private GameObject lastTarget;

		protected override void onStart ()
		{
				base.onStart ();
		GameObject t = target == null ? null : GameObjectAgent.GetAgentGameObject (gameObject,target);
				SmoothFollow s = RaceManager.Instance.CarCamera.GetComponent<SmoothFollow> ();
				if (s != null) {
						if (s.target != null) {
								lastTarget = s.target.gameObject;
						}
						if (t != null) {
							s.target = t.transform;
						} else {
								s.target=null;
						}
				}
		}

		protected override void onOver ()
		{
				base.onOver ();
				if (reCover) {
						SmoothFollow s = RaceManager.Instance.CarCamera.GetComponent<SmoothFollow> ();
						if (s != null && lastTarget != null) {
								s.target = lastTarget.transform;
						}
				}
		}

		internal override void onCopyTo (ActionBase cloneTo)
		{
				ActionCameraFollow f = (ActionCameraFollow)cloneTo;
				f.reCover = reCover;
				f.target = target;
		}
}
