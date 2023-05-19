using UnityEngine;
using System.Collections;

public partial class ContainerLoginUIController :UIControllerBase {

	// Use this for initialization
	void Start () {
		this.login.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickLogin));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClickLogin(){
		LogicManager.Instance.ActNewLogic<LogicQuicklyLogin> (null, this.OnLoginOver);
	}

	void OnLoginOver(Hashtable logicPar){
		this.CloseUI ();
	}
}
