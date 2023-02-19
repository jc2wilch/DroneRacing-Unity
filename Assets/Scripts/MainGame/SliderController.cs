using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    // distance to slide
    public float distance;

    // sliding speed
    public float speed;

    // initial position
    private Vector3 startPosition;

    // if object is currently sliding up or down
    private bool isSlidingUp;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isSlidingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }

        if (transform.position.y >= startPosition.y + distance)
        {
            isSlidingUp = false;
        }

        else if (transform.position.y <= startPosition.y - distance)
        {
            isSlidingUp = true;
        }

    }
}
