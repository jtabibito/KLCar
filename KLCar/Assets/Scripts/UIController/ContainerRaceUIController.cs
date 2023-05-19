using UnityEngine;
using System.Collections;

public partial class ContainerRaceUIController : UIControllerBase {

	// Use this for initialization
	void Start () {
		UIEventListener.Get (this.ButtonLeftTurn).onPress = this.OnPressButtonLeftTurn;
		UIEventListener.Get (this.ButtonRightTurn).onPress = this.OnPressButtonRightTurn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPressButtonLeftTurn(GameObject go,bool state)
	{
		if(state)
		{
			RaceManager.Instance.OnInput(RaceManager.InputType.it_leftDown);
		}
		else
		{
			RaceManager.Instance.OnInput(RaceManager.InputType.it_noInput);
		}
	}

	void OnPressButtonRightTurn(GameObject go,bool state)
	{
		if(state)
		{
			RaceManager.Instance.OnInput(RaceManager.InputType.it_rightDown);
		}
		else
		{
			RaceManager.Instance.OnInput(RaceManager.InputType.it_noInput);
		}
	}
}
