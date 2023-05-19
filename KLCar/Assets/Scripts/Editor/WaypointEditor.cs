/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//path start/finalize manager
[CustomEditor(typeof(WaypointManager))]
public class WaypointEditor : Editor
{
    private WaypointManager script;     //manager reference
    private bool placing = false;   //if we are placing new waypoints in editor
    private GameObject path; //new path gameobject
    private string pathName = "";    //new path name
    private PathManager pathMan; //Path Manager reference to edit waypoint array
    private List<GameObject> wpList = new List<GameObject>();   //temporary list for editor created waypoints in a path

    //scene view input
    public void OnSceneGUI()
    {
        //if left mouse button was clicked, in combination with alt key and placing is true, enable waypoint editing
        //(we clicked to start a new path)
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && Event.current.alt && placing)
        {
            //create a ray to get where we clicked on terrain/ground/objects in scene view and pass in mouse position
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            //ray hit something
            if (Physics.Raycast(worldRay, out hitInfo))
            {
                //call this method when you've used an event.
                //the event's type will be set to EventType.Used,
                //causing other GUI elements to ignore it
                Event.current.Use();

                //place a waypoint at clicked point
                PlaceWaypoint(hitInfo.point);
            }
        }
    }

    //inspector input
    public override void OnInspectorGUI()
    {
        //show default variables of script "WaypointManager"
        DrawDefaultInspector();
        //get WaypointManager.cs reference
        script = (WaypointManager)target;

        //make the default styles used by EditorGUI look like controls
        EditorGUIUtility.LookLikeControls();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        //draw path text label
        GUILayout.Label("Enter Path Name: ", EditorStyles.boldLabel, GUILayout.Height(15));
        //display text field for creating a path with that name
        pathName = EditorGUILayout.TextField(pathName, GUILayout.Height(15));

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        //create new path button
        if (GUILayout.Button("Start Path", GUILayout.Height(40)))
        {
            //no path name defined, abort with short editor warning
            if (pathName == "")
            {
                Debug.LogWarning("no path name defined");
                return;
            }

            //path name already given, abort with short editor warning
            if (script.transform.FindChild(pathName) != null)
            {
                Debug.LogWarning("path name already given");
                return;
            }

            //already started a new path, abort further operations, editor warning
            if (placing == true)
            {
                Debug.LogWarning("path already started, use alt + left mouse button to place new waypoints within scene view");
                return;
            }

            //we passed all prior checks, toggle waypoint placing on
            placing = true;
            //create a new container transform which will hold all new waypoints
            path = new GameObject(pathName);
            //attach PathManager.cs component to this new waypoint container
            pathMan = path.AddComponent<PathManager>();
            //create waypoint array instance of PathManager
            pathMan.waypoints = new Transform[0]; 
            //reset position and parent container gameobject to this manager gameobject
            path.transform.position = script.gameObject.transform.position;
            path.transform.parent = script.gameObject.transform;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //finish path button
        if (GUILayout.Button("Finish Editing", GUILayout.Height(40)))
        {
            //return if no path was started or not enough waypoints, so wpList is empty
            if (wpList.Count < 2)
            {
                Debug.LogWarning("not enough waypoints placed");

                //if we have created a path already, destroy it again
                if (path)
                    DestroyImmediate(path);
            }
            else
            {
                //switch name of last created waypoint to waypointEnd,
                //so we will recognize this path ended (this gets an other editor gizmo)
                wpList[wpList.Count - 1].name = "WaypointEnd";
                //do the same with first waypoint
                wpList[0].name = "WaypointStart";
            }

            //toggle placing off
            placing = false;

            //clear list with temporary waypoint references,
            //we only needed this list for getting first and last waypoint easily
            wpList.Clear();
            //reset path name input field
            pathName = "";
        }

        EditorGUILayout.Space();

        GUILayout.Label("Hint:\nPress 'Start Path' to begin a new path," + 
                        "\nALT + Left Click lets you place waypoints\nonto objects." + 
                        "\nPress 'Finish Editing' to end your path.");
    }


    //create waypoint
    void PlaceWaypoint(Vector3 placePos)
    {
        //instantiate waypoint gameobject
        GameObject wayp = new GameObject("Waypoint");

        //with every new waypoint, our waypoints array should increase by 1
        //but arrays gets erased on resize...
        //here we use a classical rule of three:
        //first we create a new array, "wpCache", this will be our waypoint cache array,
        //then we copy our PathManager waypoints array into that newly created array,
        //now we can resize the waypoints array, because we cached its values.
        //with the array resized, we recopy old information from wpCache into it.
        //finally, the last (new) entry should be a reference to the newly created waypoint gO
        //result: a resized array with old information and one new entry.
        Transform[] wpCache = new Transform[pathMan.waypoints.Length];
        System.Array.Copy(pathMan.waypoints, wpCache, pathMan.waypoints.Length);

        pathMan.waypoints = new Transform[pathMan.waypoints.Length+1];
        System.Array.Copy(wpCache, pathMan.waypoints, wpCache.Length);
        pathMan.waypoints[pathMan.waypoints.Length - 1] = wayp.transform;

        //this is executed on placing of the first waypoint,
        //we position our path container transform to first waypoint position,
        //so the transform (and grab/rotate/scale handles) aren't out of sight
        if (wpList.Count == 0)
            pathMan.transform.position = placePos;

        //position current waypoint at clicked position in scene view
        wayp.transform.position = placePos;
        //look up and parent it to the defined path 
        wayp.transform.parent = pathMan.transform;
        //add waypoint to temporary list
        wpList.Add(wayp);
    }
}
