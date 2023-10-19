using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class HeroInputManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayers;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 20f;
    private bool isFacingRight=true;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

void Start()
{
    respawnPoint = transform.position; 
    fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
}

void Update()
{
    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    if(!isFacingRight && horizontal >0f)
    {
        Flip();
    }
    else if(isFacingRight && horizontal < 0f)
    {
        Flip();
    }

}

public void Jump(InputAction.CallbackContext context)
{
    if(context.performed && isGrounded())
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }
    if(context.canceled && rb.velocity.y >0f)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.5f);
    }

}

private bool isGrounded()
{
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayers);
}

private void Flip()
{
    isFacingRight = !isFacingRight;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
}

public void Move(InputAction.CallbackContext context)
{
    horizontal = context.ReadValue<Vector2>().x;
}

//Ne pas oublier de mettre un box collider et un rigidbody sur les deux éléments 
//qui vont intéragir.
private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.tag == "Falldetector")
    {
        //SI JE VEUX FAIRE APPARAITRE MON PERSO A SON ENDROIT DE DEPART//
        //transform.position = respawnPoint;
        
        //SI JE LE METS JUSTE EN GAME OVER APRES UN FALL//
        SceneManager.LoadScene("GameOver");

        
        Debug.Log("touché");
    }
}

}