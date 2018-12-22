using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged : MonoBehaviour
{
    public Animator animator;
    public static bool golpeado;

    private float duracionDaño;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        golpeado = false;
        duracionDaño = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (golpeado) {
            animator.SetBool("GetsHit", golpeado);
            StartCoroutine(RecibirDaño());
        }
            
    }

    IEnumerator RecibirDaño()
    {
        yield return new WaitForSeconds(duracionDaño);
        golpeado = false;
        animator.SetBool("GetsHit", golpeado);

    }
}
