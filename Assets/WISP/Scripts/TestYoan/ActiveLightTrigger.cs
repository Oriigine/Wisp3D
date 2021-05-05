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
        m_LightToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider l_Trigger)
    {
        if(m_IsActive == false)
        {
            m_LightToActivate.SetActive(true);
        }
    }

    private void Update()
    {
        //if()
    }
}
