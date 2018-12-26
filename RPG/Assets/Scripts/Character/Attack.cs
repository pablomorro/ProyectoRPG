using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private enum TipoAtaque
    {
        rapido,
        fuerte
    }

    [SerializeField] private float radiusAttack;
    [SerializeField] private int comboAttack;
    [SerializeField] private float comboTimeLimit;
    [SerializeField] private LayerMask enemigoLayer;

    [SerializeField] private Transform attackChecker; //Transform Player
                                                      
    private Vector2 origen; //Origen del ataque

    private float damage; //Daño del arma
    private TipoAtaque ataque;
    private float time;  
    private bool completado;
    private float duracionAtaqueRapido;
    private float realizarAtaqueRapido;

    public static bool attack;
    public float secondsBetweenAttacks = 1f;
    public Animator animator;

    private bool doingDamage = false; //se usa para evitar que dos animaciones llamen al evento de realizar daño a la vez

    // Start is called before the first frame update
    void Start()
    {       
        animator = GetComponent<Animator>();

        attack = false;
        completado = true;

        duracionAtaqueRapido = 1f;
        realizarAtaqueRapido = 0.7f; //Momento en el cual se comprueba si el ataque ha dado a algo

        radiusAttack = 0.5f;
        damage = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && completado)
        {   //Left mouse
            completado = false;
            AtaqueRapido();
        }


        else if (Input.GetButtonDown("Fire2") && completado)
        {   //Right mouse
            completado = false;
            //AtaqueFuerte();
        }
        /*
        if (time > comboTimeLimit && comboAttack != 0)
        {
            comboAttack = 0;
            time = 0;
        }
        else
        {
            time += Time.fixedDeltaTime;
        }
       */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        origen = new Vector2(
            transform.position.x - 0.1f,
            transform.position.y);

        Gizmos.DrawWireSphere(origen, radiusAttack);
    }

    private void CheckAttack()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        origen = new Vector2(
            transform.position.x - 0.1f,
            transform.position.y);

        var rayo = Physics2D.RaycastAll(origen, direction, radiusAttack, 1 << LayerMask.NameToLayer("Enemy"));
        
        foreach (var x in rayo) {
            if (x.collider.gameObject.tag.Equals("Enemy")) {
                Debug.Log(x.collider.gameObject.name);
                AI_live.golpeado = true; //Enemigo hace la animacion del golpe
                AI_live.enemyLive -= damage;
                Debug.Log(AI_live.enemyLive);
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

    void PararAtaque()
    {
        attack = false;
        animator.SetBool("Attack", attack);

        //yield return new WaitForSeconds(secondsBetweenAttacks);

        completado = true;

    }

    public void PerformDamage(AnimationEvent evt)
    {

        if (evt.animatorClipInfo.weight > 0.3)
        {
            // Do handle animation event
            Debug.Log("Doing damage");
            doingDamage = true;
            CheckAttack();
            PararAtaque();
            doingDamage = false;
        }
        else
        {
            //Debug.Log(evt.animatorClipInfo.weight);
        }
    }
}
