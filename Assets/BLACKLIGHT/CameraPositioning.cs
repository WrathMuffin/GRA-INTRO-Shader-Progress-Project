using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
    public Transform player;
    public float speed;
    private Vector3 target_position;
    private Vector3 new_position;
    public Vector3 min_position;
    public Vector3 max_position;
    public Vector3 inital_offset;

    void Start()
    {
        // min_position.x = player.transform.position.x;
        // min_position.y = player.transform.position.y;
        // max_position.x = player.transform.position.x;
        // max_position.y = player.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != player.position){
            
            target_position = new Vector3(player.position.x + inital_offset.x, player.position.y + inital_offset.y, player.position.z + inital_offset.z);

            Vector3 camera_boundary_position = new Vector3(Mathf.Clamp(target_position.x, min_position.x, max_position.x), Mathf.Clamp(target_position.y, min_position.y, max_position.y), gameObject.transform.position.z);

            new_position = Vector3.Lerp(transform.position, camera_boundary_position, speed);

            transform.position = new_position;
        }
    }
}
