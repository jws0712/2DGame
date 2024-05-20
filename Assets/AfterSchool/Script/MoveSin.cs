using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Sin : MonoBehaviour
{
    [Header("속도, 길이")]

    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float length = 1f;

    private float runningTime = 0f;
    private float xPos = 0f;
    private float yPos = 0f;
    void Start()
    {
        xPos = this.transform.position.x;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        this.transform.position = new Vector2(xPos, yPos);
    }
}
