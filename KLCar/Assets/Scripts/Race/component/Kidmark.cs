using UnityEngine;
using System.Collections;

/// <summary>
/// 胎痕的代码.保证胎痕在一定时间内,会自动回收.
/// </summary>
public class Kidmark :MonoBehaviour
{
		public GameObjectPool pool;
		public float duration = 0.5f;
		private float time;
		
		void Start ()
		{
				time = 0;
		}

		void Update ()
		{
				if (time > duration) {
						time = 0;
						
						if (pool != null) {
								pool.destoryObject (this.gameObject);
						} else {
								DestroyObject (this.gameObject);
						}
				} else {
						time += Time.deltaTime;
				}
		}
		
}
