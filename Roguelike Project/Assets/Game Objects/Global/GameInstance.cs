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
        levelOrder = ShuffleArray(levelNames);
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
    #region LevelNames
    static string Level1 = "BaseLevel1";
    static string Level2 = "BaseLevel2";
    static string Level3 = "BaseLevel3";
    static string Level4 = "BaseLevel4";
    static string Level5 = "BaseLevel5";
    static string Level6 = "BaseLevel6";
    string[] levelNames = {Level1,Level2,Level3,Level4,Level5,Level6};
    string[] levelOrder;
    #endregion
    string[] ShuffleArray(string[] levelnames)
    {
        for (int i = 0; i < levelnames.Length; i++)
        {
            string str = levelnames[i];
            int randomizeArray = UnityEngine.Random.Range(0, i);
            levelnames[i] = levelnames[randomizeArray];
            levelnames[randomizeArray] = str;
        }
        return levelnames;
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levelOrder[LevelNumber - 1]);
    }
}
