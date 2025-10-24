using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public bool capybara = false;
    public Boss boss;
    float initial_health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (capybara){
            initial_health = boss.enemy_hp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (capybara){
            //GetComponent<Image>().fillAmount = 0.5f;
            GetComponent<Image>().fillAmount = (boss.enemy_hp/initial_health);
        }
    }
}
