using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//GameController
//Andrew Trinidad
//301021154
//Last Modified: Oct 4, 2019
//Program Description: This controller runs the main logic of the game 
//and the scene management.

public class GameController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    [Header("UI Management")]
    public Text livesLabel;
    public Text scoreLabel;
    public GameObject startLabel;
    public GameObject endLabel;
    public Text highScoreLabel;
    public Text congratsLabel;
    public GameObject startButton;
    public GameObject restartButton;

    public GameObject highScore;

    public GameObject grid;
    
    //Public Properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            if (_lives < 1)
            {
                SceneManager.LoadScene("End");
            }

            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;

            if (highScore.GetComponent<HighScore>().score < _score)
            {
                highScore.GetComponent<HighScore>().score = _score;
            }

            scoreLabel.text = "Score: " + _score.ToString();

        }
    }

    void Start()
    {
        highScore = GameObject.Find("HighScore");
        startButton = GameObject.Find("StartButton");
        grid = GameObject.Find("Grid");
        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        restartButton = GameObject.Find("RestartButton");

        DontDestroyOnLoad(highScore);
        SceneConfiguration();

    }

    //Manages Scenes and enables/disables various UI elements.
    private void SceneConfiguration()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startButton.SetActive(true);
                grid.SetActive(false);
                startLabel.SetActive(true);
                endLabel.SetActive(false);
                highScoreLabel.enabled = false;
                restartButton.SetActive(false);
                congratsLabel.enabled = false;
                break;
            case "Level1":
                scoreLabel.enabled = true;
                livesLabel.enabled = true;
                startButton.SetActive(false);
                grid.SetActive(true);
                startLabel.SetActive(false);
                endLabel.SetActive(false);
                highScoreLabel.enabled = false;
                restartButton.SetActive(false);
                congratsLabel.enabled = false;
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startButton.SetActive(false);
                grid.SetActive(false);
                startLabel.SetActive(false);
                endLabel.SetActive(true);
                highScoreLabel.enabled = true;
                restartButton.SetActive(true);
                congratsLabel.enabled = false;
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;
            case "Finish":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startButton.SetActive(false);
                grid.SetActive(false);
                startLabel.SetActive(false);
                endLabel.SetActive(false);
                highScoreLabel.enabled = true;
                restartButton.SetActive(true);
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                congratsLabel.enabled = true;
                break;


        }

        Lives = 5;
        Score = 0;
    }

    void Update()
    {

    }

    //When Start Button is Clicked Load Level 1
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

    //When Restart Button is Clicked Load Level 1.
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

}
