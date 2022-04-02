using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int lives = 0; //cantidad de click para destruirse
    [SerializeField] protected float speed = 0f; //velocidad de desplazamiento

    protected BoxCollider2D col = null;
    protected bool dead = false;

    public int Lives => lives;
    public float Speed => speed;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void RecieveDamage(int damageAmount)
    {
        if (!dead)
        {
            lives -= damageAmount;

            if (lives <= 0)
            {
                dead = true;
            }
        }
    }
}
