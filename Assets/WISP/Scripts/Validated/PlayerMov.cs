using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov: MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody m_Rb;

    Vector3 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
    }

    private void FixedUpdate()
    {
        m_Rb.MovePosition(m_Rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

