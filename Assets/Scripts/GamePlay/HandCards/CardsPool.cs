using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;
public class CardsPool
{
    public int MaxCardsCount { get; set; }
    private List<PlayableCard> _cardsPool;
    
    public CardsPool(int maxCardsCount)
    {
        MaxCardsCount = maxCardsCount;
        _cardsPool = new List<PlayableCard>();
    }
    //填充卡牌池
    public void CloneCardsPool(CardsPool cardsPool)
    {
        _cardsPool.Clear();
        foreach (var card in cardsPool._cardsPool)
        {
            _cardsPool.Add(card);
        }
        MaxCardsCount = cardsPool.MaxCardsCount;
        Shuffle();
    }

    //洗牌
    public void Shuffle()
    {
        for (int i = 0; i < _cardsPool.Count; i++)
        {
            int index = Random.Range(0, _cardsPool.Count);
           PlayableCard temp = _cardsPool[i];
            _cardsPool[i] = _cardsPool[index];
            _cardsPool[index] = temp;
        }
    }
    
    //从卡池中抽取一张卡牌
    public PlayableCard GetCard()
    {
        if (_cardsPool.Count > 0)
        {
            PlayableCard card = _cardsPool.First();
            _cardsPool.RemoveAt(0);
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
            if (_cardsPool.Count > 0)
            {
                cards.Add(_cardsPool.First());
                _cardsPool.RemoveAt(0);
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
        return _cardsPool;
    }
    
    //添加卡牌
    public void AddCard(PlayableCard card)
    {
        _cardsPool.Add(card);
    }
    public void AddCards(List<PlayableCard> cards)
    {
        foreach (var card in cards)
        {
            _cardsPool.Add(card);
        }
    }
    //移除卡牌
    public void RemoveCard(PlayableCard card)
    {
        _cardsPool.Remove(card);
    }
}
