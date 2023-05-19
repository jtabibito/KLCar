using UnityEngine;
using System.Collections;

public class TestUI : MonoBehaviour {

	GameObject ButtonTrigger;
	GameObject ButtonState;
	GameObject r1;
	GameObject r2;
	GameObject r3;
	GameObject r4;
	GameObject r5;
	GameObject r6;
	int nowState=0;
	void Awake()
	{
		ButtonTrigger=this.transform.FindChild("ButtonTrigger").gameObject;
		ButtonState=this.transform.FindChild("ButtonState").gameObject;
		r1=GameObject.Find("r1");
		r2=GameObject.Find("r2");
		r3=GameObject.Find("r3");
		r4=GameObject.Find("r4");
		r5=GameObject.Find("r5");
		r6=GameObject.Find("r6");


		ButtonTrigger.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonTrigger));
		ButtonState.GetComponent<UIButton>().onClick.Add(new EventDelegate(this.OnClickButtonState));
		nowState=this.r1.GetComponent<Animator>().GetInteger("roleState");
	}

	void OnClickButtonTrigger()
	{
		this.r1.GetComponent<Animator>().SetTrigger("t_attack");
		this.r2.GetComponent<Animator>().SetTrigger("t_attack");
		this.r3.GetComponent<Animator>().SetTrigger("t_attack");
		this.r4.GetComponent<Animator>().SetTrigger("t_attack");
		this.r5.GetComponent<Animator>().SetTrigger("t_attack");
		this.r6.GetComponent<Animator>().SetTrigger("t_attack");
	}

	void OnClickButtonState()
	{
		if(nowState==5)
		{
			nowState=0;
		}
		else
		{
			nowState=nowState+1;
		}
		this.r1.GetComponent<Animator>().SetInteger("roleState",this.nowState);
		this.r2.GetComponent<Animator>().SetInteger("roleState",this.nowState);
		this.r3.GetComponent<Animator>().SetInteger("roleState",this.nowState);
		this.r4.GetComponent<Animator>().SetInteger("roleState",this.nowState);
		this.r5.GetComponent<Animator>().SetInteger("roleState",this.nowState);
		this.r6.GetComponent<Animator>().SetInteger("roleState",this.nowState);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
