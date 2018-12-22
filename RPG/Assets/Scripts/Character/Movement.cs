using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float moveHorizontal;
    private float moveVertical;
    public float speed;

    public Rigidbody2D player;
    private Vector2 movement;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        FaceMouse();
    }

    // - - - - - - - - Movimiento Jugador - - - - - - - -\\
    private void PlayerMovement()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;
        moveVertical = Input.GetAxisRaw("Vertical") * speed;

        movement = new Vector2(moveHorizontal, moveVertical);
        player.velocity = movement * speed;

        if (moveHorizontal == 0 && moveVertical == 0)
            animator.SetBool("parado", true);
        else
            animator.SetBool("parado", false);

        //Velocidad
        animator.SetFloat("VelocidadH", moveHorizontal);
        animator.SetFloat("VelocidadV", moveVertical);                  
        
    }

    // - - - - - - - - Jugador mira al ratón - - - - - - - -\\
    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        //Posicion
        animator.SetFloat("PositionX", direction.x);
        animator.SetFloat("PositionY", direction.y);

    }
}
