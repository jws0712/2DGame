using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField]
    private float MoveSpeed;

    public Vector2 dir;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
    }

    private void FixedUpdate()
    {
        Move(dir.x);
    }

    void Move(float Horizontal)
    {
        rb.AddForce(Vector2.right * Horizontal * MoveSpeed);
    }
}
