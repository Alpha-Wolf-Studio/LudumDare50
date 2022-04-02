using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask = 0;

    void Start()
    {
        
    }

    void Update()
    {
        ClickAttack();
    }

    private void ClickAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (Utils.CheckLayerInMask(enemyMask, hit.collider.gameObject.layer))
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    enemy.RecieveDamage();
                }
            }
        }
    }
}
