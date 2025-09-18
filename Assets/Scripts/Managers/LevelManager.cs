using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class LevelManager : MonoBehaviour
{
    #region Static instance
    // Public static property to access the singleton instance of GameStateManager
    public static LevelManager instance
    {
        get
        {
            // If the instance is not set, instantiate a new one from resources
            if (_instance == null)
            {
                // Loads the GameStateManager prefab from Resources folder and instantiates it
                var go = (GameObject)GameObject.Instantiate(Resources.Load("LevelManager"));
            }
            // Return the current instance (if set) or the newly instantiated instance
            return _instance;
        }
        // Private setter to prevent external modification of the instance
        private set { }
    }
    // Private static variable to hold the singleton instance of GameStateManager
    private static LevelManager _instance;
    #endregion


    public int nextScene;
    //private int currentScene;

    public void Awake()
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


    public void LoadNextLevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene <= SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(nextScene);            
        }

        else if (nextScene > SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("All levels complete!");
        }
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneId);

        
        if (sceneId == 0) // Loading Main Menu
        {            
            GameStateManager.instance.SwitchToState(GameState_MainMenu.instance);
        }
        else // Gameplay level
        {
            GameStateManager.instance.SwitchToState(GameState_Aim.instance);
        }
    }

    public void LoadMainMenuScene()
    {
        LoadScene(0);
        GameStateManager.instance.SwitchToState(GameState_GameInit.instance);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameStateManager.instance.SwitchToState(GameState_Aim.instance);

        // this corrects an issue when scene is reloaded, input stops responding... re-initializes the Input Map?
        // InputManager.instance.SetActionMap_Gameplay();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int LevelCount = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Scene Loaded: " + scene.name + " Build Index: " + scene.buildIndex);

        if (scene.buildIndex > 0)
        {
            // Get a reference to the level info Script for that level
            LevelInfo _levelInfo = FindObjectOfType<LevelInfo>();

            // Get the # shots(attempts) available for the level and update the UI
            GameManager.instance.shotsLeft = _levelInfo.ShotsToComplete;
            UIManager.instance.UpdateShotsleft(_levelInfo.ShotsToComplete);

            // Update the current level # on the UI
            UIManager.instance.UpdateLevelCount(LevelCount);

            // Set the ball to the current level start position           
            BallManager.instance.SetBallToStartPosition();
            //Debug.Break();

            // Set the camera to the current level start position
            CameraManager.instance.ResetCameraPosition();
        }

        else if (scene.buildIndex == 0)
        {
            // Noting really needed here, buildIndex 0 = MainMenu scene,
            // Which would be loaded with via 'LoadMainMenuScene' which also switches to the GameInitState which handles all the prep/resetting for the MainMenu.
            // Leaving this here in case of debugging or future use.
        }
        // (Unsuscribe) Stop listening for sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}



