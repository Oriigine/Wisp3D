using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interraction : MonoBehaviour
{
    [SerializeField]
    private List<Interractible> m_Interractibles = new List<Interractible>();

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
        }
    }

    void OpenDoor()
    {
        Debug.Log("jsuiaktivaient");
    }
}
