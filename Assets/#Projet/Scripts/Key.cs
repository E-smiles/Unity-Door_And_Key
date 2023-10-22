using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Le manager doit connaitre la clé et la clé doit connaitre le manager
    [HideInInspector] public LevelManager manager;

    
    //Vérifier ce qui vient de rentrer est bien le player
    //Désactivé le player au lieu de le détruire
    void OnTriggerEnter2D(Collider2D other){
        //Si je suis bien devant un game objet player alors mon propre game object se désactive
        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
            manager.keyIsReached();
        }

    }
}
