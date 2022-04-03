using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ForwardMovement : MovementBase
{
    [Header("Lerp Config")]
    [SerializeField] private float lerpTime = 1f;
    [SerializeField] private float currentLerpTime;
    [SerializeField] private bool isLerping;


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
        if (isLerping)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                isLerping = false;
            }

            float percentComplete = currentLerpTime / lerpTime;
            rb.MovePosition(Vector3.Lerp(startPos, target.transform.position, percentComplete));
        }
    }
}
