using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager:MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public Action OnGameStart;
    public Action OnGameEnd;
    public Action OnGamePause;
    public Action OnTurnStart;
    public Action OnTurnEnd;
    
    public bool IsGameStart;
    public int defeatBaseCount;
    public int turnCount;
    public int myMoney;
    //public int waste;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Instance.OnGameStart+=InitStartGame;
        Instance.OnTurnEnd+=EndGameCheck;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!IsGameStart)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("End Turn");
            EndTurn();
            Debug.Log("Begin Turn");
            StartTurn();
        }
    }

    public void InitStartGame()
    {
        turnCount = 0;
        myMoney = 10;
        //waste = 5;
    }

    public void EndGameCheck()
    {
        if(myMoney <= 0)
        {
            Debug.Log("myMoney Count <= 0, Game Over");
            EndGame();
        }
        else
        {
            Debug.Log("You have " + myMoney + " myMoney" + " and " + defeatBaseCount + " defeat base");
        }
    }
    
    //触发游戏开始事件
    public void StartGame()
    {
        OnGameStart.Invoke();
    }
    
    //触发游戏结束事件
    public void EndGame()
    {
        OnGameEnd.Invoke();
    }
    
    //触发游戏暂停事件
    public void PauseGame()
    {
        OnGamePause.Invoke();
    }
    
    //触发回合开始事件
    public void StartTurn()
    {
        turnCount++;
        OnTurnStart?.Invoke();
    }
    
    //触发回合结束事件
    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }
}
