using ProtoBuf;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using MyGameProto;

public class TestScene1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(!MainState.Instance.isRun)
		{
			LogicManager.Instance.ActNewLogic<LogicEnterGame>(null,null);
			MainState.Instance.isRun=true;
		}
	}

}
