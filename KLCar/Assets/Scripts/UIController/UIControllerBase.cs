using UnityEngine;
using System.Collections;

public abstract class UIControllerBase : MonoBehaviour {
	public void CloseUI()
	{
		Destroy (this.gameObject);
	}
}
