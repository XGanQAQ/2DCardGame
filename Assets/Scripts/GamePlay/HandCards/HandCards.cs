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
    public void DrewCards(CardsPool card,int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_cards.Count>=MaxHandCardsCount)
            {
                Debug.Log("手牌已满");
                break;
            }
            _cards.Add(card.GetCard());
        }
        TriggerOnCardDrew();
    }
    
    //出牌
    public void PlayHandCard(PlayableCard card)
    {
        if (_cards.Contains(card))
        {
            _cards.Remove(card);
        }
        card.Play();
        TriggerOnCardPlayed();
    }
    
    public List<PlayableCard> GetCardsList()
    {
        return _cards;
    }
}