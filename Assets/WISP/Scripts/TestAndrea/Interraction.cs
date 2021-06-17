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

    public Vector3 position;

    private void Update()
    {
        Activationconditon();
    }

    private void Activationconditon()
    {
        bool flag = false;
        //On regarde si tout les interractibles sont activés
        foreach (Interractible p_Interractible in m_Interractibles)
        {
            if (p_Interractible.IsActive)
            {
                //Si oui on passe le flag a true
                flag = true;

            }
            if (!p_Interractible.IsActive)
            {
                //si non bah rip 
                flag = false;
                break;
            }
        }

        if (flag)
        {
            //Si flag est true on ouvre la porte :)
            OpenDoor();
            SoundManager.PlaySound3d(SoundManager.SoundEnum.DoorOpening, position);
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
