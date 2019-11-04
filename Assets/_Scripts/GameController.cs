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

    public GameObject highScore;

    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            if(_lives < 1)
            {

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

            if(highScore.GetComponent<HighScore>().score < _score)
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

       Lives = 5;
       Score = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
