using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{

    private AI_Attack ai_attack;

    private EnemyAI movementAI;

    // Start is called before the first frame update
    void Start()
    {
        ai_attack = GetComponentInParent<AI_Attack>();
        movementAI = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //El jugador está al alcance. Detener el movimiento del enemigo
            movementAI.FinishCurrentPath();

            //Start following the player
            ai_attack.PerformAttack(0, col.gameObject.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //Debug.Log("Attack");
            //Start following the player
            ai_attack.PerformAttack(0, col.gameObject.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //El jugador esta fuera de alcance, hay que mover al enemigo
            // ai_attack.PerformAttack(0, col.gameObject.transform.position);

            movementAI.target = col.gameObject.transform;
            movementAI.StartLookingForPath();
        }
    }
}
