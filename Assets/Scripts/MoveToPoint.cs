using UnityEngine;

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
        // Start moving towards point B first
        targetPoint = pointB;
    }

    void Update()
    {
        if (isWaiting || pointA == null || pointB == null){
            return;
        }

        // Move toward the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if we've reached the target
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    private System.Collections.IEnumerator WaitAndSwitch()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Switch target point
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
        isWaiting = false;
    }

}