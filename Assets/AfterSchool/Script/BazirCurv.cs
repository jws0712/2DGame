using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazirCurv : MonoBehaviour
{
    [Range(0f, 1f)] public float t;
    public Transform[] poses;
    public Transform target;

    [Header("Condition")]
    public bool slowDeph1;
    public bool slowDeph2;
    public bool slowDeph3;
    public Vector3 textOffset;

    [Header("Texts")]
    public GameObject abText;
    public GameObject bcText;
    public GameObject cdText;
    public GameObject abbcText;
    public GameObject bccdText;

    [Header("Line")]
    public LineRenderer abLine;
    public LineRenderer bcLine;
    public LineRenderer cdLine;
    public LineRenderer abbcLine;
    public LineRenderer bccdLine;
    public LineRenderer abbcbccdLine;

    private void Update()
    {
        Vector3 vecPos = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t);
        Vector3 nextVec = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t + Time.deltaTime);

        var dir = nextVec - vecPos;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(0, 0, z);

        target.position = vecPos;

        gizmosDraw(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t);
    }

    Vector3 cubicBezierVec(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        var ab = Vector3.Lerp(a, b, t);
        var bc = Vector3.Lerp(b, c, t);
        var cd = Vector3.Lerp(c, d, t);

        var abbc = Vector3.Lerp(ab, bc, t);
        var bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }

    void gizmosDraw(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        abText.SetActive(slowDeph1);
        bcText.SetActive(slowDeph1);
        cdText.SetActive(slowDeph1);

        abLine.gameObject.SetActive(slowDeph1);
        bcLine.gameObject.SetActive(slowDeph1);
        cdLine.gameObject.SetActive(slowDeph1);

        abbcLine.gameObject.SetActive(slowDeph2);
        bccdLine.gameObject.SetActive(slowDeph2);

        abbcbccdLine.gameObject.SetActive(slowDeph3);

        var ab = Vector3.Lerp(a, b, t);
        abLine.SetPositions(new Vector3[2] { a, b });
        abText.transform.position = ab + textOffset;


        var bc = Vector3.Lerp(b, c, t);
        abLine.SetPositions(new Vector3[2] { b, c });
        abText.transform.position = bc + textOffset;


        var cd = Vector3.Lerp(c, d, t);
        abLine.SetPositions(new Vector3[2] { c, d });
        abText.transform.position = cd + textOffset;


        var abbc = Vector3.Lerp(ab, bc, t);
        abLine.SetPositions(new Vector3[2] { ab, cd });
        abText.transform.position = abbc + textOffset;


        var bccd = Vector3.Lerp(bc, cd, t);
        abLine.SetPositions(new Vector3[2] { bc, cd });
        abText.transform.position = bccd + textOffset;


        var abbcbccd = Vector3.Lerp(abbc, bccd, t);
        abLine.SetPositions(new Vector3[2] { abbc, bccd });
        abText.transform.position = abbcbccd + textOffset;
    }
}

