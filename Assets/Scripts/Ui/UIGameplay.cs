using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [Header("Gameplay References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CanvasGroup gameplayPanelGroup;
    [SerializeField] private Image levelBoatImage;
    [SerializeField] private TMPro.TextMeshProUGUI levelTextComponent;
    [SerializeField] private Button pauseButton;

    [Header("Gameplay Configurations")] 
    private float boatEndOffset = 200;
    [SerializeField] private bool levelTextFade = true;
    [SerializeField] private float levelTextFadeSpeed = 1;
    [SerializeField] private float levelTextTimeOnScreen = 2;

    [Header("Lost Screen References")]
    [SerializeField] private CanvasGroup lostScreenPanelGroup;
    [SerializeField] private TMPro.TextMeshProUGUI timeSurvivedTextComponent;
    [SerializeField] private Button returnToMainMenuButton;
    [SerializeField] private Button restartButton;

    [Header("Pause References")]
    [SerializeField] CanvasGroup pausePanelGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private IEnumerator fadeInIEnumerator;
    private IEnumerator fadeOutIEnumerator;

    private IEnumerator updateBoatIEnumerator;
    private IEnumerator levelTextIEnumerator;

    private Vector2 levelBoatStartingPosition;
    private Vector2 levelBoatEndingPosition;
    private float levelBoatCurrentFloat;
    private bool updateBoat;

    private void Awake()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumePlay);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        returnToMainMenuButton.onClick.AddListener(GoToMainMenu);
        restartButton.onClick.AddListener(RestartGame);

        gameManager.OnNewLevelStarted += NewLevelStarted;
        gameManager.OnGameLost += OnGameLost;
    }

    private void Start()
    {
        Vector2 anchoredPosition = levelBoatImage.rectTransform.anchoredPosition;
        levelBoatStartingPosition = anchoredPosition;
        levelBoatEndingPosition = anchoredPosition;
        levelBoatEndingPosition.x += boatEndOffset;
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(PauseGame);
        resumeButton.onClick.RemoveListener(ResumePlay);
        mainMenuButton.onClick.RemoveListener(GoToMainMenu);

        returnToMainMenuButton.onClick.RemoveListener(GoToMainMenu);
        restartButton.onClick.RemoveListener(RestartGame);
    }

    private void NewLevelStarted() 
    {
        updateBoat = true;

        levelBoatImage.rectTransform.anchoredPosition = levelBoatStartingPosition;
        levelBoatCurrentFloat = 0;

        if (updateBoatIEnumerator != null) StopCoroutine(updateBoatIEnumerator);
        updateBoatIEnumerator = UpdateBoatCoroutine();
        StartCoroutine(updateBoatIEnumerator);

        if (levelTextIEnumerator != null) StopCoroutine(levelTextIEnumerator);
        levelTextIEnumerator = ShowTextCoroutine();
        StartCoroutine(levelTextIEnumerator);

    }

    private void OnGameLost() 
    {
        if (fadeInIEnumerator != null) StopCoroutine(fadeInIEnumerator);
        if (fadeOutIEnumerator != null) StopCoroutine(fadeOutIEnumerator);

        fadeInIEnumerator = OpenPanelCoroutine(lostScreenPanelGroup);
        fadeOutIEnumerator = ClosePanelCoroutine(gameplayPanelGroup);

        StartCoroutine(fadeInIEnumerator);
        StartCoroutine(fadeOutIEnumerator);

        timeSurvivedTextComponent.text = "Time survived: " + gameManager.GetCurrentTime().ToString("0") + " seconds";
    }

    private void PauseGame() 
    {
        gameManager.ChangeTimeScale(0);

        if (fadeInIEnumerator != null) StopCoroutine(fadeInIEnumerator);
        if (fadeOutIEnumerator != null) StopCoroutine(fadeOutIEnumerator);

        fadeInIEnumerator = OpenPanelCoroutine(pausePanelGroup);
        fadeOutIEnumerator = ClosePanelCoroutine(gameplayPanelGroup);

        StartCoroutine(fadeInIEnumerator);
        StartCoroutine(fadeOutIEnumerator);
    }

    private void ResumePlay() 
    {
        gameManager.ChangeTimeScale(1);

        if (fadeInIEnumerator != null) StopCoroutine(fadeInIEnumerator);
        if (fadeOutIEnumerator != null) StopCoroutine(fadeOutIEnumerator);

        fadeInIEnumerator = OpenPanelCoroutine(gameplayPanelGroup);
        fadeOutIEnumerator = ClosePanelCoroutine(pausePanelGroup);

        StartCoroutine(fadeInIEnumerator);
        StartCoroutine(fadeOutIEnumerator);
    }

    private void RestartGame() 
    {
        FadeSingleton.Get().LoadScene(FadeSingleton.SceneIndex.GAMEPLAY);
    }

    private void GoToMainMenu() 
    {
        FadeSingleton.Get().LoadScene(FadeSingleton.SceneIndex.MAIN_MENU);
    }

    private IEnumerator OpenPanelCoroutine(CanvasGroup group) 
    {
        while(group.alpha < 1) 
        {
            group.alpha += Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private IEnumerator ClosePanelCoroutine(CanvasGroup group)
    {
        while (group.alpha > 0)
        {
            group.alpha -= Time.unscaledDeltaTime;
            yield return null;
        }
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    private IEnumerator UpdateBoatCoroutine() 
    {
        while (updateBoat) 
        {
            levelBoatCurrentFloat = gameManager.GetCurrentTime() / gameManager.GetCurrentLevelMaxTime();
            levelBoatImage.rectTransform.anchoredPosition = Vector3.Lerp(levelBoatStartingPosition, levelBoatEndingPosition, levelBoatCurrentFloat);
            yield return null;
        }
    }

    private IEnumerator ShowTextCoroutine() 
    {
        if (levelTextFade) 
        {
            levelTextComponent.text = "Level " + (gameManager.GetCurrentLevel() + 1).ToString();

            while (levelTextComponent.alpha < 1) 
            {
                levelTextComponent.alpha += Time.deltaTime * levelTextFadeSpeed;
                yield return null;
            }
            levelTextComponent.alpha = 1;

            yield return new WaitForSeconds(levelTextTimeOnScreen);

            while (levelTextComponent.alpha > 0)
            {
                levelTextComponent.alpha -= Time.deltaTime * levelTextFadeSpeed;
                yield return null;
            }
            levelTextComponent.alpha = 0;
        }
    }

}
