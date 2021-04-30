using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Control controller = other.gameObject.GetComponent<Control>();
        if (controller != null)
        {
            controller.healthchange(-10);

            Debug.Log("Current Health - " + controller.healthnow);
        }
    }
}
