using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : Entity
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<ShipPlayer>();
        if (player)
        {
            player.RecieveDamage(damage);
            Destroy(gameObject);
        }
    }
}
