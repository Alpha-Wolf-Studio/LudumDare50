using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class LightingBoltSummoner : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float speedAux;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private bool hasEnteredIntoTheScreen;
    [SerializeField] private GameObject lightingBolt;
    [SerializeField] private float timeToSummon;
    [SerializeField] private float timeElapsed;
    [SerializeField] private float distanceToMove;
    [SerializeField] private bool lightingHasBeenSummoned;
    [SerializeField] private Vector3 startPosition;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        speedAux = speed;
        startPosition = transform.position;
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

        if (Mathf.Abs(transform.position.x - startPosition.x) >= distanceToMove)
        {
            speed = 0;
        }
        if(speed == 0 && !lightingHasBeenSummoned)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > timeToSummon-1)
            {
                lightingHasBeenSummoned = true;
                GameObject lightingBoltInstance = Instantiate(lightingBolt,transform.position,Quaternion.identity);
                Destroy(lightingBoltInstance, 1);
            }
        }
        if(speed == 0 && lightingHasBeenSummoned)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeToSummon)
            {
                speed = -speedAux;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movementVector * speed * Time.deltaTime);
    }

}
