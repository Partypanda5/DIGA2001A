using System.Collections;
using UnityEngine;

public class UIJFScriptTwo : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 1f;
    private bool isRotating = false;    // Track if the camera is currently rotating
    //public GameObject[] UIElements;     // UI elements to toggle
    //public GameObject initialButton;    // Initial button to disable after rotation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCameraLeftBy90Degrees();
    }

   
    // Method to rotate the camera left by 90 degrees
    public void RotateCameraLeftBy90Degrees()
    {
        if (!isRotating) // Prevent triggering multiple rotations simultaneously
        {
            StartCoroutine(RotateCameraCoroutine(90f));
        }
    }

    // Coroutine to smoothly rotate the camera
    private IEnumerator RotateCameraCoroutine(float angle)
    {
        isRotating = true;

        Quaternion startRotation = mainCamera.transform.rotation;                       // Initial rotation
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -angle, 0);       // Target rotation

        float rotationProgress = 0f;
        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / angle);               // Normalize the rotation speed
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress); // Smoothly interpolate
            yield return null;
        }

        mainCamera.transform.rotation = endRotation; // Ensure exact final rotation
        isRotating = false;

        //initialButton.SetActive(false);

        /*foreach (GameObject UIElement in UIElements)
        {
            UIElement.SetActive(true);
        }*/
    }
}
