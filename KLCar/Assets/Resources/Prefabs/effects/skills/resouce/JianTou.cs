using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏中可以自动飞行,并且爆炸的对象.
/// </summary>
public class JianTou : MonoBehaviour
{

		/// <summary>
		/// 最大速度.
		/// </summary>
		public int speed = 5;
		/// <summary>
		/// 开始的速度.
		/// </summary>
		public int startSpeed = 5;
		/// <summary>
		/// 最大转弯速度.
		/// </summary>
		public float maxRotateSpeed;
		/// <summary>
		/// 加速倍率.
		/// </summary>
		public float addSpeedRate = 0.1f;
		/// <summary>
		/// 火箭起始位置.偏移.
		/// </summary>
		public Vector3 posOffset;
		/// <summary>
		/// 爆炸效果.可以有PlayAble组件.如果没有.那么撞到东西就消失.
		/// </summary>
		public GameObject explodeEffect;
		/// <summary>
		/// 附加在车身上的效果.可以使一个playAble(以及SkillBase).
		/// </summary>
		public GameObject carHitEffect;
		/// <summary>
		/// 发射这个对象的物体.排除与其的碰撞.
		/// </summary>
		public GameObject parent;
		private WayPortRunner waypointRunner;
		public Vector3 currentVct;
		private float aliveTime = 0;
		public float maxAliveTime = 3f;
		public float lockTargetDistance = 60;
		public Transform target;
		private List<GameObject> obstacleCars;
	 
		void Awake ()
		{
				waypointRunner = gameObject.AddComponent <WayPortRunner> ();
				obstacleCars = RaceManager.Instance.ObstacleCars;
		}

		public Transform getNearstCar (out float distance)
		{
				float min = float.MaxValue;
				Transform t = null;
				foreach (GameObject obj in obstacleCars) {
						if (t == null) {
								t = obj.transform;
								min = Vector3.Distance (transform.position, obj.transform.position);
						} else {
								float v = Vector3.Distance (transform.position, obj.transform.position);
								if (v < min) {
										min = v;
										t = obj.transform;
								}
						}
				}
				distance = min;
				return t;
		}

		void Start ()
		{
//		transform.position = waypointRunner.currentWayPoint.position;
				transform.position += posOffset;
				rigidbody.velocity = waypointRunner.currentWayPoint.forward;
		}

		void Update ()
		{
				float dis = 0f;
				Transform car = getNearstCar (out dis);
				if (car != null && dis < lockTargetDistance) {
						 //飞向目标.
//					Vector3 v= car.position-transform.position;
					
				} else {
					//跟随路径飞行.
				}
				Vector3 v = rigidbody.velocity;
				float current = v.magnitude;
				//		if ( current!= speed) {
				v = waypointRunner.currentWayPoint.forward;
				v.y = 0;
				current = Mathf.Lerp (current, speed, addSpeedRate);
				v = MathUtils.getVectorByLength (v, current);
				rigidbody.velocity = v;
				currentVct = v;
				//		}
				aliveTime += Time.deltaTime;
				if (aliveTime >= maxAliveTime) {
					OnHitObj ();
					
				}
				//transform.forward = rigidbody.velocity.normalized;//waypointRunner.currentWayPoint.forward;//MathUtils.getRotationByVector (v);
		}

		void OnTriggerEnter (Collider col)
		{
				if (col.gameObject == parent || !isOtherCar (col)) {
						return;
				}
				{
						CarEngine e = col.gameObject.GetComponent<CarEngine> ();
//						if (e != null ) {
						if (e != null && e.getCarState (CarState.YingShen)) {
								return;
						}
				}
				col.gameObject.SendMessage ("onHitByJianTou", this);
				EffectManager.playEffect (col.gameObject.transform, carHitEffect);
				OnHitObj ();
		} 

		void OnHitObj ()
		{
				EffectManager.playEffect (null, explodeEffect, transform.position);
				Destroy (this.gameObject);
		}

		bool isOtherCar (Collider col)
		{
				if (col.gameObject.layer == LayerMask.NameToLayer ("PlayerCar") || col.gameObject.layer == LayerMask.NameToLayer ("ObstacleCar")) {
						return true;
				} else {
						return false;
				}
		}
}
