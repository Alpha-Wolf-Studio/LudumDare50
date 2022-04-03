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
        public List<GameObject> airSideEnemiesPrefab;
        public List<GameObject> onWaterEnemiesPrefab;
        public List<GameObject> underWaterEnemiesPrefab;
    }

    private enum SpawnType { UNDERWATER, ONWATER, AIR, AIR_SIDE, SIZE}

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
        StopCurrentLevel();
        spawnIEnumerator = SpawnCoroutine();
        StartCoroutine(spawnIEnumerator);
    }

    public void StopCurrentLevel() 
    {
        if (spawnIEnumerator != null) StopCoroutine(spawnIEnumerator);
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

        var valid = CheckIfTypeIsValid(spawnType);
        if (valid == typeValidity.COMPLETE_NULL) return;

        while (CheckIfTypeIsValid(spawnType) != typeValidity.OK)
        {
            spawnType = (SpawnType)Random.Range(0, (int)SpawnType.SIZE);
        }

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

            case SpawnType.AIR_SIDE:

                int airSideSpawnIndex = Random.Range(0, currentConfiguration.airSideEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.airSideEnemiesPrefab[airSideSpawnIndex];

                var airSideSpriteRenderer = enemyToSpawn.GetComponent<SpriteRenderer>();
                if (airSideSpriteRenderer) EnemyGraphicBoundsExtents = airSideSpriteRenderer.bounds.extents;

                int newRandomDirection = Random.Range(0, 2);
                newSpawnPosition.x += newRandomDirection == 0 ? minCamera.x - EnemyGraphicBoundsExtents.x : maxCamera.x + EnemyGraphicBoundsExtents.x;
                newSpawnPosition.y = maxCamera.y - EnemyGraphicBoundsExtents.y;

                break;

            default:
                break;
        }

        var enemyGameobject = Instantiate(enemyToSpawn, newSpawnPosition, Quaternion.identity, transform);

        var enemyMovement = enemyGameobject.GetComponent<MovementBase>();
        if(enemyMovement) enemyMovement.SetNewTarget(currentPlayer.transform);

        
    }

    enum typeValidity { COMPLETE_NULL, TYPE_NULL, OK}

    private typeValidity CheckIfTypeIsValid(SpawnType type) 
    {
        int underWaterAmount = currentConfiguration.underWaterEnemiesPrefab.Count;
        int onWaterAmount = currentConfiguration.onWaterEnemiesPrefab.Count;
        int airAmount = currentConfiguration.airEnemiesPrefab.Count;
        int airSideAmount = currentConfiguration.airSideEnemiesPrefab.Count;

        int fullAmount = underWaterAmount + onWaterAmount + airAmount + airSideAmount;

        if (fullAmount == 0) return typeValidity.COMPLETE_NULL;


        switch (type)
        {
            case SpawnType.UNDERWATER:
                if (currentConfiguration.underWaterEnemiesPrefab.Count > 0) return typeValidity.OK;
                break;
            case SpawnType.ONWATER:
                if (currentConfiguration.onWaterEnemiesPrefab.Count > 0) return typeValidity.OK;
                break;
            case SpawnType.AIR:
                if (currentConfiguration.airEnemiesPrefab.Count > 0) return typeValidity.OK;
                break;
            case SpawnType.AIR_SIDE:
                if (currentConfiguration.airSideEnemiesPrefab.Count > 0) return typeValidity.OK;
                break;
        }

        return typeValidity.TYPE_NULL;
    }

}
