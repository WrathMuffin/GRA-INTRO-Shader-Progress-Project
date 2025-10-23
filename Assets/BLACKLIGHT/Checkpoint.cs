using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool animate_player = false;
    public Sprite active_sprite;
    public Vector2 player_pos;
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
            PlayerMovement.respawn_point = gameObject;
            GetComponent<SpriteRenderer>().sprite = active_sprite;
            if (animate_player){
                collider.GetComponent<Animator>().SetTrigger("ThumbUp");
                collider.transform.position = player_pos;
                PlayerMovement.controllable = false;
            }
        }
    }  
}
