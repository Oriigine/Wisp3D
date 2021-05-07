using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interraction : MonoBehaviour
{
    [SerializeField]
    private List<Interractible> m_Interractibles = new List<Interractible>();

    [SerializeField]
    private GameObject m_Door = null;

    [SerializeField]
    private bool m_IsOpen = false;

    private void Update()
    {
        Activationconditon();
    }

    private void Activationconditon()
    {
        bool flag = false;

        foreach (Interractible p_Interractible in m_Interractibles)
        {
            if (p_Interractible.IsActive)
            {
                flag = true;

            }
            if (!p_Interractible.IsActive)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            OpenDoor();
            m_IsOpen = true;
        }
    }

    void OpenDoor()
    {
        if(m_IsOpen == true)
        {
            m_Door.SetActive(false);
        }
    }
}
