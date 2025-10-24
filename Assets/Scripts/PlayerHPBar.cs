using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public NewMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health == 5){
            GetComponent<Image>().fillAmount = 1.0f;
        }

        else if (player.health == 4){
            GetComponent<Image>().fillAmount = 0.7f;
        }

        else if (player.health == 3){
            GetComponent<Image>().fillAmount = 0.55f;
        }

        else if (player.health == 2){
            GetComponent<Image>().fillAmount = 0.39f;
        }

        else if (player.health == 1){
            GetComponent<Image>().fillAmount = 0.2f;
        }
    }
}
