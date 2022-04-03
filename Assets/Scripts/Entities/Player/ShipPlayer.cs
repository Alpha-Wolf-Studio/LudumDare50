using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlayer : Entity
{
    [Header("Enemy Attack Config")]
    [SerializeField] private LayerMask enemyMask = 0;

    [Header("Repair Ship Config")]
    [SerializeField] private Image imageWaterShip;
    [SerializeField] private float sinkingSpeed = 1;
    
    private float currentLife;
    private bool sinking = false;
    public bool Sinking => sinking;

    private void Awake()
    {
        OnDamageReceive += ReduceCurrentLife;
    }

    private void Start()
    {
        currentLife = (float)maxLives;
    }

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
                else if (gameObject.layer == hit.collider.gameObject.layer)
                {
                    if (sinking)
                    {
                        RepairShip();
                    }
                }

            }
        }

    }
    private void SinkingShip()
    {
        if (sinking) 
        {
            currentLife -= sinkingSpeed * Time.deltaTime;
            if (currentLife < 0)
            {
                dead = true;
                OnDied?.Invoke();
                Destroy(gameObject);
            }
        }
    }
    public void RepairShip()
    {
        sinking = false;
    }

    private void ReduceCurrentLife(int life) 
    {
        currentLife -= life;
        sinking = true;
    }

    private void UpdateUi()
    {
        imageWaterShip.fillAmount = 1 - currentLife / maxLives;
    }
}