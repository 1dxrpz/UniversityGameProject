using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DrawSolidArc))]
public class ParticleController : MonoBehaviour
{
    [Range(0, 360)]
    public float Angle = 0f;
    [Range(0, 360)]
    public float Radius = 10f;
    public float Min = 1f;
    public float Max = 10f;

    void OnDrawGizmosSelected()
    {
        float rayRange = 10.0f;
        float halfFOV = Radius / 2.0f;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + Angle, Vector3.forward);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + Angle, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        Gizmos.DrawRay(transform.position, upRayDirection);
        Gizmos.DrawRay(transform.position, downRayDirection);
        Gizmos.DrawLine(transform.position + downRayDirection, transform.position + upRayDirection);
        Gizmos.DrawWireMesh
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
