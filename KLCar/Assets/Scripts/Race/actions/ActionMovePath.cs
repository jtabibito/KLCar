using UnityEngine;
using System.Collections;

/// <summary>
/// A等待指定长度的时间.什么都不做.
/// </summary>
public class ActionMovePath : ActionBase
{
	 
		public Transform[] pos;
		public Vector3[] vectorPos;
		public Easetype easetype;
		/// <summary>
		/// 在移动的过程中,是否一直注视某个位置.通常使用lookTarget
		/// </summary>
		public Vector3 looktargetPos;
		/// <summary>
		/// 在移动的过程中,是否一直注视某个目标.支持GameObjectAgent.
		/// </summary>
		public GameObject lookTarget;
		/// <summary>
		/// 在多长时间类注视到目标.
		/// </summary>
		public float looktime;
		/// <summary>
		/// 让自己正对这移动方向.看着自己的路径移动.
		/// </summary>
		public bool orienttopath;
		/// <summary>
		/// true表示从当前位置移动到第一个位置,在往后移动.false表示,直接跳到第一个位置,再往后移动.
		/// </summary>
		public bool fromCurrentPos = true;

		/// <summary>
		/// 设定的移动位置是本地位置,还是相对于全局的位置.
		/// </summary>
		public bool isLocalModel = false;

		internal override void onCopyTo (ActionBase cloneTo)
		{
		 
		}

		protected override void onStart ()
		{
				Hashtable h = iTween.Hash ();
				h.Add ("time", time);
				if (easetype != Easetype.Default) {
						h.Add ("easetype", easetype.ToString ());
				}
				if (lookTarget != null) {
						h.Add ("looktarget", GameObjectAgent.getAgentTransform (gameObject, lookTarget));
				} else if (looktargetPos != Vector3.zero) {
						h.Add ("looktarget", looktargetPos);		
				} else if (orienttopath) {
						h.Add ("orienttopath", true);
				}
//		if (looktime != 0) {
				h.Add ("looktime", looktime);
//		}
				h.Add ("space", isLocalModel ? Space.Self : Space.World);
				h.Add ("movetopath", true);
				Vector3 v;
				if (pos.Length != 0) {
						h.Add ("path", pos);
						v = pos [0].position;
				} else {
						h.Add ("vectorPos", vectorPos);
						v = vectorPos [0];
				}
				if (!fromCurrentPos) {
						if (isLocalModel) {
								transform.localPosition = v;
						} else {

								transform.position = v;
						}
				}
				iTween.MoveTo (gameObject, h);
		}
}
