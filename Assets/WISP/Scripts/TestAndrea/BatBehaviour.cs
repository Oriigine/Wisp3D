using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_PlayerDetectionRange = 30;

   

    [SerializeField]
    private float m_LightDetectionRange = 30;

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


    [SerializeField]
    private LayerMask m_LayerToDetect;

    [SerializeField]
    private LayerMask m_ObstacleLayer;

    [SerializeField]
    private Collider[] m_InteractibleDetecte;


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

        if ( m_EnnemiState == BatStates.Sleepy && Vector3.Distance(transform.position, m_Player.position) < m_PlayerDetectionRange)
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

        //s'il est à une position random et que le flah n'est plus activé alors il revient à sa position
        if ( transform.position != m_TargetPosition && m_Detect.BatIsDetected == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        }



        LightDetectionPos();

    }

    void LightDetectionPos()
    {
        // Retourne tout les GPE présent dans la zone de détection
       m_InteractibleDetecte = Physics.OverlapSphere(transform.position, m_LightDetectionRange, m_LayerToDetect);


        // On vérifie si le tableau n'est pas vide
        if (m_InteractibleDetecte.Length > 0)
        {
            Debug.Log("y a tablo torche ");
            // Pour chaque élément (collider2D) dans ce tableau
            foreach (Collider item in m_InteractibleDetecte)
            {
                // On envoie un linecast dans sa direction
                RaycastHit l_TestCollision;
                Physics.Linecast(transform.position, item.transform.position, out l_TestCollision, m_ObstacleLayer);

                // Si l'objet touché par le linecast est le même que celui détecté à l'origine,
                // ça veut dire que la vision n'est pas occulté par un élément
                // Et si la distance de l'élément détecté est inférieur à la distance d'activation
                if (l_TestCollision.collider == item && m_Detect.BatIsDetected == false && l_TestCollision.collider.gameObject.GetComponent<ActiveLightTrigger>().LightActivaded == true)
                {
                    transform.position = l_TestCollision.collider.transform.position;
                }
             
            }
        }
    }
}
