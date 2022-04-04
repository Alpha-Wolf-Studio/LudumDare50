using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    [SerializeField] private forwardAligment axisDirection = forwardAligment.FRONT;
    enum forwardAligment { FRONT, BACK, UP, DOWN}

    private Transform target = null;
    public void SetTarget(Transform target) => this.target = target;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!target) return;
        var dir = target.position - transform.position;
        dir.Normalize();

        switch (axisDirection)
        {
            case forwardAligment.FRONT:
                transform.right = dir.normalized;

                break;
            case forwardAligment.BACK:
                transform.right = -dir.normalized;

                break;
            case forwardAligment.UP:
                transform.up = dir.normalized;

                break;

            case forwardAligment.DOWN:
                transform.up = -dir.normalized;
                break;
            default:
                break;
        }

        bool invert = transform.position.x < target.position.x;

        if (invert)
        {
            renderer.flipY = true;
        }
        else 
        {
            renderer.flipY = false;
        }
    }
}
