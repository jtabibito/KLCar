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

//custom iMove inspector
//inspect iMove.cs and extend from Editor class
[CustomEditor(typeof(iMove))]
public class iMoveEditor : Editor
{
    //define Serialized Objects we want to use/control from iMove.cs
    //this will be our serialized reference to the inspected script
    private SerializedObject m_Object;
    //observe property "sizeToAdd" of iMove.cs for displaying a custom input field
    private SerializedProperty m_Size;

    //delay array size, define path to know where to lookup for this variable
    //(we expect an array, so it's "name_of_array.data_type.size")
    private static string spArraySize = "StopAtPoint.Array.size";
    //.data gives us the data of the array,
    //we replace this {0} token with an index we want to get
    private static string spArrayData = "StopAtPoint.Array.data[{0}]";

    //inspector scrollbar x/y position, modified by mouse input
    private Vector2 scrollPos;
    //whether delay settings menu should be visible
    private bool showDelaySetup = false;
    //variable to set all delay slots to this value
    private float delayAll = 0;


    //called whenever this inspector window is loaded 
    public void OnEnable()
    {
        //we create a reference to our script object by passing in the target
        m_Object = new SerializedObject(target);
        //get reference to iMove's variable "sizeToAdd"
        m_Size = m_Object.FindProperty("sizeToAdd");
    }


    //returns PathManager component of variable "pathContainer" for later use
    //if no Path Container is set, this will return null, so we need to check that below
    private PathManager GetPathTransform()
    {
        //get pathContainer from serialized property and return its PathManager component
        return m_Object.FindProperty("pathContainer").objectReferenceValue as PathManager;        
    }


    private float[] GetStopPointArray()
    {
        //get array count from Path Manager component by accessing waypoints length,
        //and store value into var arrayCount. why length+1?
        //here we do a little trick: when we modify the last waypoint delay value
        //for example to 5, so our walker should wait 5 seconds at the last waypoint,
        //and then switch the path of this walker, because of array resizing all later
        //waypoints have a 5 second delay too. So we increase the length by 1, this slot
        //will be zero because we do not use it, and on resizing this value will be used.
        var arrayCount = GetPathTransform().waypoints.Length+1;
        //create new float array with size of arrayCount
        var floatArray = new float[arrayCount];

        //get StopAtPoint array count from serialized property
        //and store its int value into var array
        var array = m_Object.FindProperty(spArraySize);
        //resize StopAtPoint array to waypoint array count
        array.intValue = arrayCount;

        //loop over waypoints
        for (var i = 0; i < arrayCount; i++)
        {
            //for each one use "FindProperty" to get the associated object reference
            //of StopAtPoint array, string.Format replaces {0} token with index i
            //and store the object reference value as type of float in floatArray[i]
            floatArray[i] = m_Object.FindProperty(string.Format(spArrayData, i)).floatValue;
        }
        //finally return that array copy for modification purposes
        return floatArray;
    }


    private void SetPointDelay(int index, float value)
    {
        //similiar to GetPointDelay(), find serialized property which belongs to index
        //and set this value to parameter float "value" directly
        m_Object.FindProperty(string.Format(spArrayData, index)).floatValue = value;
    }


    private float GetPointDelay(int index)
    {
        //similiar to SetPointDelay(), this will return the delay value from array at index position
        //and returns it instead of modifying
        return m_Object.FindProperty(string.Format(spArrayData, index)).floatValue;
    }


    //called whenever the inspector gui gets rendered
    public override void OnInspectorGUI()
    {
        //this pulls the relative variables from unity runtime and stores them in the object
        //always call this first
        m_Object.Update();

        //show default iMove.cs public variables in inspector
        DrawDefaultInspector();

        //get Path Manager component by calling method GetWaypointArray()
        var path = GetPathTransform();

        EditorGUILayout.Space();
        //make the default styles used by EditorGUI look like controls
        EditorGUIUtility.LookLikeControls();
        //display custom float input field to change value of variable "sizeToAdd"
        EditorGUILayout.PropertyField(m_Size);

        //draw bold delay settings label
        GUILayout.Label("Delay Settings:", EditorStyles.boldLabel);

        //check whether a Path Manager component is set, if not display a label
        if (path == null)
        {
            GUILayout.Label("No path set.");
			
			//get StopAtPoint array count from serialized property and resize it to zero
			//(in case of previously defined delay settings, clear old data)
			m_Object.FindProperty(spArraySize).intValue = 0;
        }
        //path is set and boolean for displaying delay settings is true
        //(button below was clicked)
        else if (showDelaySetup)
        {
            //get StopAtPoint array reference by calling method GetStopPointArray()
            var stopPoints = GetStopPointArray();

            EditorGUILayout.BeginHorizontal();
            //begin a scrolling view inside GUI, pass in Vector2 scroll position 
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(105));

            //loop through waypoint array
            for (int i = 0; i < path.waypoints.Length; i++)
            {
                GUILayout.BeginHorizontal();
                //draw label with waypoint index,
                //increased by one (so it does not start at zero)
                GUILayout.Label((i + 1) + ".", GUILayout.Width(20));
                //create a float field for every waypoint delay slot
                var result = EditorGUILayout.FloatField(stopPoints[i], GUILayout.Width(50));

                //if the float field has changed, set waypoint delay to new input
                //(within serialized StopAtPoint array property)
                if (GUI.changed)
                    SetPointDelay(i, result);

                GUILayout.EndHorizontal();
            }
            //ends the scrollview defined above
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginVertical();

            //draw button for hiding of delay settings
            if (GUILayout.Button("Hide Delay Settings"))
            {
                showDelaySetup = false;
            }

            //draw button to set all delay value slots to the value specified in "delayAll"
            if (GUILayout.Button("Set All:"))
            {
                //loop through all delay slots, call SetPointDelay() and pass in "delayAll"
                for (int i = 0; i < stopPoints.Length; i++)
                    SetPointDelay(i, delayAll);
            }

            //create a float field for being able to change variable delayAll
            delayAll = EditorGUILayout.FloatField(delayAll, GUILayout.Width(50));

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }
        else
        {
            //if path is set but delay settings are not shown,
            //draw button to toggle showDelaySetup
            if (GUILayout.Button("Show Delay Settings"))
            {
                showDelaySetup = true;
            }
        }
        
        //we push our modified variables back to our serialized object
        //always call this after all fields and buttons
        m_Object.ApplyModifiedProperties();
    }


    //if this path is selected, display small info boxes above all waypoint positions
    void OnSceneGUI()
    {
        //again, get Path Manager component
        var path = GetPathTransform();

        //do not execute further code if we have no path defined
        //or delay settings are not visible
        if (path == null || !showDelaySetup) return;

        //get waypoints array of Path Manager
        var waypoints = path.waypoints;

        //begin GUI block
        Handles.BeginGUI();
        //loop through waypoint array
        for (int i = 0; i < waypoints.Length; i++)
        {
            //translate waypoint vector3 position in world space into a position on the screen
            var guiPoint = HandleUtility.WorldToGUIPoint(waypoints[i].transform.position);
            //create rectangle with that positions and do some offset
            var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 60, 100, 20);
            //draw box at rect position with current waypoint name
            GUI.Box(rect, "Waypoint: " + (i + 1));
            //create rectangle and position it below
            var rectDelay = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 100, 20);
            //draw box at rectDelay position with current delay at that waypoint
            GUI.Box(rectDelay, "Delay: " + GetPointDelay(i));
        }
        Handles.EndGUI(); //end GUI block
    }
}
