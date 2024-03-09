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
    private bool IsJumpH;

    float Horizontal;

    Rigidbody2D rb;

    Animator animator;

    [Header("Keybord")]
    public KeyCode JumpKey = KeyCode.Space;




    private void Awake()
    {
        Application.targetFrameRate = 75;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        grounded = false;
        IsJump = false;
        IsJumpH = false;
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(JumpKey) && grounded)
        {
            IsJump = true;
            animator.SetBool("IsJump", true);
        }

        if (rb.velocity.x == 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Walk");
        }

        Xfilp();

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

    void Xfilp()
    {
        if(Horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(Horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
            animator.SetTrigger("Idle");
            animator.SetBool("IsJump", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
        
    }
}
