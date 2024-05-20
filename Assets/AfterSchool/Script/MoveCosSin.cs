using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCosSin : MonoBehaviour
{
    [Header("속도, 길이")]

    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float radius = 1f;

    private float runningTime = 0f;
    private Vector2 newPos = new Vector2();
    void Start()
    {
        newPos.x = this.transform.position.x;
        newPos.y = this.transform.position.y;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        float x = radius * Mathf.Cos(runningTime);
        float y = radius * Mathf.Sin(runningTime);
        newPos = new Vector2(x, y);
        this.transform.position = newPos;
    }
}
