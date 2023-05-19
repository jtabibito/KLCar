using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MySplinePathWaypoints))]
public class SplinePathWaypointsEditor : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();
		MySplinePathWaypoints script = (MySplinePathWaypoints)target;
		if (GUILayout.Button("Extends Path Way Points"))
		{
			script.ExtendsRoadPoints();
			Repaint();
		}
	}
}
