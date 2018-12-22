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

    [SerializeField] private float radiusAttackCheck;
    [SerializeField] private int comboAttack;
    [SerializeField] private float comboTimeLimit;
    [SerializeField] private LayerMask enemigoLayer;

    [SerializeField] private Transform attackChecker;

    private int damage;
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
        attack = false;
        completado = true;
        animator = GetComponent<Animator>();
        duracionAtaqueRapido = 1f;
        realizarAtaqueRapido = 0.7f;
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

        if (time > comboTimeLimit && comboAttack != 0)
        {
            comboAttack = 0;
            time = 0;
        }
        else
        {
            time += Time.fixedDeltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackChecker.position, radiusAttackCheck);
    }

    private void CheckAttack()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(attackChecker.position, radiusAttackCheck, enemigoLayer);
        if (enemigos.Length != 0)
        {
            Damaged.golpeado = true;
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
