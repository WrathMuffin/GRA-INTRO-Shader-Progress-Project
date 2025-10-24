using UnityEngine;
using System.Collections;
public class MoveToPoint : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float wait_time = 1f;

    private Transform target;
    private bool isWaiting = false;
    public Animator animator;
    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        if (isWaiting || pointA == null || pointB == null){
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f){
            StartCoroutine(WaitAndSwitch());
        }
    }

    private IEnumerator WaitAndSwitch(){
        isWaiting = true;
        yield return new WaitForSeconds(wait_time);

        target = (target == pointA) ? pointB : pointA;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
        isWaiting = false;
    }

}