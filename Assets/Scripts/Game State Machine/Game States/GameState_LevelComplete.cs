using UnityEngine;
using UnityEngine.SceneManagement;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)


public class GameState_LevelComplete : IGameState
{
    #region Static instance
    private static GameState_LevelComplete _instance;

    public static GameState_LevelComplete instance
    {
        get
        {
            if (_instance == null) { _instance = new GameState_LevelComplete(); }
            return _instance;
        }
    }
    #endregion


    CameraManager _cameraManager; 
    UIManager _uiManager; 

    void awake()
    {
        _cameraManager = CameraManager.instance;
        _uiManager = UIManager.instance;
    }


    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        // TODO: create a method that slows down time duing the Level complete state
        // perhaps a coroutine that gradually lerps to a minimum time scale.


        // Lock the camera's rotation to prevent further movement.
        CameraManager.instance.DisableCameraRotation();

        // Check if the current level is the last level by comparing the build index 
        // of the active scene with the total number of scenes in the build settings.
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // If this is the last level, show the "Game Complete" UI.
            UIManager.instance.UIGameComplete();
        }
        else
        {
            // If this is not the last level, show the "Level Complete" UI.
            UIManager.instance.UILevelComplete();
        }
    }


    public void FixedUpdateState(GameStateManager gameStateManager) { }

    public void UpdateState(GameStateManager gameStateManager) { }

    public void LateUpdateState(GameStateManager gameStateManager) { }

    public void ExitState(GameStateManager gameStateManager)
    {
        // Unlock the camera's rotation to allow movement in the next state.  **** shouldnt we just do this in the enter state of the next state?
        CameraManager.instance.EnableCameraRotation();
    }
}