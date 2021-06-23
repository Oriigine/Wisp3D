using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndFade : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Plane;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndDoor")
        {
            m_Plane.GetComponent<MeshRenderer>().material.DOFade(1, 2);
            gameObject.GetComponent<Bis_Mondhor_PlayerController>().enabled = false;
        }
    }
}
