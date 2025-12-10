using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int startScore = 0;
    private int currentScore;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentScore = startScore;
        UpdateScoreUI();
    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }
    public void RemoveScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();

        if (currentScore <= 0)
        {
            Lose();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    private void Lose()
    {
        SceneManager.LoadScene("Restart");
    }
}
