using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_PlayerDetectionRange = 30;

    public Vector3 position;

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
    private LayerMask m_TorchLayer;

    [SerializeField]
    private Collider[] m_InteractibleDetecte;   
    
    [SerializeField]
    private Animator m_BatAnimator;


    [SerializeField]
    private Vector3 m_TorchPos;
    // Start is called before the first frame update
    void Start()
    {
        m_BatAnimator.SetBool("Idle", true);
        m_StaticPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_EnnemiState);
        Debug.Log(m_Detect.BatIsDetected);
        float l_Step = m_Speed * Time.deltaTime;

        // Si l'ennemi dort et que le joueur entre dans sa zone de detection,il ouvre les yeux.

        if (m_EnnemiState == BatStates.Sleepy && Vector3.Distance(transform.position, m_Player.position) < m_PlayerDetectionRange)
        {
            m_BatAnimator.SetBool("Idle", false);
            m_BatAnimator.SetBool("Awake", true);

            m_EnnemiState = BatStates.OpenedEyes;
            SoundManager.PlaySound3d(SoundManager.SoundEnum.BatOpeningEyes, position);
        }

        //// si l'ennemi � les yeux ouverts et que le joueur l'�claire avec le flash, l'ennemi stocke la position du joueur et se met en mode "rush"

        if (m_EnnemiState == BatStates.OpenedEyes && m_Detect.BatIsDetected == true)

        {
            m_BatAnimator.SetBool("Awake", false);
            m_BatAnimator.SetBool("RushA", true);


            m_TargetPosition = m_Player.position;
            Debug.Log(m_TargetPosition);
            m_EnnemiState = BatStates.Rush;
            SoundManager.PlaySound3d(SoundManager.SoundEnum.BatCharging, position);

        }

        //// si l'ennemi est en mode rush et qu'il est �clair� par le flash alors il fonce sur la position du joueur

        if (m_EnnemiState == BatStates.Rush )
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, l_Step);
            transform.LookAt(new Vector3(m_TargetPosition.x,m_TargetPosition.y + 90,m_TargetPosition.z));
        }

        //// Si la position de l'ennemi a atteind celle du player et qu'il est en etat "Rush" alors on lance l'etat "comeback"

        if (transform.position == m_TargetPosition && m_EnnemiState == BatStates.Rush)
        {
            m_BatAnimator.SetBool("RushA", false);
            m_BatAnimator.SetBool("HitTarget", true);


            m_EnnemiState = BatStates.ComeBack;
        }

        // // si on est en "comeback" alors il revient a sa position originale

        if (m_EnnemiState == BatStates.ComeBack)
        {
            m_BatAnimator.SetBool("Return", false);
            m_BatAnimator.SetBool("RushA", true);


            transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        }

        ////s'il est a sa position originale et en etat comeback, il se rendort

        if (transform.position == m_StaticPosition && m_EnnemiState == BatStates.ComeBack)
        {
            m_BatAnimator.SetBool("RushA", false);
            m_BatAnimator.SetBool("HitInitialPos", true);


            m_EnnemiState = BatStates.OpenedEyes;
        }

        //if (transform.position != m_StaticPosition && transform.position != m_TargetPosition &&  m_Detect.BatIsDetected == false)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, l_Step);

        //}

        if (transform.position == m_TargetPosition )
        {
            m_EnnemiState = BatStates.ComeBack;
        }
        ////s'il est � une position random et que le flah n'est plus activ� alors il revient � sa position
        //if (transform.position == m_TargetPosition && m_Detect.BatIsDetected == false)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, m_StaticPosition, l_Step);
        //    m_EnnemiState = BatStates.OpenedEyes;
        //}


        if(m_EnnemiState != BatStates.Rush && m_EnnemiState != BatStates.Sleepy)
        {
            LightDetectionPos();

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        float l_Timer = 10f;

        if(m_EnnemiState == BatStates.Rush && collision.gameObject.layer != m_LayerToDetect)
        {

            while(l_Timer > 0)
            {
                l_Timer -= Time.deltaTime;
            }
            if(l_Timer <= 0)
            {
                m_BatAnimator.SetBool("RushA", false);
                m_BatAnimator.SetBool("HitWall", true);

                m_EnnemiState = BatStates.ComeBack;
                l_Timer = 10f;
            }
        }
    }

    void LightDetectionPos()
    {
        // Retourne tout les GPE pr�sent dans la zone de d�tection
        m_InteractibleDetecte = Physics.OverlapSphere(transform.position, m_LightDetectionRange, m_TorchLayer);
        float l_Step = m_Speed * Time.deltaTime;


        // On v�rifie si le tableau n'est pas vide
        if (m_InteractibleDetecte.Length > 0)
        {
            Debug.Log("y a tablo torche ");
            float l_MinDist = 0;
            GameObject nearestGameObject = null;
            // Pour chaque �l�ment (collider2D) dans ce tableau
            foreach (Collider item in m_InteractibleDetecte)
            {

                if (l_MinDist == 0)
                {
                    l_MinDist = Vector3.Distance(transform.position, item.transform.position);
                }


                else if(Vector3.Distance(transform.position, item.transform.position) < l_MinDist)
                {
                    l_MinDist = Vector3.Distance(item.transform.position, transform.position);
                    nearestGameObject = item.transform.gameObject;
                }
                
                // On envoie un linecast dans sa direction
                RaycastHit l_TestCollision;
                Physics.Linecast(transform.position, item.transform.position, out l_TestCollision, m_ObstacleLayer);


                // Si l'objet touch� par le linecast est le m�me que celui d�tect� � l'origine,
                // �a veut dire que la vision n'est pas occult� par un �l�ment
                // Et si la distance de l'�l�ment d�tect� est inf�rieur � la distance d'activation
                if (l_TestCollision.collider == item && m_EnnemiState != BatStates.Rush && l_TestCollision.collider.gameObject.GetComponent<Interractible>().IsActive == true)
                {
                    m_TorchPos = l_TestCollision.collider.transform.position;
                    m_StaticPosition = m_TorchPos;
                    l_TestCollision.collider.gameObject.GetComponent<Interractible>().IsActive = false;
                    transform.position = Vector3.MoveTowards(transform.position, m_TorchPos, l_Step); /*l_TestCollision.collider.transform.position;*/

                }
             
            }
        }
    }
}
