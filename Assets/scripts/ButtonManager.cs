using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public Transform mainCan;
    public Transform menuCan;
    public Transform pauseScreen;
    private bool gameOnPause = false;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLoaded;
        SingletonCan.OnInstaniate += InitCan;

    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLoaded;
        SingletonCan.OnInstaniate -= InitCan;
    }

    private void InitCan()
    {
        mainCan = SingletonCan.instance.transform.Find("Main");
        menuCan = SingletonCan.instance.transform.Find("Menu");
        pauseScreen = mainCan.Find("Pause");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOnPause = !gameOnPause;
            StopGame(gameOnPause);
        }
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
        SceneManager.LoadScene(1);
        Reset();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Reset();
    }

    public void StopGame(bool stop)
    {
        if (stop)
        {
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0.0f;

        }
        else
        {
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1.0f;

        }
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            GameManager.instance.StartGame();
            mainCan.gameObject.SetActive(true);
            menuCan.gameObject.SetActive(false);

        }
        else if (scene.name == "Menu")
        {
            if (!mainCan)
                return;
            mainCan.gameObject.SetActive(false);
            menuCan.gameObject.SetActive(true);
        }
    }

    private void Reset()
    {
        pauseScreen.gameObject.SetActive(false);
        gameOnPause = false;
        Time.timeScale = 1.0f;
    }
}
