using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{

    [SerializeField] private bool lookAt = true;
    [SerializeField] private bool invertOnX = true;
    [SerializeField] private forwardAligment axisDirection = forwardAligment.FRONT;
    enum forwardAligment { FRONT, BACK, UP, DOWN}

    private Transform target = null;
    public void SetTarget(Transform target) => this.target = target;

    private SpriteRenderer mainRenderer;

    [Space(10)]
    [SerializeField] private SpriteRenderer[] extraRenderersToFlip;

    private void Awake()
    {
        mainRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!target) return;

        if (lookAt) 
        {
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
        }

        if (invertOnX) 
        {
            bool invert = transform.position.x < target.position.x;

            if (invert)
            {
                if(axisDirection == forwardAligment.FRONT || axisDirection == forwardAligment.BACK) 
                {
                    mainRenderer.flipY = true;
                    foreach (var renderer in extraRenderersToFlip)
                    {
                        renderer.flipY = true;
                    }
                }
                else 
                {
                    mainRenderer.flipX = true;
                    foreach (var renderer in extraRenderersToFlip)
                    {
                        renderer.flipX = true;
                    }
                }
            }
            else 
            {
                if (axisDirection == forwardAligment.FRONT || axisDirection == forwardAligment.BACK)
                {
                    mainRenderer.flipX = false;
                    foreach (var renderer in extraRenderersToFlip)
                    {
                        renderer.flipX = false;
                    }
                }
                else 
                {
                    mainRenderer.flipY = false;
                    foreach (var renderer in extraRenderersToFlip)
                    {
                        renderer.flipY = false;
                    }
                }
            }
        }
    }
}
