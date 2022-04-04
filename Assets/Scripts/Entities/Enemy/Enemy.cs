using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : Entity
{
    public void Start()
    {
        OnDied += Death;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<ShipPlayer>();
        if (player)
        {
            player.RecieveDamage(damage);
            Death();
        }
    }

    private void Death()
    {
        AudioManager.Get().PlaySoundEffect("enemy_death");
        Destroy(gameObject);
    }
}
