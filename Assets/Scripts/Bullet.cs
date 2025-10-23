using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float bullet_speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyAfterAWhile", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(bullet_speed * transform.localScale.x, 0, 0);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Blocker")){
            Destroy(gameObject);
        }
    }

    void DestroyAfterAWhile(){
        Destroy(gameObject);
    }
}
