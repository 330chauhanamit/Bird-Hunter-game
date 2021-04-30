using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcontrol : MonoBehaviour
{
    public Transform tracking;
    float smoothspeed = 0.125f;
    public Vector3 offset;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredposition = tracking.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;


    }
}
