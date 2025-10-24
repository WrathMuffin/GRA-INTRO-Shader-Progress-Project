using UnityEngine;
using System.Collections;
public class MoveToPoint : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 1f;

    private Transform targetPoint;
    private bool isWaiting = false;
    public Animator animator;
    void Start()
    {
        targetPoint = pointB;
    }

    void Update()
    {
        if (isWaiting || pointA == null || pointB == null){
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f){
            StartCoroutine(WaitAndSwitch());
        }
    }

    private IEnumerator WaitAndSwitch(){
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Switch target point
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
        isWaiting = false;
    }

}