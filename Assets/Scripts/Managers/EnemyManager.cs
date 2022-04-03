using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelSpawnConfigurations 
    {
        [SerializeField][HideInInspector] private string name;
        public void SetName(string name) => this.name = name;

        [Header("Spawn Time Configurations")]
        public float timeBetweenSpawns;
        public float minTimeBetweenSpawns;
        public float spawnAccelerationSpeed;

        [Header("Enemies References")]
        public List<EnemySpawn> enemiesSpawn;
    }



    [System.Serializable]
    public class EnemySpawn 
    {
        [SerializeField][HideInInspector] private string name;
        public void SetName(string name) => this.name = name; 
        public enum SpawnType { UNDERWATER, ONWATER, AIR, AIR_SIDE, AIR_CENTER, WATER_CENTER}
        public SpawnType currentSpawnType;
        public GameObject prefab;

        public Vector3 GetSpawnPosition(Vector3 playerPosition) 
        {

            Camera camera = Camera.main;

            Vector2 minCamera = camera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 maxCamera = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));

            Vector3 newSpawnPosition = playerPosition;
            Vector3 EnemyGraphicBoundsExtents = Vector3.zero;

            switch (currentSpawnType)
            {
                case SpawnType.UNDERWATER:

                    var underWaterSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (underWaterSpriteRenderer) EnemyGraphicBoundsExtents = underWaterSpriteRenderer.bounds.extents;

                    newSpawnPosition.y += minCamera.y - EnemyGraphicBoundsExtents.y;
                    newSpawnPosition.x += Random.Range(minCamera.x, maxCamera.x);

                    break;


                case SpawnType.ONWATER:

                    var onWaterSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (onWaterSpriteRenderer) EnemyGraphicBoundsExtents = onWaterSpriteRenderer.bounds.extents;

                    int randomDirection = Random.Range(0, 2);
                    newSpawnPosition.x += randomDirection == 0 ? minCamera.x - EnemyGraphicBoundsExtents.x : maxCamera.x + EnemyGraphicBoundsExtents.x;

                    break;


                case SpawnType.AIR:

                    var airSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (airSpriteRenderer) EnemyGraphicBoundsExtents = airSpriteRenderer.bounds.extents;

                    newSpawnPosition.y += maxCamera.y + EnemyGraphicBoundsExtents.y;
                    newSpawnPosition.x += Random.Range(minCamera.x, maxCamera.x);

                    break;


                case SpawnType.AIR_SIDE:

                    var airSideSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (airSideSpriteRenderer) EnemyGraphicBoundsExtents = airSideSpriteRenderer.bounds.extents;

                    int newRandomDirection = Random.Range(0, 2);
                    newSpawnPosition.x += newRandomDirection == 0 ? minCamera.x - EnemyGraphicBoundsExtents.x : maxCamera.x + EnemyGraphicBoundsExtents.x;
                    newSpawnPosition.y = maxCamera.y - EnemyGraphicBoundsExtents.y;

                    break;

                case SpawnType.AIR_CENTER:
                    var airCenterSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (airCenterSpriteRenderer) EnemyGraphicBoundsExtents = airCenterSpriteRenderer.bounds.extents;
                    newSpawnPosition.x = Mathf.Lerp(minCamera.x, maxCamera.x, 0.5f);
                    newSpawnPosition.y = maxCamera.y + EnemyGraphicBoundsExtents.y;
                    break;

                case SpawnType.WATER_CENTER:
                    var waterCenterSpriteRenderer = prefab.GetComponent<SpriteRenderer>();
                    if (waterCenterSpriteRenderer) EnemyGraphicBoundsExtents = waterCenterSpriteRenderer.bounds.extents;
                    newSpawnPosition.x = Mathf.Lerp(minCamera.x, maxCamera.x, 0.5f);
                    newSpawnPosition.y = maxCamera.y - EnemyGraphicBoundsExtents.y;
                    break;

                default:
                    break;
            }
            return newSpawnPosition;
        }
    }

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

        if (currentConfiguration.enemiesSpawn.Count == 0) return;
        int enemyIndex = Random.Range(0, currentConfiguration.enemiesSpawn.Count);
        var enemyToSpawn = currentConfiguration.enemiesSpawn[enemyIndex];

        Vector3 newSpawnPosition = enemyToSpawn.GetSpawnPosition(currentPlayer.transform.position);

        var enemyGameobject = Instantiate(enemyToSpawn.prefab, newSpawnPosition, Quaternion.identity, transform);

        var enemyMovement = enemyGameobject.GetComponent<MovementBase>();
        if(enemyMovement) enemyMovement.SetNewTarget(currentPlayer.transform);

        
    }

    private void OnValidate()
    {
        for (int i = 0; i < configurations.Count; i++)
        {
            configurations[i].SetName("Level " + (i + 1).ToString());
            for (int j = 0; j < configurations[i].enemiesSpawn.Count; j++)
            {
                configurations[i].enemiesSpawn[j].SetName("Enemy " + (j + 1).ToString());
            }
        }
    }
}
