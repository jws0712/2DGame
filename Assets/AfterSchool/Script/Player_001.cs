using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_001 : MonoBehaviour
{
    [Header("이동, 회전에 대한 선형보간")]
    public Transform model;
    [SerializeField] [Range(0f, 10f)] private float moveSpeed = 5f;

    float x, y;

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey) return;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        Vector3 nor = new Vector3 (x, y, 0).normalized;
        transform.Translate(nor * moveSpeed * Time.deltaTime);

        float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;

        Quaternion lookRot = Quaternion.Euler(0, 0, z - 90f);

        model.rotation = Quaternion.Lerp(model.rotation, lookRot, Time.deltaTime * moveSpeed);
    }
}
