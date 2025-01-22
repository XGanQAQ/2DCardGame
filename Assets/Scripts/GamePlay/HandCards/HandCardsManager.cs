using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;


public class HandCardsManager : MonoBehaviour
{
    public int maxHandCardsCount = 8;
    public HandCards MyHandCards; //手牌
    public CardsPool DrawnCardsPool = new CardsPool(60); //抽牌池
    public CardsPool DiscardCardsPool = new CardsPool(60); //弃牌池
    public CardsPool UsedCardsPool = new CardsPool(60); //使用过的卡牌池
    public CardsPool ModelCardsPool = new CardsPool(60); //模板卡牌池
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        MyHandCards= new HandCards (maxHandCardsCount); //手牌
    }
    void Start()
    {
        GameManager.Instance.OnTurnStart+=DrewTwoCard; //回合开始抽两张牌
        GameManager.Instance.OnGameStart+=FirstTurnFunc; //开始游戏的卡牌初始化
        //填充抽牌池
        for (int i = 0; i < 20; i++)
        {
            DrawnCardsPool.AddCard(new MinionCard("小兵"+i,"小兵",1,1,1));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintCardsName(DrawnCardsPool.GetCardsList());
            PrintCardsName(MyHandCards.GetCardsList());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DrawnCardsPool.Shuffle();
        }
    }
    
    private void FirstTurnFunc()
    {
        if (GameManager.Instance.turnCount == 0)
        {
            MyHandCards.DrewCards(DrawnCardsPool,5);//游戏开始抽五张牌
        }
    }
    //从卡池抽一张牌
    private void DrewOneCard()
    {
        MyHandCards.DrewCards(DrawnCardsPool,1);
    }
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
