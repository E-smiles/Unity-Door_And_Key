using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using System.Debug;

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
    public float jumpforce = 20;

    bool isGrounded;
    public Transform groundCheck;

    //Depuis Unity je vais définir les éléments qui sont concidérés comme "sols"
    public LayerMask groundLayer;
    //Je selectionne ma platforme, je vais sur l'onglet Layer -> créer un new layer -> j'ai ajouté le layer ground -> Je lui assigne -> Je retourne
    //Dans mon player et dans ma variable Ground Layer je lui assigne le layer ground.

    void Awake()
    {
        actions = new InputManager();
        //ça va aller chercher mon objet, ça va aller chercher la composante du mouvement
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
            rb.AddForce(Vector2.up*jumpforce); //100
        }
    }
void FixedUpdate()
//Je vais calculer le mouvement
{
    //Je vais regarder grace à la fct overlapcircle si mon empty est bien sur le sol (je dois passer trois parametres car mon bool va toujours me renvoyer true si je ne mets pas un layer masque)
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer);
    //Debug.log(isGrounded);

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
        //  if(move.x != 0){
        // // sprite.flix est egale vrai et la condition -> Si movex est différent de zero
        //     mySprite.flipX = move.x <0;
        // }

}


}

//Test ISGROUNDED

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(SpriteRenderer))]
// public class HeroInputManager : MonoBehaviour
// {
//     private InputManager actions;
//     private InputAction moveAction;
//     private InputAction jumpAction;
//     private Rigidbody2D rb;
//     private Animator animator;
//     private SpriteRenderer mySprite;
//     private bool isJumping = false;
//     private bool isGrounded = false;

//     [SerializeField] private LayerMask groundLayer;
//     [SerializeField] private Transform groundCheck;
//     [SerializeField] private float groundCheckRadius = 0.2f;
//     public float speed = 3;

//     private void Awake()
//     {
//         actions = new InputManager();
//         moveAction = actions.Player.Move;
//         moveAction.Enable();

//         actions.Player.Jump.Enable();

//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         mySprite = GetComponent<SpriteRenderer>();
//     }

//     private void OnEnable()
//     {
//         actions.Player.Jump.started += JumpStart;
//         actions.Player.Jump.canceled += JumpCancel;
//     }

//     private void OnDisable()
//     {
//         actions.Player.Jump.started -= JumpStart;
//         actions.Player.Jump.canceled -= JumpCancel;
//     }

//     private void JumpStart(InputAction.CallbackContext context)
//     {
//         if (isGrounded)
//         {
//             isJumping = true;
//             rb.AddForce(Vector2.up * 50);
//         }
//     }

//     private void JumpCancel(InputAction.CallbackContext context)
//     {
//         isJumping = false;
//     }

//     private void FixedUpdate()
//     {
//         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

//         Vector2 move = moveAction.ReadValue<Vector2>() * Time.fixedDeltaTime * speed;
//         rb.MovePosition(transform.position + (Vector3)move);

//         float velocity = Mathf.Abs(move.x);
//         animator.SetFloat("Speed", velocity);

//         if (move.x > 0)
//         {
//             mySprite.flipX = false;
//         }
//         else if (move.x < 0)
//         {
//             mySprite.flipX = true;
//         }
//     }
// }
