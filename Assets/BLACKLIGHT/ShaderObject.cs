using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShaderObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (ShadeManager.shade_state == ShadeManager.ShadeState.LIGHT){
            if (gameObject.layer == 5){
                if (gameObject.CompareTag("Health")){
                    GetComponent<Image>().color = Color.black;
                }

                else{
                    GetComponent<TMP_Text>().color = Color.black;
                }
            }

            else{
                GetComponent<SpriteRenderer>().color = Color.black;
            }
        }

        else{
            if (gameObject.layer == 5){
                if (gameObject.CompareTag("Health")){
                    GetComponent<Image>().color = Color.white;
                }

                else{
                    GetComponent<TMP_Text>().color = Color.white;
                }
            }

            else{
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
