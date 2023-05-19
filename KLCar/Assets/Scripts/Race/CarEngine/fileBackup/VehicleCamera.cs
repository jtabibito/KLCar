using UnityEngine;
using System.Collections;

public class VehicleCamera : MonoBehaviour
{
	
		public Transform target;
		public float smooth = 0.3f;
		
		public float distance = 3.55f;
		public float haight = 1.5f;
		public float angle = 1.0f;
		public string[] cameraSwitchView;
		public GUISkin GUISkin;
		private float yVelocity = 0.0f;
		private float xVelocity = 0.0f;
		/**
	 * 冲刺的镜头距离.
	 */
		public float fireDistanceOffset = 5f;
		/**
		 * 摄像机冲刺时,拉远的速率.值越大,拉动越快.
		 */
		public float cameraMoveSpeed=0.02f;
		private int Switch;
		private Transform parent;
		private float totalDistance = 0f;

		/**
			 * 默认屏幕抖动的强度.
		 */
		public float snakeInTensity=30.0f;
		/**
			 * 默认屏幕抖动的时间.
		 */
		public float snakeDuration=0.5f;
		/**
	 * 当前是否正在播放冲刺的镜头.
	 */
		private bool  isPlayFire;
		
		void Start ()
		{
				parent = transform.parent;
				totalDistance = distance;
		}

		public void playFire ()
		{
				if (isPlayFire)
						return;
				isPlayFire = true;
		}

		public void stopFire ()
		{
				if (!isPlayFire)
						return;
				isPlayFire = false;
		}
		
		void Update ()
		{
			if (Time.timeScale == 0)
						return;
				if (isPlayFire) {
					totalDistance = Mathf.Lerp (totalDistance, this.distance + fireDistanceOffset, cameraMoveSpeed);
				} else {
					totalDistance = Mathf.Lerp (totalDistance, this.distance, cameraMoveSpeed);
				}
		
		
//		var carScript = (VehicleControl)target.GetComponent<VehicleControl>();
//		camera.fieldOfView = Mathf.Clamp(carScript.speed / 20.0f + 60.0f, 60, 70);
		
		
		
				if (Input.GetKeyDown (KeyCode.C)) {
						Switch++;
						if (Switch > cameraSwitchView.Length) {
								Switch = 0;
						}
				}
		
		
		
				if (Switch == 0) {
						// Damp angle from current y-angle towards target y-angle
			
			
			
			
			
						transform.parent = parent;
			
						float yAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y,
			                                     target.eulerAngles.y, ref yVelocity, smooth);
			
			
						float xAngle = Mathf.SmoothDampAngle (transform.eulerAngles.x,
			                                     target.eulerAngles.x, ref xVelocity, smooth);
			
						// Position at the target
						Vector3 position = target.position;
						// Then offset by distance behind the new angle
						position += Quaternion.Euler (xAngle, yAngle, 0) * new Vector3 (0, 0, -totalDistance);
						// Apply the position
						//  transform.position = position;
			
						// Look at the target
						transform.eulerAngles = new Vector3 (xAngle + angle, yAngle, 0);
			
						var direction = transform.rotation * -Vector3.forward;
						var targetDistance = AdjustLineOfSight (target.position + new Vector3 (0, haight, 0), direction);
			
//						transform.position=Vector3.Lerp(transform.position,target.position + new Vector3 (0, haight, 0) + direction * targetDistance,0.2f);
						transform.position = target.position + new Vector3 (0, haight, 0) + direction * targetDistance;
			
			
				} else {
						Transform go = target.FindChild (cameraSwitchView [Switch - 1]);
						if (go != null) {
								transform.localPosition = Vector3.zero;//go.transform.position;
								transform.parent = go.transform.transform;
								transform.rotation = Quaternion.Lerp (transform.rotation, go.transform.rotation, Time.deltaTime * 10.0f);
						}
			
				}
		
		
		
		
		
		
		
		}
		/**
		 * 播放一个镜头震动.
		 */
		public void playSnake(float intensity,float duration)
		{
			iTween.ShakeRotation (gameObject, iTween.Hash ("y",intensity,"time",duration));
		}

		public void playSnake()
		{
			playSnake (snakeInTensity,snakeDuration);
		}
		public LayerMask lineOfSightMask = 0;
	
		float AdjustLineOfSight (Vector3 target, Vector3 direction)
		{
		
		
				RaycastHit hit;
		
				if (Physics.Raycast (target, direction, out hit, totalDistance, lineOfSightMask.value))
						return hit.distance;
				else
				return totalDistance;
		
		}
		/**
		 * 播放一个
		 */
		public void playBounce()
		{
		}
		public void OnGUI ()
		{
		
		
//		GUI.skin = GUISkin;
//		
//		
//		
//		
//		if (GUI.Button(new Rect(20, 206, 100, 20), "Vehicle 1"))
//		{
//			Application.LoadLevel(0);
//		}
//		
//		if (GUI.Button(new Rect(120, 206, 100, 20), "Vehicle 2"))
//		{
//			Application.LoadLevel(1);
//		}
//		
//		
//		GUI.Box(new Rect(5, 5, 250, 200), "");
//		
//		GUI.Label(new Rect(10, 10, 200, 50), "VCAR (keys to control the car)");
//		
//		GUI.Label(new Rect(10, 50, 200, 50), "C key to change camera");
//		
//		GUI.Label(new Rect(10, 80, 200, 50), "SPACE key to handbrake");
//		
//		GUI.Label(new Rect(10, 100, 250, 50), "ARROWS keys or WASD keys to drive the car");
//		
//		
//		GUI.Label(new Rect(10, 150, 200, 50), "R key to Rest Scene");
//		
//		
//		
//		if (Input.GetKeyDown(KeyCode.R))
//		{
//			Application.LoadLevel(Application.loadedLevel);
//		}
//		
		
		
		}
	
	
	
	
}
