using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private bool parado;

    public Transform target;
    public float updateRate;
  
    private Seeker seeker;

    private Rigidbody2D rb;
    //the calculated path

    public Path path;

    //The AI´s speed per second
    public float speed = 10f;

    [HideInInspector]
    public bool pathIsFinished = false;

    //The max distance from the AI to a waypoint for it to continue to the next one
    public float maxWaypointDistance = 3f;

    //The waypoint we are currently moving towards
    private int currentWaypoint;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            Debug.Log("no target");
            return;
        }

        //Start new path to the target position and calls the OnPathCompleted function once is done with the calculation of the path
        seeker.StartPath(transform.position, target.position, OnPathCompleted);

        parado = true;
    }

    public void OnPathCompleted(Path p)
    {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

   private void UpdatePath()
    {
        if (target == null)
        {
            //TODO: busqueda del jugador
            Debug.Log("no target, stoping");
            rb.velocity = Vector3.zero;
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathCompleted);
 
    }

    void FixedUpdate()
    {

        if (target == null || path == null)
        {
            //TODO: busqueda del jugador
            return;
        }
        else
        {
            if (currentWaypoint >= path.vectorPath.Count)
            {
                if (pathIsFinished)
                {
                    //Debug.Log("Path finished, no more waypoints");
                    return;
                }
                //the path has ended
                Debug.Log("Path finished");
                pathIsFinished = true;

                parado = true;
                animator.SetBool("Parado", false);

                rb.velocity = new Vector3(0, 0, 0);
                return;
            }
            else
            {
                pathIsFinished = false;
                parado = false;
                animator.SetBool("Parado", true);

                //direction to the next waypoint;
                Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position);
                //dir.z = 0;
                dir *= (speed * Time.fixedDeltaTime);

                //move the ai
               
                rb.velocity = new Vector3(dir.x, dir.y, 0);

                //Animacion Movimiento
                if (!parado)
                {
                    animator.SetFloat("VelocidadX", dir.x * 10f);
                    animator.SetFloat("VelocidadY", dir.y * 10f);
                }

                float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (dist < maxWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
        }
    }

    public void StopLookingForPath()
    {
        CancelInvoke("UpdatePath");
    }

    public void StartLookingForPath()
    {
        if (target == null)
        {
            Debug.Log("no target");
            return;
        }

        InvokeRepeating("UpdatePath", 0f, (1 / updateRate));
    }

    public void MoveToLocationWithoutUpdate(Transform t)
    {
        CancelInvoke();
        target = t;
        seeker.StartPath(transform.position, target.position, OnPathCompleted);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colided with" + collision.gameObject.name);
        
    }
}
