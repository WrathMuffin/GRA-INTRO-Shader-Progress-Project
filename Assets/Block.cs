using UnityEngine;

public class Block : MonoBehaviour
{
    Animator animator;
    public int health = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        if (collider.CompareTag("Bullet")){
            Destroy(collider.gameObject);
            health = health - 1;
            animator.SetTrigger("BlockDamage");

            if (health < 1){
                Destroy(gameObject);
            }
        }
    }
}
