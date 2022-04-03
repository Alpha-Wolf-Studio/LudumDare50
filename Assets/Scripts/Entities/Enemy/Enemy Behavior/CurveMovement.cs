using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CurveMovement : MovementBase
{
    [Header("Curve movement configurations")]
    [SerializeField] private float arcOffset = 1;
    [SerializeField] private float speed = 1;

    private float currentLerpTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, target.position, currentLerpTime);
        Vector2 differenceVector = target.position - transform.position;
        newPosition += Vector2.Perpendicular(differenceVector) * Mathf.Sin(currentLerpTime) * arcOffset;
        currentLerpTime += Time.fixedDeltaTime * speed;
        rb.MovePosition(newPosition);
    }
}
