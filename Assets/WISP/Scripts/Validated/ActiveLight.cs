using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public Vector3 position;

    [SerializeField]
    public GameObject m_Light;
  
    [SerializeField]
    private DetectionBehaviour m_Detect;

    [SerializeField]
    private Interractible m_Interractible;

    [SerializeField]
    private bool IsAlreadyActive = false;


    private void Start()
    {
       m_Detect = gameObject.GetComponent<DetectionBehaviour>();
       m_Interractible = gameObject.GetComponent<Interractible>();
    }
    void Update()
    {
        //Si l'objet se fait détécter alors on allume sa light
        //On dit qu'il est actif
        //Et on passe son booléen de son "m_Interractible" à true
        if (m_Detect.IsDetected)
        {
            m_Light.SetActive(true);
            IsAlreadyActive = true;
            m_Interractible.IsActive = true;
            SoundManager.PlaySound3d(SoundManager.SoundEnum.LightDetector, position);

        }
        //Sinon si il n'est pas détécté ou qu'il n'est pas déja acif
        //Sa light n'est pas activé 
        //Et on passe le booléen de "m_Interractible" à false
        else if (m_Detect.IsDetected == false && IsAlreadyActive == false)
        {
            m_Light.SetActive(false);
            m_Interractible.IsActive = false;
        }
    }
}
