using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BombardmentMovement : MovementBase
{

    [SerializeField] private float speed;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private GameObject bullet;
    [SerializeField] private bool hasEnteredIntoTheScreen;
    [SerializeField] private float timeBetweenShoots;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEnteredIntoTheScreen && spr.isVisible)
        {
            hasEnteredIntoTheScreen = true;
            StartCoroutine(Shoot(timeBetweenShoots));
        }
        if (!spr.isVisible && hasEnteredIntoTheScreen)
        {
            Destroy(gameObject);
        }
        
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
