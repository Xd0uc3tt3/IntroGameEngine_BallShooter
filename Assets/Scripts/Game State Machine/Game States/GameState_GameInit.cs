using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameState_GameInit : IGameState
{
    // This state is used the first time the game is Initialized (launched/opened)
    // It will be used to set up all default settings
    // I realize this likley could be done in the MainMenu state as returning to it could count as a game reset of sorts... but it seems cleaner to have it's own state for this


    #region Static instance
    private static GameState_GameInit _instance;

    public static GameState_GameInit instance
    {
        get
        {
            if (_instance == null) { _instance = new GameState_GameInit(); }                
            return _instance;
        }
    }
    #endregion

    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = false;

        CameraManager.instance.UseMainMenuCamera();        

        // Enable Ball & AimGuide
        BallManager.instance.ball.SetActive(true);
        BallManager.instance.aimGuide.SetActive(true);

        // Enable all UI Panels
        UIManager.instance.gamePlayUI.SetActive(true);
        UIManager.instance.mainMenuUI.SetActive(true);
        UIManager.instance.levelCompleteUI.SetActive(true);
        UIManager.instance.levelFailedUI.SetActive(true);
        UIManager.instance.gameCompleteUI.SetActive(true);
        UIManager.instance.pauseMenuUI.SetActive(true);

        // Switch to MainMenu state
        GameStateManager.instance.SwitchToState(GameState_MainMenu.instance);




    }

    public void FixedUpdateState(GameStateManager gameStateManager) { }
    public void UpdateState(GameStateManager gameStateManager) { }
    public void LateUpdateState(GameStateManager gameStateManager) { }


    public void ExitState(GameStateManager gameStateManager) 
    {
        // Disable Ball & AimGuide
        BallManager.instance.ball.SetActive(false);
        BallManager.instance.aimGuide.SetActive(false);



        // Disable all UI Panels
        UIManager.instance.DisableAllUIPanels();
    }

}
