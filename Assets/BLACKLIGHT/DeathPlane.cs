using Unity.VisualScripting;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameObject health1, health2, health3;
    public Transform respawn_point;
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
            PlayerMovement.health = PlayerMovement.health - 1;
            HealthCheck(collider);
        }
    }  

    void HealthCheck(Collider2D player){
        if (PlayerMovement.health != 0){
     
            player.transform.position = respawn_point.position;
        }
        
        else{
            PlayerMovement.health = 3;
            player.transform.position = PlayerMovement.respawn_point.transform.position;
        }
    }
}
