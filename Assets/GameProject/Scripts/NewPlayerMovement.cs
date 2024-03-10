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

    private bool OnGround = false;

    Rigidbody2D rb;
    Animator anim;
    new SpriteRenderer renderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, Ground);

        if (Input.GetButtonDown("Jump") && OnGround)
        {
            Jump();
        }

        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        anim.SetFloat("Horizontal", Mathf.Abs(dir.x));

    }

    private void FixedUpdate()
    {
        Move(dir.x);
        Physics();
    }

    void Move(float Horizontal)
    {
        rb.AddForce(Vector2.right * Horizontal * MoveSpeed);

        if (Mathf.Abs(rb.velocity.x) < 0.001f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if ((Horizontal < 0 && faceRight) || (Horizontal > 0 && !faceRight))
        {
            if(Mathf.Abs(rb.velocity.x) > changeMoment || Mathf.Abs(rb.velocity.x) < -changeMoment)
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
    }
    void Physics()
    {

        if (OnGround)
        {
            anim.SetBool("IsJump", false);
            if (Mathf.Abs(dir.x) < 0.4f)
            {
                rb.drag = Drag;
            }
            else
            {
                rb.drag = 0;
            }
            rb.gravityScale = 0;
        }
        else
        {
            anim.SetBool("IsJump", true);
            rb.gravityScale = gravity;
            rb.drag = Drag * 0.15f;
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
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
        faceRight = !faceRight;
        transform.localScale = new Vector3(faceRight ? 1f : -1f, 1f, 1f);
        anim.SetBool("CDir", true);
        yield return new WaitForSeconds(waitTime);
        IsFlip = false;
        anim.SetBool("CDir", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}
