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
    public Vector2 direction; //direccion del jugador

    private float damage; //Daño del enemigo
    private TipoAtaque ataque;
    private float time;
    private bool completado;
    private float duracionAtaqueRapido;
    private float realizarAtaqueRapido;

    public static bool attack;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        attack = false;
        completado = true;

        duracionAtaqueRapido = 1f;
        realizarAtaqueRapido = 0.7f; //Momento en el cual se comprueba si el ataque ha dado a algo

        radiusAttack = 0.85f;
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
            AtaqueFuerte();
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
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }

    private void CheckAttack()
    {

        var rayo = Physics2D.RaycastAll(transform.position, direction, radiusAttack, 1 << LayerMask.NameToLayer("Player"));

        foreach (var x in rayo)
        {
            Debug.Log("Player");
            if (x.collider.gameObject.tag.Equals("Player"))
            {
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
        StartCoroutine(PararAtaque());

        StartCoroutine(RealizarAtaque());
    }

    private void AtaqueFuerte()
    {
        ataque = TipoAtaque.fuerte;

        //Animacion ataque
        attack = true;
        animator.SetBool("Attack", attack);
        StartCoroutine(PararAtaque());

        StartCoroutine(RealizarAtaque());
    }

    IEnumerator PararAtaque()
    {

        yield return new WaitForSeconds(duracionAtaqueRapido);

        attack = false;
        completado = true;
        animator.SetBool("Attack", attack);
    }

    IEnumerator RealizarAtaque()
    {

        yield return new WaitForSeconds(realizarAtaqueRapido);

        CheckAttack();
    }

}
