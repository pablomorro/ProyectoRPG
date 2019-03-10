using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float hitPoints = 100;

    public static int experiencePoints = 0;
    public int currentLevel = 1;

    public Animator animator;

    private void Update()
    {
        if (hitPoints < 0)
        {
            KillPlayer();
        }

        if (experiencePoints >= 100)
        {
            Debug.Log(experiencePoints);
            experiencePoints = experiencePoints % 100;
            LevelUp();
        }
    }

    private void KillPlayer()
    {
        animator.SetTrigger("Dying");
    }

    internal void TakeDamage(float damage)
    {
        hitPoints -= damage;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void LevelUp()
    {
        currentLevel++;
        Debug.Log(experiencePoints);
    }
}
