using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected Transform target;
    public void SetNewTarget(Transform target) => this.target = target; 
}
