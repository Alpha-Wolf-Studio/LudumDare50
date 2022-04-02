using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int lives = 0; //cantidad de click para destruirse
    [SerializeField] protected float speed = 0f; //velocidad de desplazamiento
    [SerializeField] protected Vector3 size = Vector3.zero; //tamaño

    protected BoxCollider2D col = null;
    protected bool dead = false;

    public int Lives => lives;
    public float Speed => speed;
    public Vector3 Size => size;

    private void Start()
    {
        transform.localScale = size;

        col = GetComponent<BoxCollider2D>();
    }

    public void RecieveDamage()
    {
        if (!dead)
        {
            lives--;

            if (lives <= 0)
            {
                dead = true;
            }
        }
    }
}
