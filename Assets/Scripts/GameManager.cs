using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int startScore = 0;
    private int currentScore;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
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
        currentScore -= amount;
        UpdateScoreUI();

        if (currentScore <= 0)
            Lose();
    }

    private void UpdateScoreUI()
    {
        if (scoreText)
            scoreText.text = $"Score: {currentScore}";
    }

    private void Lose()
    {
        SceneManager.LoadScene("Restart");
    }
}
