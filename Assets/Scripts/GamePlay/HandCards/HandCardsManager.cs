using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;


public class HandCardsManager : MonoBehaviour
{
    public int maxHandCardsCount = 8;
    public HandCards MyHandCards; //手牌
    public CardsPool DrawnCardsPool; //抽牌池
    public CardsPool DiscardCardsPool; //弃牌池
    public CardsPool UsedCardsPool; //使用过的卡牌池
    public CardsPool ModelCardsPool; //模板卡牌池
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        MyHandCards= new HandCards (maxHandCardsCount); //手牌
        DrawnCardsPool = new CardsPool(60); //抽牌池
        DiscardCardsPool = new CardsPool(60); //弃牌池
        UsedCardsPool = new CardsPool(60); //使用过的卡牌池
        ModelCardsPool = new CardsPool(60); //模板卡牌池
    }
    void Start()
    {
        GameManager.Instance.HandCardsManager = this;
        //填充抽牌池
        GameManager.Instance.OnGameStart+= ()=>
        {
            for (int i = 0; i < 20; i++)
            {
                DrawnCardsPool.AddCard(GameManager.Instance.CardFactory.RandomCreateCard());
            }
        }; 
        GameManager.Instance.OnTurnStart+=DrewTwoCard; //回合开始抽两张牌
        GameManager.Instance.OnGameStart+=FirstTurnFunc; //开始游戏的卡牌初始化
    }
    
    private void FirstTurnFunc()
    {
        if (GameManager.Instance.PlayerData.turnCount == 0)
        {
            MyHandCards.DrewCards(DrawnCardsPool,5);//游戏开始抽五张牌
        }
    }
    //从卡池抽一张牌
    public void DrewCard(int count)
    {
        MyHandCards.DrewCards(DrawnCardsPool,count);
    }
    
    //从卡池抽两张牌，用来注册回合开始事件的方法
    private void DrewTwoCard()
    {
        MyHandCards.DrewCards(DrawnCardsPool,2);
    }
    
    #region PrintTest
    public static void PrintCardsName(List<PlayableCard> cards)
    {
        
        string cardsName="";
        
        foreach (PlayableCard card in cards)
        {
            cardsName += card.Name + " ";
        }
        Debug.Log(nameof(cards) +"  : " + cardsName);
    }
    #endregion
}
