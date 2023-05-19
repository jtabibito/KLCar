using UnityEngine;
using System.Collections;

public partial class ContainerHallUIController : UIControllerBase {
	

	// Use this for initialization
	void Start () {
		this.ButtoncarList.GetComponent<UIButton> ().onClick.Add(new EventDelegate (this.OnClickButtoncarList));
		this.Buttonworld.GetComponent<UIButton> ().onClick.Add (new EventDelegate (this.OnClickButtonworld));
	}
	
	// Update is called once per frame
	void Update () {
//		this.LabelDiamond.GetComponent<UILabel>().text = MainState.Instance.playerInfo.diamond.ToString();
//		this.LabelGold.GetComponent<UILabel> ().text = MainState.Instance.playerInfo.gold.ToString();
//		this.LabelPower.GetComponent<UILabel> ().text = "0";
//		this.LabelVipNumber.GetComponent<UILabel> ().text = "0";
//		this.LabelGonggao.GetComponent<UILabel> ().text = "新版游戏横空出世！！！";
	}

	void OnClickButtoncarList(){
		Debug.Log ("ok");
	}

	void OnClickButtonworld()
	{
		LogicManager.Instance.ActNewLogic<LogicEnterRace> (null,null);
//		RaceManager.Instance.InitRace ("1");
	}
}
