using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public GameObject fallDetecor;

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
    
    respawnPoint = transform.position; 
    fallDetecor.transform.position = new Vector2(transform.position.x, fallDetecor.transform.position.y);


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


private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.tag == "Falldetecor")
    {
        transform.position = respawnPoint;
        Debug.Log("touché");
    }
}

}
