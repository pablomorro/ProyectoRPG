using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_movement : MonoBehaviour
{

    private Animator animator;
    //private NavMeshAgent nav;
    //private SphereCollider col;
    private GameObject player;

    /*
    [SerializeField] private Transform myEnemy; //Transform del enemigo
    [SerializeField] private float radiusFindPlayer;  //Radio de busqueda del player
    [SerializeField] private LayerMask playerLayer;
    */

    public float speed = 0.0f;
    public float h = 0.0f, v = 0.0f; //velocidades horizaontales y verticales
    public bool attack = false, die = false;

    [SerializeField] public bool DEBUG = true, DEBUG_DRAW = true;

    public Vector2 direccion; //Donde está player en relacion al NPC
    public float distance = 0.0f; //Distancia entre player y NPC
    public float angle = 0.0f; //Angulo entre player y NPC
    public bool playerInSight = false; //Esta el jugador en el fov del NPC
    public float fieldOfViewAngle = 120;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //nav = GetComponent<NavMeshAgent>();
        //col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }   

    private void FixedUpdate()
    {
        h = angle;
        v = distance;
        speed = distance / Time.deltaTime;
        if (DEBUG) {
            Debug.Log(string.Format("H:{0} - V{1}, speed:{2}", h, v, speed));
        }

        /*animator.SetFloat("Speed", speed);
        animator.SetFloat("AngularSpeed", h);
        animator.SetBool("Attack", attack);*/
    }

    private void OnTriggerStay2d(Collider2D collision)
    {
        Debug.Log("Entrando");
        if (collision.transform.tag.Equals("Player")) {
            //vector = destino - origen
            direccion = collision.transform.position - this.transform.position;
            distance = Vector2.SqrMagnitude(direccion) - 1f;
            angle = Vector2.Dot(this.transform.forward, collision.transform.position);

            if (DEBUG_DRAW) {
                Debug.DrawLine(this.transform.position, direccion * 20, Color.red);
                Debug.DrawLine(collision.transform.position, direccion * 20, Color.blue);
            }
        }
    }

}

    