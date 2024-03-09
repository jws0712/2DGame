using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float Drag;
    [SerializeField]
    private Sprite Changedir;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float changeMoment;

    private bool faceRight = true;
    private Vector2 dir;
    Rigidbody2D rb;
    Animator anim;
    new SpriteRenderer renderer;
    private bool IsFlip;

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
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if (rb.velocity.x == 0 && !IsFlip)
        {
            anim.SetTrigger("Idle");
        }
        else if (dir.x != 0 && !IsFlip)
        {
            anim.SetTrigger("Walk");
        }
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
    void Physics()
    {
        if (Mathf.Abs(dir.x) < 0.4f)
        {
            rb.drag = Drag;
        }
        else
        {
            rb.drag = 0;
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
}
