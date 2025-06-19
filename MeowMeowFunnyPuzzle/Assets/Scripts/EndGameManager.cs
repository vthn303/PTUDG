using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 


public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
    public int couterValue;
}

public class EndGameManager : MonoBehaviour
{
    public EndGameRequirements requirements;
    public GameObject movesLabel, timeLabel;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public Text counter;
    public int currentCouterValue;
    private Board board;
    private float timerSeconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetupGame();
        
    }

    void SetupGame()
    {
        currentCouterValue = requirements.couterValue;
        if (requirements.gameType == GameType.Moves)
        {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }
        else
        {
            timerSeconds = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" + currentCouterValue;
    }


    public void DecreaseCounterValue()
    {
        if(board.currentState != GameState.pause)
        {
            currentCouterValue--;
            counter.text = "" + currentCouterValue;
            if (currentCouterValue <= 0)
            {
                LoseGame();
            }

        }
        

    }

    public void WinGame()
    {
        youWinPanel.SetActive(true);
        board.currentState = GameState.win;
        currentCouterValue = 0;
        counter.text = "" + currentCouterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();

    }

    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("Lose");
        currentCouterValue = 0;
        counter.text = "" + currentCouterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();

    }
    // Update is called once per frame
    void Update()
    {
        if(requirements.gameType == GameType.Time && currentCouterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if(timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
