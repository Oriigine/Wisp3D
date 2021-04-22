using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public GameObject light;
    public DetectionBehaviour detect;
    private bool IsAlreadyActive = false;

    void Update()
    {
        if (detect.IsDetected)
        {
            light.SetActive(true);
            IsAlreadyActive = true;

        }
        else if (detect.IsDetected == false && IsAlreadyActive == false)
        {
            light.SetActive(false);
        }
    }
}
