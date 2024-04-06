using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour, IDirection
{
    public CircleCollider2D circleCollider;
    public Rigidbody2D RB;
    public Animator animator;
    public PlayerScript Player;

    public IDirection.Direction8 direction8 { get; set; }

    public IDirection.Direction8 VectorToDirection(Vector2 vector)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Player = FindFirstObjectByType<PlayerScript>();

    }


    // Start is called before the first frame update
    void Start()
    {
        direction8 = Player.direction8;
        Debug.Log(direction8);
        RB.velocity = new Vector2(1,0);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
