using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //On déclare une key de type Key
    public Key key;
    public Door door;
    public string nextLevel;
    public bool isMenuScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!isMenuScene){
            //Tu peux sauver ton jeu, vu que c'est une methode static je peux la récupérer
            DataManager.Save();
        //Si il ne connait pas la clé, on lui met une erreur
        if(key == null){
            Debug.LogError("LevelManager need a key.", gameObject);
        }
        else {
            //Le manager de cette clé c'est moi même.
            key.manager = this;
        }
        }

        if(door == null){
            Debug.LogError("LevelManager need a door.", gameObject);
        }
        else{
            //Le manager de cette porte c'est moi même.
            door.manager = this;
        }
    }
    

    public void keyIsReached(){
        door.Opening();
    }

    public void DoorIsReached(){
        SceneManager.LoadScene(nextLevel);
    }

}
