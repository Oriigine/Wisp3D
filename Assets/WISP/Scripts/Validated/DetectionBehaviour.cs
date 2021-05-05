using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool m_IsDetected = false;

    [SerializeField]
    private bool m_BatIsDetected = false;

    public bool IsDetected
    {
        get
        {
            return m_IsDetected;
        }

        set
        {
            m_IsDetected = value;
        }
    }


    public bool BatIsDetected
    {
        get
        {
            return m_BatIsDetected;
        }

        set
        {
            m_BatIsDetected = value;
        }
    }


}
