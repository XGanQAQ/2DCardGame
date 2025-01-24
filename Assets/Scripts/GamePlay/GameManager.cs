using System;
using GamePlay;
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
    
    public CardFactory CardFactory;
    public string CardDataPath = "CardConfig";
    
    public HandCardsManager HandCardsManager;
    public Store Store;
    
    public Action OnGameStart;
    public Action OnGameEnd;
    public Action OnGamePause;
    public Action OnTurnStart;
    public Action OnTurnEnd;
    
    public PlayerData PlayerData;
    public bool IsGameStart;

    private void Awake()
    {
        _instance = this;
        CardFactory = new CardFactory(CardDataPath);
        CardFactory.LoadCardData();
        Store = new Store(CardFactory);
    }

    private void Start()
    {
        OnGameStart+=InitStartGame;
        OnTurnEnd+=EndGameCheck;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!IsGameStart)
        {
            StartGame();
            IsGameStart = true;
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
        PlayerData.turnCount = 0;
        PlayerData.myMoney = 100;
        //waste = 5;
    }

    public void EndGameCheck()
    {
    }
    
    //触发游戏开始事件
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
    
    //触发游戏结束事件
    public void EndGame()
    {
        OnGameEnd?.Invoke();
    }
    
    //触发游戏暂停事件
    public void PauseGame()
    {
        OnGamePause?.Invoke();
    }
    
    //触发回合开始事件
    public void StartTurn()
    {
        PlayerData.turnCount++;
        OnTurnStart?.Invoke();
    }
    
    //触发回合结束事件
    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }
}
