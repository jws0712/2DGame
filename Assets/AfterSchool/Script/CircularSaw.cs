using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [Header("직선이동 대한 선형보간")]
    public Transform[] poses;
    public Transform saw;
    public float duration = 0f;
    [SerializeField]
    [Range(0f, 500f)] private float rotSpeed = 100f;
    [SerializeField]
    [Range(0, 5f)] private float maxDuration = 3f;
    int dir = 1;
    // Update is called once per frame
    void Update()
    {
        duration += dir * Time.deltaTime;

        if(duration > maxDuration || duration < 0)
        {
            dir *= -1;
        }

        saw.position = Vector3.Lerp(poses[0].position, poses[1].position, duration / maxDuration);
        saw.Rotate(new Vector3(0,0,dir) * Time.deltaTime * rotSpeed);
    }
}
