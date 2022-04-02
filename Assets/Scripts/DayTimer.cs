using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{
    [SerializeField] private RectTransform[] images = null;
    [SerializeField] private float speed = 0f; //va muy lento, poner valores elevados

    private float height = 0f;
    private float start = 0f;
    private float end = 0f;

    private void Start()
    {
        height = images[0].rect.height;
        start = height * (images.Length - 1);
        end = -height;

        images[0].anchoredPosition = new Vector2(0f, height);
        images[images.Length - 1].anchoredPosition = new Vector2(0f, end);
    }

    private void Update()
    {
        ParallaxHor();
    }

    private void ParallaxHor()
    {
        float despY = speed * Time.deltaTime;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition -= new Vector2(0f, despY);

            if (images[i].anchoredPosition.y < end)
            {
                Vector2 pos = images[i].anchoredPosition;
                images[i].anchoredPosition = new Vector2(pos.x, start);
            }
        }
    }
}
