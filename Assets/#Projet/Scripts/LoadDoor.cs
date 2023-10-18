using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDoor : MonoBehaviour
{        void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Player"))
                DataManager.Load();
    }
}
