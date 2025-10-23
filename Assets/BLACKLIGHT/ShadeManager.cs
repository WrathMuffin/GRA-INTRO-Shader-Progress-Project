using UnityEngine;

public class ShadeManager : MonoBehaviour
{
    public static ShadeState shade_state;
    public GameObject volume;
    public GameObject player;
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
        //     if (shade_state == ShadeState.LIGHT){
        //         shade_state = ShadeState.DARK;
        //         player.GetComponent<SpriteRenderer>().color = Color.white;
        //         cam.backgroundColor = Color.black;

        //     }

        //     else{
        //         shade_state = ShadeState.LIGHT;
        //         player.GetComponent<SpriteRenderer>().color = Color.black;
        //         cam.backgroundColor = Color.white;
        //     }
        // }

        // if (shade_state == ShadeState.LIGHT){
        //     volume.SetActive(false);
        // }

        // else{
        //     volume.SetActive(true);
        // }
    }

    public enum ShadeState{
        LIGHT,
        DARK
    }
}
