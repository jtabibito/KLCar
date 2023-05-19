using UnityEngine;
using System.Collections;

public class RoleAnimatorController : MonoBehaviour {

	Animator anim;

	void Awake()
	{
		anim=this.GetComponent<Animator>();
	}

	public void TriggerAct(string triggerName)
	{
		this.anim.SetTrigger(name);
	}
}
