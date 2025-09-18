using UnityEngine;
public class GameState_LevelFailed : IGameState
{
    // Note: LevelCompleteState and LevelFailedState likely could be using the same UIPanel (UI_Results) 
    // we can use can then feed the data into the UI panel based on the state that is active
    // This may require enabling/disabling some buttons for different logic?

    #region Static instance
    private static GameState_LevelFailed _instance;

    public static GameState_LevelFailed instance
    {
        get
        {
            if (_instance == null) { _instance = new GameState_LevelFailed(); }
            return _instance;
        }
    }
    #endregion



    public void EnterState(GameStateManager gameStateManager)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        UIManager.instance.UILevelFailed();
        CameraManager.instance.DisableCameraRotation();
    }

    public void FixedUpdateState(GameStateManager gameStateManager) { }

    public void UpdateState(GameStateManager gameStateManager) { }

    public void LateUpdateState(GameStateManager gameStateManager) { }

    public void ExitState(GameStateManager gameStateManager)
    {
        CameraManager.instance.EnableCameraRotation();
    }
}
