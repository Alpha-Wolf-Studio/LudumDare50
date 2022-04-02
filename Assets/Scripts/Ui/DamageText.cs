using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshPro>();
    }

    public void SetText(float damage, Color color)
    {
        txt.text += damage.ToString();
        txt.color = color;
    }
}
