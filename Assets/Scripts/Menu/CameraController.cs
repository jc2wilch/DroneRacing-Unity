using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public GameObject mainCanvVisible;
    public Button bodyColorButton;
    public Button fanColorButton;
    public Transform drone;
    public Vector3 offset;
    public float moveSpeed;

    public CanvasGroup customizeCanvas;
    public GameObject customCanvVisible;
    public float fadeInSpeed;
    public float fadeInDelay;


    private float currentAlpha;
    private float timer;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        timer = fadeInDelay;
    }

    void Update()
    {
        if (!mainMenu.gameObject.activeInHierarchy)
        {
            Vector3 targetPosition = drone.position + offset; // calculate the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime); // interpolate the position towards the target position

            Quaternion targetRotation = Quaternion.LookRotation(drone.position - transform.position, Vector3.up); // calculate the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime); // interpolate the rotation towards the target rotation
            
            
            
            /*
            if (customizeCanvas.gameObject.activeInHierarchy == false)
            {
                customizeCanvas.gameObject.SetActive(true);
                bodyColorButton.gameObject.SetActive(true);
                fanColorButton.gameObject.SetActive(true);
            }
            
            timer -= Time.deltaTime; // decrement the timer
            if (timer <= 0) // if the timer has reached zero
            {
                currentAlpha = Mathf.Lerp(currentAlpha, 1.0f, fadeInSpeed * Time.deltaTime); // interpolate the current alpha towards 1
                customizeCanvas.interactable = true;
                customizeCanvas.alpha = currentAlpha; // set the canvas group's alpha to the current alpha
            }
            */
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, moveSpeed * Time.deltaTime);
        }

    }
}
