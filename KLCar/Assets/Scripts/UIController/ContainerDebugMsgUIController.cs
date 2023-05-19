using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class ContainerDebugMsgUIController : UIControllerBase {

	public static List<string> debugMsgs=new List<string>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string setMsg="";
		foreach(string getMsg in debugMsgs)
		{
			setMsg+=getMsg+"\n";
		}
		this.LabelDebugMsg.GetComponent<UILabel>().text=setMsg;
	}

	public static void AddMsg(string msg)
	{
		debugMsgs.Add(msg);
	}

	public void CleanMsg()
	{
		debugMsgs.Clear();
	}
}
