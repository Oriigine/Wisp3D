using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLightTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject m_LightToActivate = null;

    [SerializeField]
    private Interractible m_Interact;

    [SerializeField]
    private bool m_IsActive = false;

    private void Awake()
    {
        //On set la light a �teinte de base
        m_Interact = GetComponent<Interractible>();
        m_LightToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider l_Trigger)
    {
        //Si l'objet n'a pas �t� activ�
        if(m_Interact.IsActive == false)
        {
            //On active la light qu'on veut activer
            m_LightToActivate.SetActive(true);

            //Si l'objet n'est pas activ� 
            if(m_Interact != null)
            {
                //On l'active
                m_Interact.IsActive = true;
                
            }
        }
    }


    //public bool LightActivaded
    //{
    //    get { return m_IsActive; }
    //    set { m_IsActive = value; }
    //}
    
}
