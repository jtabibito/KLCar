using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public static Test instance;
	public   GameObject  obj;
	public AudioClip clip;
	public AudioSource c;
	void Start ()
	{
		c = audio;
		Debug.Log ("start>"+audio);
		DestroyObject (audio);
		Debug.Log ("end>"+audio+" "+c);
		c.volume = 0.2f;
	}
	void Update()
	{
		Debug.Log ("start----->"+audio+" "+c);
		DestroyObject (audio);
		Debug.Log ("end------>"+audio);
	}
}
