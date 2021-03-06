﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_live : MonoBehaviour
{
    public static float enemyLive;

    public Animator animator;
    public static bool golpeado;

    private float duracionDaño;
    private float duracionMuerte;

    //Experiencia que droppea el enemigo
    public int experience;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        golpeado = false;

        duracionDaño = 0.7f;
        duracionMuerte = 1f;

        enemyLive = 20f;

        experience = GameMaster.rnd.Next(115, 125);
        Debug.Log(experience);
    }

    internal float GetEnemyLife()
    {
        return enemyLive;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (golpeado)
        {
            if (enemyLive <= 0)
            {
                animator.SetBool("Dies", true);
                Player.experiencePoints += experience;
                StartCoroutine(Dying());
            }
            else
            {
                animator.SetBool("GetsHit", golpeado);
                StartCoroutine(RecibirDaño());
            }
            
        }

    }

    IEnumerator RecibirDaño()
    {
        yield return new WaitForSeconds(duracionDaño);
        golpeado = false;
        animator.SetBool("GetsHit", golpeado);

    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(duracionMuerte);


        Destroy(this.gameObject); //Se elimina el enemigo
        

    }
}
