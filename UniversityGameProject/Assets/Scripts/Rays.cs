using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rays : MonoBehaviour
{
    Vector3 rt;
    Quaternion q;
    Vector3 ls;
    void Start()
    {
        rt = transform.rotation.eulerAngles;
        ls = transform.localScale;
    }
    void Update()
    {
        rt.z += .2f;
        q.eulerAngles = rt;
        transform.rotation = q;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, .001f);
    }
}
