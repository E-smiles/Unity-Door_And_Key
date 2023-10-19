using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCTOQUIT : MonoBehaviour
{
     void Update()
    {
        //TROUVER COMMENT LE FAIRE FONCTIONNER
        if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("J'ai bien quitté le jeu");
        }
    }
}
