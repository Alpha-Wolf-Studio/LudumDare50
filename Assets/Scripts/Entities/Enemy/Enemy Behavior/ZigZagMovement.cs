using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ZigZagMovement : MovementBase
{
    [Header("ZigZag Config")]
    [SerializeField] private float speed;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    private Rigidbody2D rb;
    private Vector3 pos;
    private Vector3 axis;
    private Vector3 direction;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        direction = (target.position - transform.position).normalized;
        axis = Vector3.Cross(direction, transform.forward).normalized;
    }

    private void FixedUpdate()
    {
        pos += direction * Time.deltaTime * speed;
        rb.MovePosition(pos + axis * Mathf.Sin(Time.time * frequency) * magnitude);
    }
}
