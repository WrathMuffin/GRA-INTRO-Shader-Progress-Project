using UnityEngine;
using UnityEngine.Splines;

public class AnimationTest : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode shootKey = KeyCode.E;
    public KeyCode longShootKey = KeyCode.Q;

    public float jump = 10f;
    public float junpCoolDown;

    public LayerMask ground;
    bool isGrounded = true;

    private Rigidbody rb;
    private SplineAnimate splineAnimator;
    private Animator animator;

    private bool isJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        splineAnimator = GetComponent<SplineAnimate>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("isWalking", false);

        if (Input.GetKeyDown(shootKey))
        {
            animator.SetTrigger("shootTrig");
        }

        if (Input.GetKeyDown(longShootKey))
        {
            animator.SetTrigger("longShootTrig");
        }

        if (isGrounded)
        {
            animator.SetBool("isFall", false);
        }

        if (!isGrounded)
        {
            animator.SetBool("isFall", true);
        }


        if (Input.GetKeyDown(jumpKey) && !isJump && isGrounded)
        {
            isJump = true;
            Jump();

            animator.SetTrigger("jumpTrig");
            Invoke(nameof(ResetJump), junpCoolDown); // delay to not jump wwehn holdign down keyyy
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jump, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        isJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
