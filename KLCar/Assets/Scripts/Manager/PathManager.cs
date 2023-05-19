/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PathManager : MonoBehaviour
{
    //array to store all waypoint transforms of this path
    public Transform[] waypoints;

    public Color color1 = new Color(1, 0, 1, 0.5f); //cube color
    public Color color2 = new Color(1, 235 / 255f, 4 / 255f, 0.5f); //sphere color

    //waypoint gizmo radius
    private float radius = .4f;
    //waypointStart/-End box gizmo size
    private Vector3 size = new Vector3(.7f, .7f, .7f);


    void OnDrawGizmos()
    {
        //differ between children waypoint types:
        //waypointStart or waypointEnd, draw small cube gizmo using color2
        //standard waypoint, draw small sphere using color1
        foreach (Transform child in transform)
        {
            if (child.name == "Waypoint")
            {
                //assign chosen color2 to current gizmo color
                Gizmos.color = color2;
                //draw wire sphere at waypoint position
                Gizmos.DrawWireSphere(child.position, radius);
            }
            else
            {
                //assign chosen color1 to current gizmo color
                Gizmos.color = color1;
                //draw wire cube at waypoint position
                Gizmos.DrawWireCube(child.position, size);
            }
        }

        //let iTween draw lines between waypoints with color2
        iTween.DrawLine(waypoints, color2);
    }
}
