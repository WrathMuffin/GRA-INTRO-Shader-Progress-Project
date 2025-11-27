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
    public GameObject bulletEffect;


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
            determineBulletSpawn(collider);
            //GameObject effect = Instantiate(bulletEffect, collider.transform.position, Quaternion.LookRotation(this.transform.up));

            Destroy(collider.gameObject);
        }
    }

    void determineBulletSpawn(Collider collider)
    {
        //basically the direction of the colliing bullet to the boss is checked, and then it gets
        //corrected otherwise the angle of the bullet hit effect will always be at a tilted angle.
        //if the angle is in the positives then it will play at the corresponding front 
        //if its in the negatives it plays at the corresponding back
        Vector3 directionOfBossToBullet = collider.transform.position - this.transform.position;
        if (directionOfBossToBullet.x >0)
        {
            directionOfBossToBullet.x = 90;
        }
        if (directionOfBossToBullet.x < 0)
        {
            directionOfBossToBullet.x = -90;
        }
        GameObject effect = Instantiate(bulletEffect, collider.transform.position, Quaternion.LookRotation(directionOfBossToBullet));
    }
    

    private IEnumerator Death(){
        death_cam.SetActive(true);
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2.0f);
        Time.timeScale = 1.0f;
        scene_loader.SetActive(true);
    }
}
