using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class LightingBoltSummoner : MovementBase
{
    [Header("Movement Config")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float speed;
    [SerializeField] private float speedAux;
    [SerializeField] private float distanceToMove;
    [SerializeField] private Vector3 movementVector;
    [Header("Lighting Bolt Config")]
    [SerializeField] private GameObject lightingBolt;
    [SerializeField] private float timeToSummon;
    [SerializeField] private float timeElapsed;
    [SerializeField] private bool lightingHasBeenSummoned;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if(transform.position.x > target.position.x)
        {
            speed = -speed;
        }
        speedAux = speed;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

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
                Destroy(gameObject, 6);
                GameObject lightingBoltInstance = Instantiate(lightingBolt, transform.position, Quaternion.identity);
                lightingBoltInstance.GetComponent<MovementBase>().SetNewTarget(target);
                lightingBoltInstance.GetComponent<LookAt2D>().SetTarget(target);
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
