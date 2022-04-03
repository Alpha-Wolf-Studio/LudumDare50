using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OceanTransicion : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] image;
    [SerializeField] private float speed = 5f; //va muy lento, poner valores elevados

    private bool firstImageUsed;

    private bool secondImageUsed;
    private bool alreadyFadedDown;

    private void Start()
    {
        firstImageUsed = false;
        secondImageUsed = false;
        alreadyFadedDown = false;
    }

    private void Update()
    {
        ParallaxHor();
    }

    private void ParallaxHor()
    {
        if (image[0].color.a < 1 && !firstImageUsed)
        {
            IncreaseAlpha(image[0]);
            if (image[0].color.a >= 1)
            {
                firstImageUsed = true;
            }
        }
        else if (image[1].color.a < 1 && !secondImageUsed)
        {
            IncreaseAlpha(image[1]);
        }
        else if (image[1].color.a >= 1 && !alreadyFadedDown)
        {
            SetAlpha(image[0], 0);
            alreadyFadedDown = true;
            secondImageUsed = true;
        }
        else if (secondImageUsed && image[1].color.a > 0)
        {
            DecreaseAlpha(image[1]);
        }
        else if (secondImageUsed && image[1].color.a <= 0)
        {
            firstImageUsed = false;
            secondImageUsed = false;
            alreadyFadedDown = false;
        }
    }

    private void DecreaseAlpha(UnityEngine.UI.Image img)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - Time.deltaTime * speed);
    }

    private void IncreaseAlpha(UnityEngine.UI.Image img)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime * speed);
    }

    private void SetAlpha(UnityEngine.UI.Image img, float alpha)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
    }
}
