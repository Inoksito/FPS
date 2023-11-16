using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score=0;
    public bool gamePaused;
    public static GameManager instance;
    private GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateGamePause();
        }
    }

    private void UpdateGamePause()
    {
        gamePaused = !gamePaused;
        Time.timeScale= (gamePaused ? 0 : 1);
        Cursor.lockState = (gamePaused) ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void UpdateScore(int points)
    {
        score += points;
        HUDController.instance.UpdateScoreHUD(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
