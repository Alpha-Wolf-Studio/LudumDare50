using UnityEngine;
using UnityEngine.UI;

public class ShipPlayer : Entity
{
    [SerializeField] private LayerMask enemyMask = 0;
    [SerializeField] private Image imageWaterShip;
    void Update()
    {
        ClickAttack();
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
    void UpdateUi()
    {
        imageWaterShip.fillAmount = (maxLives - lives) / (float) maxLives;
    }
}
