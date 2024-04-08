using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public CircleCollider2D circleCollider;
    public Rigidbody2D RB;
    public Animator animator;
    public PlayerScript Player;

    float deathTime = 1.2f;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Player = FindFirstObjectByType<PlayerScript>();
        circleCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime < 0)
        {
            Destroy(gameObject);
        }
        if (deathTime <= 0.15)
        {
            circleCollider.enabled = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            IDamagable e = collision.GetComponent<IDamagable>();
            if (e != null)
            {
                e.Damage(Player.bombDamage);
            }
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.RB.velocity = Vector3.zero;
            }
        }
    }
}
