using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    BoxCollider2D boxCollider;
    GameManager gameManager;
    GameInstance gameInstance;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        gameInstance = FindFirstObjectByType<GameInstance>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameInstance.OnExitLobby();
    }
}
