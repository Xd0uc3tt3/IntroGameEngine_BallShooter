using UnityEngine;
using Cinemachine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

// Potential better way to control the Camera Rotation https://smontambault.medium.com/unity-cinemachine-with-new-input-system-b608e13997c7



public class CameraManager : MonoBehaviour
{
    #region Static instance
    // Public static property to access the singleton instance of GameStateManager
    public static CameraManager instance
    {
        get
        {
            // If the instance is not set, instantiate a new one from resources
            if (_instance == null)
            {
                // Loads the GameStateManager prefab from Resources folder and instantiates it
                var go = (GameObject)GameObject.Instantiate(Resources.Load("GameStateManager"));
            }
            // Return the current instance (if set) or the newly instantiated instance
            return _instance;
        }
        // Private setter to prevent external modification of the instance
        private set { }
    }
    // Private static variable to hold the singleton instance of GameStateManager
    private static CameraManager _instance;
    #endregion


    public GameObject VCamGameplay;
    public GameObject VCamMainMenu;

    public CinemachineBrain cinemachineBrain;

    public CinemachineFreeLook freeLookCamera;
    public Transform lookAtTarget;

    public Vector3 cameraOffset = new Vector3(0, 5, -10);



    private void Awake()
    {
        #region Singleton Pattern
        // If there is an instance, and it's not me, delete myself.
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion
    }


    public void UseMainMenuCamera()
    {
        VCamGameplay.SetActive(false);
        VCamMainMenu.SetActive(true);
    }

    public void UseGameplayCamera()
    {
        VCamGameplay.SetActive(true);
        VCamMainMenu.SetActive(false);
    }

    public void DisableCameraRotation()
    {
        VCamGameplay.SetActive(false); }

    public void EnableCameraRotation()
    {
        VCamGameplay.SetActive(true);
    }

    // set gameplay camera to a target orientation
    public void SetGameplayCameraOrientation(Vector3 targetOrientation)
    {
        VCamGameplay.transform.LookAt(targetOrientation);
    }


   
    public void ResetCameraPosition()
    {
        var offset = freeLookCamera.LookAt.rotation * new Vector3(0, 0, -14);

        freeLookCamera.ForceCameraPosition(freeLookCamera.LookAt.position + offset, freeLookCamera.LookAt.rotation);
        freeLookCamera.m_YAxis.Value = 0.5f;
    }





}
