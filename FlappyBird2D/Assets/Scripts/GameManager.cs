using System;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text yourScoreText;
    [SerializeField] private Text bestScoreText;

    [SerializeField] private Player player;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject scoreBoard;

    [SerializeField] private bool isFirstSession;

    public static bool isBirdEnable;

    private int score;
    private void Awake()
    {
        isBirdEnable = true;
        isFirstSession = true;
        Pause();
    }

    public void Play()
    {
        scoreText.enabled = true;
        isBirdEnable = true;
        score = 0;
        scoreText.text = score.ToString();

        gameOver.SetActive(false);
        playButton.SetActive(false);
        scoreBoard.SetActive(false);
        yourScoreText.enabled = false;
        bestScoreText.enabled = false;

        Time.timeScale = 1f;
        player.enabled = true;

        DeleteAllPipes();
    }

    private void Pause()
    {
        scoreText.enabled = false;
        if (isFirstSession)
        {
            scoreText.enabled = true;
            gameOver.SetActive(false);
            scoreBoard.SetActive(false);
            yourScoreText.enabled = false;
            bestScoreText.enabled = false;
        }
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        isBirdEnable = false;
        isFirstSession = false;

        playButton.SetActive(true);
        gameOver.SetActive(true);
        scoreBoard.SetActive(true);
        yourScoreText.enabled = true;
        bestScoreText.enabled = true;
        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        yourScoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void DeleteAllPipes()
    {
        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();
        foreach (var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }
}
