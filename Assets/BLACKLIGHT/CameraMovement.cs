using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float new_camera_min_x;
    public float new_camera_max_x;
    public float new_camera_min_y;
    public float new_camera_max_y;
    public Vector3 new_player_position;
    public bool change_x = false;
    public bool change_y = false;
    CameraPositioning camera_control;
    public bool change_camera_size;
    public float camera_size = 4.8f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        camera_control = Camera.main.GetComponent<CameraPositioning>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
         if(collider.tag.Equals("Player")){
            
            if (change_x == true){
                camera_control.min_position.x = new_camera_min_x;
                camera_control.max_position.x = new_camera_max_x;
            }

            if (change_y == true){
                camera_control.min_position.y = new_camera_min_y;
                camera_control.max_position.y = new_camera_max_y;
            }

            if (change_camera_size)
            {
                Camera.main.orthographicSize = camera_size;
                Camera.main.transform.localScale = new Vector3(2, 2, 1);
            } 

            collider.transform.position += new_player_position;
         }
    }
}
