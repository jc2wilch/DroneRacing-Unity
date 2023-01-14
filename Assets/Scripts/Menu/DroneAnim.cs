using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DroneAnim : MonoBehaviour
{
    public float thrust = 10.0f;
    public float rotationSpeed = 1f;
    public float liftDuration = 3.0f;
    public float liftForce = 10.0f;

    private bool inAction = false;
    private int randomNumber = -1;
    private float timer = 0.0f;
    List<int> usedNums = new List<int> { };

    private float duration = 3.0f;
    private float waitTime = 4.0f;
    private float elapsedTime = 0.0f;

    private Quaternion startRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    private Quaternion leftMidRotation = Quaternion.Euler(0.0f, 0.0f, 7.5f);
    private Quaternion rightMidRotation = Quaternion.Euler(0.0f, 0.0f, -15f);
    private Quaternion endRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    private float startHeight = 120f;

    private void Start()
    {
        startHeight = transform.position.y;
    }

    private void moveUp()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * liftForce);
    }

    private void moveDown()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * (-liftForce));
    }

    private void moveLeft()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * (-thrust));

        if (elapsedTime <= duration)
        {
            Quaternion targetRotation = endRotation;
            if (elapsedTime < (duration / 2))
            {
                targetRotation = Quaternion.Slerp(transform.rotation, leftMidRotation, Time.fixedDeltaTime);
            }
            else
            {
                if (elapsedTime < (duration / 2) + 0.015)
                {
                    leftMidRotation = transform.rotation;
                }
                targetRotation = Quaternion.Slerp(leftMidRotation, endRotation, (elapsedTime - (duration / 2)) / (duration / 2));
            }
            transform.rotation = targetRotation;  // apply the interpolated rotation to the object
            elapsedTime += Time.fixedDeltaTime;
        }
        if (elapsedTime > duration)
        {
            transform.rotation = endRotation;  // set the final rotation
            elapsedTime = 0.0f;
        }
    }

    private void moveRight()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * thrust);

        if (elapsedTime <= duration)
        {
            Quaternion targetRotation = endRotation;
            if (elapsedTime < (duration / 2))
            {
                targetRotation = Quaternion.Slerp(transform.rotation, rightMidRotation, Time.fixedDeltaTime);
            }
            else
            {
                if (elapsedTime < (duration / 2) + 0.015)
                {
                    rightMidRotation = transform.rotation;
                }
                targetRotation = Quaternion.Slerp(rightMidRotation, endRotation, (elapsedTime - (duration / 2)) / (duration / 2));
            }
            transform.rotation = targetRotation;  // apply the interpolated rotation to the object
            elapsedTime += Time.fixedDeltaTime;
        }
        if (elapsedTime > duration)
        {
            transform.rotation = endRotation;  // set the final rotation
            elapsedTime = 0.0f;
        }
    }

    private void spin()
    {
        if (elapsedTime <= duration)
        {
            // Calculate the rotation amount based on the elapsed time
            float rotationAmount = 360.0f * (elapsedTime / duration);

            // Rotate the object by the calculated amount
            transform.rotation = Quaternion.Euler(0.0f, rotationAmount, 0.0f);

            // Increment the elapsed time
            elapsedTime += Time.fixedDeltaTime;
        }
        if (elapsedTime > duration)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            elapsedTime = 0.0f;
        }
    }

    void FixedUpdate()
    {
        if (!inAction)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= waitTime)
            {
                inAction = true;
                timer = 0.0f;
                randomNumber = Random.Range(0, 5);
                
                if (usedNums.Count > 0 && usedNums.Count < 5)
                {
                    bool found = false;
                    do
                    {
                        foreach (int num in usedNums)
                        {
                            found = false;
                            if (randomNumber == num)
                            {
                                randomNumber = Random.Range(0, 5);
                                found = true;
                                break;
                            }
                        }
                    }
                    while (found);
                    usedNums.Add(randomNumber);
                }
                else if (usedNums.Count >= 5)
                {
                    usedNums.Clear();
                    randomNumber = Random.Range(0, 5);
                    usedNums.Add(randomNumber);
                }
                else if (usedNums.Count == 0)
                {
                    randomNumber = Random.Range(0, 5);
                    usedNums.Add(randomNumber);
                }
            }
        }
        else
        {
            timer += Time.fixedDeltaTime;
            if (timer >= duration)
            {
                inAction = false;
                timer = 0.0f;
            }

            if (randomNumber == 0)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                moveUp();
            }
            else if (randomNumber == 1)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                moveDown();
            }
            else if (randomNumber == 2)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
                moveLeft();
            }
            else if (randomNumber == 3)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
                moveRight();
            }
            else if (randomNumber == 4)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                spin();
            }
        }
    }
}
