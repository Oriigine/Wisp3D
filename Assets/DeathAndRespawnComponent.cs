using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAndRespawnComponent : MonoBehaviour
{
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Respawn();
        }
    }


    void Respawn()
    {
        Debug.Log("respawn");
        SceneManager.LoadScene("EnnemiTest");
    }
}
