using UnityEngine;
using System.Collections;
/// <summary>
/// 虚拟现实的摄像头.可以自动将当前GameObject上的摄像头转换为3D立体摄像头.可以随意开关.
/// </summary>
public class VRCamera : MonoBehaviour {

	public float eyeOffset=1.5f;
	public float lookPointDiraction=200;
	private GameObject le;
	private GameObject re;
	private GameObject lookPoint;
	private Camera parentCamera;
	void Start () {
	
	}
	void OnEnable()
	{
		if (parentCamera == null)
		{
			parentCamera = gameObject.camera;
			lookPoint = new GameObject ("lookPoint");
			lookPoint.transform.parent=transform;
			le = createCamera (true);
			re = createCamera (false);
		} else
		{
			le.SetActive(true);
			re.SetActive(true);
		}
		parentCamera.camera.enabled=false;
	}
	GameObject createCamera( bool isLeft)
	{
		GameObject le;
		le =new GameObject(isLeft?"leftEye":"rightEye");
		Camera c= le.AddComponent<Camera>();
		le.transform.parent=transform;
		c.camera.fieldOfView = parentCamera.fieldOfView;
		c.camera.rect = isLeft ? new Rect (0,0,0.5f,1) : new Rect (0.5f,0,0.5f,1);
		return le;
	}
	void OnDisable()
	{
		if (parentCamera != null)
		{
			le.SetActive(false);
			re.SetActive(false);
			parentCamera.camera.enabled=true;
		}
	}
	void Update () {
//		Vector3 v= lookPoint.transform.localPosition;
//		v.z = lookPointDiraction;
		lookPoint.transform.localPosition = new Vector3(0,0,lookPointDiraction);

//		v= le.transform.localPosition;
//		v.x = -eyeOffset / 2;
		le.transform.localPosition = new Vector3( -eyeOffset / 2,0,0);
		le.transform.LookAt (lookPoint.transform );
		//
//		v= re.transform.localPosition;
//		v.x = eyeOffset / 2;
		re.transform.localPosition = new Vector3( eyeOffset / 2,0,0);
		re.transform.LookAt (lookPoint.transform );
	}
}
