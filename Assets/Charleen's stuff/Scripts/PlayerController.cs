using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Movement Settings")]
    public float speed = 10f;
    public float jump = 10f;
    public float junpCoolDown;
    public float speedInAir;
    public float groundDrag;

    [Header("Touch grass")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Transform myDir;

    private Rigidbody rb;
    private float hori, verti;
    private Vector3 moveDir;

    bool isJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // check if there is a gorund by shoting a raycast line donwward (the line is halfplayers height plus a little extra to make sure it touch ground)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // check inputs
        Inputs();

        // do drag
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }

        else
        {
            rb.linearDamping = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movements();
        LimitSpeed();
    }

    private void Inputs()
    {
        hori = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");


        if (Input.GetButton("Jump") && !isJump && isGrounded)
        {
            isJump = true;
            Jump();

            Invoke(nameof(ResetJump), junpCoolDown); // delay to not jump wwehn holdign down keyyy
        }
    }

    private void Movements()
    {
        moveDir = myDir.right * hori + myDir.forward * verti;

        // moving on the ground
        if (isGrounded) { rb.AddForce(moveDir.normalized * speed, ForceMode.Force); }

        // moving in air
        // moves fasster when midair
        else if (!isGrounded) { rb.AddForce(moveDir.normalized * speed * speedInAir, ForceMode.Force); }

    }

    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limmit teh velocity
        if (flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            rb.linearVelocity = new Vector3(limitVel.x, rb.linearVelocity.y, limitVel.z);
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
}