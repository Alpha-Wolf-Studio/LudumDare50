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
        GUILayout.Space(30f);

        if (GUILayout.Button("Start Next Level"))
        {
            myScript.StartNewLevel();
        }
    }
}
#endif