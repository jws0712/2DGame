using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 InputVector;
    Rigidbody2D rigid;

    private void Awake()
    {
        Application.targetFrameRate = 75;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 nextPos = rigid.position + InputVector * speed * Time.deltaTime;
        rigid.MovePosition(nextPos);
    }

    void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }

}
