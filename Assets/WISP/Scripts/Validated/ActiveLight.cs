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
        //Si l'objet se fait d�t�cter alors on allume sa light
        //On dit qu'il est actif
        //Et on passe son bool�en de son "m_Interractible" � true
        if (m_Detect.IsDetected)
        {
            m_Light.SetActive(true);
            IsAlreadyActive = true;
            m_Interractible.IsActive = true;
            SoundManager.PlaySound3d(SoundManager.SoundEnum.LightDetector, position);

        }
        //Sinon si il n'est pas d�t�ct� ou qu'il n'est pas d�ja acif
        //Sa light n'est pas activ� 
        //Et on passe le bool�en de "m_Interractible" � false
        else if (m_Detect.IsDetected == false && IsAlreadyActive == false)
        {
            m_Light.SetActive(false);
            m_Interractible.IsActive = false;
        }
    }
}
