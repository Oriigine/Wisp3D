using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float m_SmoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 m_Offset;
    
    private void FixedUpdate()
    {
        Vector3 l_TargetPos = target.position + m_Offset;

        Vector3 l_SmoothPos = Vector3.Lerp(transform.position, l_TargetPos, m_SmoothSpeed);

        transform.position = l_SmoothPos;
    }
}
