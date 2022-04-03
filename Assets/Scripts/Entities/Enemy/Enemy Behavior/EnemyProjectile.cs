using System.Collections;
using UnityEngine;

public class EnemyProjectile : MovementBase
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float offset = 0f;
    [SerializeField] private float sizeTimer = 0f;

    [Header("Attack configuration"), Space]
    [SerializeField] private float attackDelay = 0f;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectilesParent = null;

    private float attackTimer = 0f;
    private bool inPositionToAttack = false;
    private bool attackEnabled = false;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (!inPositionToAttack)
        {
            MoveToTarget();
        }
        else
        {
            if (!attackEnabled)
                return;

            Attack();
        }
    }

    private void MoveToTarget()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(target.transform.position, transform.position) < offset)
        {
            inPositionToAttack = true;
            StartCoroutine(IncrementSize());
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackDelay)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            //projectile.transform.parent = projectilesParent;
            projectilePrefab.transform.position = transform.position;

            attackTimer = 0f;
        }
    }

    private IEnumerator IncrementSize()
    {
        float timer = 0f;
        while (timer < sizeTimer)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / sizeTimer);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(attackDelay);

        attackEnabled = true;
        yield return null;
    }
}
