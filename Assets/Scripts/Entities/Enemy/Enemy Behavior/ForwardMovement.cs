using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ForwardMovement : MovementBase
{
    [Header("Forward Config")]
    [SerializeField] float forwardSpeed = 1f;

    private float currentLerpTime;

    private Rigidbody2D rb;
    private Vector3 startPos;
    
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
    void FixedUpdate()
    {
        currentLerpTime += Time.deltaTime * forwardSpeed;
        rb.MovePosition(Vector3.Lerp(startPos, target.transform.position, currentLerpTime));
    }
}
