using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//On lui dit qu'on a besoin d'un rigid body (etre sur qu'il soit deja la)
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroInputManager : MonoBehaviour
{
    //Je créer un variable de type InputManager qui s'appelle actions.
    private InputManager actions;
    //InputAction c'est un type qui viens avec l'input systeme
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D rb;
    public float speed = 3;
    private Animator animator;
    private SpriteRenderer mySprite;
    private bool IsJumping = false;
    void Awake()
    {
        actions = new InputManager();
        //ça va aller chercher mon objet, ça va aller chercher la composante du muvement
        moveAction = actions.Player.Move;
        //Si je ne fais pas ça il ne démarre pas l'action
        moveAction.Enable();

        actions.Player.Jump.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        actions.Player.Jump.started += JumpStart;
        actions.Player.Jump.canceled += JumpCancel;
    }
    void OnDisable(){
        actions.Player.Jump.started -= JumpStart;
        actions.Player.Jump.canceled -= JumpCancel;

    }
    void JumpStart(InputAction.CallbackContext context){
    IsJumping = true;
    }

    void JumpCancel(InputAction.CallbackContext context){
    IsJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsJumping){
            rb.AddForce(Vector2.up*400);
        }
    }
void FixedUpdate()
//Je vais calculer le mouvement
{
    //Je vais recupérer le vector2 de mon movaction, il va me donner le déplacement que je vais vouloir faire
    Vector2 move = moveAction.ReadValue<Vector2>() * Time.fixedDeltaTime * speed;
    //Transform.position ça veut dire ma position actuelle
    rb.MovePosition(transform.position + (Vector3)move);

    float velocity = Mathf.Abs(move.x);
    animator.SetFloat("Speed", velocity);

        if(move.x > 0){
            mySprite.flipX = false;
        }
        else if (move.x <0) {
            mySprite.flipX = true;
        }

        //Ou en ternaire
        //
        // if(move.x != 0){
        // sprite.flix est egale vrai et la condition -> Si movex est différent de zero
        //     mySprite.flipX = move.x <0;
        // 

}


}
