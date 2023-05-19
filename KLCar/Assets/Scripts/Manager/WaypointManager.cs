/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WaypointManager : MonoBehaviour {

    //this dictionary stores all path names and for each path its manager component with waypoint positions
    //enemies will receive their specific path component
    public static readonly Dictionary<string, PathManager> Paths = new Dictionary<string, PathManager>();


    //execute this before any other Start() or Update() function
    //since we need the data of all paths before we call them
    void Awake()
    {
        //for each child/path of this gameobject, add path to dictionary
        foreach (Transform path in transform) 
        {
            AddPath(path.gameObject);
        }
    }

	
	//this adds a path to the dictionary above, so our walker objects can access them
	public static void AddPath(GameObject path)
	{
        //check if path contains the name "Clone" (path was instantiated)
        if (path.name.Contains("Clone"))
        {
            //replace/remove "(Clone)" with an empty character
            path.name = path.name.Replace("(Clone)", "");
        }

        //check if path dictionary already contains this path name
        if (Paths.ContainsKey(path.name))
        {
            //debug warning and abort
            Debug.LogWarning("Called AddPath() but Scene already contains Path " + path.name + ".");
            return;
        }

		//get PathManager component
        PathManager pathMan = path.GetComponent<PathManager>();
        
        //if pathMan is null, so our path GameObject has no PathManager, debug warning and abort
        if (pathMan == null)
        {
            Debug.LogWarning("Called AddPath() but Transform " + path.name + " has no PathManager attached.");
            return;
        }

        //add path name and its manager reference to above dictionary to allow indirect access
        Paths.Add(path.name, pathMan);
	}

}
