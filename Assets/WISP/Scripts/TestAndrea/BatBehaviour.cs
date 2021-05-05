using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_DetectionRange = 30;

    [SerializeField]
    private float m_Speed = 5;

    [SerializeField]
    private Transform m_Player;

    [SerializeField]
    private BatStates m_EnnemiState = BatStates.Sleepy;


    [SerializeField]
    private DetectionBehaviour m_Detect;

    private Vector3 m_StaticPosition;
    private Vector3 m_TargetPosition;


    // Start is called before the first frame update
    void Start()
    {
        m_EnnemiState = BatStates.Sleepy;
        m_StaticPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float l_Step = m_Speed * Time.deltaTime;
        if ( m_EnnemiState == BatStates.Sleepy && Vector3.Distance(transform.position, m_Player.position) < m_DetectionRange)
        {
            Debug.Log("Openedeyes");
            m_EnnemiState = BatStates.OpenedEyes;
        }

        if (m_EnnemiState == BatStates.OpenedEyes && m_Detect.BatIsDetected == true)

        {
            m_TargetPosition = m_Player.position;
            m_EnnemiState = BatStates.Rush;
        }
        if (m_EnnemiState == BatStates.Rush && m_Detect.BatIsDetected == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, l_Step);
        }
        
    }
}
