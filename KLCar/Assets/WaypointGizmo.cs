using UnityEngine;
using System.Collections;

public class WaypointGizmo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        renderer.enabled = false;
    }
}
