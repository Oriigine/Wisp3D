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
        m_Interact = GetComponent<Interractible>();
        m_LightToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider l_Trigger)
    {
        if(m_IsActive == false)
        {
            m_LightToActivate.SetActive(true);
            if(m_Interact != null)
            {
                m_Interact.IsActive = true;
            }
        }
    }


    public bool LightActivaded
    {
        get { return m_IsActive; }
        set { m_IsActive = value; }
    }
    
}
