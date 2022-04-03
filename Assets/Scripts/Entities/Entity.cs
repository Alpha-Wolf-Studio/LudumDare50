using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Config")]
    [SerializeField] protected int lives = 0; //cantidad de click para destruirse
    [SerializeField] protected int damage = 1;
    [Header("Damage Text Config")]
    [SerializeField] private Color color;
    [SerializeField] private GameObject damageTextPrefab;

    protected BoxCollider2D col = null;
    protected bool dead = false;
    public int Lives => lives;
    public int Damage => damage;

    public System.Action OnDied;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void RecieveDamage(int damageAmount)
    {
        if (!dead)
        {
            lives -= damageAmount;
            if (damageTextPrefab != null)
            {
                var damageTextInstance = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);

                damageTextInstance.GetComponent<DamageText>().SetText(damageAmount, color);
            }
            
            if (lives <= 0)
            {
                dead = true;
                OnDied?.Invoke();
                Destroy(gameObject); //Cambiar si hay animacion
            }
        }
    }
}
