using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;
    public GameObject PlayerModel1;
    public GameObject PlayerModel2;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    int playerHealth = 100;
    int points;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        // Disable both first
        PlayerModel1.SetActive(false);
        PlayerModel2.SetActive(false);

        // Enable based on GameManager side
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.side == Side.Israel)
            {
                PlayerModel1.SetActive(true);
                PlayerModel2.SetActive(false);
            }
            else if (GameManager.Instance.side == Side.Palestine)
            {
                PlayerModel1.SetActive(false);
                PlayerModel2.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Wynik: " + points.ToString();
        hpText.text = "HP: " + playerHealth.ToString();
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public void ReducePlayerHealth(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        playerHealth = 100;
        points = 0;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
