using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

public class Projectile : MonoBehaviour, IDirection
{
    public CircleCollider2D circleCollider;
    public Rigidbody2D RB;
    public Animator animator;
    public PlayerScript Player;
    private float projSpeed = 5;

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

        direction8 = Player.direction8;
        FixProjectileOffset(direction8);
        RB.rotation = Direction8ToFloat(direction8);

    }
    

    // Start is called before the first frame update
    void Start()
    {
        Vector2 _direction = Direction8ToVector2(direction8);
        RB.velocity = new Vector2(_direction.x * projSpeed, _direction.y * projSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("enemy");
            
            Destroy(gameObject);
        }
    }

    public float Direction8ToFloat(IDirection.Direction8 direction8)
    {
        switch (direction8)
        {
            case IDirection.Direction8.E:
                return 0;

            case IDirection.Direction8.NE:
                return 45;
 
            case IDirection.Direction8.N:
                return 90;

            case IDirection.Direction8.NW:
                return 135;

            case IDirection.Direction8.W:
                return 180;

            case IDirection.Direction8.SW:
                return 225;

            case IDirection.Direction8.S:
                return 270;

            case IDirection.Direction8.SE:
                return 315;

            default:
                return 0;
        }
    }
    public Vector2 Direction8ToVector2(IDirection.Direction8 direction8)
    {
        switch (direction8)
        {
            case IDirection.Direction8.E:
                return new Vector2(1,0).normalized;

            case IDirection.Direction8.NE:
                return new Vector2(1, 1).normalized;

            case IDirection.Direction8.N:
                return new Vector2(0, 1).normalized;

            case IDirection.Direction8.NW:
                return new Vector2(-1, 1).normalized;

            case IDirection.Direction8.W:
                return new Vector2(-1, 0).normalized;

            case IDirection.Direction8.SW:
                return new Vector2(-1, -1).normalized;

            case IDirection.Direction8.S:
                return new Vector2(0, -1).normalized;

            case IDirection.Direction8.SE:
                return new Vector2(1, -1).normalized;

            default:
                return new Vector2(1, 0).normalized;
        }
    }
    public void FixProjectileOffset(IDirection.Direction8 direction8)
    {
        switch (direction8)
        {
            case IDirection.Direction8.E:
                RB.position += new Vector2(0.5f, -0.1f);
                break;

            case IDirection.Direction8.NE:
                RB.position += new Vector2(0.8f, 0.3f);
                break;

            case IDirection.Direction8.N:
                RB.position += new Vector2(0.5f, 1f);
                break;

            case IDirection.Direction8.NW:
                RB.position += new Vector2(-0.1f, 1f);
                break;

            case IDirection.Direction8.W:
                RB.position += new Vector2(-0.4f, 0.9f);
                break;

            case IDirection.Direction8.SW:
                RB.position += new Vector2(-0.8f, 0.4f);
                break;

            case IDirection.Direction8.S:
                RB.position += new Vector2(-0.5f, 0);
                break;

            case IDirection.Direction8.SE:
                RB.position += new Vector2(0.1f, -0.2f);
                break;

            default:
                break;
        }
    }
}
