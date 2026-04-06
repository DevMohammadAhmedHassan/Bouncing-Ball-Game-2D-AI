using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject pausePanel;
    public GameObject BG;
    public GameObject losePanel;

    public BallController ballController;
    public TileColor[] allTiles;

    bool gameOver = false;
    

    void Start()
    {
        Application.targetFrameRate =60;
        score = 0;
        scoreText.text = "0";

        UpdateDifficulty();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void UpdateDifficulty()
    {
        if (score < 200)
        {
            ballController.bounceForce = 7f;
            ballController.rb.gravityScale = 1;
            ballController.activeColorCount = 2;
            ActivateTiles(2);
        }
        else if (score < 400)
        {
            ballController.bounceForce = 9f;
            ballController.rb.gravityScale = 1.3f;
            ballController.activeColorCount = 3;
            ActivateTiles(3);
        }
        else
        {
            ballController.bounceForce = 10f;
            ballController.rb.gravityScale = 1.5f;
            ballController.activeColorCount = 4;
            ActivateTiles(4);
        }
    }

    void ActivateTiles(int count)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            bool shouldBeActive = i < count;
            allTiles[i].gameObject.SetActive(shouldBeActive);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "" + score.ToString();

        UpdateDifficulty();
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("GAME OVER");

        finalScoreText.text = "" + score.ToString();
        losePanel.SetActive(true);
        BG.SetActive(false);
        Time.timeScale = 0f;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
