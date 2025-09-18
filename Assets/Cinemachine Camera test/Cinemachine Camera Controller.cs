using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomSpeed = 0.1f;

    [SerializeField] private InputManager _inputManager;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineCollider cameraCollider;

    private float minDistance;
    private float maxDistance;


   


    // Start is called before the first frame update
    void Start()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        cameraCollider = virtualCamera.GetComponent<CinemachineCollider>();

        // Get the min and max distances from the Cinemachine Collider
        if (cameraCollider != null)
        {
            minDistance = cameraCollider.m_MinimumDistanceFromTarget;
            maxDistance = cameraCollider.m_DistanceLimit;
        }
        else
        {
            Debug.LogWarning("CinemachineCollider not found on the virtual camera.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        /*

        // Access the cameraZoomInput from the InputManager
        //float zoomInput = _inputManager.cameraZoomInput;
        //Debug.Log(zoomInput);

        
            // Adjust the camera distance based on scroll input
            float newDistance = framingTransposer.m_CameraDistance - zoomInput * zoomSpeed;

            // Clamp the new distance within the min and max limits
            newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);

            // Set the new camera distance
            framingTransposer.m_CameraDistance = newDistance;

        */
        
    }
}

