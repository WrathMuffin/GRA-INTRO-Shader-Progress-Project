using Unity.VisualScripting;
using UnityEngine;

public class ShadeBlock : MonoBehaviour
{
    SpriteRenderer sr;
    SpriteRenderer player_sr;
    public float r_threshold = 0.9f;
    // publicbool white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player_sr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShadeManager.shade_state == ShadeManager.ShadeState.LIGHT){
            r_threshold = 0.9f;
            if (sr.color.r > r_threshold){
                GetComponent<BoxCollider2D>().enabled = false;
            }

            else{
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        else{
            r_threshold = 0.1f;
           if (sr.color.r < r_threshold){
                GetComponent<BoxCollider2D>().enabled = false;
            }

            else{
                GetComponent<BoxCollider2D>().enabled = true;
            } 
        }
        
    }
}
