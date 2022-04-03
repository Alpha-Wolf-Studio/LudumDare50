using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlayer : Entity
{
    [SerializeField] private LayerMask enemyMask = 0;
    [SerializeField] private Image imageWaterShip;
    [SerializeField] private float sinkingCoef;
    [SerializeField] private float speedSinking;
    [SerializeField] private float sinkingRepairClick;
    [SerializeField] private float currentLive;

    void Update()
    {
        ClickAttack();
        SinkingShip();
        UpdateUi();
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
                    enemy.RecieveDamage(damage);
                }
            }
        }
    }
    private void SinkingShip()
    {
        sinkingCoef = 1 - lives / (float) maxLives;
        currentLive -= sinkingCoef * speedSinking * Time.deltaTime;

    }
    public void RepairShip()
    {
        currentLive += sinkingRepairClick;
        if (currentLive > 100) 
            currentLive = 100;
    }
    private void UpdateUi()
    {
        imageWaterShip.fillAmount = 1 - currentLive / 100;
    }
}