using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameInstance gameInstance;
    public Vector2[] enemySpawns = new Vector2[15];
    public Vector2[] trapSpawns = new Vector2[5];
    // Start is called before the first frame update
    void Start()
    {
        gameInstance = FindFirstObjectByType<GameInstance>();

        int numberOfEnemySpawns = gameInstance.LevelNumber * 2 + 1;
        int numberOfTrapSpawns = gameInstance.LevelNumber;

        Vector2[] tempEnemySpawns = ShuffleArray(enemySpawns);
        for (int i = 0; i < numberOfEnemySpawns; i++)
        {
            print(tempEnemySpawns[i]);
        }
        Vector2[] tempTrapSpawns = ShuffleArray(trapSpawns);
        for (int i = 0;i < numberOfTrapSpawns; i++)
        {

        }
        
    }

    Vector2[] ShuffleArray(Vector2[] vector2s)
    {
        for (int i = 0; i < vector2s.Length; i++)
        {
            Vector2 vec = vector2s[i];
            int randomizeArray = Random.Range(0, i);
            vector2s[i] = vector2s[randomizeArray];
            vector2s[randomizeArray] = vec;
        }
        return vector2s;
    }
}
