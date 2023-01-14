using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public Transform drone;
    public Vector3 offset;
    public float moveSpeed;

    public CanvasGroup customizeCanvas;
    public float fadeInSpeed;
    public float fadeInDelay;


    private float currentAlpha;
    private float timer;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        timer = fadeInDelay;
    }

    void Update()
    {
        if (mainMenu.alpha == 0)
        {
            Vector3 targetPosition = drone.position + offset; // calculate the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime); // interpolate the position towards the target position

            Quaternion targetRotation = Quaternion.LookRotation(drone.position - transform.position, Vector3.up); // calculate the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime); // interpolate the rotation towards the target rotation

            if (customizeCanvas.enabled == false)
            {
                customizeCanvas.enabled = true;
            }

            timer -= Time.deltaTime; // decrement the timer
            if (timer <= 0) // if the timer has reached zero
            {
                currentAlpha = Mathf.Lerp(currentAlpha, 1.0f, fadeInSpeed * Time.deltaTime); // interpolate the current alpha towards 1
                customizeCanvas.alpha = currentAlpha; // set the canvas group's alpha to the current alpha
            }
        }
    }
}
