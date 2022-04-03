using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BombardmentMovement : MovementBase
{
    [Header("Movement Config")]
    [SerializeField] private float speed = 1;
    [Header("Bullet Config")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 1;
    private float time = 0;
    private float distanceToPlayer;
    private Vector3 startPos;
    private bool comesFromLeft;
    private bool alreadyShooted;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        distanceToPlayer = Mathf.Abs(target.position.x - transform.position.x);
        if(startPos.x < target.position.x)
        {
            comesFromLeft = true;
        }
    }

    private void Update()
    {
        time += Time.deltaTime * speed;
        if (comesFromLeft)
        {
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(distanceToPlayer * 2, 0, 0), time);
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, startPos - new Vector3(distanceToPlayer * 2, 0, 0), time);
        }
        if (time >= 0.48 && !alreadyShooted)
        {
            alreadyShooted = true;
            var bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            var bulletMovement = bulletInstance.GetComponent<BulletMovement>();
            int bulletDamage = GetComponent<Entity>().Damage;
            bulletMovement.SetBullet(bulletDamage, bulletSpeed);
            bulletMovement.SetNewTarget(target);
        }
        else if (time >= 1)
        {
            Destroy(gameObject);
        }
    }
}

