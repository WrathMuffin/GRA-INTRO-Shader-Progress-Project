using UnityEngine;

public class Block : MonoBehaviour
{
    Animator animator;
    public int health = 5;
    public GameObject bulletEffect, explosionEffect;
    public Transform spawnEXPLOSION;
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

            if(ShaderToggle.post_process){
                GameObject effect = Instantiate(bulletEffect, collider.transform.position, Quaternion.LookRotation(this.transform.up*-1));
            }

            if (health < 1){
                if(ShaderToggle.post_process){
                    GameObject explosion = Instantiate(explosionEffect, spawnEXPLOSION);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
