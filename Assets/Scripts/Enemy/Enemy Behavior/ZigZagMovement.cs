using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ZigZagMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool hasEnteredIntoTheScreen;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private SpriteRenderer spr;
    private Vector3 pos;
    private Vector3 axis;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        pos = transform.position;
        pos = transform.position;
        axis = transform.up;
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
    }

    private void FixedUpdate()
    {
        pos += Vector3.right * Time.deltaTime * speed;
        rb.MovePosition(pos + axis * Mathf.Sin(Time.time * frequency) * magnitude);
    }
}
