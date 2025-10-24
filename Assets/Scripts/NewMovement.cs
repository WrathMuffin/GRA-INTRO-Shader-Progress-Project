using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMovement : MonoBehaviour
{
    public static int level = 0;
    public Rigidbody rb;
    public Transform transform_body;
    public float jump_power;
    private float jump_input;
    public float move_speed;
    // private bool isGrounded = true;
    public bool isGrounded;
    public int health = 5;
    public GameObject bullet;
    public GameObject reverse_bullet;
    public Transform bullet_point;

    public Vector2 ground_box_size;
    public float ground_box_distance = 1.0f;

    private float horizontal;
    private bool facing_right;
    public Animator animator;

    private bool on_wall;
    public float wall_smoothness = 2.0f;

    private bool is_wall_jumping;
    private float wall_jump_direction;
    public float wall_jump_time = 0.2f;
    private float wall_jump_counter;
    public float wall_jump_duration = 0.4f;
    public Vector2 wall_jump_power = new Vector2(8.0f, 16.0f);
    public static GameObject respawn_point;
    public GameObject health1, health2, health3;
    public Material[] original_materials;
    public Material[] invincible_material;
    public float invincible_timer = 0.0f;
    public float invincible_threshold = 2.0f;
    public SkinnedMeshRenderer object_renderer;
    public bool controllable = false;
    public GameObject death_cam;
    public AudioSource death_source;
    public AudioClip death_clip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
        invincible_timer = invincible_threshold;
        original_materials = object_renderer.materials;
        death_cam.SetActive(false);
    
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable){
            
            if (isGrounded){
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("WADOWD");
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_power);
                }
            }
        
            jump_input = Input.GetAxis("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
            
            if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W)) && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }

            if (!is_wall_jumping){
                if (Input.GetKey(KeyCode.D)){
                    facing_right = true;
                }

                else if (Input.GetKey(KeyCode.A)){
                    facing_right = false;
                }

                if (facing_right){
                    GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }

                else{
                    GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                }
            }

        SpawnBullet();
        }
        AnimateMovement();
        UponDeath();
    }

    void FixedUpdate(){
        if (!is_wall_jumping){
            rb.linearVelocity = new Vector2(horizontal * move_speed, rb.linearVelocity.y);
        }
    }

    private void AnimateMovement(){
        animator.SetFloat("Moving", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("AirMoving", rb.linearVelocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = false;
        }
    }

    private void SpawnBullet(){
        if (Input.GetKeyDown(KeyCode.Q)){
            animator.SetTrigger("shootTrig");

            if (facing_right){
                Instantiate(bullet, bullet_point.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            }

            else{
                Instantiate(reverse_bullet, bullet_point.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            }
        }
    }

    private void UponDeath(){
        if (health < 1){
            death_source.Stop();
            death_source.PlayOneShot(death_clip);
            StartCoroutine(Death());
        }

        if (invincible_timer < invincible_threshold){
            object_renderer.materials = invincible_material;
            invincible_timer = invincible_timer + Time.deltaTime;
            gameObject.layer = 7;
        }

        else{
            object_renderer.materials = original_materials;
            gameObject.layer = 3;
        }
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.layer == 8){
            health = health - 1;
            invincible_timer = 0.0f;
        }

    }

    private IEnumerator Death(){
        death_cam.SetActive(true);
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
