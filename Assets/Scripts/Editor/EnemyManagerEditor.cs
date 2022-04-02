#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (EnemyManager)target;
        GUILayout.Space(30f);

        if (GUILayout.Button("Spawn Random Enemy"))
        {
            myScript.SpawnRandomEnemy();
        }
    }
}
#endif