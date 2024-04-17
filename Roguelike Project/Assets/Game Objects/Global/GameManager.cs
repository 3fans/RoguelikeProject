using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameInstance gameInstance;
    public GameObject enemy;
    public GameObject trap;

    public Vector2[] enemySpawns = new Vector2[10];
    public Vector2[] trapSpawns = new Vector2[5];
    public Vector2[] powerUpSpawns = new Vector2[2];
    public GameObject[] PowerUps = new GameObject[4];

    int numberOfEnemySpawns;
    int numberOfTrapSpawns;
    int enemiesRemaining;

    void Start()
    {
        gameInstance = FindFirstObjectByType<GameInstance>();

        numberOfEnemySpawns = gameInstance.LevelNumber * 2 ;
        enemiesRemaining = numberOfEnemySpawns;
        numberOfTrapSpawns = gameInstance.LevelNumber;

        Vector2[] tempEnemySpawns = ShuffleArray(enemySpawns);
        for (int i = 0; i < numberOfEnemySpawns; i++)
        {
            GameObject.Instantiate(enemy, tempEnemySpawns[i], new Quaternion(0, 0, 0, 0));
        }
        Vector2[] tempTrapSpawns = ShuffleArray(trapSpawns);
        for (int i = 0;i < numberOfTrapSpawns; i++)
        {
            GameObject.Instantiate(trap, tempTrapSpawns[i], new Quaternion(0, 0, 0, 0));
        }
        
    }
    public void OnEnemyDeath()
    {
        numberOfEnemySpawns--;
        if (numberOfEnemySpawns == 0)
        {
            GameInstance.Instance.OnExitLevel();
            ShuffleGameObjects(PowerUps);
            GameObject.Instantiate(PowerUps[0], powerUpSpawns[0], new Quaternion(0, 0, 0, 0));
            GameObject.Instantiate(PowerUps[1], powerUpSpawns[1], new Quaternion(0, 0, 0, 0));
        }
    }
    Vector2[] ShuffleArray(Vector2[] vector2s)
    {
        for (int i = 0; i < vector2s.Length; i++)
        {
            Vector2 vec = vector2s[i];
            int randomizeArray = UnityEngine.Random.Range(0, i);
            vector2s[i] = vector2s[randomizeArray];
            vector2s[randomizeArray] = vec;
        }
        return vector2s;
    }

    GameObject[] ShuffleGameObjects(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject obj = gameObjects[i];
            int randomizeObj = UnityEngine.Random.Range(0, i);
            gameObjects[i] = gameObjects[randomizeObj];
            gameObjects[randomizeObj] = obj;
        }
        return gameObjects;

    }
}
