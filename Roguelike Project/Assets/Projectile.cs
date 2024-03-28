using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public Rigidbody2D RB;
    public Animator animator;
    private int Direction = 0;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    public Projectile(int dir)
    {
        Direction = dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        RB.velocity = new Vector2(1 * Direction, 1 * Direction);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
