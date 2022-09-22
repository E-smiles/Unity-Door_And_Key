using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Vecteur qui fait (0,1) translation c'est le mouvement
    public Vector2 translation = Vector2.up;
    //Le range va afficher dans unity un range entre 0 et 10
    [Range(0f,10f)]
    public float timeToMove = 1;

    private bool reverse = false;

    private Vector3 start;
    private Vector3 end;
    void Start()
    {
        start = transform.position;
        //La position du transform + le vecteur de la translation
        end = transform.position + (Vector3)translation;
        //Lance la coroutine
        StartCoroutine(Move());
    }
//Coroutine -> chaque fois qu'on va faire une frame on va bougr d'un certain nombre
//Aller dans un sens puis dans l'autre
   private IEnumerator Move(){
    //J'ai besoin d'une variable pour stocker le temps
    //
    float time = 0f;
    //La proportion de chemin qu'a deja fait ma platform
    float ratio = 0f;
    //Tant que le ration est plus petit que 1, le chemin est fini alors 
    //J'update le temps

    while(ratio <1){

        while(ratio <1){
            time += Time.deltaTime;
            ratio = time/timeToMove;
            if(reverse)transform.position = Vector3.Lerp(end,start,ratio);
            //// Lerp nous permet de ne pas faire le calcule, il va 
            //Calculer le nombre de pourcent qu'il reste entre les 2 données et il nous donne le ratio
            else transform.position = Vector3.Lerp(start, end,ratio);
            //Attend la fin de la frame pour faire quoi que se soit
            yield return null;
        }

        time = 0f;
        ratio = 0f;
        reverse = !reverse;
        }
    }
//Parentage deparentage pour que le joueur puisse être dessus sans avoir de lag
private void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Player")){
        other.transform.parent = transform;
    }
}

private void OnTriggerExit2D(Collider2D other){
    if(other.CompareTag("Player")){
        other.transform.parent = null;
    }
}

}
