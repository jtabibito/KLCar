using UnityEngine;
using System.Collections;

public class SoundRetriever : MonoBehaviour
{
	private float time;
	void Start ()
	{
		 
	}
	void Update ()
	{
	
	}
	void FixedUpdate()
	{
		time += Time.deltaTime;
		if (time > 0.5f)
		{
			time=0;
			onNextFrame();
		}
	}
	void onNextFrame()
	{
		SoundManager.gc ();
	}
	void OnLevelWasLoaded (int level)
	{
		SoundManager.resetListener ();
	}

}

