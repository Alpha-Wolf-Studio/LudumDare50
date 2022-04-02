using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelSpawnConfigurations 
    {
        public string name;

        [Header("Spawn Time Configurations")]
        public float timeBetweenSpawns;
        public float minTimeBetweenSpawns;
        public float spawnAccelerationSpeed;

        [Header("Enemies References")]
        public List<GameObject> airEnemiesPrefab;
        public List<GameObject> onWaterEnemiesPrefab;
        public List<GameObject> underWaterEnemiesPrefab;
    }

    private enum SpawnType { UNDERWATER, ONWATER, AIR, SIZE}

    [SerializeField] private List<LevelSpawnConfigurations> configurations;

    private LevelSpawnConfigurations currentConfiguration;
    private ShipPlayer currentPlayer;
    private IEnumerator spawnIEnumerator;
    private float currentTimeBetweenSpawn;

    public void SetNewLevel(int level, ShipPlayer player) 
    {
        currentConfiguration = configurations[level];
        currentPlayer = player;
    }

    public void StartCurrentLevel() 
    {
        if (spawnIEnumerator != null) StopCoroutine(spawnIEnumerator);
        spawnIEnumerator = SpawnCoroutine();
        StartCoroutine(spawnIEnumerator);
    }

    private IEnumerator SpawnCoroutine() 
    {
        currentTimeBetweenSpawn = currentConfiguration.timeBetweenSpawns;
        float t = 0;

        while (true) 
        {
            if (currentTimeBetweenSpawn - currentConfiguration.spawnAccelerationSpeed * Time.deltaTime > currentConfiguration.minTimeBetweenSpawns)
            {
                currentTimeBetweenSpawn -= currentConfiguration.spawnAccelerationSpeed * Time.deltaTime;
            }

            t += Time.deltaTime;
            if(t > currentTimeBetweenSpawn) 
            {
                t = 0;
                SpawnRandomEnemy();
            }

            yield return null;
        }
    }

    public void SpawnRandomEnemy() 
    {

        if(currentPlayer == null) 
        {
            currentPlayer = FindObjectOfType<ShipPlayer>();
            Debug.LogWarning("EnemyManager sin player encontrado. Player buscado a la fuerza");
        }

        if(currentConfiguration == null) 
        {
            currentConfiguration = configurations[0];
            Debug.LogWarning("EnemyManager sin configuracion actual encontrada. Se setea la primera de la lista");
        }

        Vector3 newSpawnPosition = currentPlayer.transform.position;

        SpawnType spawnType = (SpawnType)Random.Range(0, (int)SpawnType.SIZE);

        Camera camera = Camera.main;

        Vector2 minCamera = camera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxCamera = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));

        GameObject enemyToSpawn = null;
        Vector3 EnemyGraphicBoundsExtents = Vector3.zero;

        switch (spawnType)
        {
            case SpawnType.UNDERWATER:

                int underWaterSpawnIndex = Random.Range(0, currentConfiguration.underWaterEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.underWaterEnemiesPrefab[underWaterSpawnIndex];

                var underWaterSpriteRenderer = enemyToSpawn.GetComponent<SpriteRenderer>();
                if(underWaterSpriteRenderer) EnemyGraphicBoundsExtents = underWaterSpriteRenderer.bounds.extents;

                newSpawnPosition.y += minCamera.y - EnemyGraphicBoundsExtents.y;
                newSpawnPosition.x += Random.Range(minCamera.x, maxCamera.x);

                break;

            case SpawnType.ONWATER:

                int onWaterSpawnIndex = Random.Range(0, currentConfiguration.onWaterEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.onWaterEnemiesPrefab[onWaterSpawnIndex];

                var onWaterSpriteRenderer = enemyToSpawn.GetComponent<SpriteRenderer>();
                if (onWaterSpriteRenderer) EnemyGraphicBoundsExtents = onWaterSpriteRenderer.bounds.extents;

                int randomDirection = Random.Range(0, 2);
                newSpawnPosition.x += randomDirection == 0 ? minCamera.x - EnemyGraphicBoundsExtents.x : maxCamera.x + EnemyGraphicBoundsExtents.x;


                break;

            case SpawnType.AIR:

                int airSpawnIndex = Random.Range(0, currentConfiguration.airEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.airEnemiesPrefab[airSpawnIndex];

                var airSpriteRenderer = enemyToSpawn.GetComponent<SpriteRenderer>();
                if (airSpriteRenderer) EnemyGraphicBoundsExtents = airSpriteRenderer.bounds.extents;

                newSpawnPosition.y += maxCamera.y + EnemyGraphicBoundsExtents.y;
                newSpawnPosition.x += Random.Range(minCamera.x, maxCamera.x);

                break;

            default:
                break;
        }

        var enemyGameobject = Instantiate(enemyToSpawn, newSpawnPosition, Quaternion.identity, transform);

        var enemyMovement = enemyGameobject.GetComponent<MovementBase>();
        if(enemyMovement) enemyMovement.SetNewTarget(currentPlayer.transform);

        
    }
}
