using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTan : MonoBehaviour
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
        yPos = this.transform.position.y;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        xPos = Mathf.Tan(runningTime) * length;
        this.transform.position = new Vector2(xPos, yPos);
    }
}
