using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelsTimeConfigurations 
    {
        [SerializeField][HideInInspector] private string name;
        public void SetName(string name) => this.name = name;
        public float timeForNextLevel;
    }

    public System.Action OnNewLevelStarted;

    public System.Action OnGameLost;

    [Header("References")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private ShipPlayer player;
    [Header("Configuration")]
    [SerializeField] private List<LevelsTimeConfigurations> timesConfigurations;

    private int currentLevel = -1;
    public int GetCurrentLevel() => currentLevel;

    private float currentLevelTime = 0;
    private IEnumerator timeIEnumerator;

    private void Awake()
    {
        player.OnDied += PlayerDied;
    }
    private void PlayerDied() 
    {
        ChangeTimeScale(0);
        OnGameLost?.Invoke();
    }

    public void StartNewLevel() 
    {
        IncreaseCurrentLevel();
        SetCurrentLevel();
        StartCurrentLevel();
    }

    private void IncreaseCurrentLevel() 
    {
        currentLevel++;
        if (currentLevel == timesConfigurations.Count)
        {
            currentLevel = 0;
        }
    }

    private void SetCurrentLevel() 
    {
        enemyManager.SetNewLevel(currentLevel, player);
        cameraManager.SetNewLevel(currentLevel);
    }

    private void StartCurrentLevel()
    {
        enemyManager.StartCurrentLevel();

        if (timeIEnumerator != null) StopCoroutine(timeIEnumerator);
        timeIEnumerator = TimeCoroutine();
        StartCoroutine(timeIEnumerator);

        OnNewLevelStarted?.Invoke();
    }

    private IEnumerator TimeCoroutine() 
    {
        currentLevelTime = 0;
        while (currentLevelTime < timesConfigurations[currentLevel].timeForNextLevel) 
        {
            currentLevelTime += Time.deltaTime;
            yield return null;
        }
        StartNewLevel();
    }

    public void ChangeTimeScale(float newScale) 
    {
        Time.timeScale = newScale;
    }

    private void OnValidate()
    {
        for (int i = 0; i < timesConfigurations.Count; i++)
        {
            timesConfigurations[i].SetName("Level " + (i + 1).ToString());
        }
    }

}
