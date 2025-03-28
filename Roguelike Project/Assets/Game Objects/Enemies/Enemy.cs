using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using static IDirection;

public class Enemy : MonoBehaviour, IDamagable, IDirection, ITriggerCheckable
{

    [field: SerializeField] public float MaxHealth { get; set; } = 5;
    private float healthScaling = 1.5f;
    public float CurrentHealth { get; set; }
    public IDirection.Direction8 direction8 { get; set; } = Direction8.E;
    public Rigidbody2D RB { get; set; }
    public Animator animator { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    private float startHealth = 0;
    [field: SerializeField] public GameObject damageNumberObject { get; set; }
    public GameManager gameManager { get; private set; }

    #region State Machine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyWanderState WanderState { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyDamagedState DamagedState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public PlayerScript Player { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    #endregion
    public float attackDamage = 1f;
    #region Walk Variables
    public float moveSpeed = 0.1f;
    #endregion

    public float attackTimer = 0;
    public float attackCooldown = 0.5f;
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        
        WanderState = new EnemyWanderState(this, StateMachine);
        IdleState = new EnemyIdleState(this,StateMachine);
        DamagedState = new EnemyDamagedState(this,StateMachine);
        AttackState = new EnemyAttackState(this,StateMachine);

        //Add enemy states instances public EnemyWalkState WalkState { get; set; }
    }
    void Start()
    {
        float pow = GameInstance.Instance.LevelNumber - 1;
        CurrentHealth = MaxHealth * Mathf.Pow(healthScaling, pow);
        startHealth = CurrentHealth;
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = FindFirstObjectByType<PlayerScript>();
        gameManager = FindFirstObjectByType<GameManager>();

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

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        LifeIndicator();
        float yOffset = 0.7f;
        GameObject damageNumber = GameObject.Instantiate(damageNumberObject, new Vector3(RB.position.x, RB.position.y + yOffset, -1), new Quaternion(0, 0, 0, 0));
        if (damageNumber.GetComponent<ITextDisplay>() != null)
        {
            ITextDisplay textDisplay = damageNumber.GetComponent<ITextDisplay>();
            textDisplay.SetText(damageAmount.ToString());
            textDisplay.SetColor(Color.white);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
        StateMachine.ChangeState(DamagedState);
    }

    public void Die()
    {
        //Death animation
        gameManager.OnEnemyDeath();
        Destroy(gameObject);
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

    public void AttackTimerCountDown()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = 0;
        }
    }

    private void LifeIndicator()
    {
        float gb = CurrentHealth / startHealth;
        spriteRenderer.color = new Color(255, gb, gb);
    }
}
