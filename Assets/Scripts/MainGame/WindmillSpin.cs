using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
    // The axis around which the object should spin
    public Vector3 spinAxis = Vector3.right;

    // The speed at which the object should spin, in degrees per second
    public float spinSpeed = 360f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the spin axis by the specified amount
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);
    }
}
