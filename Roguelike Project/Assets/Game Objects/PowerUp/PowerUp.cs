using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Powerup
    {
        None,
        ProjSpeed,
        ProjDamage,
        BombSpeed,
        BombDamage
    }
    public Powerup powerup = Powerup.None;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (powerup)
            {
                case Powerup.None:
                    break;
                case Powerup.ProjSpeed:
                    GameInstance.Instance.OnProjSpeedPowerup();
                    break;
                case Powerup.ProjDamage:
                    GameInstance.Instance.OnProjDamagePowerup();
                    break;
                case Powerup.BombSpeed: 
                    GameInstance.Instance.OnBombSpeedPowerup();
                    break;
                case Powerup.BombDamage:
                    GameInstance.Instance.OnBombDamagePowerup();
                    break;
                default:
                    break;
            }
            GameInstance.Instance.LoadLevel();
        }
    }
}
