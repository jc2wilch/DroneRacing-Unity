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
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, moveSpeed * Time.deltaTime);
        }

    }
}
