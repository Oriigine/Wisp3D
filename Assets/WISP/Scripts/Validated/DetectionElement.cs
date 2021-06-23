using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectionElement : MonoBehaviour
{
    // Enorme zone qui renvoie tout les éléments interractibles à proximité
    public float m_DetectionRange = 30;
    // Zone d'activation
    public float m_ActivationRange = 10;
    public LayerMask m_LayerToDetect;
    public LayerMask m_Ground;
    public LayerMask m_EnemyLayer;

    public Vector3 position;

    //Variables concernant les lights
    [SerializeField]
    private GameObject m_FlashLight;

    [SerializeField]
    private ParticleSystem m_PassiveParticle;

    [SerializeField]
    private ParticleSystem m_FlashParticle;



    private Light l_FlashParam;

    [SerializeField]
    private bool m_FlashActivated = false;
    [SerializeField]
    private float m_TimeToFlashOn = 10;
    [SerializeField]
    private float m_TimeToFlashOff = 10;

    [SerializeField]
    private float m_MinRange = 0f; // min range
    [SerializeField]
    private float m_MaxRange = 40; // max range


    [SerializeField]
    private float m_Counter = 0f;

    float m_Time = 0;

    [SerializeField]
    float m_FlashDuration = 0f;

    [SerializeField]
    Collider[] m_Bats;

    [SerializeField]
    Collider[] m_InteractibleDetecte;


    [SerializeField]
   private XboxMapping m_XboxMapping;


    //bool isPassivePlaying = false;

    private void Start()
    {
        //j'assigne le component light2d de ma light à une variable l_FlashParamù

        //m_PassiveParticle.emission.enabled(true);
        if (m_FlashParticle.isPlaying) m_FlashParticle.Stop();
        if(!m_PassiveParticle.isPlaying) m_PassiveParticle.Play();
        l_FlashParam = m_FlashLight.GetComponent<Light>();
    }

    //void togglePlay()
    //{
    //   // isPassivePlaying = !isPassivePlaying;
    //    if (!m_PassiveParticle.isPlaying) m_PassiveParticle.Play();
    //    else m_PassiveParticle.Stop();
    //    // m_PassiveParticle.Play(isPassivePlaying);
    //}
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    togglePlay();
        //    Debug.Log("play function called");
        //}
        
        
        // lorsque j'appuie sur click gauche et que m_Counter est nul
        if (Input.GetButtonDown("Fire1") || m_XboxMapping.InputBool && m_Counter <= 0)
        {
            m_Time = 0;
            // si le flash n'est pas déjà activé
            if (m_FlashActivated == false)
            {
                //if(m_Counter <= 0.1)
                //{ 
                //    SoundManager.PlaySound(SoundManager.SoundEnum.PlayerFlash);
                //}
                Debug.Log("StartCor");
                // je démarre la coroutine FlahingIn qui aggrandit la range de la light (le flash s'active)
                StartCoroutine(FlashingIn(l_FlashParam));
                m_FlashDuration += 0.2f;

                if(!m_FlashParticle.isPlaying) m_FlashParticle.Play();
                Debug.Log("Play Flash");
                if (m_PassiveParticle.isPlaying) m_PassiveParticle.Stop();
                Debug.Log("Stop Passive");
            }

        }

        // Si m_Counter à une valeur supérieure ou égale à la durée d'éclairage du flash 
        if (m_Counter >= m_TimeToFlashOn)
        {

            m_Time = 0;

            // si le flash est déjà activé
            if (m_FlashActivated == true)
            {
                // on démarre la coroutine qui va éteindre le flash

                m_FlashDuration -= Time.deltaTime;
            }
        }

        if (m_FlashDuration <= 0 && Input.GetButtonUp("Fire1") || m_XboxMapping.CInputBool)
        {
            StartCoroutine(FlashingOut(l_FlashParam));

            if(!m_PassiveParticle.isPlaying) m_PassiveParticle.Play();
            Debug.Log("Play Passive");
            if(m_FlashParticle.isPlaying) m_FlashParticle.Stop();
            Debug.Log("Stop Flash");
        }

        // si le flash est déjà activé
        if (m_FlashActivated)
        {
            StartCoroutine("Detection");
            StartCoroutine("BatsDetection");

        }
        //sinon
        else
        {
            StopCoroutine("Detection");
            StopCoroutine("BatsDetection");
        }

        if (m_Bats.Length > 0)
        {
            foreach (Collider Bat in m_Bats)
            {
                RaycastHit l_TestCollision;
                Physics.Linecast(transform.position, Bat.transform.position, out l_TestCollision, m_Ground);

                if (Vector2.Distance(Bat.transform.position, transform.position) > m_ActivationRange || m_FlashActivated == false)
                {
                    // L'élément n'est pas/plus détécté
                    Bat.GetComponent<DetectionBehaviour>().BatIsDetected = false;
                }
            }
        }
    }


    IEnumerator BatsDetection()
    {
        //retourne toutes les chauves souris dasn la zone de detection
        m_Bats = Physics.OverlapSphere(transform.position, m_DetectionRange, m_EnemyLayer);

        if (m_Bats.Length > 0)
        {
            Debug.Log("y a Bats");

            foreach (Collider Bat in m_Bats)
            {
                RaycastHit l_TestCollision;
                Physics.Linecast(transform.position, Bat.transform.position, out l_TestCollision, m_Ground);

                if (l_TestCollision.collider == Bat && Vector2.Distance(Bat.transform.position, transform.position) < m_ActivationRange)
                {
                    Debug.Log("BatDetect");
                    // L'élément est détécté
                    Bat.GetComponent<DetectionBehaviour>().BatIsDetected = true;
                }
                // sinon
                else
                {
                    Debug.Log("BatDetectno");

                    // L'élément n'est pas/plus détécté
                    Bat.GetComponent<DetectionBehaviour>().BatIsDetected = false;
                }
            }
        }
        yield return null;
    }


    IEnumerator Detection()
    {
        // Retourne tout les GPE présent dans la zone de détection
        m_InteractibleDetecte = Physics.OverlapSphere(transform.position, m_DetectionRange, m_LayerToDetect);

        // On vérifie si le tableau n'est pas vide
        if (m_InteractibleDetecte.Length > 0)
        {
            Debug.Log("y a tablo");
            // Pour chaque élément (collider2D) dans ce tableau
            foreach (Collider item in m_InteractibleDetecte)
            {
                // On envoie un linecast dans sa direction
                RaycastHit l_TestCollision;
                Physics.Linecast(transform.position, item.transform.position, out l_TestCollision, m_Ground);

                // Si l'objet touché par le linecast est le même que celui détecté à l'origine,
                // ça veut dire que la vision n'est pas occulté par un élément
                // Et si la distance de l'élément détecté est inférieur à la distance d'activation
                if (l_TestCollision.collider == item && Vector2.Distance(item.transform.position, transform.position) < m_ActivationRange)
                {
                    // L'élément est détécté
                    item.GetComponent<DetectionBehaviour>().IsDetected = true;
                }
                // sinon
                else
                {
                    // L'élément n'est pas/plus détécté
                    item.GetComponent<DetectionBehaviour>().IsDetected = false;
                }
            }
        }
        yield return null;
    }

    IEnumerator FlashingIn(Light lightToFade)
    {
        // le flash n'est pas activé
        if (m_FlashActivated == false)
        {
            // Tant que m_Counter n'a pas dépassé la durée d'activation du flash
            while (m_Counter < m_TimeToFlashOn)
            {
                // si m_Time est inférieur à 1 on l'incrémente avec une certaine valeur 
                if (m_Time < 1)
                {
                    m_Time += 12 * Time.deltaTime;
                }

                // on incrémente m_Counter 2x plus vite que m-Time pour qu'ils arrivent au même moment à leur valeur max
                m_Counter += 24 * Time.deltaTime;

                // On effectue un lerp entre valeur min et max des inner et outer range de la light

                lightToFade.range = Mathf.Lerp(m_MinRange, m_MaxRange, m_Time);
                Debug.Log("ca flash");

                // le flash est activé

                yield return m_FlashActivated = true;

            }
        }
    }

    IEnumerator FlashingOut(Light lightToFade)
    {
        // si le flash n'est pas activé
        if (m_FlashActivated == true)
        {
            //Tant que m_Counter ne retourne pas àsa valeur d'origine (0)
            while (m_Counter > m_TimeToFlashOff)
            {
                // si m_Time est inférieur à 1 on l'incrémente avec une certaine valeur 
                if (m_Time < 1)
                {
                    m_Time += 0.5f * Time.deltaTime;
                }

                // on décrémente m_Counter 2x plus vite que m-Time pour qu'ils arrivent au même moment à leur valeur max
                m_Counter -= Time.deltaTime;

                // On effectue un lerp entre valeur max et min des inner et outer range de la light

                lightToFade.range = Mathf.Lerp(m_MaxRange, m_MinRange, m_Time);
                Debug.Log("ca d flash");

                // Le flash est désactivé
                yield return m_FlashActivated = false;
            }

        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, m_DetectionRange);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, m_ActivationRange);
    }
}
