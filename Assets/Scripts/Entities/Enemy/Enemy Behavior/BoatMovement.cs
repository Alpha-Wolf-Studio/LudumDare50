using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class BoatMovement : MovementBase
{
    [SerializeField] private bool hasEnteredIntoTheScreen;
    [Header("Boat Config")]
    [SerializeField] private float speed;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    [SerializeField] private GameObject targett;
    [SerializeField] private float speedAux;
    [SerializeField] private float distanceToMove;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timeToShoot;
    [SerializeField] private float timeElapsed;
    [SerializeField] private bool bulletHasBeenShooted;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;
    private Vector3 pos;
    private Vector3 startPos;
    private Vector3 axis;
    private Vector3 direction;


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
        pos = transform.position;
        direction = (targett.transform.position - transform.position).normalized;
        axis = transform.right;
        speedAux = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEnteredIntoTheScreen && spr.isVisible)
        {
            hasEnteredIntoTheScreen = true;
        }
        if (!spr.isVisible && hasEnteredIntoTheScreen)
        {
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position,startPos) >= distanceToMove)
        {
            speed = 0;
        }
        if (speed == 0 && !bulletHasBeenShooted)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeToShoot - 1)
            {
                bulletHasBeenShooted = true;
                GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
                Destroy(bulletInstance, 3);
            }
        }
        if (speed == 0 && bulletHasBeenShooted)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeToShoot)
            {
                speed = -speedAux;
            }
        }
    }

    private void FixedUpdate()
    {
        pos += direction * Time.deltaTime * speed;
        rb.MovePosition(pos + axis * Mathf.Sin(Time.time * frequency) * magnitude);
    }
}
