using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform transform_body;
    public float jump_power;
    private float jump_input;
    public float move_speed;
    // private bool isGrounded = true;
    private bool ground_below;
    public static int health = 3;

    public Vector2 ground_box_size;
    public float ground_box_distance = 1.0f;
    public static PlayerShade player_shade;

    private float horizontal;
    private bool facing_right;
    public Animator animator;
    public Transform wall_check;
    public LayerMask wall_layer;

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
    public static bool controllable = true;
    public VideoPlayer vid_play;
    public GameObject raw_image;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform_body = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        if (GetComponent<SpriteRenderer>().color == Color.white){
            player_shade = PlayerShade.WHITE;
        }

        else{
            player_shade = PlayerShade.BLACK;
        }
        facing_right = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 3){
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(true);
        }

        else if (health == 2){
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(false);
        }

        else if (health == 1){
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
        }
        

        if (GetComponent<SpriteRenderer>().color == Color.white){
            player_shade = PlayerShade.WHITE;
        }

        else{
            player_shade = PlayerShade.BLACK;
        }
        //Move();

        jump_input = Input.GetAxis("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        if (controllable){

            if (ground_below){
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_power);
                }
            }
        

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
                    GetComponent<Transform>().localScale = new Vector2(1, 1);
                }

                else{
                    GetComponent<Transform>().localScale = new Vector2(-1, 1);
                }
            }
        }

        else{
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        Animate();

        WallSlide();
        WallJump();        
    }

    void FixedUpdate(){

        if (!is_wall_jumping){
            rb.linearVelocity = new Vector2(horizontal * move_speed, rb.linearVelocity.y);
        }

        // if (jump_input > 0){
        //     Jump();
        // }
        GroundCheck();
    }

    void Jump(){
        rb.AddForce(transform.up * jump_power, ForceMode2D.Impulse);
    }

    void Move(){
        // if (Input.GetKey(KeyCode.D)){
        //     transform_body.position += transform.right * Time.deltaTime * move_speed;
        //     transform_body.rotation = Quaternion. Euler(0, 0, 0);
        // }

        // if (Input.GetKey(KeyCode.A)){
        //     transform_body.position += transform.right * Time.deltaTime * move_speed;
        //     transform_body.rotation = Quaternion. Euler(0, 180, 0);
        // }
    }

    private void GroundCheck(){
        RaycastHit2D[] ground_ray = Physics2D.BoxCastAll(transform.position, ground_box_size, 0, -transform.up, ground_box_distance);

        for (int i = 0; i < ground_ray.Length; i++){
            if (ground_ray[i].collider.gameObject.layer == 0 || ground_ray[i].collider.gameObject.layer == 6){
                ground_below = true;
                break;
            }

            else{
                ground_below = false;
            }
        }

        Debug.Log(ground_below);
    }
    private void Animate(){

        if(!on_wall){
            if (rb.linearVelocity.x != 0 && rb.linearVelocity.y == 0){
                animator.SetInteger("State", 3);
            }

            else{
                animator.SetInteger("State", 0);
            }

            if (rb.linearVelocity.y > 0){
                animator.SetInteger("State", 1);
            }

            else if (rb.linearVelocity.y < 0){
                animator.SetInteger("State", 2);
            }
        }

        else{
            animator.SetInteger("State", 4);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * ground_box_distance, ground_box_size);
    }
    public enum PlayerShade{
        WHITE,
        BLACK
    }

    void ChangeLight(){

    }

    bool IsOnWall(){
        return Physics2D.OverlapCircle(wall_check.position, 0.2f, wall_layer);
    }

    void WallSlide(){
        if (!ground_below && IsOnWall() && horizontal != 0){
            on_wall = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wall_smoothness, float.MaxValue));
        }

        else{
            on_wall = false;
        }
    }

    void WallJump(){
        if (on_wall){
            is_wall_jumping = false;
            wall_jump_direction = -transform.localScale.x;
            wall_jump_counter = wall_jump_time;

            CancelInvoke(nameof(StopWallJumping));
        }

        else{
            wall_jump_counter -= Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump"))&& wall_jump_counter > 0){
            is_wall_jumping = true;
            rb.linearVelocity = new Vector2(wall_jump_direction * wall_jump_power.x, wall_jump_power.y);
            wall_jump_counter = 0.0f;

            if (transform.localScale.x != wall_jump_direction){
                facing_right = !facing_right;
                Vector3 local_scale = transform.localScale;
                local_scale.x *= -1f;
                transform.localScale = local_scale;
            }

            Invoke(nameof(StopWallJumping), wall_jump_duration);
        }
    }

    void StopWallJumping(){
        is_wall_jumping = false;
    }

    public void PlayVideo(){
        Debug.Log("EDUI");
        raw_image.SetActive(true);
        vid_play.enabled = true;
    }
}
