using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeSingleton : MonoBehaviourSingleton<FadeSingleton>
{
    [SerializeField] private Animator animator;
    [SerializeField] private float fadeSpeed = 1;

    private int sceneIndex;

    public void LoadScene(int scene)
    {
        animator.SetFloat("Speed", fadeSpeed);
        animator.SetTrigger("Fade");
        sceneIndex = scene;
    }

    private void FadeEvent()
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
