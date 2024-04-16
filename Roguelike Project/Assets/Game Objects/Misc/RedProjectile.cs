using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectile : MonoBehaviour, IDirection, IProjectile
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D RB;
    public Animator animator;
    public PlayerScript Player;
    public float projDamage = 2f;
    public float projSpeed = 5;
    

    private float deathTime = 0.3f;
    bool isDying = false;

    public IDirection.Direction8 direction8 { get; set; }

    public IDirection.Direction8 VectorToDirection(Vector2 vector)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Player = FindFirstObjectByType<PlayerScript>();

    }
    public void SetDirection(IDirection.Direction8 direction)
    {
        direction8 = direction;
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
        if (isDying)
        {
            deathTime -= Time.deltaTime;
            if (deathTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {

            IDamagable e = collision.GetComponent<IDamagable>();
            if (e != null)
            {
                e.Damage(projDamage);
            }

            animator.SetTrigger("Die");
            isDying = true;
            RB.velocity = Vector2.zero;
        }
        if (collision.CompareTag("Wall"))
        {
            animator.SetTrigger("Die");
            isDying = true;
            RB.velocity = Vector2.zero;
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
                return new Vector2(1, 0).normalized;

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
