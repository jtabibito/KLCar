using UnityEngine;
using System.Collections;

public class BrakeEffector: MonoBehaviour
{
		// 轮子碰撞体
		public WheelCollider flWheelCollider;
		public WheelCollider frWheelCollider;
		public WheelCollider rlWheelCollider;
		public WheelCollider rrWheelCollider;
		/**
		 * 偏移声音.
		 */
		public AudioClip brakeSound;
		private AudioSource brakeSoundPlayer;
		/**
		 * 漂移灰尘.
		 */
		public GameObject duck;
		private GameObject[] duckPlayers;
		/**
		 * 漂移划痕.
		 */
		public GameObject skidMark;
		private bool isStart = false;
		private WheelCollider[] wheels;
		/**
		 * 冲刺的效果.
		 */
		public GameObject fireEffect;
		/**
		 * 是否正在冲刺.
		 */
		private bool isPlayFire;
		/**
		 * 车辆碰撞声音.
		 */
		public AudioClip colliderCarSound;
		/**
		 * 道路碰撞声音.
		 */
		public AudioClip colliderRoundSound;
		/**
		 * 碰撞车辆的效果.
		 */
		public GameObject colliderCarEffect;
		/**
		 * 碰撞道路的效果.
		 */
		public GameObject colliderRoadSound;
		void Start ()
		{
				wheels = new WheelCollider[4];
				wheels [0] = flWheelCollider;
				wheels [1] = frWheelCollider;
				wheels [2] = rlWheelCollider;
				wheels [3] = rrWheelCollider;
			 
				brakeSoundPlayer = gameObject.AddComponent ("AudioSource") as AudioSource;
				brakeSoundPlayer.clip = brakeSound;
				brakeSoundPlayer.loop = true;
				brakeSoundPlayer.volume = 1;//0.5f;
				brakeSoundPlayer.playOnAwake = false;
				brakeSoundPlayer.pitch = 1f;
				brakeSoundPlayer.rolloffMode = AudioRolloffMode.Linear;
				brakeSoundPlayer.maxDistance = 50;
				//brakeSoundPlayer.Play();
				addDuckEffectPlayer ();
				addSkidMarkPlayer ();
				if (fireEffect != null) {
						Vector3 v = fireEffect.transform.localPosition;
						fireEffect.transform.parent = transform.parent.Find ("CarBody").transform;
						fireEffect.transform.localPosition = v;
				}
		}

		public void addDuckEffectPlayer ()
		{
				duckPlayers = new GameObject[4];

				for (int i=0; i<4; i++) {
						Transform wheelTransform = wheels [i].transform;
						duckPlayers [i] = (GameObject)Instantiate (duck);
						Transform obj = duckPlayers [i].transform;
						obj.parent = wheelTransform.parent;
						obj.position = wheelTransform.position;
						obj.gameObject.layer = 0;
				}
		}

		public void addSkidMarkPlayer ()
		{
		
		}

		void Update ()
		{
				if (!isStart)
						return;
				//创建胎痕.
//				for (int i=0; i<wheels.Length; i++) {
//						WheelCollider tf = wheels [i];
//						WheelHit hit;
//						if (tf.GetGroundHit (out hit)) {
//								Vector3 pos = hit.point;
//								pos.y += 0.1f;
//								GameObject ob=(GameObject)Instantiate (skidMark, pos, transform.rotation);
//								ob.transform.parent=transform.parent;
//						}
//				}
		 
		}

		public void playBrake ()
		{
				if (isStart)
						return;
				isStart = true;
				brakeSoundPlayer.Play ();
				for (int i=0; i<duckPlayers.Length; i++) {
						duckPlayers [i].particleEmitter.emit = true;
				}
		}

		public void stopBrake ()
		{
				if (!isStart)
						return;
				isStart = false;
				brakeSoundPlayer.Stop ();
				for (int i=0; i<duckPlayers.Length; i++) {
						duckPlayers [i].particleEmitter.emit = false;
				}
		}
		/**
		 * 开始冲刺
		 */
		public void playFire ()
		{
				if (isPlayFire) {
						return;
				}
				isPlayFire = true;
				if (fireEffect != null) {
						fireEffect.GetComponent<ParticleEmitter> ().emit = true;
				}
				rigidbody.velocity += rigidbody.velocity.normalized * 20;
				SmoothFollow vc = GameObject.Find ("CarCamera").GetComponent<SmoothFollow> ();
				if (vc.target == transform) {
						vc.playFire ();
				}
		}
		/**
		 * 停止冲刺.
		 */
		public void stopFire ()
		{
				if (!isPlayFire)
						return;
				isPlayFire = false;
				if (fireEffect != null) {
						fireEffect.GetComponent<ParticleEmitter> ().emit = false;
						rigidbody.velocity -= rigidbody.velocity.normalized * 20;
				}
				SmoothFollow vc = GameObject.Find ("CarCamera").GetComponent<SmoothFollow> ();
				if (vc.target == transform) {
						vc.stopFire ();
				}
		}
}
