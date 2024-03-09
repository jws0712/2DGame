using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;

    public float JumpPower;

    private bool grounded;

    private bool IsJump;

    float Horizontal;

    Rigidbody2D rb;

    

    private void Awake()
    {
        Application.targetFrameRate = 75;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        grounded = false;
        IsJump = false;
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            IsJump = true;
        }

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (IsJump)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            IsJump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
