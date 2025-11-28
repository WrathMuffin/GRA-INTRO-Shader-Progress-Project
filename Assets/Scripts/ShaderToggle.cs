using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShaderToggle : MonoBehaviour
{
    public GameObject lighting;
    public Camera main_cam;
    public Material pixel_effect;
    public static bool post_process = true;
    public GameObject[] particles;
    UniversalAdditionalCameraData cam_data;

    void Start(){
        cam_data = main_cam.GetUniversalAdditionalCameraData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)){
            if (post_process){
                post_process = false;
                lighting.SetActive(false);
                cam_data.renderPostProcessing = false;
                pixel_effect.SetFloat("BlendAmt", 0);

                if (particles.Length != 0){
                    for (int i = 0; i < particles.Length; i++){
                        particles[i].SetActive(false);
                    }
                }
            }

            else{
                post_process = true;
                lighting.SetActive(true);
                cam_data.renderPostProcessing = true;
                pixel_effect.SetFloat("BlendAmt", 1);
                
                if (particles.Length != 0){
                    for (int i = 0; i < particles.Length; i++){
                        particles[i].SetActive(true);
                    }
                }
            }
        }
    }
}
