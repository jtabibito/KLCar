using UnityEngine;
using System.Collections;
/**
 * 描述车的状态.和各种信息.
 */
public class CarInfo : MonoBehaviour {

	public int maxSpeed=10;
	private float currentSpeed=0;
	public float speedAdd=0;

	public float offsetX;
	/**
	 * 是否使用路点的力.
	 * 
	 */
	public bool useWayPortForce;
	/**
	 * 是否支持手动控制.
	 */
	public bool isCanControll;
	void Start () {
	
	}
	
	public float speed
	{
		set{
			if(value>maxSpeed)
			{
				value=maxSpeed;
			}
			currentSpeed=value;
		}
		get{
			return currentSpeed;
		}
	}
	void Update () {
		speed += speedAdd;
	}
}
