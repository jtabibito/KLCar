using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		SoundManager.bg.play ("aaa/bbb/bgSound",true);//在背景组中播放循环声音.
//		SoundManager.effect.play ("bbbbbbbb");//特效组中播放一个音效.
//		SoundManager.bg.stopAll ();//停止背景组中所有声音.等效于:SoundManager.getSoundGroup("bg").stopAll();
//		SoundManager.play ("abc/ddd",false,"scence");//播放场景组中的声音.
//		SoundManager.play ("abc/bbb");//在默认特效组中播放声音.
//		SoundManager.getSoundGroup ("effect").volume=0.5f;//设置特效组的音量.
		SoundManager.mute = true;//全局静音.
		SoundManager.bg.mute = true;//某个组静音.
		SoundManager.getSoundGroup ("bg").mute = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{
			SoundManager.play("Resources/Prefabs/SceneView/Sounds/IDle1");
		}
	}
}
