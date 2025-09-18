using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class GameState_Credits : IGameState
{
    #region Static instance
    private static GameState_Rolling _instance;

    public static GameState_Rolling instance
    {
        get
        {
            if (_instance == null) { _instance = new GameState_Rolling(); }                
            return _instance;
        }
    }
    #endregion


    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = false;

        // maybe wrap the next 2 in an if statement to check if the last state was paused so we can set everyting back to whats needed for this state.        
        BallManager.instance.aimGuide.SetActive(false);    // AimGuide should already be deactivated coming into this state, this is just a redundancy

        //Redundancies
        CameraManager.instance.UseGameplayCamera();        // cameraManager should already be set to gameplay camera coming into this state, this is just a redundancy
        CameraManager.instance.EnableCameraRotation();     // cameraRotation should already be unlocked coming into this state, this is just a redundancy
        
        UIManager.instance.UIGamePlay();                   // shares same UI as AimState, should already be active coming into this state, this is just a redundancy

        UIManager.instance.modeText.text = "Wait for ball to stop";

        // Start the coroutine to check if the ball has stopped after a delay
        BallManager.instance.StartCoroutine(BallManager.instance.CheckBallStoppedAfterDelay());

        // Subscribe to Input Events       
        InputManager.instance.TogglePauseEvent += HandlePause;  
    }

    #region Input Events
    private void HandlePause()
    {
        GameStateManager.instance.Pause();
    }
    #endregion

    public void FixedUpdateState(GameStateManager gameStateManager) { }

    public void UpdateState(GameStateManager gameStateManager)
    {
        // The ballManager will watch for the ball to stop rolling and then set the ballStopped bool to true

        // rather than a bool... maybe just have this be a method thats called from the ball manager once the ball has stopped and it's time to check if the level is complete or not.
        if (BallManager.instance.ballStopped == true)
        {
            Debug.Log("Ball has stopped");

            if (GameManager.instance.shotsLeft > 0)
            {
                // this means the goal has not been reached and there are still shots left
                // thought... would it make sense to have a bool for LevelComplete when the goal is reached that could be checked against here? It may not be needed as the logic already in place should interrupt this state before it gets here.
                GameStateManager.instance.SwitchToState(GameState_Aim.instance);
            }
            else if (GameManager.instance.shotsLeft <= 0)
            {
                //you failed the level you fool... trigger the level failed state
                GameStateManager.instance.SwitchToState(GameState_LevelFailed.instance);
            }
        }    
    }

    public void LateUpdateState(GameStateManager gameStateManager) { }

    public void ExitState(GameStateManager gameStateManager)
    {
        // Unsubscribe from Input Events        
        InputManager.instance.TogglePauseEvent -= HandlePause;
    }



}
