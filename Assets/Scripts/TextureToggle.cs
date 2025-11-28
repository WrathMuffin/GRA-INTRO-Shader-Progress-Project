using UnityEngine;

public class TextureToggle : MonoBehaviour
{
    public Material[] charco_shaders;
    public Material[] stencil_shaders;
    public Material[] john_shaders;
    bool textures_on = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)){
            if (textures_on){
                for (int i = 0; i < charco_shaders.Length; i++){
                    charco_shaders[i].DisableKeyword("_APPLY_TEXTURE_YES");
                    charco_shaders[i].EnableKeyword("_APPLY_TEXTURE_NO");
                    Debug.Log("Pray");
                }

                if(!(stencil_shaders.Length == 0)){
                    for (int i = 0; i < stencil_shaders.Length; i++){
                        stencil_shaders[i].DisableKeyword("_TEXTUREMODE_TEXTUREON");
                        stencil_shaders[i].EnableKeyword("_TEXTUREMODE_TEXTUREOFF");
                    }
                }
                textures_on = false;
            }

            else{
                for (int i = 0; i < charco_shaders.Length; i++){
                    charco_shaders[i].DisableKeyword("_APPLY_TEXTURE_NO");
                    charco_shaders[i].EnableKeyword("_APPLY_TEXTURE_YES");
    
                }

                if(!(stencil_shaders.Length == 0)){
                    for (int i = 0; i < stencil_shaders.Length; i++){
                        stencil_shaders[i].DisableKeyword("_TEXTUREMODE_TEXTUREOFF");
                        stencil_shaders[i].EnableKeyword("_TEXTUREMODE_TEXTUREON");
                    }
                }
                textures_on = true;
            }
        }
    }
}
