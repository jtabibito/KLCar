using UnityEngine;
using System.Collections;

public abstract class TriggerItemBase : MonoBehaviour {

	protected virtual void Awake()
	{
		this.gameObject.layer = LayerMask.NameToLayer ("TriggerItem");
	}

	public void TriggerByCar(CarEngine car)
	{
		this.OnTriggerCarHandler (car);
	}

	public abstract void OnTriggerCarHandler(CarEngine car);
}
