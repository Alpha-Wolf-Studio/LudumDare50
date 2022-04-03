using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    private Transform target = null;
    public void SetTarget(Transform target) => this.target = target;

    void Update()
    {
        if (!target) return;
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
