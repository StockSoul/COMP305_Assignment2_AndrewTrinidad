using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;

    public GameObject startButton;
    public GameObject restartButton;

    public GameObject highScore;

    public GameObject grid;

    public GameObject startLabel;
    public GameObject endLabel;
    public Text highScoreLabel;



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





    // Start is called before the first frame update
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
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;


        }

        Lives = 5;
        Score = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

}
