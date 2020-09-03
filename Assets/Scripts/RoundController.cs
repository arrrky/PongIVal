using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    readonly int winScore = 10;    
    bool isRoundActive = false;
    bool isPause = false;

    public BallController BallController;
    public GameObject racketLeft, racketRight, ball, 
                      rightWins, leftWins, pressSpace, menuButton, restartButton, P2, AI, pressEsc, sceneChoice;

    int scoreLeft, scoreRight = 0;
    public Text textScoreLeft, textScoreRight;

    Vector2 ballStartPosition;
    Vector2 racketLeftStartPosition;
    Vector2 racketRightStartPosition;       

    private void Start()
    {
        Time.timeScale = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sceneChoice = GameObject.Find("SceneChoice");
        SceneChoice sceneChoiceScript = sceneChoice.GetComponent<SceneChoice>();
        pressSpace.SetActive(true);        

        switch(sceneChoiceScript.Choice)
        {
            case (int)SceneChoice.GameMode.AI:
                racketRight.GetComponent<EnemyController>().enabled = true;
                AI.SetActive(true);
                break;
            case (int)SceneChoice.GameMode.Player:
                racketRight.GetComponent<MoveRacket>().enabled = true;
                P2.SetActive(true);
                break;
        }
        
        textScoreLeft.text = scoreLeft.ToString();
        textScoreRight.text = scoreRight.ToString();

        racketLeftStartPosition = racketLeft.transform.position;
        racketRightStartPosition = racketRight.transform.position;
        ballStartPosition = ball.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0 && isRoundActive == false)
        {
            isRoundActive = true;
            pressSpace.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1 && isRoundActive == false)
        {
            isRoundActive = false;
            pressSpace.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }        
    }

    public void NewRoundStart(string wallName)
    {
        isRoundActive = false;
        pressSpace.SetActive(true);
        ScoreChange(wallName);
        Time.timeScale = 0;
        PlayFieldInStartPosition(wallName);
        if (!GameOver(scoreLeft, scoreRight))
        {         
            // Если игра еще не закончилась, устанавливает velocity мяча (скорость и направление) в зависимости от того, кто взял раунд
            ball.GetComponent<Rigidbody2D>().velocity = wallName == "WallRight" ? Vector2.right * BallController.ballSpeed : Vector2.left * BallController.ballSpeed;
        }
    }

    void ScoreChange(string wallName)
    {
        switch (wallName)
        {
            case "WallRight":
                scoreLeft++;
                textScoreLeft.text = scoreLeft.ToString();
                break;
            case "WallLeft":
                scoreRight++;
                textScoreRight.text = scoreRight.ToString();
                break;
        }
    }

    bool GameOver(int scoreLeft, int scoreRight)
    {
        if (scoreLeft == winScore)
        {
            SetUIOnGameOver();             
            leftWins.SetActive(true);   
            return true;
        }
        if (scoreRight == winScore)
        {
            SetUIOnGameOver();         
            rightWins.SetActive(true);  
            return true;
        }
        return false;
    }   

    void PlayFieldInStartPosition(string wallName)
    {
        // Ставим мяч на сторону того, кто забил
        ball.transform.position = wallName == "WallRight" ? ballStartPosition : -ballStartPosition;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        RacketsInStartPosition(racketLeft, racketRight);        
    }

    void RacketsInStartPosition(GameObject racketLeft, GameObject racketRight)
    {
        racketLeft.transform.position = racketLeftStartPosition;
        racketRight.transform.position = racketRightStartPosition;
    }    

    // Устанавливаем нужные элементы интерфейса
    void SetUIOnGameOver()
    {
        pressSpace.SetActive(false);
        ball.SetActive(false);
        menuButton.SetActive(true);
        restartButton.SetActive(true);
        restartButton.GetComponent<Button>().Select();
    }

    private void PauseGame()
    {
        isPause = !isPause;
        switch(isPause)
        {
            case true:
                Time.timeScale = 0;
                SetUIOnPause(isPause);
                pressSpace.SetActive(false);
                break;
            case false:
                Time.timeScale = 1;
                SetUIOnPause(isPause);  
                break;     
        }        
    }

    private void SetUIOnPause(bool isPause)
    {
        pressEsc.SetActive(isPause);
        menuButton.SetActive(isPause);
        restartButton.SetActive(isPause);
        restartButton.GetComponent<Button>().Select();      
    }
}
