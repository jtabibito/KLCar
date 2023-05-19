using UnityEngine;
using System.Collections;
/**
 * 控制赛车比赛时的显示状态.
 * 
 */
public class carShowStateControll : MonoBehaviour {
	/**
	 * 转弯的总方向偏移.
	 * 
	 */
	private float offset;
	/**
	 * 当前的转弯状态.
	 */
	private int _currentTurnState;
	/**
	 * 最大的偏移值.
	 */
	public int maxTrunOffset=45;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float last = offset;
		offset = Mathf.Lerp (offset, maxTrunOffset * _currentTurnState, 0.2f*Time.deltaTime);
		if (last != offset) {
			transform.localRotation=Quaternion.AngleAxis(offset,Vector3.forward);
		}
	}
	/**
	 * 当前的转弯状态.只接受值0,1,-1
	 */
	public int currentTurnState
	{
		get
		{
			return _currentTurnState;
		}
		set {
			if(value==0||value==1||value==-1)
			{
				_currentTurnState=value;
			}else{
				Debug.LogWarning("设置车的旋转状态错误.只能接受0,1,-1,当前为:"+value);
			}
		}
	}

}
