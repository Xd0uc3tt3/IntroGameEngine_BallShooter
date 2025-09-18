using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, GameInput.IGameplayActions
{
    #region Static instance
    // Public static property to access the singleton instance of GameStateManager
    public static InputManager instance
    {
        get
        {
            // If the instance is not set, instantiate a new one from resources
            if (_instance == null)
            {
                // Loads the GameStateManager prefab from Resources folder and instantiates it
                var go = (GameObject)GameObject.Instantiate(Resources.Load("InputManager"));
            }
            // Return the current instance (if set) or the newly instantiated instance
            return _instance;
        }
        // Private setter to prevent external modification of the instance
        private set { }
    }
    // Private static variable to hold the singleton instance of GameStateManager
    private static InputManager _instance;
    #endregion

    public GameInput gameInput;

    private void Awake()
    {
        // Initialize the GameInput instance
        if (gameInput == null)
        {
            gameInput = new GameInput();
        }

        // Register this class to receive input callbacks
        gameInput.Gameplay.SetCallbacks(this);

        #region Singleton Pattern
        // If there is already an instance, and it's not me, delete myself.
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion        
    }



    private void OnEnable()
    {
        // Enable the input system
        gameInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        // Disable the input system when the object is disabled
        gameInput.Gameplay.Disable();
    }




    #region Input Events    

    public event Action<Vector2> CameraOrbitEvent;

    public event Action ShootBallEvent;

    public event Action TogglePauseEvent;
    //public event Action ResumeEvent;

    public event Action <Vector2> CameraZoomEvent;

    #endregion



    public void OnCameraOrbit(InputAction.CallbackContext context)
    {        
         Debug.Log("");
         CameraOrbitEvent?.Invoke(context.ReadValue<Vector2>());       
    }

    public void OnShootBall(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("");
            ShootBallEvent?.Invoke();
        }
    }

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("");
            TogglePauseEvent?.Invoke();
        }
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        CameraZoomEvent?.Invoke(context.ReadValue<Vector2>());
    }






    /*

    // don't really need to feed this as the cameraOrbit input is read direcly by
    // the "Cinemachine Input Provider" component on the Gameplay Virtual Camera
    public void OnCameraOrbit(InputAction.CallbackContext context)
    {
        CameraOrbitEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShootBall(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            ShootBallEvent?.Invoke();
            Debug.Log("Shoot Ball");
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetActionMap_UI();
        }
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        
    }


    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetActionMap_Gameplay();
        }
    }

    */
}
