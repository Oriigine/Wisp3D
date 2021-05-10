using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAndRespawnComponent : MonoBehaviour
{
   
    // La fonction Respawn se joue dès lors que le joueur entre en colision avec un objet qui porte le tag "enemy"
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Respawn();
        }
    }

    //La fonction reload la scene.
    void Respawn()
    {
        Debug.Log("respawn");
        SceneManager.LoadScene("EnnemiTest");
    }
}
