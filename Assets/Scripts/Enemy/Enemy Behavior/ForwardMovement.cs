using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ForwardMovement : MonoBehaviour
{
    [SerializeField] float lerpTime = 1f;
    [SerializeField] float currentLerpTime;
    [SerializeField] bool isLerping;

    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;

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
            rb.MovePosition(Vector3.Lerp(startPos, endPos, percentComplete));
        }
    }
}
