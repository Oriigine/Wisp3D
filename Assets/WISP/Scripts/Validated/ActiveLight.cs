using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public GameObject light;
  
    private DetectionBehaviour m_Detect;
    private Interractible m_Interractible;
    private bool IsAlreadyActive = false;


    private void Start()
    {
       m_Detect = gameObject.GetComponent<DetectionBehaviour>();
       m_Interractible = gameObject.GetComponent<Interractible>();
    }
    void Update()
    {
        if (m_Detect.IsDetected)
        {
            light.SetActive(true);
            IsAlreadyActive = true;
            m_Interractible.IsActive = true;

        }
        else if (m_Detect.IsDetected == false && IsAlreadyActive == false)
        {
            light.SetActive(false);
            m_Interractible.IsActive = false;
        }
    }
}
