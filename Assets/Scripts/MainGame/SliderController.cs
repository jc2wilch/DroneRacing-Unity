using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    // The distance to slide the object
    public float distance;

    // The speed at which the object should slide
    public float speed;

    // The initial position of the object
    private Vector3 startPosition;

    // Whether the object is currently sliding up or down
    private bool isSlidingUp;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // If the object is currently sliding up, increment its position by the speed times Time.deltaTime
        if (isSlidingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        // If the object is currently sliding down, decrement its position by the speed times Time.deltaTime
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }

        // If the object's current position is greater than or equal to its start position plus the distance, set isSlidingUp to false
        if (transform.position.y >= startPosition.y + distance)
        {
            isSlidingUp = false;
        }
        // If the object's current position is less than or equal to its start position, set isSlidingUp to true
        else if (transform.position.y <= startPosition.y - distance)
        {
            isSlidingUp = true;
        }

    }
}
