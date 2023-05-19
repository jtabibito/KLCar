using UnityEngine;
using System.Collections;
/**
 * 车辆组件的基类.提供几个常用函数.
 */
public class CarComponentBase : MonoBehaviour {
	private Transform _showBody;
	private CarEngine _carEngine;
	private CarEffectsConfig _effectsConfig;
	void Aweek()
	{
		_carEngine=GetComponent<CarEngine> ();
		_carEngine.carBody = _showBody;
		//_showBody.GetChild ();
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Transform getShowBody 
	{
		get
		{
			return _showBody;
		}
	}
	public CarEngine carEngine
	{
		get
		{
			return _carEngine;
		}
	}
	public CarEffectsConfig effects
	{
		get
		{
			return _effectsConfig;
		}
	}
}
