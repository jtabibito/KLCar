using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏对象的代理者组件.拥有这个组件的对象可以代理其他GameObject对象.
/// </summary>
public class GameObjectAgent  : MonoBehaviour
{
 
		/// <summary>
		/// 要代理的目标是什么.也可以通过覆盖getAgent()函数来实现更负责的功能.
		/// </summary>
		public GameObject agent;
		/// <summary>
		/// 取得我所代理的对象.
		/// </summary>
		/// <param name="requester">请求获取代理的对象.,有些代理可能会给不同的请求者返回不同的对象.</param>
		/// <returns>The agent.</returns>
		public virtual GameObject getAgent (object requester)
		{
				return agent;
		}
		/// <summary>
		/// 取得obj指定的代理对象.如果他没有代理任何对象.则返回null.如果returnSelf=true,但没有代理组件时,返回自己.
		/// </summary>
		/// <returns>The agent game object.</returns>
		/// <param name="obj">Object.</param>
		/// <param name="returnSelf">Object.</param>
		public static GameObject GetAgentGameObject (object requester, GameObject obj, bool returnSelf)
		{
				if (obj == null) {
						return null;
				}
				GameObjectAgent a = obj.GetComponent <GameObjectAgent> ();
				if (a == null) {
						return returnSelf ? obj : null;
				} else {
						GameObject agent = a.getAgent (requester);
						if (agent == null && returnSelf) {
								return obj;
						} else {
								return agent;
						}
				}
		}
		/// <summary>
		/// 取得指定代理的目标.obj为请求代理的对象.
		/// </summary>
		/// <returns>The agent transform.</returns>
		/// <param name="requester">Requester.</param>
		/// <param name="obj">Object.</param>
		public static Transform getAgentTransform (object requester, GameObject agent)
		{
				if (agent == null) {
						return null;		
				} else {
						return GetAgentGameObject (requester, agent).transform;
				}
		}
		/// <summary>
		/// 取得obj指定的代理对象.如果他没有代理任何对象.则返回自己.
		/// </summary>
		/// <returns>The agent game object.</returns>
		/// <param name="obj">Object.</param>
		/// <param name="returnSelf">Object.</param>
		public static GameObject GetAgentGameObject (object requester, GameObject agent)
		{
				return GetAgentGameObject (requester, agent, true);
		}
}
