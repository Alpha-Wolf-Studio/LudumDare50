using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePause : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void SetTimeScale(float time)
    {
        gameManager.ChangeTimeScale(time);
    }
}
