using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDirection;

public class PlayerScript : MonoBehaviour, IDamagable, IDirection
{


    [field: SerializeField] public float MaxHealth { get; set; } = 5;
    public float CurrentHealth { get; set; }
    private float startHealth = 0;
    public Rigidbody2D RB { get; set; }
    public PlayerControls PlayerControls { get; set; }
    public Animator animator { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    [field: SerializeField] public GameObject projectile {  get; set; }
    [field: SerializeField] public GameObject bombObject { get; set; }
    [field: SerializeField] public GameObject damageNumberObject { get; set; }

    public IDirection.Direction8 direction8 { get; set; } = IDirection.Direction8.E;

    #region State Machine Variables
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerWalkState WalkState { get; set; }
    public PlayerAttacKState AttackState { get; set; }
    public PlayerBombState BombState { get; set; }
    
    #endregion

    #region Input and Walk Variables
    public float moveSpeed = 3.0f;
    public InputAction move;
    public InputAction attack;
    public InputAction secondaryFire;
    #endregion

    #region Attack Variables
    public float shootTimer = 0;
    public float bombTimer = 0;
    public float shootCooldown = 0.8f;
    public float bombCooldown = 1.5f;
    public float shootDamage = 2f;
    public float bombDamage = 4f;
    #endregion


    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        attack = PlayerControls.Player.Fire;
        secondaryFire = PlayerControls.Player.SecondFire;
        move.Enable();
        attack.Enable();
        secondaryFire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        secondaryFire.Disable();
    }
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        LifeIndicator();
        float yOffset = 1f;
        GameObject damageNumber = GameObject.Instantiate(damageNumberObject, new Vector3(RB.position.x, RB.position.y + yOffset, 0), new Quaternion(0, 0, 0, 0));
        if (damageNumber.GetComponent<ITextDisplay>() != null)
        {
            ITextDisplay textDisplay = damageNumber.GetComponent<ITextDisplay>();
            textDisplay.SetText(damageAmount.ToString());
            textDisplay.SetColor(Color.red);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameInstance.Instance.OnPlayerDeath();
    }
    private void Awake()
    {
        PlayerControls = new PlayerControls();

        StateMachine = new PlayerStateMachine();
     
        WalkState = new PlayerWalkState(this, StateMachine);
        AttackState = new PlayerAttacKState(this, StateMachine);
        BombState = new PlayerBombState(this, StateMachine);
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = GameInstance.Instance.CurrentPlayerStats.maxHealth;
        shootCooldown = GameInstance.Instance.CurrentPlayerStats.shootCooldown;
        bombCooldown = GameInstance.Instance.CurrentPlayerStats.bombCooldown;
        shootDamage = GameInstance.Instance.CurrentPlayerStats.shootDamage;
        bombDamage = GameInstance.Instance.CurrentPlayerStats.bombDamage;
        startHealth = CurrentHealth;

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

    public void ShootTimerCountDown()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = 0;
        }
    }
    public void BombTimerCountDown()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
            bombTimer = 0;
        }
    }
    private void LifeIndicator()
    {
        float gb = CurrentHealth / startHealth;
        spriteRenderer.color = new Color(255, gb, gb);
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
            else if ((angle >= a + 6 * A && angle < a + 7 * A))
            {
                return Direction8.SE;
            }
            else
            {
                return Direction8.E;
            }
        }
    }
}
