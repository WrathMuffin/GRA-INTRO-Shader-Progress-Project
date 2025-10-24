using UnityEngine;

public class LeapingBoss : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Settings")]
    public float speed = 2f;      
    public float jumpHeight = 2f;  
    public float waitTime = 1f;  

    private Transform targetPoint;

    public NewMovement player;

    void Start()
    {
        targetPoint = pointB;
        Invoke("DoIt", 2.0f);
    }

    void DoIt(){
        StartCoroutine(LeapToTarget());
    }

    private System.Collections.IEnumerator LeapToTarget()
    {
        while (true)
        {
            if (pointA == null || pointB == null)
                yield break;

            Vector3 start = transform.position;
            Vector3 end = targetPoint.position;
            float journey = 0f;

            // Leap animation
            while (journey <= 1f)
            {
                journey += Time.deltaTime * speed;

                // Lerp position horizontally
                Vector3 horizontalPos = Vector3.Lerp(start, end, journey);

                // Add arc for the jump
                float heightOffset = jumpHeight * Mathf.Sin(Mathf.PI * journey);

                // Combine horizontal and vertical positions
                transform.position = new Vector3(horizontalPos.x, horizontalPos.y + heightOffset, horizontalPos.z);

                yield return null;
            }

            // Snap to target to prevent rounding errors
            transform.position = end;

            // Wait before next leap
            yield return new WaitForSeconds(waitTime);

            // Switch targets
            //targetPoint = (targetPoint == pointA) ? pointB : pointA;
            if (transform.position.x - player.gameObject.transform.position.x > 0){
                GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }

            else{
                GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, -270, 0));
            }
            targetPoint = player.gameObject.transform;
        }
    }
}