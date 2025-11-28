using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShaderToggle : MonoBehaviour
{
    public GameObject lighting;
    public Camera main_cam;
    public static bool post_process = true;
    public GameObject[] particles;
    UniversalAdditionalCameraData cam_data;
    public string featureName = "My FullScreen Pass";

    //RendererFeature targetFeature;

    void Start(){
        // var data = cam.GetUniversalAdditionalCameraData();
        // var renderer = data.scriptableRenderer;

        // foreach (var f in renderer.rendererFeatures)
        // {
        //     if (f.name == featureName)
        //     {
        //         targetFeature = f;
        //         break;
        //     }
        // }

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

                var urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
                var renderer = urp.scriptableRenderer;  

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
                
                if (particles.Length != 0){
                    for (int i = 0; i < particles.Length; i++){
                        particles[i].SetActive(true);
                    }
                }
            }
        }
    }
}
