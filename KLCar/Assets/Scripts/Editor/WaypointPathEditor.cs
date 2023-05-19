using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(WaypointPath))]
public class WaypointPathEditor : Editor
{
    private static bool m_editMode = false;
    private static string m_preName = "wp";
    private static string m_folderName = "wps";
    private GameObject m_container;
    public GameObject waypointFolder;
	public bool m_batchCreating = false;
    private bool m_lastFrameBatchCreating = false;

    void OnSceneGUI()
    {
        
        if (m_editMode)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                           
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit;               

                if (m_container == null)
                {
                    Debug.LogError("No container found. Place waypoints in scenes directly after pressing the Waypoint Editor button.");
                    m_editMode = false;
                    Repaint();
                }

                if (m_editMode) //2011-04-11 cse
                {               //2011-04-11 cse
                    if (Physics.Raycast(ray, out hit))
                    {
						Debug.Log("hitTaget="+hit.collider.gameObject.name);
                        int counter = 1;
                        string fullPreName;
                        fullPreName = "/" + m_folderName + "/" + m_preName + "_";
                        while (GameObject.Find(fullPreName + counter.ToString()) != null)
                        {
                            counter++;
                        }

                        Undo.RegisterSceneUndo("Create new Waypoint");
                        GameObject prefab = Resources.LoadAssetAtPath("Assets/OtherResources/Waypoint.prefab", typeof(GameObject)) as GameObject;
                        GameObject waypoint = Instantiate(prefab) as GameObject;
                        Vector3 myPosition;
                        myPosition = hit.point;
                        myPosition.y = (float)myPosition.y + (float)(waypoint.transform.localScale.y / 2);

                        waypoint.transform.position = myPosition;
                        waypoint.name = m_preName + "_" + counter.ToString();
                        waypoint.transform.parent = m_container.transform;
                        Waypoint aiwaypointScript = waypoint.GetComponent<Waypoint>();
                        EditorUtility.SetDirty(waypoint);

                        //rotate last WP 
                        GameObject lastWP = GameObject.Find(fullPreName + (counter - 1).ToString());
                        if (lastWP != null)
                        {
                            lastWP.transform.LookAt(waypoint.transform);
                            EditorUtility.SetDirty(lastWP);
                        }
                    }
					
                    m_editMode = false;
					
                }//2011-04-11 cse 
            }
        }
    }

    public override void OnInspectorGUI()
    {
        WaypointPath script = (WaypointPath)target;

        script.folderName = EditorGUILayout.TextField("WP Parent", script.folderName);
        script.preName = EditorGUILayout.TextField("WP Prefix", script.preName);
		script.batchCreating = EditorGUILayout.Toggle("Batch Creating", script.batchCreating);		

        m_preName = script.preName;
        m_folderName = script.folderName;   
		m_batchCreating = script.batchCreating;		
				
		if (m_lastFrameBatchCreating ==true && m_batchCreating==false)
		{			
			m_editMode = false;
		}
		
        if (m_editMode)
        {
            if (GUILayout.Button("Right Click in Scene View"))
            {
                                
            }
        }
        else
        {
            if (GUILayout.Button("Press for new Waypoint") || m_batchCreating)
            {
                m_editMode = true;             
                                      
                m_container = GameObject.Find(m_folderName);                
                if (m_container == null)
                {
                    waypointFolder = new GameObject();
                    waypointFolder.name = m_folderName;
                    m_container = waypointFolder;                    
                }   				
                
            }

            if (GUILayout.Button("Down To Ground"))
            {
                m_container = GameObject.Find(m_folderName);                
                if (m_container != null)
                {
                    int nPt = m_container.transform.childCount;
                    int layerMask = (1 << LayerMask.NameToLayer("Terrain")) | (1 << LayerMask.NameToLayer("Road"));
                    for (int i = 0; i < nPt; i++)
                    {
                        Transform pt = m_container.transform.GetChild(i);

                        RaycastHit hit;
                        if (Physics.Raycast(pt.position, Vector3.up, out hit, int.MaxValue, layerMask) ||
                            Physics.Raycast(pt.position, -Vector3.up, out hit, int.MaxValue, layerMask)
                            )
                        {
                            Vector3 pos = pt.position;
                            pos.y = hit.point.y + 0.5f;
                            pt.position = pos;
                            EditorUtility.SetDirty(pt.gameObject);
                        }

                    }
                }
                
            }

//			if(GUILayout.Button("Creat Road Data"))
//			{
//				List<Transform> wayPoints=script.CreatWayPointsData();
//				RaceRoadData rrd=ScriptableObject.CreateInstance<RaceRoadData>();
//				rrd.wayPoints=new List<WaypointTF>();
//				foreach(Transform tf in wayPoints)
//				{
//					WaypointTF wtf=new WaypointTF();
//					wtf.position=tf.position;
//					wtf.rotation=tf.rotation;
//					rrd.wayPoints.Add(wtf);
//				}
//				string dataPath = "Assets/Resources/Datas/RaceRoadDatas/road1.asset";
//				AssetDatabase.CreateAsset(rrd,dataPath);
//			}

            /*
            float shrinkStep = int.Parse(EditorGUILayout.TextField("shrink step", "1"));

            if (GUILayout.Button("Shrink path"))
            {
                m_container = GameObject.Find(m_folderName);
                if (m_container != null)
                {
                    int nPt = m_container.transform.childCount;
                    int layerMask = (1 << LayerMask.NameToLayer("Terrain")) | (1 << LayerMask.NameToLayer("Road"));
                    for (int i = 0; i < nPt; i++)
                    {
                        Transform pt = m_container.transform.GetChild(i);
                        pt.position = pt.position + shrinkStep * pt.right;

                        RaycastHit hit;
                        if (Physics.Raycast(pt.position, Vector3.up, out hit, int.MaxValue, layerMask) ||
                            Physics.Raycast(pt.position, -Vector3.up, out hit, int.MaxValue, layerMask)
                            )
                        {
                            Vector3 pos = pt.position;
                            pos.y = hit.point.y;
                            pt.position = pos;
                            EditorUtility.SetDirty(pt.gameObject);
                        }

                    }
                }
            }

            if (GUILayout.Button("Expand path"))
            {
                m_container = GameObject.Find(m_folderName);
                if (m_container != null)
                {
                    int nPt = m_container.transform.childCount;
                    int layerMask = (1 << LayerMask.NameToLayer("Terrain")) | (1 << LayerMask.NameToLayer("Road"));
                    for (int i = 0; i < nPt; i++)
                    {
                        Transform pt = m_container.transform.GetChild(i);
                        pt.position = pt.position - shrinkStep * pt.right;

                        RaycastHit hit;
                        if (Physics.Raycast(pt.position, Vector3.up, out hit, int.MaxValue, layerMask) ||
                            Physics.Raycast(pt.position, -Vector3.up, out hit, int.MaxValue, layerMask)
                            )
                        {
                            Vector3 pos = pt.position;
                            pos.y = hit.point.y;
                            pt.position = pos;
                            EditorUtility.SetDirty(pt.gameObject);
                        }

                    }
                }
            }*/
        }
		
		m_lastFrameBatchCreating = m_batchCreating;
		
    }


}
