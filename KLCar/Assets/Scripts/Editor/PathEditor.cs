/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//advantages you get from using Serialized Objects
//opportunity to:
//-use custom inspector windows
//-modify scripts of prefabs or revert specific values by right clicking
//-undo and redo
//-control common data types (only pass in a value, automatically use the right data type) 

//custom PathManager inspector
//inspect PathManager.cs and extend from Editor class
[CustomEditor(typeof(PathManager))]
public class PathEditor : Editor
{
    //define Serialized Objects we want to use/control from PathManager.cs
    //this will be our serialized reference to the inspected script
    private SerializedObject m_Object;
    //serialized waypoint array
    private SerializedProperty m_Waypoint;
    //serialized waypoint array count
    private SerializedProperty m_WaypointsCount;
    //serialized scene view gizmo colors
    private SerializedProperty m_Color1;
    private SerializedProperty m_Color2;

    //waypoint array size, define path to know where to lookup for this variable
    //(we expect an array, so it's "name_of_array.data_type.size")
    private static string wpArraySize = "waypoints.Array.size";
    //.data gives us the data of the array,
    //we replace this {0} token with an index we want to get
    private static string wpArrayData = "waypoints.Array.data[{0}]";


    //called whenever this inspector window is loaded 
    public void OnEnable()
    {
        //we create a reference to our script object by passing in the target
        m_Object = new SerializedObject(target);

        //from this object, we pull out the properties we want to use
        //these are just the names of our variables within PathManager.cs
        //for example here we set our serialized color properties
        m_Color1 = m_Object.FindProperty("color1");
        m_Color2 = m_Object.FindProperty("color2");

        //set serialized waypoint array count by passing in the path to our array size
        m_WaypointsCount = m_Object.FindProperty(wpArraySize);
    }


    private Transform[] GetWaypointArray()
    {
        //get array count from serialized property and store its int value into var arrayCount
        var arrayCount = m_Object.FindProperty(wpArraySize).intValue;
        //create new waypoint transform array with size of arrayCount
        var transformArray = new Transform[arrayCount];
        //loop over waypoints
        for (var i = 0; i < arrayCount; i++)
        {
            //for each one use "FindProperty" to get the associated object reference
            //of waypoints array, string.Format replaces {0} token with index i
            //and store the object reference value as type of transform in transformArray[i]
            transformArray[i] = m_Object.FindProperty(string.Format(wpArrayData, i)).objectReferenceValue as Transform;
        }
        //finally return that array copy for modification purposes
        return transformArray;
    }


    private void SetWaypoint(int index, Transform waypoint)
    {
        //similiar to GetWaypointArray(), find serialized property which belongs to index
        //and set this value to parameter transform "waypoint" directly
        m_Object.FindProperty(string.Format(wpArrayData, index)).objectReferenceValue = waypoint;
    }

    private Transform GetWaypointAtIndex(int index)
    {
        //similiar to SetWaypoint(), this will find the waypoint from array at index position
        //and returns it instead of modifying
        return m_Object.FindProperty(string.Format(wpArrayData, index)).objectReferenceValue as Transform;
    }

    private void RemoveWaypointAtIndex(int index)
    {
        //register all scene objects so we can undo to this current state
        //before removing a waypoint easily
        Undo.RegisterSceneUndo("WPDeleted");

        //call GetWaypointAtIndex() to get the corresponding waypoint to "index",
        //and destroy the whole gameobject in editor
        DestroyImmediate(GetWaypointAtIndex(index).gameObject);

        //iterate over the array, starting at index,
        //call SetWaypoint(i) to get the current waypoint
        //and replace it with the next one passing in GetWaypointAtIndex(i+1) 
        for (int i = index; i < m_WaypointsCount.intValue - 1; i++)
            SetWaypoint(i, GetWaypointAtIndex(i + 1));

        //decrement array count by 1
        m_WaypointsCount.intValue--;
    }

    private void AddWaypointAtIndex(int index)
    {
        //register all scene objects so we can undo to this current state
        //before adding this waypoint easily
        Undo.RegisterSceneUndo("WPAdd");

        //increment array count so the waypoint array is one unit larger
        //(this slot is added at the end of the array)
        m_WaypointsCount.intValue++;

        //backwards loop through array:
        //since we're adding a new waypoint for example in the middle of the array,
        //we need to push all existing waypoints after that selected waypoint
        //1 slot upwards to have one free slot in the middle. So:
        //we're doing exactly that and start looping at the end downwards to the selected slot
        for (int i = m_WaypointsCount.intValue - 1; i > index ; i--)
        {
            //get waypoint which is 1 position lower and move it up to the current slot
            SetWaypoint(i, GetWaypointAtIndex(i - 1));
        }

        //create new waypoint gameobject
        GameObject wp = new GameObject("Waypoint");
        //set its position to the last one
        wp.transform.position = GetWaypointAtIndex(index).position;
        //parent it to the path gameobject
        //(we do not have direct access to transform.parent since this is an editor script,
        //so we get the parent of the last waypoint and use that)
        wp.transform.parent = GetWaypointAtIndex(index).parent;
        //finally, set this new waypoint after the one clicked in waypoints array
        SetWaypoint(index + 1, wp.transform);
		//set the created waypoint to be the active selection in scene view
        //(so we can immediately move it to the desired position)
		Selection.activeGameObject = wp;
    }


    //called whenever the inspector gui gets rendered
    public override void OnInspectorGUI()
    {
        //this pulls the relative variables from unity runtime and stores them in the object
        //always call this first
        m_Object.Update();

        //create new property fields for editing waypoint gizmo colors 
        EditorGUILayout.PropertyField(m_Color1);
        EditorGUILayout.PropertyField(m_Color2);

        //get waypoint array by calling method GetWaypointArray()
        var waypoints = GetWaypointArray();

        //let iTween calculate path length of all waypoints
        float pathLength = iTween.PathLength(waypoints);
        //path length label, show calculated path length
        GUILayout.Label("Path Length: " + pathLength);

        //waypoint index header
        GUILayout.Label("Waypoints: ", EditorStyles.boldLabel);

        //loop through the waypoint array
        for (int i = 0; i < waypoints.Length; i++)
        {
            GUILayout.BeginHorizontal();
            //indicate each array slot with index number in front of it
            GUILayout.Label((i + 1)+".", GUILayout.Width(20));
            //create an object field for every waypoint
            var result = EditorGUILayout.ObjectField(waypoints[i], typeof(Transform), true) as Transform;

            //if the object field has changed, set waypoint to new input
            //(within serialized waypoint array property)
            if (GUI.changed)
                SetWaypoint(i, result);

            //display an "Add Waypoint" button for every array row except the last one
            //on click we call AddWaypointAtIndex() to insert a new waypoint slot AFTER the selected slot
            if (i < waypoints.Length - 1 && GUILayout.Button("+", GUILayout.Width(30f)))
                AddWaypointAtIndex(i);

            //display an "Remove Waypoint" button for every array row except the first and last one
            //on click we call RemoveWaypointAtIndex() to delete the selected waypoint slot
            if (i > 0 && i < waypoints.Length - 1 && GUILayout.Button("-", GUILayout.Width(30f)))
                RemoveWaypointAtIndex(i);

            GUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        //move all waypoints down to the ground - button
        if (GUILayout.Button("Place to Ground"))
        {
            //register all scene objects so we can undo to this current state
            //before placing all waypoints to ground easily
            Undo.RegisterSceneUndo("WPPlace");

            //for each waypoint of this path
            foreach (Transform trans in waypoints)
            {
                //define ray to cast downwards waypoint position
                Ray ray = new Ray(trans.position + new Vector3(0, 2f, 0), -trans.up);
                RaycastHit hit;
                //cast ray against ground, if it hit:
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //position y values of waypoint to hit point
                    trans.position = new Vector3(trans.position.x, hit.point.y, trans.position.z);
                }
            }
        }

        EditorGUILayout.Space();

        //invert direction of whole path
        if (GUILayout.Button("Invert Direction"))
        {
            //register all scene objects so we can undo to this current state
            //before inverting all waypoints easily
            Undo.RegisterSceneUndo("WPInvert");

            //to save all old positions and know where they were before
            //so we can reverse the whole path, we need to copy our
            //current waypoint array into a new one.
            //Array.Copy() just gives us references and would change both arrays,
            //so we classically create a new array with length of the current one,
            //loop through them and copy all position data to the newly created array 
            Vector3[] waypointCopy = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypointCopy[i] = waypoints[i].position;
            }

            //now as we have a copy of our waypoint array,
            //we loop through the old one beginning from the first waypoint,
            //and set them to the ones in our copied array,
            //starting from the last in descending order,
            //so we completely reversed the order
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i].position = waypointCopy[waypointCopy.Length-1-i];
            }
        }

        //we push our modified variables back to our serialized object
        //always call this after all fields and buttons
        m_Object.ApplyModifiedProperties();
    }


    //if this path is selected, display small info boxes above all waypoint positions
    void OnSceneGUI()
    {
        //again, get waypoint array
        var waypoints = GetWaypointArray();
        //do not execute further code if we have no waypoints defined
        //(just to make sure, practically this can not occur)
        if (waypoints.Length == 0) return;

        //begin GUI block
        Handles.BeginGUI();
        //loop through waypoint array
        for (int i = 0; i < waypoints.Length; i++)
        {
            //translate waypoint vector3 position in world space into a position on the screen
            var guiPoint = HandleUtility.WorldToGUIPoint(waypoints[i].transform.position);
            //create rectangle with that positions and do some offset
            var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 100, 20);
            //draw box at position with current waypoint name
            GUI.Box(rect, "Waypoint: " + (i + 1));
        }
        Handles.EndGUI(); //end GUI block
    }
}
