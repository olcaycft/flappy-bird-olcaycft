using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Player player;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    
    private int score;
    private void Awake()
    {
        Application.targetFrameRate=60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        
        gameOver.SetActive(false);
        playButton.SetActive(false);
        
        Time.timeScale = 1f;
        player.enabled = true;
        
        DeleteAllPipes();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        player.enabled=false;
    }
    
    public void GameOver()
    {
        Pause();
        playButton.SetActive(true);
        gameOver.SetActive(true);
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
 