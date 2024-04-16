using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public struct PlayerStats
    {
        public float maxHealth;
        public float shootCooldown;
        public float bombCooldown;
        public float shootDamage;
        public float bombDamage;

        public PlayerStats(float mh, float sc, float bc, float sd, float bd)
        {
            maxHealth = mh;
            shootCooldown = sc;
            bombCooldown = bc;
            shootDamage = sd;
            bombDamage = bd;
        }

    }
    public enum EGamePhase
    {
        Unknown,
        MainMenu,
        Lobby,
        MainLevel
    }
    public static PlayerStats DefaultPlayerStats = new PlayerStats(2,0.8f,1.5f,2,4);
    public PlayerStats CurrentPlayerStats = DefaultPlayerStats;
    
        
    
    public EGamePhase Phase {  get; private set; } = EGamePhase.Unknown;
    public int LevelNumber { get; private set; } = 1;


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
        Phase = EGamePhase.Lobby;
    }
    public void OnExitLobby()
    {
        Phase = EGamePhase.MainLevel;
        LevelNumber = 1;
        LoadLevel();
        
    }
    public void OnExitLevel()
    {
        LevelNumber++;
        if (LevelNumber >= 3)
        {
            Debug.Log("YouWIn");
        }
    }

    static string Level1 = "BaseLevel1";
    private void LoadLevel()
    {
        SceneManager.LoadScene(Level1);
    }
}
