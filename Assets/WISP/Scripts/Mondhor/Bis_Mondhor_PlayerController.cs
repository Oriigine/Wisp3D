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
    private float MaxSpeed = 0.5f;

    [SerializeField]
    private float Acceleration = 1f;

    [SerializeField]
    private float Deceleration = 2f;

    private void Update()
    {

        HorizontalSpeed = Input.GetAxis("Horizontal");

    }

}
