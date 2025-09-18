using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameState_Paused : IGameState
{
    #region Static instance
    private static GameState_Paused _instance;

    public static GameState_Paused instance
    {
        get
        {
            if (_instance == null) { _instance = new GameState_Paused(); }
            return _instance;
        }
    }
    #endregion



    public void EnterState(GameStateManager gameStateManager)
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        UIManager.instance.UIPaused();
        CameraManager.instance.DisableCameraRotation();

        // Suscribe to input events
        InputManager.instance.TogglePauseEvent += HandleResume;
       


    }

    #region Events
    private void HandleResume()
    {        
        GameStateManager.instance.Resume();
        Debug.Log("Resume triggered in gameState_Paused");
    }






    #endregion


    public void FixedUpdateState(GameStateManager gameStateManager) {    }

    public void UpdateState(GameStateManager gameStateManager)
    {
        /*

        // Pressing ESC key will unpause the game, Switch to last state stored
        if(InputManager.instance.pauseTriggered)
        {
            GameStateManager.instance.UnPause();
            Debug.Log("Unpausing");
            InputManager.instance.pauseTriggered = false;
        }
        */
    }

    public void LateUpdateState(GameStateManager gameStateManager)  { }



    public void ExitState(GameStateManager gameStateManager)
    {
        Time.timeScale = 1f;
        CameraManager.instance.EnableCameraRotation();


        // Unsubscribe from input events
        //InputManager.instance.ResumeEvent -= HandleResume;
    }
}
