using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAndRespawnComponent : MonoBehaviour
{


   

    [SerializeField]
    private Transform m_Checkpoint;
    // La fonction Respawn se joue dès lors que le joueur entre en colision avec un objet qui porte le tag "enemy"
    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "enemy")
        {
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            Debug.Log("cachange");
            m_Checkpoint.position = transform.position;
        }
    }

    //La fonction reload la scene.
    void Respawn()
    {
        transform.position = m_Checkpoint.position;
    }
}
