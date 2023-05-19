/*  This file is part of the "Simple Waypoint System" project by baronium3d.
 *  This project is available on the Unity Store. You are only allowed to use these
 *  resources if you've bought them from the Unity Assets Store.
 */

using UnityEngine;
using System.Collections;

//movement script
public class iMove : MonoBehaviour
{
    //which path to call
    public PathManager pathContainer;
    //should this gameobject start to walk on game launch?
    public bool onStart = false;
    //should this gameobject walk to the first waypoint or just spawn there?
    public bool moveToPath = false;
    //should this gameobject look to its target point?
    public bool orientToPath = false;
    //delay for each waypoint
    [HideInInspector]
    public float[] StopAtPoint;    
    //custom object size to add
    [HideInInspector]
    public float sizeToAdd = 0;

    //time or speed value
    public float speed = 5;
    //animation easetype
    public iTween.EaseType easetype = iTween.EaseType.linear;
    //animation looptype
    public LoopType looptype = LoopType.pingPong;
    //enum to choose from available looptypes
    public enum LoopType 
    {
        none,
        loop,
        pingPong,
        random
    }

    //cache all waypoint position references of requested path
    private Transform[] waypoints;
    //location indicator
    private int currentPoint = 0;
    //used on looptype = pingpong for counting currentpoint backwards
    private bool repeat = false;

    //we have the choice between 2 different move options:
    //time in seconds one node step will take to complete
    //or animation based on speed
    public TimeValue timeValue = TimeValue.speed;
    public enum TimeValue
    {
        time,
        speed
    }

	//animation to play during walk time
	public AnimationClip walkAnim;
	//animation to play during waiting time
    public AnimationClip idleAnim;
    //whether animations should fade in over a period of time or not
    public bool crossfade = false;

	
    //initialize waypoint positions
    //checks if gameobject should move on game start
    void Start()
    {
        //start moving instantly
        if (onStart)
            StartMove();
    }


    //can be called from an other script also to allow start delay
    public void StartMove()
    {
        //if we start the game and no path Container is set, debug a warning and return
        if (pathContainer == null)
        {
            Debug.LogWarning(gameObject.name + " has no path! Please set Path Container.");
            return;
        }

        //get Vector3 array with waypoint positions
        waypoints = pathContainer.waypoints;

        //if we should not walk to the first waypoint,
        //we set this gameobject position directly to it and launch the next waypoint routine
        if (!moveToPath)
        {
            //we also add a defined size to our object height,
            //so our gameobject could "stand" on top of the path.
            transform.position = waypoints[currentPoint].position + new Vector3(0, sizeToAdd, 0);
            //we're now at the first waypoint position,
            //so directly call the next waypoint
            StartCoroutine("NextWaypoint");
            return;
        }

        //move to the next waypoint
        Move(currentPoint);
    }


    //attach a new iTween MoveTo component to our gameobject which moves us to the next waypoint
    //(defined by passed argument)
    void Move(int point)
    {
        //if a walk animation is attached to this walker object and set,
        //fade idle animation out (crossfade = true) and fade walk anim in,
        //or play it instantly (crossfade = false)
        if (walkAnim)
        {
            if (crossfade)
                animation.CrossFade(walkAnim.name, 0.2f);
            else
                animation.Play(walkAnim.name);
        }

        //create a hashtable to store iTween parameters
        Hashtable iTweenHash = new Hashtable();

        //prepare iTween's parameters, you can look them up here
        //http://itween.pixelplacement.com/documentation.php#MoveTo
        ////////////////////////////////////////////////////////////
        //we also add a defined value to our gameobject position, so it walks "on" the path
        iTweenHash.Add("position", waypoints[point].position + new Vector3(0, sizeToAdd, 0));
        iTweenHash.Add("easetype", easetype);
        iTweenHash.Add("orienttopath", orientToPath);
        iTweenHash.Add("oncomplete", "NextWaypoint");

        //differ between TimeValue like mentioned above at enum TimeValue
        if (timeValue == TimeValue.time)    //use time
        {
            iTweenHash.Add("time", speed);
        }
        else //use speed
        {
            iTweenHash.Add("speed", speed);
        }

        //move this gameobject to the defined waypoint with given arguments
        iTween.MoveTo(gameObject, iTweenHash);
    }


    //this method gets called at the end of one iTween animation (after each waypoint)
    //and moves us to the next one
    IEnumerator NextWaypoint()
    {
        //only delay waypoint movement if delay settings are edited to avoid unnecessary frame yield,
        //so StopAtPoint array and current value have to be greater than zero
        if (StopAtPoint.Length > 0 && StopAtPoint[currentPoint] > 0)
        {
            //if an idle animation is attached and set,
            //and if crossfade is checked, fade walk animation out and fade idle in
            //else play it instantly
            if (idleAnim)
            {
                if (crossfade)
                    animation.CrossFade(idleAnim.name, 0.2f);
                else
                    animation.Play(idleAnim.name);
            }
            //wait seconds defined in StopAtPoint at current waypoint position
            yield return new WaitForSeconds(StopAtPoint[currentPoint]);
        }

        //we differ between all looptypes, because each one has a specific property
        switch (looptype)
        {
                //LoopType.none means, there will be no repeat,
                //so we just count up till the last waypoint and move this gameobject one by one 
            case LoopType.none:
                if (currentPoint < waypoints.Length - 1)
                    currentPoint++;
                else //abort movement if we reached the last waypoint
                    yield break;
                break;

                //in a loop, we count up till the last waypoint (like LoopType.none),
                //but then we set our position indicator back to zero and start from the beginning
            case LoopType.loop:
                //we reached the last waypoint
                if (currentPoint == waypoints.Length - 1)
                {
                    currentPoint = 0;
                    StartMove();
                    //abort further execution and do not call Move() at the end of NextWaypoint(),
                    //because this would overwrite StartMove()
                    yield break;
                }
                else
                {
                    //we're not at the end, count up waypoint index, move forward
                    currentPoint++;
                }
                break;

                //on LoopType.pingPong, we count up till the last waypoint (like with the two others before)
                //and then we decrease our location indicator till it reaches zero again to start from the beginning.
                //to achieve that, and differ between back and forth, we use the boolean "repeat"
            case LoopType.pingPong:
                //if we reached the last waypoint, set repeat to true,
                //so we start decrease currentPoint again
                //(if-else repeat query below)
                if (currentPoint == waypoints.Length - 1)
                {
                    repeat = true;
                }
                else if (currentPoint == 0)
                {
                    //when currentPoint reaches zero (our start pos),
                    //disable repeating and move forth (count up) again
                    //(if-else repeat query below)
                    repeat = false;
                }

                //repeating mode is on, decrease currentPoint one by one to move backwards
                if (repeat)
                {
                    currentPoint--;
                }
                else //repeating mode off, increase currentPoint to move forwards
                {
                    currentPoint++;
                }
                break;

                //on LoopType.random, we calculate a random waypoint between zero and max
                //waypoint count and move to that, but make sure we do not move to the same again
            case LoopType.random:
                //store old current point for being able to compare old and new point
                int oldPoint = currentPoint;
                //calculate a random point between zero and waypoint count
                do
                {
                    currentPoint = Random.Range(0, waypoints.Length);
                }
                //as long as old point is equal to calculated one, so we compute it again
                while (oldPoint == currentPoint);
                break;
        }

        //move to the calculated waypoint
        Move(currentPoint);
    }


    //method to change the current path of this walker object
    public void SetPath(PathManager newPath)
    {
        //disable any running movement methods
        Stop();
        //set new path container
        pathContainer = newPath;
        //get new waypoint positions of our new path
        waypoints = pathContainer.waypoints;
        //reset current waypoint index to zero
        currentPoint = 0;
        //restart/continue movement on new path
        StartMove();
    }

    
    //disables any running movement methods
    public void Stop()
    {
        //exit waypoint coroutine
        StopCoroutine("NextWaypoint");
        //destroy current iTween movement component
        iTween.Stop(gameObject);
    }
    

    //stops movement of our walker object and sets it back to first waypoint 
    public void Reset()
    {
        //disable any running movement methods
        Stop();
        //reset current waypoint index to zero
        currentPoint = 0;
        //position this walker at our first waypoint, with our additional height
        transform.position = waypoints[currentPoint].position + new Vector3(0, sizeToAdd, 0);
    }
}