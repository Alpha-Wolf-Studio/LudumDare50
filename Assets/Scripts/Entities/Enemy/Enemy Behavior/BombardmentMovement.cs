using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BombardmentMovement : MovementBase
{
    [Header("Enemy Config")]
    [SerializeField] private float speed;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private bool hasEnteredIntoTheScreen;
    [Header("Bullet Config")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timeBetweenShoots;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movementVector * speed * Time.deltaTime);
    }

    private IEnumerator Shoot(float timeBetweenShoots)
    {
        while (spr.isVisible && hasEnteredIntoTheScreen)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenShoots);
            yield return null;
        }
        yield return null;    
    }
}
