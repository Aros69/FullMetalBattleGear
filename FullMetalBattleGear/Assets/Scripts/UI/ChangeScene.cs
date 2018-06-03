using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject pressAnyButtonImage;

    [Header("Transition")]
    [SerializeField] float speedFade = 1;
    [SerializeField] Image fadeImage;

    private bool loadingToGame = true;
    private bool onTitleScreen = true;
    private bool isReady;

    private string currentSceneLD;
    private string currentSceneLA;

    private AsyncOperation asyncScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        LoadingScreen.SetActive(false);
        fadeImage.gameObject.SetActive(false);
        pressAnyButtonImage.gameObject.SetActive(false);
        isReady = false;
    }

    void Update()
    {
        if (isReady && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            StartGame();
        }
    }

    public void ChangeScenes()
    {
        StartCoroutine(LoadingCoroutineTitleScreen());
    }

    IEnumerator LoadingCoroutineTitleScreen()
    {
        StartCoroutine(FadeIn());
        Time.timeScale = 0;

        float delay = (1 / speedFade);
        Debug.Log(delay);
        yield return new WaitForSecondsRealtime(delay);

        if (!LoadingScreen.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(false);

            SetLoading(false);
            loadingToGame = false;

            LoadingScreen.SetActive(true);

            AsyncOperation asyncScene1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            while (!asyncScene1.isDone)
                yield return null;

            SetReady();
        }
    }



    void SetReady()
    {
        isReady = true;
        pressAnyButtonImage.SetActive(true);
    }

    void SetLoading(bool fromGame)
    {
        Time.timeScale = 0;
        isReady = false;

        if (fromGame)
            StartCoroutine(FadeIn());
        else
            EndSetLoading();
    }

    void EndSetLoading()
    {
        LoadingScreen.SetActive(true);
    }


    void StartGame()
    {
        LoadingScreen.SetActive(false);
        pressAnyButtonImage.SetActive(false);
        isReady = false;
        Time.timeScale = 1;

        if (loadingToGame)
            StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        //fadeImage.gameObject.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * speedFade;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return new WaitForEndOfFrame();
        }
    }


    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float t = 1;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * speedFade;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return new WaitForEndOfFrame();
        }
        fadeImage.gameObject.SetActive(false);
    }

}
