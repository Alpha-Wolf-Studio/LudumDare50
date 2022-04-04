using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeSingleton : MonoBehaviourSingleton<FadeSingleton>
{
    [SerializeField] private Animator animator;
    [SerializeField] private float fadeSpeed = 1;

    public enum SceneIndex { MAIN_MENU, GAMEPLAY } 

    private SceneIndex sceneIndex;

    public void LoadScene(SceneIndex scene)
    {
        animator.SetFloat("Speed", fadeSpeed);
        animator.SetTrigger("Fade");
        sceneIndex = scene;
    }

    private void FadeEvent()
    {
        SceneManager.LoadScene((int)sceneIndex);
    }


}
