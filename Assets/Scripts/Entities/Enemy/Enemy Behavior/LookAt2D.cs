using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{

    [SerializeField] forwardAligment axisToAlign = forwardAligment.FRONT;
    enum forwardAligment { FRONT, BACK, UP, DOWN}

    private Transform target = null;
    public void SetTarget(Transform target) => this.target = target;

    void Update()
    {
        if (!target) return;
        var dir = target.position - transform.position;

        switch (axisToAlign)
        {
            case forwardAligment.FRONT:
                transform.up = dir.normalized;
                break;
            case forwardAligment.BACK:
                transform.up = -dir.normalized;
                break;
            case forwardAligment.UP:
                transform.up = Vector3.Cross(Vector3.forward,  dir.normalized);
                break;
            case forwardAligment.DOWN:
                transform.up = Vector3.Cross(-Vector3.forward, dir.normalized);
                break;
            default:
                break;
        }

    }
}
