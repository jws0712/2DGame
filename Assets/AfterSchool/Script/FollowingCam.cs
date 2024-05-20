using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCam : MonoBehaviour
{
    [Header("카메라 이동에 대한 성형보간")]
    [SerializeField]
    [Range(0f, 10f)] private float moveSpeed = 5f;
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        if(target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 0, -10f), Time.deltaTime * moveSpeed);
    }
}
