using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;
public class CardsPool
{
    public int MaxCardsCount { get; set; }
    public List<PlayableCard> Cards;
    
    public CardsPool(int maxCardsCount)
    {
        MaxCardsCount = maxCardsCount;
        Cards = new List<PlayableCard>();
    }
    //填充卡牌池
    public void CloneCardsPool(CardsPool cardsPool)
    {
        Cards.Clear();
        foreach (var card in cardsPool.Cards)
        {
            Cards.Add(card);
        }
        MaxCardsCount = cardsPool.MaxCardsCount;
        Shuffle();
    }

    //洗牌
    public void Shuffle()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            int index = Random.Range(0, Cards.Count);
           PlayableCard temp = Cards[i];
            Cards[i] = Cards[index];
            Cards[index] = temp;
        }
    }
    
    //从卡池中抽取一张卡牌
    public PlayableCard GetCard()
    {
        if (Cards.Count > 0)
        {
            PlayableCard card = Cards.First();
            Cards.RemoveAt(0);
            return card;
        }
        else
        {
            Debug.Log("INFO:卡池已空");
            return null;
        }
    }
    
    public List<PlayableCard> GetCards(int count)
    {
        List<PlayableCard> cards = new List<PlayableCard>();
        for (int i = 0; i < count; i++)
        {
            if (Cards.Count > 0)
            {
                cards.Add(Cards.First());
                Cards.RemoveAt(0);
            }
            else
            {
                Debug.Log("INFO:卡池已空");
                break;
            }
        }
        return cards;
    }

    //返回卡牌池中的卡牌列表
    public List<PlayableCard> GetCardsList()
    {
        return Cards;
    }
    
    //添加卡牌
    public void AddCard(PlayableCard card)
    {
        Cards.Add(card);
    }
    public void AddCards(List<PlayableCard> cards)
    {
        foreach (var card in cards)
        {
            Cards.Add(card);
        }
    }
    //移除卡牌
    public void RemoveCard(PlayableCard card)
    {
        Cards.Remove(card);
    }
}
