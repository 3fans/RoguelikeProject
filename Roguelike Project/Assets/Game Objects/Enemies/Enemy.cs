using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDirection;

public class Enemy : MonoBehaviour, IDamagable, IDirection, ITriggerCheckable
{

    [field: SerializeField] public int MaxHealth { get; set; } = 5;
    public int CurrentHealth { get; set; }
    public IDirection.Direction8 direction8 { get; set; } = Direction8.E;
    public Rigidbody2D RB { get; set; }
    public Animator animator { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }

    #region State Machine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyWanderState WanderState { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    #endregion

    #region Walk Variables
    public float moveSpeed = 3.0f;
    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        
        WanderState = new EnemyWanderState(this, StateMachine);
        IdleState = new EnemyIdleState(this,StateMachine);

        //Add enemy states instances public EnemyWalkState WalkState { get; set; }
    }
    void Start()
    {
        CurrentHealth = MaxHealth;

        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    public Direction8 VectorToDirection(Vector2 vector)
    {
        if (vector == Vector2.zero)
        {
            return Direction8.Zero;
        }
        else
        {
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

            if (vector.y < 0)
            {
                angle = 360 + angle;
            }

            float A = 45f;
            float a = A / 2;

            if (angle >= -a && angle < a)
            {
                return Direction8.E;
            }
            else if (angle >= a && angle < a + A)
            {
                return Direction8.NE;
            }
            else if (angle >= a + A && angle < a + 2 * A)
            {
                return Direction8.N;
            }
            else if (angle >= a + 2 * A && angle < a + 3 * A)
            {
                return Direction8.NW;
            }
            else if (angle >= a + 3 * A && angle < a + 4 * A)
            {
                return Direction8.W;
            }
            else if (angle >= a + 4 * A && angle < a + 5 * A)
            {
                return Direction8.SW;

            }
            else if (angle >= a + 5 * A && angle < a + 6 * A)
            {
                return Direction8.S;
            }
            else
            {
                return Direction8.SE;
            }
        }
    }

    public void SetAggroedStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }
}
