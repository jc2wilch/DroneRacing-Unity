using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0.0f, mouseX, 0.0f) * rotationSpeed;
        transform.Rotate(rotation);
    }
}
