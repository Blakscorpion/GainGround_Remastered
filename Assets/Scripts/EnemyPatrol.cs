using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float speed =1;
    public Transform[] waypoints;
    private Transform WaypointTarget;
    private int destPoint =0;
    private float distanceBetweenObjects;

    [SerializeField]
    private int DetectionArea = 5;
    [SerializeField]
    private Transform EnnemyTarget;
    
    public bool isPatroling= true;
    public bool isBackToBase = false;

    // Start is called before the first frame update
    void Start()
    {
        WaypointTarget = waypoints[0];
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // If there is a target to identify, the ennemy attacks the player or patrol if it's too far away 
       if (EnnemyTarget)
       {
            distanceBetweenObjects = Vector3.Distance(transform.position, EnnemyTarget.position);

            if (isInRange())
            {
                Attacking();
            }
            else if (isPatroling)
            {
                Patroling();
            }
            else if (isBackToBase==false)
            {
                BackToBase(waypoints[0]);
            }
       }
       // If there is NO target to identify, we assign the player as the target of the ennemy (if it's on the map) 
       else
       {
            if (GameObject.FindWithTag("Player")){
                EnnemyTarget=GameObject.FindWithTag("Player").transform;
            }
            else if (isPatroling)
            {
                Patroling();
            }
            else if (isBackToBase == false)
            {
                BackToBase(waypoints[0]);
            }
        }
    }

    void Patroling()
    {
        Vector3 dir = WaypointTarget.position - transform.position;
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector3.MoveTowards(transform.position, WaypointTarget.position, step);
        
        if(Vector3.Distance(transform.position, WaypointTarget.position) < 0.3f)
        {
            destPoint = (destPoint +1) % waypoints.Length;
            WaypointTarget = waypoints[destPoint];
        }
    }

    void Attacking()
    { 
        transform.position = Vector3.MoveTowards(transform.position, EnnemyTarget.position, speed * Time.deltaTime);
        isBackToBase=false;
        animator.SetBool("backToBase", false);
    }

    void BackToBase(Transform initialLocation)
    {
        Vector3 dir = initialLocation.position - transform.position;
        float step = speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, initialLocation.position) < 0.2f)
        {
            isBackToBase = true;
            animator.SetBool("backToBase", true);
        }
        else
        {
            // move sprite towards the base location
            transform.position = Vector3.MoveTowards(transform.position, initialLocation.position, step);
        }
    }

    private bool isInRange()
    {
        if (distanceBetweenObjects <= DetectionArea)
        {
            return true;
        }  
        return false;
    }
}
