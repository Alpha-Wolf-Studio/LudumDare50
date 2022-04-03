using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{

    [SerializeField] bool invertX = false;
    [SerializeField] forwardAligment axisDirection = forwardAligment.FRONT;
    enum forwardAligment { FRONT, BACK, UP, DOWN}

    private Transform target = null;
    public void SetTarget(Transform target) => this.target = target;

    void Update()
    {
        if (!target) return;
        var dir = target.position - transform.position;


        switch (axisDirection)
        {
            case forwardAligment.FRONT:
                if (invertX) transform.right = dir.normalized;
                else transform.up = dir.normalized;

                break;
            case forwardAligment.BACK:
                if (invertX) transform.right = -dir.normalized;
                else transform.up = -dir.normalized;

                break;
            case forwardAligment.UP:
                if (invertX) transform.right = Vector3.Cross(Vector3.forward, dir.normalized);
                else transform.up = Vector3.Cross(Vector3.forward, dir.normalized);

                break;

            case forwardAligment.DOWN:
                if (invertX) transform.right = Vector3.Cross(-Vector3.forward, dir.normalized);
                else transform.up = Vector3.Cross(-Vector3.forward, dir.normalized);
                break;
            default:
                break;
        }
    }
}
