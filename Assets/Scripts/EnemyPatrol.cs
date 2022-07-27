
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField]
    private float speed =2;
    public Transform[] waypoints;
    private Transform WaypointTarget;
    private int destPoint =0;

    [SerializeField]
    private int DetectionArea = 5;
    [SerializeField]
    private Transform EnnemyTarget;
    
    public bool isPatroling= true;

    // Start is called before the first frame update
        void Start()
    {
        WaypointTarget = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {

       if (EnnemyTarget)
       {
            if (isInRange())
            {
                Attacking();
            }
            else if (isPatroling)
            {
                Patroling();
            }
       }
       else
        {
            if (GameObject.FindWithTag("Player"))
            {
                EnnemyTarget=GameObject.FindWithTag("Player").transform;
            }
            Patroling();
        }
      
    }

    void Patroling()
    {
         Vector3 dir = WaypointTarget.position - transform.position;
       // transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
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
    }

    private bool isInRange()
    {
        if ((this.transform.position.x-EnnemyTarget.transform.position.x <= DetectionArea) & (this.transform.position.y-EnnemyTarget.transform.position.y <= DetectionArea))
        {
            return true;
        }
        
        return false;
        
    }
}
