using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bis_Mondhor_PlayerController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 0f;

    [SerializeField]
    private float VerticalSpeed = 0f;

    [SerializeField]
    private float MaxSpeed = 0.1f;

    [SerializeField]
    private float Acceleration = 1f;

    [SerializeField]
    private float Deceleration = 1f;

    private bool normalized = false;

    Rigidbody m_Rigidbody;

    public float GetAxisH = 0;
    public float GetAxisV = 0;

    //Les booléens indiquant si on a appuyé sur les inputs horizontaux
    private bool D_Pressed;
    private bool Q_Pressed;

    //Pareil mais pour les inputs verticaux
    private bool Z_Pressed;
    private bool S_Pressed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collision")
        {
            HorizontalSpeed = 0;
            VerticalSpeed = 0;
        }
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(HorizontalSpeed > MaxSpeed)
        {
            HorizontalSpeed = MaxSpeed;
        }

        if(HorizontalSpeed < MaxSpeed * -1)
        {
            HorizontalSpeed = MaxSpeed * -1;
        }



        //Mémoire tampon du GetAxis
        if(Input.GetAxis("Horizontal") > 0.1)
        {
            GetAxisH = 1;
        }
        else if (Input.GetAxis("Horizontal") < -0.1)
        {
            GetAxisH = -1;
        }
        else if (Input.GetAxis("Horizontal") < 0.1 && Input.GetAxis("Horizontal") > -0.1)
        {
            GetAxisH = 0;
        }


        if (Input.GetAxis("Vertical") > 0.1)
        {
            GetAxisV = 1;
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            GetAxisV = -1;
        }
        else if (Input.GetAxis("Vertical") < 0.1 && Input.GetAxis("Vertical") > -0.1)
        {
            GetAxisV = 0;
        }



        #region DETECTION TOUCHES

        if (GetAxisH == 1)
        {
            D_Pressed = true;
        }

        if (GetAxisH == -1)
        {
            Q_Pressed = true;
        }

        if (D_Pressed == true && GetAxisH == 0)
        {
            D_Pressed = false;
        }

        if (Q_Pressed == true && GetAxisH == 0)
        {
            Q_Pressed = false;
        }



        if (GetAxisV == 1)
        {
            Z_Pressed = true;
        }

        if (GetAxisV == -1)
        {
            S_Pressed = true;
        }

        if (Z_Pressed == true && GetAxisV == 0)
        {
            Z_Pressed = false;
        }

        if (S_Pressed == true && GetAxisV == 0)
        {
            S_Pressed = false;
        }

        #endregion DETECTION TOUCHES



        #region TOUCHES PRESSEES

        if (D_Pressed == true)
        {
            if (HorizontalSpeed < Input.GetAxis("Horizontal") * MaxSpeed)
            {       
                HorizontalSpeed += Acceleration * Time.deltaTime;
            }
            m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);
        }

        if (Q_Pressed == true)
        {
            if (HorizontalSpeed > Input.GetAxis("Horizontal") * MaxSpeed)
            {
                HorizontalSpeed -= Acceleration * Time.deltaTime;
            }
            m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);
        }



        if (Z_Pressed == true)
        {
            if (VerticalSpeed < Input.GetAxis("Vertical") * MaxSpeed)
            {
                VerticalSpeed += Acceleration * Time.deltaTime;
            }
            m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);
        }

        if (S_Pressed == true)
        {
            if (VerticalSpeed > Input.GetAxis("Vertical") * MaxSpeed)
            {
                VerticalSpeed -= Acceleration * Time.deltaTime;
            }
            m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);
        }
        #endregion TOUCHES PRESSEES

        #region TOUCHES RELACHEES
        if (D_Pressed == false && HorizontalSpeed > 0)
        {            
            HorizontalSpeed -= Deceleration * Time.deltaTime;
        }
        m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);

        if (Q_Pressed == false && HorizontalSpeed < 0)
        {
            HorizontalSpeed += Deceleration * Time.deltaTime;
        }
        m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);

        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed >= -0.2 && HorizontalSpeed <= 0.2)
        {
            HorizontalSpeed = 0;
        }



        if (Z_Pressed == false && VerticalSpeed > 0)
        {
            VerticalSpeed -= Deceleration * Time.deltaTime;
        }
        m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);

        if (S_Pressed == false && VerticalSpeed < 0)
        {
            VerticalSpeed += Deceleration * Time.deltaTime;
        }
        m_Rigidbody.velocity = new Vector3(HorizontalSpeed, VerticalSpeed, 0);

        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed >= -0.2 && VerticalSpeed <= 0.2)
        {
            VerticalSpeed = 0;
        }

        #endregion TOUCHES RELACHEES
    }

}
