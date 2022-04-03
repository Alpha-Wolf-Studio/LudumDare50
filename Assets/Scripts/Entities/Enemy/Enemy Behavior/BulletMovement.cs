using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MovementBase
{
    private Vector3 startPos;
    private float time = 0;
    private int damage = 1;
    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, target.position, time);
    }

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
}
