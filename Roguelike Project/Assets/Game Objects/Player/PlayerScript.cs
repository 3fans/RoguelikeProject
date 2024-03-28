using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDirection;

public class PlayerScript : MonoBehaviour, IDamagable, IDirection
{


    [field: SerializeField] public int MaxHealth { get; set; } = 5;
    public int CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public PlayerControls PlayerControls { get; set; }
    public Animator animator { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    [field: SerializeField] public GameObject projectile {  get; set; } 

    public IDirection.Direction8 direction8 { get; set; } = IDirection.Direction8.E;

    #region State Machine Variables
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerWalkState WalkState { get; set; }
    
    #endregion

    #region Walk Variables
    public float moveSpeed = 3.0f;
    public InputAction move;
    public InputAction attack;
    #endregion



    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        attack = PlayerControls.Player.Fire;
        move.Enable();
        attack.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
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
    private void Awake()
    {
        PlayerControls = new PlayerControls();

        StateMachine = new PlayerStateMachine();

        WalkState = new PlayerWalkState(this, StateMachine);
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;

        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine.Initialize(WalkState);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
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
}
