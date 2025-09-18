using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameManager : MonoBehaviour
{
    #region Static instance
    // Public static property to access the singleton instance of GameStateManager
    public static GameManager instance
    {
        get
        {
            // If the instance is not set, instantiate a new one from resources
            if (_instance == null)
            {
                // Loads the GameStateManager prefab from Resources folder and instantiates it
                var go = (GameObject)GameObject.Instantiate(Resources.Load("GameManager"));
            }
            // Return the current instance (if set) or the newly instantiated instance
            return _instance;
        }
        // Private setter to prevent external modification of the instance
        private set { }
    }
    // Private static variable to hold the singleton instance of GameStateManager
    private static GameManager _instance;
    #endregion



    // with the refactor GameManager Doesn't really manager anything anymore...
    // it does hold the ShotsLeft varialble but thats it..


    [Header("Gameplay Info")]
    public int shotsLeft = 0;

    [Header("Per Level Info")]
    public LevelInfo _levelInfo;
    public GameObject startPosition;


    [Header("Script References")]
    public GameStateManager _gameStateManager;
    public BallManager _ballManager;
    public LevelManager _levelManager;
    public UIManager _uIManager;
    public CameraManager _cameraManager;

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


        // Contribution: Daniel Nascimento
        // If any of the managers are null, attempt to assign them with children of this object.
        _gameStateManager ??= GetComponentInChildren<GameStateManager>();
        _ballManager ??= GetComponentInChildren<BallManager>();
        _levelManager ??= GetComponentInChildren<LevelManager>();
        _uIManager ??= GetComponentInChildren<UIManager>();
        _cameraManager ??= GetComponentInChildren<CameraManager>();
    }







}
