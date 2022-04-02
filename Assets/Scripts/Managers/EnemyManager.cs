using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelSpawnConfigurations 
    {
        public string name;

        [Header("Spawn Location Configurations")]
        public float verticalOffset;
        public float horizontalOffset;

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

    [SerializeField] List<LevelSpawnConfigurations> configurations;

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

        float newOffsetX = 0;
        int spawnIndex = 0;
        GameObject enemyToSpawn = null;

        switch (spawnType)
        {
            case SpawnType.UNDERWATER:

                spawnIndex = Random.Range(0, currentConfiguration.underWaterEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.underWaterEnemiesPrefab[spawnIndex];

                newSpawnPosition.y -= currentConfiguration.verticalOffset;
                newOffsetX = Random.Range(-currentConfiguration.horizontalOffset, currentConfiguration.horizontalOffset);
                newSpawnPosition.x += newOffsetX;

                break;

            case SpawnType.ONWATER:

                spawnIndex = Random.Range(0, currentConfiguration.onWaterEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.onWaterEnemiesPrefab[spawnIndex];

                int randomDirection = Random.Range(0, 2);
                newOffsetX = randomDirection == 0 ? currentConfiguration.horizontalOffset : -currentConfiguration.horizontalOffset;
                newSpawnPosition.x += newOffsetX;
                break;

            case SpawnType.AIR:

                spawnIndex = Random.Range(0, currentConfiguration.airEnemiesPrefab.Count);
                enemyToSpawn = currentConfiguration.airEnemiesPrefab[spawnIndex];

                newSpawnPosition.y += currentConfiguration.verticalOffset;
                newOffsetX = Random.Range(-currentConfiguration.horizontalOffset, currentConfiguration.horizontalOffset);
                newSpawnPosition.x += newOffsetX;
                break;

            default:
                break;
        }

        var enemy = Instantiate(enemyToSpawn, newSpawnPosition, Quaternion.identity, transform);

        var enemyMovement = enemy.GetComponent<MovementBase>();
        if(enemyMovement) enemyMovement.SetNewTarget(currentPlayer.transform);
    }

}
