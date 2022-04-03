using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CurveBulletMovement : MovementBase
{
    private Vector3 startPos;
    private int damage = 1;
    private float speed = 1;
    private float arcOffset = 1;
    private float currentLerpTime = 0;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<ShipPlayer>();
        if (player)
        {
            player.RecieveDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetBullet(int bulletDamage, float bulletSpeed)
    {
        damage = bulletDamage;
        speed = bulletSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, target.position, currentLerpTime);
        Vector2 differenceVector = target.position - transform.position;
        newPosition += Vector2.Perpendicular(differenceVector) * Mathf.Sin(currentLerpTime) * arcOffset;
        currentLerpTime += Time.fixedDeltaTime * speed;
        rb.MovePosition(newPosition);
    }
}