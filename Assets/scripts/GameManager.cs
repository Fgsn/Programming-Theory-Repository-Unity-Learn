using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public TextMeshProUGUI Text;
    [SerializeField] public Slider slider;
    public bool gameIsOver { get; set; } = false;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerController.OnDied += GameOver;
        PlayerController.OnLiveChange += UpdateHpBar;
    }

    private void OnDestroy()
    {
        PlayerController.OnDied -= GameOver;
        PlayerController.OnLiveChange -= UpdateHpBar;

    }
    public void GameOver()
    {
        Text.text = "Game Over";
        slider.value = 0;
        gameIsOver = true;
        ButtonManager.instance.StopGame(gameIsOver);
    }

    public void StartGame()
    {
        Text.text = "";
        slider.value = slider.maxValue;
        gameIsOver = false;
    }

    public void UpdateHpBar(int maxLive, int live)
    {
        slider.value = (float)live / (float)maxLive;
    }
}
