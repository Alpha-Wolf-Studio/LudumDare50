#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (GameManager)target;

        if (Application.isPlaying)
        {
            GUILayout.Label("Current Level: " + (myScript.GetCurrentLevel() + 1).ToString());
            GUILayout.Space(10f);
            if (GUILayout.Button("Start Next Level"))
            {
                myScript.StartNewLevel();
            }
        }
    }
}
#endif