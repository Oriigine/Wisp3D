using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov: MonoBehaviour
{

    private float XLeft, XRight, YUp, YDown;
    public float moveSpeed = 5f;

    public Rigidbody m_Rb;

    Vector3 movement;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        //Debug.Log(XLeft);
        //Debug.Log(XRight);
        //Debug.Log(YDown);
        //Debug.Log(YUp);
    }

    private void FixedUpdate()
    {
        m_Rb.MovePosition(m_Rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    XLeft = Time.fixedDeltaTime;
        //    XRight = 0;
        //    YUp = 0;
        //    YDown = 0;
        //}
        //else
        //{
        //    XLeft = 0;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    XRight += Time.fixedDeltaTime;
        //    YUp = 0;
        //    YDown = 0;
        //    XLeft = 0;
        //}
        //else
        //{
        //    XRight = 0;
        //}

        //if (Input.GetKey(KeyCode.Z))
        //{
        //    YUp += Time.fixedDeltaTime;
        //    YDown = 0;
        //    XLeft = 0;
        //    XRight = 0;
        //}
        //else
        //{
        //    YUp = 0;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    YDown += Time.fixedDeltaTime;
        //    YUp = 0;
        //    XRight = 0;
        //    XLeft = 0;
        //}
        //else
        //{
        //    YDown = 0;
        //}

        //if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        //{
        //    YDown = Time.fixedDeltaTime;
        //    YUp = 0;
        //    XRight = Time.fixedDeltaTime;
        //    XLeft = 0;
        //}

        //if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Q))
        //{
        //    YDown = Time.fixedDeltaTime;
        //    YUp = 0;
        //    XRight = 0;
        //    XLeft = Time.fixedDeltaTime;
        //}

        //if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Q))
        //{
        //    YDown = 0;
        //    YUp = Time.fixedDeltaTime;
        //    XRight = 0;
        //    XLeft = Time.fixedDeltaTime;
        //}

        //if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.D))
        //{
        //    YDown = 0;
        //    YUp = Time.fixedDeltaTime;
        //    XRight = Time.fixedDeltaTime;
        //    XLeft = 0;
        //}

        //float x = Input.GetAxis ("Horizontal") * Time.fixedDeltaTime;
        //float y = Input.GetAxis("Vertical") * Time.fixedDeltaTime;

    //    if (XLeft != 0 && Input.GetKey(KeyCode.Q))
    //    {
    //        m_Rb.AddForce(Vector2.left * XLeft * moveSpeed);
    //    }
     
    //    if (XRight != 0 && Input.GetKey(KeyCode.D))
    //    {
    //        m_Rb.AddForce(Vector2.right * XRight * moveSpeed);
    //    }
     
    //    if (YUp != 0 && Input.GetKey(KeyCode.Z))
    //    {
    //        m_Rb.AddForce(Vector2.up * YUp * moveSpeed);
    //    }
        
    //    if (YDown != 0 && Input.GetKey(KeyCode.S))
    //    {
    //        m_Rb.AddForce(Vector2.down * YDown * moveSpeed);
    //    }
      

    //    if (XLeft >= 0.02f)
    //    {
    //        XLeft = 0.02f;
    //    }


    //    if (XRight >= 0.02f)
    //    {
    //        XRight = 0.02f;
    //    }



    //    if (YUp >= 0.02f)
    //    {
    //        YUp = 0.02f;
    //    }


    //    if (YDown >= 0.02f)
    //    {
    //        YDown = 0.02f;
    //    }



    }
}

