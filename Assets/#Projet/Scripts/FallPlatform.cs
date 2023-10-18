using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void AddGravity(){
        rb.gravityScale = 2f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Invoke("AddGravity", 1f);
        }
    }
}
