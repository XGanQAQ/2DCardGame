using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class HandCards
{
    public event Action OnCardDrew;
    public event Action OnCardPlayed;
    public int MaxHandCardsCount { get; set; }
    private List<PlayableCard> _cards;
    public HandCards(int maxCardsCount)
    {
        MaxHandCardsCount = maxCardsCount;
        _cards = new List<PlayableCard>();
    }
    
    public int GetHandCardsCount()
    {
        return _cards.Count;
    }
    private void TriggerOnCardDrew()
    {
        OnCardDrew?.Invoke();
    }
    
    private void TriggerOnCardPlayed()
    {
        OnCardPlayed?.Invoke();
    }
    
    //抽牌
    //从卡池抽取count张牌
    public void DrewCards(CardsPool cardPool,int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_cards.Count>=MaxHandCardsCount)
            {
                Debug.Log("INFO:你的手牌已满，无法继续抽牌");
                break;
            }
            var card = cardPool.GetCard();
            if (card==null)
            {
                Debug.Log("INFO:卡池已空");
                break;
            }
            _cards.Add(cardPool.GetCard());
            TriggerOnCardDrew();
        }
    }
    
    //出牌
    public void PlayHandCard(PlayableCard card)
    {
        if (_cards.Contains(card))
        {
            _cards.Remove(card);
        }
        card.PlayCard();
        TriggerOnCardPlayed();
    }
    
    public List<PlayableCard> GetCardsList()
    {
        return _cards;
    }
}