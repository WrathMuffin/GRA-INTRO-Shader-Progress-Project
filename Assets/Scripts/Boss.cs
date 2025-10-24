using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float enemy_hp = 200;
    public int damage_take = 5;
    public Animator animator;
    public GameObject scene_loader;
    public bool BitHub = false;
    public AudioSource music;
    public GameObject end;
    public AudioClip death;
    public GameObject death_cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_hp <= 0){
            if(!BitHub){
                StartCoroutine(Death());
            }

            else{
                music.Stop();
                music.PlayOneShot(death);
                end.SetActive(true);
                Invoke("SwitchIt", 2.0f);
            }
        }
    }

    void SwitchIt(){
        scene_loader.SetActive(true);
    }
    void OnTriggerEnter(Collider collider){
        if (collider.CompareTag("Bullet")){
            enemy_hp = enemy_hp - damage_take;
            animator.SetTrigger("Damage");
            Destroy(collider.gameObject);
        }
    }

    private IEnumerator Death(){
        death_cam.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1.0f;
        scene_loader.SetActive(true);
    }
}
