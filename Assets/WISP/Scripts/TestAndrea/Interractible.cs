using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractible : MonoBehaviour
{
    [SerializeField]
    private bool m_IsActive;


    //Accesseur permettant de r�cup�rer et de modifi� le bool�en "IsActive"
    public bool IsActive
    {
        get { return m_IsActive; }

        set { m_IsActive = value; }
    }
}
