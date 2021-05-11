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
        //On set la light a éteinte de base
        m_LightToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider l_Trigger)
    {
        //Si l'objet n'a pas été activé
        if(m_IsActive == false)
        {
            //On active la light qu'on veut activer
            m_LightToActivate.SetActive(true);

            //Si l'objet n'est pas activé 
            if(m_Interact != null)
            {
                //On l'active
                m_Interact.IsActive = true;
            }
        }
    }
}
