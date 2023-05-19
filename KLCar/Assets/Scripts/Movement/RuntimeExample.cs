/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using System.Collections;

//this class demonstrates the use of SWS at runtime
public class RuntimeExample : MonoBehaviour
{
    //path to instantiate at runtime
    public GameObject pathPrefab;
    //walker object to instantiate at runtime
    public GameObject walkerPrefab;


    //start both example coroutines
    void Start()
    {
        StartCoroutine("RuntimeInstantiation");
        StartCoroutine("ChangePathAtRuntime");
    }

    //--------- this short example method visualizes: ---------\\
    //*instantiate a walker object and a path at runtime
    //*add path reference to our WaypointManager so other have access to it
    //*set path container of this object and start moving
    //*reposition path (walker object automatically gets new waypoint positions)
    //*stop movement
    //*continue movement
    IEnumerator RuntimeInstantiation()
    {
        //instantiate walker prefab
        GameObject walkerObj = (GameObject)Instantiate(walkerPrefab, transform.position, Quaternion.identity);
        //instantiate path prefab
        GameObject newPath = (GameObject)Instantiate(pathPrefab, transform.position, Quaternion.identity);
        //rename the path to ensure it is unique
        newPath.name = "RuntimePath1";

        //add newly instantiated path to the WaypointManager dictionary
        WaypointManager.AddPath(newPath);

        //get iMove component of this walker object
        iMove walkeriM = walkerObj.GetComponent<iMove>();
        //set path container to path instantiated above - access WaypointManager dictionary
        //and start movement on new path
        walkeriM.SetPath(WaypointManager.Paths["RuntimePath1"]);

        Debug.Log("[RuntimeExample.cs] Ex1: Instantiated: " + 
                    newPath.name + " and " + walkerObj.name + " running.");

        //wait few seconds
        yield return new WaitForSeconds(5);

        //change instantiated path position so we can distinguish it from example two
        //get path transform and reposition it 10 z units further away
        newPath.transform.position += new Vector3(-20, 0, 0);

        Debug.Log("[RuntimeExample.cs] Ex1: Repositioned: " + newPath.name);

        //wait few seconds
        yield return new WaitForSeconds(10);

        //stop any movement and reset to first waypoint
        walkeriM.Reset();
        //to only stop it instead, use
        //walkeriM.Stop();

        Debug.Log("[RuntimeExample.cs] Ex1: Resetted: " + walkerObj.name);

        //wait few seconds
        yield return new WaitForSeconds(10);

        //set moveToPath boolean of instantiated walker to true,
        //so on calling StartMove() it does not appear at the next waypoint but walks to it instead
        walkeriM.moveToPath = true;
        //continue movement
        walkeriM.StartMove();

        Debug.Log("[RuntimeExample.cs] Ex1: Continued movement on: " + newPath.name);
    }


    //--------- this short example method visualizes: ---------\\
    //*instantiate a walker object and a path at runtime,
    //*reposition path, but our walker object does not use it yet
    //*add path reference to our WaypointManager so other have access to it
    //*set path container of path instantiated in method above ("RuntimePath1") and start moving
    //*change path at runtime - switch from "RuntimePath1" to "RuntimePath2"
    IEnumerator ChangePathAtRuntime()
    {
        //instantiate walker prefab
        GameObject walkerObj = (GameObject)Instantiate(walkerPrefab, transform.position, Quaternion.identity);
        //instantiate path prefab
        GameObject newPath = (GameObject)Instantiate(pathPrefab, transform.position, Quaternion.identity);
        //rename the path to ensure it is unique
        newPath.name = "RuntimePath2";

        //change instantiated path position so we can distinguish it from example one
        newPath.transform.position += new Vector3(24.5f, 0, 0);

        //add newly instantiated path to the WaypointManager dictionary
        WaypointManager.AddPath(newPath);

        //get iMove component of this walker object
        iMove walkeriM = walkerObj.GetComponent<iMove>();
        //set half speed of this walker
        walkeriM.speed /= 2;
        //set path container to path instantiated in "RuntimeInstantiation()"
        //- access WaypointManager dictionary and start movement on new path
        walkeriM.SetPath(WaypointManager.Paths["RuntimePath1"]);

        Debug.Log("[RuntimeExample.cs] Ex2: Instantiated: " +
            newPath.name + " and " + walkerObj.name + " running on RuntimePath1.");

        //you could call that function within one line if you don't need to change other iMove properties:
        //walkerObj.GetComponent<iMove>().SetPath(WaypointManager.Paths["RuntimePath1"]);
        //or
        //walkerObj.SendMessage("SetPath", WaypointManager.Paths["RuntimePath1"]);

        //wait few seconds
        yield return new WaitForSeconds(5);

        //set moveToPath boolean of instantiated walker to true,
        //so on calling SetPath() it does not appear at the new path but walks to it instead
        walkeriM.moveToPath = true;
        //change path to the path instantiated in this method,
        //- switch from "RuntimePath1" to "RuntimePath2"
        walkeriM.SetPath(WaypointManager.Paths[newPath.name]);

        Debug.Log("[RuntimeExample.cs] Ex2: " + walkerObj.name + " changed path to: " + newPath.name);
    }
}
