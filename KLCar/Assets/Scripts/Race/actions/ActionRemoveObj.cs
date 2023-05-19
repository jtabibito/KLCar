using UnityEngine;
using System.Collections;
/// <summary>
/// 从场景中删除指定的对象.如果要删除当前对象.则什么都不填写.
/// </summary>
public class ActionRemoveObj : ActionBase
{
		/// <summary>
		/// 可以指定名称.那么表示删除目标上的子节点.
		/// </summary>
		public string gameObjectName;
		
		protected override void onStart ()
		{
				base.onStart ();
				if (runTarget != null) {
						if (gameObjectName != null&&gameObjectName.Length!=0) {
								Transform t = gameObject.transform.FindChild (gameObjectName);
								if (t != null) {
										DestroyObject (t.gameObject);
								}
						} else {
								DestroyObject (gameObject);
						}//
				} else {
					if (gameObjectName == null) {
							DestroyObject(getActionParent());
					}else
					{
						GameObject obj= GameObject.Find(gameObjectName);
						if(obj!=null)
						{
							DestroyObject(obj);
						}
					}
				}
				
		}

		internal override void onCopyTo (ActionBase cloneTo)
		{
//				ActionRemoveObj c = (ActionRemoveObj)cloneTo;
//				c.createObject = createObject;
		}
}
