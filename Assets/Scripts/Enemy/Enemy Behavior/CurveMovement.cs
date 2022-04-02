using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CurveMovement : MovementBase
{
    [SerializeField] float lerpTime = 1f;
    [SerializeField] float currentLerpTime;
    [SerializeField] bool isLerping;

    Vector3 startPos;
    [SerializeField] Vector3 endPos;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;    

    private Rigidbody2D rb;

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
        GetCenter(Vector3.up);

        if (isLerping)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                isLerping = false;
            }

            float percentComplete = currentLerpTime / lerpTime;
            Debug.Log(percentComplete);

            rb.MovePosition(Vector3.Slerp(startRelCenter, endRelCenter, percentComplete));
        }
    }

    void GetCenter(Vector3 direction)
    {
        centerPoint = (startPos + target.transform.position) * 0.5f;
        centerPoint -= direction;
        startRelCenter = startPos - centerPoint;
        endRelCenter = target.transform.position - centerPoint;
    }
}
