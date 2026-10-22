using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //the three states the game can be in
    public enum GameState
    {
        Start,
        Playing,
        End
    }
    
    //which state the game is in right now
    public GameState state;

    //lets other scripts find this gamemanager
    public static GameManager instance;

    public GameObject startPanel;
    public GameObject endPanel;

    public TMP_Text endScoreText;
    public TMP_Text highScoreText;
    public TMP_Text topScoresText;

    // public float tempNumber;
    // public float firstNumber;
    // public float secondNumber;
    // public float thirdNumber;
    // public float fourthNumber;
    // public float fifthNumber;
    

    //on screen score text
    public TMP_Text scoreText;
    
    //on screen timer text
    public TMP_Text timerText;

    public float timeLimit = 10f;
    
    //players current score which will start at 0
    private int score = 0;

    private int highScore = 0;
        
    private float timeLeft;
    
    private List<int> topScores = new List<int>();

    void Awake()
    {
        //saves this gamemanager so other scripts can reach it
        instance = this;

        timeLeft = timeLimit;
    }
    
    //other scripts will call this to give player a point

    void Start()
    {
        GoToStart();
    }
    
    void Update()
    {
        if (state == GameState.Start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
        else if (state == GameState.Playing)
        {
            timeLeft -= Time.deltaTime;
            
            //shows time rounded up to a whole number
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                timerText.text = "Time: 0";
                EndGame();

                //Debug.Log("Time's up! final score: " + score);
            }
        }
        else if (state == GameState.End)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToStart();
            }
        }
    }

    void GoToStart()
    {
        state = GameState.Start;

        startPanel.SetActive(true);
        endPanel.SetActive(false);
        
        //this will find every piece of candy left over on screen from last round and delete it
        Candy[] leftovers = FindObjectsByType<Candy>(FindObjectsSortMode.None);
        foreach (Candy candy in leftovers)
        {
            Destroy(candy.gameObject);
        }
    }

    void StartGame()
    {
        state = GameState.Playing;
        //resets score and timer
        score = 0;
            timeLeft = timeLimit;
            scoreText.text = "m and m count: 0";
            timerText.text = "Time: " + timeLimit;
            
            startPanel.SetActive(false);
            endPanel.SetActive(false);
    }
    
    void EndGame()
    {
        state = GameState.End;

        if (score > highScore)
        {
            highScore = score;
        }

        endScoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
        
        topScores.Add(score);
        topScores.Sort();
        topScores.Reverse();

        if (topScores.Count > 5)
        {
            topScores.RemoveAt(5);
        }

        string scoreList = "TopScores\n";

        for (int i = 0; i < topScores.Count; i++)
        {
            scoreList += (i + 1) + ". " + topScores[i] + "\n";
        }

        topScoresText.text = scoreList;

        endPanel.SetActive(true);
    }

    void Ranking()
    {
        
    }
    public void AddPoint()
    {
        //ignore catches thaat  happen outside of the play
        if (state != GameState.Playing) return;
        
        score += 1;

        scoreText.text = "m and m count: " + score;
    }
}
