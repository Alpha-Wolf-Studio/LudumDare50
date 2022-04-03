using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BoatMovement : MovementBase
{
    [Header("Forward Config")]
    [SerializeField] float forwardSpeed = 0.2f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnTransform;

    private float currentLerpTime;
    private float invertedCurrentLerpTime;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private Vector3 pos;
    private Vector3 shootPos;
    private float distanceToPlayer;
    private float bulletSpeed = 0.2f;
    private float speedAux;
    private bool alreadyShooted;
    private bool comesFromLeft;
    private Vector2 minCameraPoint;
    private Vector2 maxCameraPoint;
    private Vector3 bounds;
    private Camera cam;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    void Start()
    {
        startPos = transform.position;
        if(startPos.x < target.position.x)
        {
            transform.Rotate(0, 180, 0);
            comesFromLeft = true;
        }
        speedAux = forwardSpeed;
    }

    private void Update()
    {
        if (currentLerpTime > 0.5f && !alreadyShooted)
        {
            alreadyShooted = true;
            shootPos = transform.position;
            var bulletInstance = Instantiate(bullet, bulletSpawnTransform.position, Quaternion.identity);
            var bulletMovement = bulletInstance.GetComponent<CurveBulletMovement>();
            int curveBulletDamage = GetComponent<Entity>().Damage;
            bulletMovement.SetBullet(curveBulletDamage, bulletSpeed);
            bulletMovement.SetNewTarget(target);
        }
        minCameraPoint = cam.ScreenToWorldPoint(new Vector2(0, 0));
        maxCameraPoint = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!alreadyShooted)
        {
            currentLerpTime += Time.deltaTime * forwardSpeed;
            rb.MovePosition(Vector3.Lerp(startPos, target.transform.position, currentLerpTime));
        }
        else
        {
            invertedCurrentLerpTime += Time.deltaTime * forwardSpeed;
            if (!comesFromLeft)
            {
                rb.MovePosition(Vector3.Lerp(shootPos, new Vector3(maxCameraPoint.x + GetComponent<SpriteRenderer>().bounds.extents.x, shootPos.y, 0), invertedCurrentLerpTime));
                if(invertedCurrentLerpTime >= 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                rb.MovePosition(Vector3.Lerp(shootPos, new Vector3(minCameraPoint.x - GetComponent<SpriteRenderer>().bounds.extents.x, shootPos.y, 0), invertedCurrentLerpTime));
                if (invertedCurrentLerpTime >= 1)
                {
                    Destroy(gameObject);
                }
            }
            
            
        }
        
    }
}














