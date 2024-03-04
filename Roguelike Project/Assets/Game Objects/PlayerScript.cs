using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour, IDamagable
{


    [field: SerializeField] public int MaxHealth { get; set; } = 5;
    public int CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public PlayerControls PlayerControls { get; set; }
    public Animator animator { get; set; }

    #region State Machine Variables
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerWalkState WalkState { get; set; }
    #endregion

    #region Walk Variables
    public float moveSpeed = 3.0f;
    public InputAction move;
    #endregion

    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();
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

        StateMachine.Initialize(WalkState);
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        //FILL IN AFTER STATE MACHINE
    }
    public enum AnimationTriggerType
    {
        PlayerDamaged,
        Attack
    }
    #endregion
}
