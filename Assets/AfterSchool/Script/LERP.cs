using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LERP : MonoBehaviour
{
    [Header("��ġ, �ӵ�")]

    [SerializeField] private Vector2 pos;
    [SerializeField]
    [Range(0f, 2f)] private float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Vector2.zero;
        pos = new Vector2(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector2.Lerp(this.transform.position, pos, speed * Time.deltaTime);
    }
}
