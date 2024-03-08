using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;

    float Horizontal;

    Rigidbody2D rb;

    

    private void Awake()
    {
        Application.targetFrameRate = 75;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
    }

}
