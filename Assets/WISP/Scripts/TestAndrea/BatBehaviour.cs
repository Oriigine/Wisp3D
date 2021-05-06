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
       
        m_StaticPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_EnnemiState);
        float l_Step = m_Speed * Time.deltaTime;
        if ( m_EnnemiState == BatStates.Sleepy && Vector3.Distance(transform.position, m_Player.position) < m_DetectionRange)
        {
           
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

        if( transform.position == m_TargetPosition && m_EnnemiState == BatStates.Rush)
        {
            m_EnnemiState = BatStates.ComeBack;
        }

        if ( m_EnnemiState == BatStates.ComeBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        }

        if( transform.position == m_StaticPosition && m_EnnemiState == BatStates.ComeBack)
        {
            m_EnnemiState = BatStates.Sleepy;
        }



    }
}
