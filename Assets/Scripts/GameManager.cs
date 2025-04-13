using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    GameObject gameOverText;

    [SerializeField]
    GameObject gameClearText;

    int score = 0;
    int highScore = 0;
    bool isGameOver;
    int activeEnemyNum = 0;
    bool waitForRestart = false;

    static string highScoreKey = "highScore";

    static GameManager instance;
    public static GameManager Instance
    {
        get {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    Debug.LogError(typeof(GameManager));
                }
            }
            return instance;
        }
    }

    public static bool IsGameOver { 
        get { return Instance.isGameOver; }
        set {
            if (value == true)
            {
                Instance.gameOverText.SetActive(true);
                Instance.waitForRestart = true;
            }
            Instance.isGameOver = value;
        }
    }

    public static int ActiveEnemyNum
    {
        get { return Instance.activeEnemyNum; }
        set {
            if (value < 1)
            {
                Instance.gameClearText.SetActive(true);
                Instance.waitForRestart = true;
            }
            Instance.activeEnemyNum = value;
        }
    }

    public static void AddScore(int scoreToAdd)
    {
        Instance.score += scoreToAdd;
        Instance.scoreText.text = "SCORE: " + Instance.score;
        if (Instance.highScore < Instance.score)
        {
            PlayerPrefs.SetInt(highScoreKey, Instance.score);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameClearText.SetActive(false);
        gameOverText.SetActive(false);
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        scoreText.text = "SCORE  " + score;
        highScoreText.text = "HIGH SCORE  " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForRestart)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
