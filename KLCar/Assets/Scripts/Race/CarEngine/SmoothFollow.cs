using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	public Transform target;
	public float smooth = 0.3f;
	
	public float distance = 3.55f;
	public float height = 1.5f;
	public float angle = 1.0f;
	public string[] cameraSwitchView;
//	public GUISkin GUISkin;
	private float yVelocity = 0.0f;
	private float xVelocity = 0.0f;
	/**
	 * 冲刺的镜头距离.
	 */
	public float fireDistanceOffset = 5f;
	/**
	 * 摄像机冲刺时,拉远的速率.值越大,拉动越快.
	 */
	public float cameraMoveSpeed=0.03f;
	private int Switch;
	private Transform parent;
	public float totalDistance = 0f;
	
	/**
	* 默认屏幕抖动的强度.
	 */
	public float snakeInTensity=1.0f;
	/**
			 * 默认屏幕抖动的时间.
		 */
	public float snakeDuration=0.5f;
	/**
	 * 当前是否正在播放冲刺的镜头.
	 */
	private bool  isPlayFire;
	private int _disableConter=0;
	/// <summary>
	/// 是不是在默认开始的时候,是黑幕的.
	/// </summary>
	public bool startWithFade=true;
	/// <summary>
	/// T摄像头距离裁剪.
	/// </summary>
	public float farClipPlane=300f;
	void Awake() 
	{
//		if (startWithFade)
//		{
			TweenUtils.CameraFadeTo (1,0);
//		}
		camera.farClipPlane = farClipPlane;
	}
	void Start ()
	{
		parent = transform.parent;
		totalDistance = distance;
	}
	public int disableCounter
	{
		get
		{
			return _disableConter;
		}
		set 
		{
			_disableConter=value;
			if(_disableConter>0)
			{
				enabled=false;
			}else{
				enabled=true;
			}
		}
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
		if (target == null)
						return;
		//Debug.Log(transform.eulerAngles+"  >>>>");
		if (Time.timeScale == 0)
			return;
		if (isPlayFire) {
			totalDistance = Mathf.Lerp (totalDistance, this.distance + fireDistanceOffset, cameraMoveSpeed);
		} else {
			totalDistance = Mathf.Lerp (totalDistance, this.distance, cameraMoveSpeed*0.6f);
		}
//		totalDistance = this.distance + fireDistanceOffset;
		//		var carScript = (VehicleControl)target.GetComponent<VehicleControl>();
		//		camera.fieldOfView = Mathf.Clamp(carScript.speed / 20.0f + 60.0f, 60, 70);
		
		
		
		if (Input.GetKeyDown (KeyCode.C)) {

			for(int i=0;i<=cameraSwitchView.Length;i++)
			{
				Switch++;
				if (Switch > cameraSwitchView.Length) {
					Switch = 0;
					break;
				}
				if(Switch!=0)
				{
					Transform go = target.parent.FindChild (cameraSwitchView [Switch - 1]);
					if(go!=null)
					{
						break;
					}
				}
			}
		}
		
		
		
		if (Switch == 0) {
						
			transform.parent = parent;
			
			float yAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y,
			                                      target.eulerAngles.y, ref yVelocity, smooth);
			
			
			float xAngle = Mathf.SmoothDampAngle (transform.eulerAngles.x,
			                                      target.eulerAngles.x+angle, ref xVelocity, smooth);
 
			transform.eulerAngles = new Vector3 (xAngle  , yAngle, 0);

			Vector3 direction = transform.rotation * -Vector3.forward;
			float targetDistance = AdjustLineOfSight (target.position + new Vector3 (0, height, 0), direction);
			
 			Vector3 pos=target.position + new Vector3 (0, height, 0) + direction * targetDistance;;
			transform.position = pos;
				//Vector3.Lerp(transform.position,pos,smooth);;//pos;
			
		} else {

			Transform go = target.parent.FindChild (cameraSwitchView [Switch - 1]);
			if (go != null) {
				transform.localPosition = Vector3.zero;//go.transform.position;
				transform.parent = go.transform.transform;
				transform.rotation = Quaternion.Lerp (transform.rotation, go.transform.rotation, Time.deltaTime * 10.0f);
			}
			
		}
	}
	private Vector3 lastPos1;
	private Vector3 lastPos2;
	/**
		 * 播放一个镜头震动.
		 */
	public void playSnake(float intensity,float duration)
	{
		if (target != null && target == RaceManager.Instance.PlayerCar.transform.FindChild ("Engine"))
		{
			iTween.ShakeRotation (gameObject, iTween.Hash ("y",intensity,"time",duration ));
		}
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
}

