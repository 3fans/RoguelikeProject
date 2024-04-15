using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public enum EGamePhase
    {
        Unknown,
        MainMenu,
        MainLevel
    }
    public EGamePhase Phase {  get; private set; } = EGamePhase.Unknown;
    #region Instance Management
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadPersistentLevel()
    {
        const string LevelName = "PersistentLevel";

        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            if(SceneManager.GetSceneAt(sceneIndex).name == LevelName)
            { return; }
        }

        SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);
    }

    public static GameInstance Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Found duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(Instance);
    }
    #endregion

    public void OnEnterMainMenu()
    {
        Phase = EGamePhase.MainMenu;
    }
    public void OnExitMainMenu()
    {
        Phase = EGamePhase.MainLevel;
    }
}
