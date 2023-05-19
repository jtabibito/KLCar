using UnityEngine;
using System.Collections;

public partial class ContainerServerButtonUIController : UIControllerBase {

	// Use this for initialization
	void Start () {
//		if(MainState.Instance.playerInfo.userID==-1)
//		{
//			//游客
//			this.exit.SetActive(false);
//		}

		this.into.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickInto));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClickInto()
	{
		PanelMainUIController.Instance.EnterHall ();
	}
}
