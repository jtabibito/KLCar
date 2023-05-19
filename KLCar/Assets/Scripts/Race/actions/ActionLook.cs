using UnityEngine;
using System.Collections;

/// <summary>
/// 让对象注视到指定的目标.
/// </summary>
public class ActionLook : ActionBase
{
		public enum LookType
		{
				LookTo,
				LookFrom
		}
		/// <summary>
		/// 注视的类型.是注视到某个位置,还是从某个位置开始恢复回来.
		/// </summary>
		public LookType lookType;
		/// <summary>
		/// 缓动函数.
		/// </summary>
		public Easetype easetype;
		/// <summary>
		/// T要注视的对象.
		/// </summary>
		public GameObject lookTarget;
		/// <summary>
		/// T要注视的坐标点.通常使用lookTarget代替.
		/// </summary>
		public Vector3 lookPos;

		protected override void onStart ()
		{
				Hashtable v = new Hashtable ();
				if (lookTarget != null) {
			v.Add ("looktarget", GameObjectAgent.getAgentTransform (gameObject,lookTarget));
					
				} else {
						v.Add ("looktarget", lookPos);
				}
				v.Add ("time", time);
				v.Add ("easetype", easetype.ToString ());
				switch (lookType) {
				case LookType.LookTo:
						iTween.LookTo (gameObject, v);
						break;
				case LookType.LookFrom:
						iTween.LookFrom (gameObject, v);
						break;
				}
		}

		internal override void onCopyTo (ActionBase cloneTo)
		{
				ActionLook a = (ActionLook)cloneTo;
				a.lookType = lookType;
				a.easetype = easetype;
				a.lookTarget = lookTarget;
				a.lookPos = lookPos;
		}
}
