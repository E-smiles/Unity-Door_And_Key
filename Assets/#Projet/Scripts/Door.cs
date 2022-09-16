using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pour lui dire que si il y a un animator il doit le trouver
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [HideInInspector] public LevelManager manager;
    [HideInInspector] public bool isOpen = false;
    private Animator animator;

    void Start(){
        //Va chercher le composant entre les chevrons et vous le ramène
        animator = GetComponent<Animator>();
    }

    public void Opening(){
        isOpen = true;
        //Elle gère l'animator devient true(certain que l'animation va se jouer)
        animator.SetBool("DoorIsOpen", true);
    }


    void OnTriggerEnter2D(Collider2D other){
        //Si je suis bien devant un game objet player alors mon propre game object se désactive
        if(other.CompareTag("Player")){
            if(isOpen)
                manager.DoorIsReached();
        }

    }
}
