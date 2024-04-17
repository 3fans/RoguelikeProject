using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallSpawner : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    [field: SerializeField] public GameObject projectile { get; set; }
    [field: SerializeField] public GameObject damageNumberObject { get; set; }
    public Rigidbody2D RB;

    public float shootCooldown = 1f;
    float shootTimer;


    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        float yOffset = 1.5f;
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
    }

    public void Die()
    {
        //Death animation
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        shootTimer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer <=0)
        {
            SpawnProjectiles();
            shootTimer = shootCooldown;
        }
        shootTimer -= Time.deltaTime;
    }

    void SpawnProjectiles()
    {
        GameObject _projectileE = GameObject.Instantiate(projectile, RB.position + new Vector2(0.7f,0), new Quaternion(0,0,0,0));
        IProjectile _pE = _projectileE.GetComponent<IProjectile>();
        if (_pE != null)
        {
            _pE.SetDirection(IDirection.Direction8.E);
        }
        GameObject _projectileN = GameObject.Instantiate(projectile, RB.position + new Vector2(0.5f,1.2f), Quaternion.Euler(0,0,90));
        IProjectile _pN = _projectileN.GetComponent<IProjectile>();
        if (_pN != null)
        {
            _pN.SetDirection(IDirection.Direction8.N);
        }
        GameObject _projectileW = GameObject.Instantiate(projectile, RB.position + new Vector2(-0.7f, 1f), Quaternion.Euler(0,0,180));
        IProjectile _pW = _projectileW.GetComponent<IProjectile>();
        if (_pW != null)
        {
            _pW.SetDirection(IDirection.Direction8.W);
        }
        GameObject _projectileS = GameObject.Instantiate(projectile, RB.position + new Vector2(-0.5f, -0.2f), Quaternion.Euler(0, 0, 90));
        IProjectile _pS = _projectileS.GetComponent<IProjectile>();
        if (_pS != null)
        {
            _pS.SetDirection(IDirection.Direction8.S);
        }
    }
}
