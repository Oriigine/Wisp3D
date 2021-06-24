using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLightTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject m_LightToActivate = null;

    public Vector3 position;

    public ParticleSystem LanternTriggerParticle;

    [SerializeField]
    private Interractible m_Interact;

    private void Start()
    {
        //On set la light a �teinte de base
        m_Interact = GetComponent<Interractible>();
        if(m_Interact.IsActive == true)
        {
            m_LightToActivate.SetActive(true);
        }
        else if(m_Interact.IsActive == false)
        {
            m_LightToActivate.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider l_Trigger)
    {
        //Si l'objet n'a pas �t� activ�
        if (m_Interact.IsActive == false && m_Interact != null)
        {
            //On l'active
            m_Interact.IsActive = true;
            //On active la light qu'on veut activer
            m_LightToActivate.SetActive(true);
            SoundManager.PlaySound3d(SoundManager.SoundEnum.LanternTrigger, position);
            Instantiate(LanternTriggerParticle);
            LanternTriggerParticle.Play();


        }
    }
    //private void Update()
    //{
    //    if (m_Interact.IsActive)
    //    {
    //        SoundManager.PlaySound(SoundManager.SoundEnum.LanternBurning);
    //    }
    //}


    //public bool LightActivaded
    //{
    //    get { return m_IsActive; }
    //    set { m_IsActive = value; }
    //}

}
