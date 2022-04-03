using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BombardmentMovement : MovementBase
{
    [Header("Enemy Config")]
    [SerializeField] private float time;
    [Header("Bullet Config")]
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;
    private float distanceToPlayer;
    private Vector3 startPos;
    private bool comesFromLeft;
    private bool alreadyShooted;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

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
        time += Time.deltaTime;
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
            bulletMovement.SetBulletDamage(bulletDamage);
            bulletMovement.SetNewTarget(target);
        }
        else if (time >= 1)
        {
            Destroy(gameObject);
        }
    }
}

