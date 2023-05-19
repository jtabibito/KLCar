using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MySplinePathWaypoints : SplinePath {

	public bool show = false;
	private string m_waypointPreName = "MyWaypoint";
	private string m_waypointFolder = "WayPointsEditor";	
	private Transform parent;
	private Transform createParent;
	
	protected override void Awake () {
		
		//2012-08-05 -B
//		if(active)
//			Init();
		//2012-08-05 -E
		
	}
	
	// Use this for initialization
	void Start () {
		
		//2012-08-05 -B
		//if(active)
		//	Init();
		//2012-08-05 -E		
		
		if(show)
		{			
			SetRenderer(true);
		}
		
	}

	protected virtual void OnDrawGizmos() {
		
		if (active && (!Application.isPlaying || show))
		{
			GetWaypointNames();
			FillPath();
			FillSequence();
			DrawGizmos();
		}
		
		if(!Application.isPlaying)
		{
			//			SetDrawLineToNext();
		}
		
	}
	
	public void ExtendsRoadPoints()
	{
		
		GetWaypointNames();		
		FillPath();		
		FillSequence();		
		parent = GameObject.Find(m_waypointFolder).transform;
		GameObject createParentObj = GameObject.Find (m_waypointFolder + "_create");
		if(createParentObj==null)
		{
			createParentObj=new GameObject();
			createParentObj.transform.parent=parent.parent;
			createParentObj.transform.localPosition=parent.localPosition;
			createParentObj.transform.localRotation=parent.localRotation;
			createParentObj.transform.localScale=parent.localScale;
			createParentObj.name=(m_waypointFolder + "_create");
		}
		createParent = createParentObj.transform;
		List<Transform> tfs = new List<Transform> (this.GetTransform<Transform> (createParent));
		foreach(Transform tf in tfs)
		{
			DestroyImmediate(tf.gameObject);
		}
		CreateNewWaypoints();		
//		RenamePathObjects();
		
	}

	private IEnumerable<T> GetTransform<T>(Transform targetTF)
	{ 
		IEnumerator tor = targetTF.GetEnumerator();
		while (tor.MoveNext())
		{
			object current = tor.Current;
			yield return (T)current;
		}
	}

	
	void CreateNewWaypoints()
	{
		int counter = 0;		
		//GameObject prefab = Resources.LoadAssetAtPath("Assets/AIDriverToolkit/Prefabs/Waypoint.prefab", typeof(GameObject)) as GameObject; //2012-07-29		
		GameObject go;            			
		string currentName;		
		currentName = "/" + m_waypointFolder + "/" + m_waypointPreName + "1";            
		go = GameObject.Find(currentName);		
		GameObject prefab = go;		
		
		foreach (Vector3 point in sequence)
		{
			
			counter ++;			
			
			//den letzten erzeugen wir nicht, da dieses die gleich Position hat wie der erste
			if (counter < sequence.Count || !loop)
			{
				GameObject waypoint = Instantiate(prefab) as GameObject;                  
				waypoint.transform.position = point;
				waypoint.transform.forward = (sequence[(counter + 1) % sequence.Count] - point).normalized;
				waypoint.name = m_waypointPreName + counter.ToString();
				waypoint.transform.parent = createParent;
				Waypoint aiwaypointScript = waypoint.GetComponent("AIWaypoint") as Waypoint;
				
				CopyParameters(ref waypoint, counter);
			}
		}
		
		
	}
	
	void CopyParameters(ref GameObject waypoint, int newIndex)
	{
		
		float fltOldIndex = newIndex / (steps + 1);
		
		int intOldIndex;
		
		int modIndex = newIndex % (steps + 1);
		
		if (modIndex == 0)
		{
			intOldIndex = newIndex / (steps + 1);
		}
		else
		{
			intOldIndex = 1 +(newIndex / (steps + 1));
		}
		
		
		Waypoint oldAiWaypointScript = path[intOldIndex - 1].GetComponent("AIWaypoint") as Waypoint;
		
		Waypoint aiWaypointScript = waypoint.GetComponent("AIWaypoint") as Waypoint;
		waypoint.transform.localScale = path[intOldIndex - 1].localScale;		
		waypoint.tag = path[intOldIndex - 1].gameObject.tag; 		
		
	}
	
//	void RenamePathObjects()
//	{
//		foreach(Transform current in path)
//		{
//			
//			current.gameObject.name = current.gameObject.name + "_original";
//			
//		}
//	}
	
	void FillPath() 
	{				
		bool found=true;
		int counter=1;
		
		path.Clear();
		
		while (found)
		{
			GameObject go;            			
			string currentName;
			currentName = "/" + m_waypointFolder + "/" + m_waypointPreName + counter.ToString();            
			go = GameObject.Find(currentName);
			
			if (go != null)
			{				                
				path.Add(go.transform);
				counter++;
			}
			else
			{
				found = false;               
			}
			
		}        
	}
	
	void GetWaypointNames()
	{
		WaypointPath aiWaypointEditor;
		
		aiWaypointEditor = GetComponent<WaypointPath>();
		if (aiWaypointEditor != null)
		{
			m_waypointPreName = aiWaypointEditor.preName + "_";
			m_waypointFolder = aiWaypointEditor.folderName;
		}
	}
	
	void SetRenderer(bool active)
	{
		
		bool found=true;
		int counter=1;
		
		path.Clear();
		
		while (found)
		{
			GameObject go;            			
			string currentName;
			currentName = "/" + m_waypointFolder + "/" + m_waypointPreName + counter.ToString();            
			go = GameObject.Find(currentName);
			
			if (go != null)
			{				                
				go.renderer.enabled = active;
				counter++;
			}
			else
			{
				found = false;               
			}
			
		}      
		
	}
	
	void SetDrawLineToNext()
	{
		//		if (active)
		//		{
		//			
		//		}
		//		bool found=true;
		//        int counter=1;
		//		
		//		path.Clear();
		//		
		//        while (found)
		//        {
		//			GameObject go;            			
		//			string currentName;
		//            currentName = "/" + m_waypointFolder + "/" + m_waypointPreName + counter.ToString();            
		//			go = GameObject.Find(currentName);
		//            
		//            if (go != null)
		//            {				                
		//				DrawLineToNext drawLineToNext = go.GetComponent<DrawLineToNext>() as DrawLineToNext;
		//				if (drawLineToNext !=null)
		//				{
		//					if (active)
		//					{
		//	               		drawLineToNext.active = false;
		//					}
		//					else
		//					{
		//						drawLineToNext.active = true;
		//					}
		//				}
		//				
		//            }
		//            else
		//            {
		//                found = false;               
		//            }
		//            
		//       	}      
	}	
	
}
