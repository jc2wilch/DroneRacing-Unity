using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
    // axis the object should spin around
    public Vector3 spinAxis = Vector3.right;

    // speed that the obj spins: deg/sec
    public float spinSpeed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);
    }
}
