using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float Drag;
    [SerializeField] private float waitTime;
    [SerializeField] private float changeMoment;
    private bool faceRight = true;
    private Vector2 dir;
    private bool IsFlip;


    [Header("PlayerJump")]
    [SerializeField] private float JumpSpeed;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float groundLength = 0.6f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float fallMultiplier = 5f;
    [SerializeField] private Vector3 JumpOffset;
    private bool IsJump;
    private bool OnGround = false;
    private bool OnGroundColl = false;
    private float saveX;

    Rigidbody2D rb;
    Animator anim;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnGround = Physics2D.Raycast(transform.position + JumpOffset, Vector2.down, groundLength, Ground) || Physics2D.Raycast(transform.position - JumpOffset, Vector2.down, groundLength, Ground);


        if (Input.GetButtonDown("Jump") && OnGround)
        {
            Jump();
        }

        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        anim.SetFloat("Horizontal", Mathf.Abs(dir.x));

        anim.SetFloat("AnimSpeed", Mathf.Abs(rb.velocity.x) * 0.2f);
    }

    private void FixedUpdate()
    {
        Move(dir.x);
        Physics();
    }

    void Move(float Horizontal)
    {
        rb.AddForce(Vector2.right * Horizontal * MoveSpeed);


        if ((Horizontal < 0 && faceRight) || (Horizontal > 0 && !faceRight))
        {
            if(Mathf.Abs(rb.velocity.x) > changeMoment)
            {
                StartCoroutine(PlayerFlip());
            }
            else
            {
                flip();
            }

        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
        IsJump = true;
    }
    void Physics()
    {
        bool changingDir = (dir.x > 0 && rb.velocity.x < 0) || (dir.x < 0 && rb.velocity.x > 0);

        //¶¥¿¡ ´ê¾ÒÀ»¶¼
        if (OnGround)
        {

            anim.SetBool("IsJump", false);
            if (Mathf.Abs(dir.x) < 0.4f || changingDir)
            {

                rb.drag = Drag;
            }
            else
            {
                rb.drag = 0;
            }
            rb.gravityScale = 0;
        }
        //¾È ´ê¾ÒÀ»¶§
        else
        {

            if (IsJump)
            {
                anim.SetBool("IsJump", true);
                IsJump = false;
            }
            rb.gravityScale = gravity;
            rb.drag = Drag * 0.15f;

            //¿Ã¶ó°¡´ÂÁß

            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }

            //³»·Á°¥¶§

            else if(rb.velocity.y > 0 && Input.GetButton("Jump"))
            {

                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

    }

    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(faceRight ? 1f : -1f, 1f, 1f);
    }

    IEnumerator PlayerFlip()
    {
        IsFlip = true;
        flip();
        anim.SetBool("CDir", true);
        yield return new WaitForSeconds(waitTime);
        IsFlip = false;
        anim.SetBool("CDir", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + JumpOffset, transform.position + JumpOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - JumpOffset, transform.position - JumpOffset + Vector3.down * groundLength);
    }
}
