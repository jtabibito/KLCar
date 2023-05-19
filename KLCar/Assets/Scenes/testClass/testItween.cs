using UnityEngine;
using System.Collections;

public class testItween : MonoBehaviour
{
	public Transform a;
	public Transform b;
	GameObjectPool pool;
	void Start()
	{
//		  pool= GameObjectPools.getPool (a.gameObject);
		TweenLite.doTween(gameObject, 3, TweenUtils.easeInExpo, new TweenLite.TweenLiteFunc  (tweenUpdate));
	}
	 void Update()
	{
//		if (Input.anyKeyDown ) {
//			pool.newInstance();
//				}
	}
	void tweenUpdate(object obj,float value,TweenLite  tween)
	{
		Debug.Log (tween.progress +">>>>>>>>"+value);
	}
}
