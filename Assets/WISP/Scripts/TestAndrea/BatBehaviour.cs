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
        Debug.Log(m_Detect.BatIsDetected);
        float l_Step = m_Speed * Time.deltaTime;

        // Si l'ennemi dort et que le joueur entre dans sa zone de detection,il ouvre les yeux.

        if ( m_EnnemiState == BatStates.Sleepy && Vector3.Distance(transform.position, m_Player.position) < m_DetectionRange)
        {
           
            m_EnnemiState = BatStates.OpenedEyes;
        }

        // si l'ennemi à les yeux ouverts et que le joueur l'éclaire avec le flash, l'ennemi stocke la position du joueur et se met en mode "rush"

        if (m_EnnemiState == BatStates.OpenedEyes && m_Detect.BatIsDetected == true)

        {

            m_TargetPosition = m_Player.position;
            m_EnnemiState = BatStates.Rush;

        }
       
        // si l'ennemi est en mode rush et qu'il est éclairé par le flash alors il fonce sur la position du joueur

        if (m_EnnemiState == BatStates.Rush && m_Detect.BatIsDetected == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, l_Step);
        }

        // Si la position de l'ennemi a atteind celle du player et qu'il est en etat "Rush" alors on lance l'etat "comeback"

        if( transform.position == m_TargetPosition && m_EnnemiState == BatStates.Rush)
        {
            m_EnnemiState = BatStates.ComeBack;
        }

         // si on est en "comeback" alors il revient a sa position originale

        if ( m_EnnemiState == BatStates.ComeBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        }

        //s'il est a sa position originale et en etat comeback, il se rendort

        if( transform.position == m_StaticPosition && m_EnnemiState == BatStates.ComeBack)
        {
            m_EnnemiState = BatStates.Sleepy;
        }

        //if (transform.position != m_StaticPosition && transform.position != m_TargetPosition && m_Detect.BatIsDetected == false)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, l_Step);
        //}

        if ( transform.position != m_TargetPosition && m_Detect.BatIsDetected == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        } 
        
     



    }
}
