using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EndFade : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Plane;

    [SerializeField]
    private Image m_EndPic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndDoor")
        {
            gameObject.GetComponent<Bis_Mondhor_PlayerController>().enabled = false;

            Sequence mySequence = DOTween.Sequence();
            mySequence.Insert(0, m_Plane.GetComponent<MeshRenderer>().material.DOFade(1, 1f));
            mySequence.Insert(0.75f, m_EndPic.DOFade(1, 10f));
        }
    }
}
