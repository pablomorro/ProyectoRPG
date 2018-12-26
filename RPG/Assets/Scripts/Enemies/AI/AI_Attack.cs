using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{

    private enum TipoAtaque
    {
        rapido,
        fuerte
    }

    [SerializeField] private float radiusAttack;
    [SerializeField] private int comboAttack;
    [SerializeField] private float comboTimeLimit;

    [SerializeField] private Transform attackChecker; //Transform Enemy

    private Vector2 origen; //Origen del ataque
    public Vector2 target; //posición del objetivo
    public float secondsBetweenAttacks = 1f;

    private float damage; //Daño del enemigo
    private TipoAtaque ataque;
    private float time;
    private bool completado;
    public float duracionAtaqueRapido;
    public float realizarAtaqueRapido;

    public static bool attack;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        attack = false;
        completado = true;

        duracionAtaqueRapido = 1f; //duracion de la animacion
        realizarAtaqueRapido = 0.7f; //Momento en el cual se comprueba si el ataque ha dado a algo

        radiusAttack = 0.85f;
        damage = 5f;

    }

    private void FixedUpdate()
    {
        if (target != null) {

            
            Vector2 direction = new Vector2(
               target.x - transform.position.x,
               target.y - transform.position.y);

            Debug.Log(direction);

            //Posicion
            animator.SetFloat("PosicionX", direction.x );
            animator.SetFloat("PosicionY", direction.y );

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }

    public void PerformAttack(int tipoAtaque, Vector2 target)
    {

        this.target = target;

        if (!attack && completado)
        {

            //Debug.Log("atacando");
            switch (tipoAtaque)
            {
 
                case (0):
                    AtaqueRapido();
                    break;
                case (1):
                    //AtaqueFuerte();
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckAttack()
    {
        Vector2 direction = new Vector2(
            target.x - transform.position.x,
            target.y - transform.position.y);

        var rayo = Physics2D.RaycastAll(transform.position, direction, radiusAttack, 1 << LayerMask.NameToLayer("Player"));

        foreach (var x in rayo)
        {
            if (x.collider.gameObject.tag.Equals("Player"))
            {
                //TODO: Hacer daño al jugador
                //Debug.Log("jugador alcanzado");
            }
        }

    }

    private void AtaqueRapido()
    {
        ataque = TipoAtaque.rapido;

        //Animacion ataque
        attack = true;
        animator.SetBool("Attack", attack);
        completado = false;
    }

    IEnumerator PararAtaque()
    {
        attack = false;
        animator.SetBool("Attack", attack);

        yield return new WaitForSeconds(secondsBetweenAttacks);
     
        completado = true;

    }

    public void PerformDamage(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight > 0.5)
        {
            CheckAttack();
            StartCoroutine(PararAtaque());
        }
        
    }

}
