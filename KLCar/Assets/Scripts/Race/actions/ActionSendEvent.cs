using UnityEngine;
using System.Collections;
/// <summary>
/// 向指定目标派发一个事件./// </summary>
public class ActionSendEvent : ActionBase
{
		/// <summary>
		/// 接收消息的函数名称.
		/// </summary>
		public string type;
		/// <summary>
		/// 如果不为null,则附带一个参数.
		/// </summary>
		public object param;

		protected override void onStart ()
		{
				base.onStart ();
				if (type != null) {
//						GameObject send;
//						if (sendTo == null) {
//							send = gameObject;
//						} else {
//							send = GameObjectAgent.GetAgentGameObject (gameObject,sendTo);
//						}
						if (param == null) {
								gameObject.SendMessage (type);
						} else {
								gameObject.SendMessage (type, param);
						}
				}
		}
	internal override void onCopyTo (ActionBase cloneTo)
	{
		ActionSendEvent s=(ActionSendEvent)cloneTo;
//		s.sendTo = sendTo;
		s.type = type;
		s.param = param;
	}
}
