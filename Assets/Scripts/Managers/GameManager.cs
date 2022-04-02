using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] ShipPlayer player;
    [Header("Configuration")]
    [SerializeField] private int levelsAmount = 5;

    private int currentLevel = 0;

    public void StartNewLevel() 
    {
        currentLevel++;
        if (currentLevel == levelsAmount) currentLevel = 0;
        SetCurrentLevel();
        StartCurrentLevel();
    }

    private void SetCurrentLevel() 
    {
        enemyManager.SetNewLevel(currentLevel, player);
    }

    private void StartCurrentLevel()
    {
        enemyManager.StartCurrentLevel();
    }

    public void SetTimeScale(float time) 
    {

    }

}
