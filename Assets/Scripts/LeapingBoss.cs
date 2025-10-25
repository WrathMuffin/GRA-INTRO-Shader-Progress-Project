using UnityEngine;
using System.Collections;

public class LeapingBoss : MonoBehaviour
{
    public Transform point_one;
    public Transform point_two;
    public float speed = 2f;      
    public float jumpHeight = 2f;  
    public float waitTime = 1f;  

    private Transform targetPoint;

    public NewMovement player;

    void Start()
    {
        targetPoint = point_two;
        Invoke("DoIt", 2.0f);
    }

    void DoIt(){
        StartCoroutine(LeapToTarget());
    }

    private IEnumerator LeapToTarget()
    {
        while (true)
        {
            if (point_one == null || point_two == null)
                yield break;

            Vector3 start = transform.position;
            Vector3 end = targetPoint.position;
            float journey = 0f;

            while (journey <= 1f)
            {
                journey += Time.deltaTime * speed;
                Vector3 horizontalPos = Vector3.Lerp(start, end, journey);
                float heightOffset = jumpHeight * Mathf.Sin(Mathf.PI * journey);
                transform.position = new Vector3(horizontalPos.x, horizontalPos.y + heightOffset, horizontalPos.z);
                yield return null;
            }

            transform.position = end;

            yield return new WaitForSeconds(waitTime);

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