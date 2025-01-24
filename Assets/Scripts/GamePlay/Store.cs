using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;

[Serializable]
public class Store
{
    
    [SerializeField]private int MaxCardStorageCount = 60;
    public List<PlayableCard> CardStorageList= new List<PlayableCard>();
    public List<PlayableCard> ShowCardList = new List<PlayableCard>();
    public int StoreLevel = 1;
    public int ShowCardsCount = 5;
    public int RefreshPrice = 5;
    private CardFactory _cardFactory;
    
    public Store(CardFactory cardFactory)
    {
        _cardFactory = cardFactory;
        FillCardStorage();
    }
    
    
    /// <summary>
    /// 在商店购买一张卡
    /// </summary>
    /// <param name="index">卡牌在货架的位置</param>
    /// <param name="playerData">购买者的数据</param>
    /// <returns></returns>
    public void BuyCard(PlayableCard playableCard,PlayerData playerData)
    {
        if (playerData.myMoney>=playableCard.PlayMoney)
        {
            playerData.myMoney -=playableCard.PlayMoney;
            ShowCardList.Remove(playableCard);
            GameManager.Instance.HandCardsManager.DrawnCardsPool.AddCard(playableCard);
        }
        else
        {
            Debug.Log("金币不足，无法购买");
        }
    }
    
    //从仓库中提取出卡牌，放置到展示列表中
    public void RefreshShowCard()
    {
        ShowCardList.Clear();
        if (ShowCardsCount>CardStorageList.Count)
        {
            FillCardStorage();
        }
        for (int i = 0; i < ShowCardsCount; i++)
        {
            ShowCardList.Add(CardStorageList.First());
            CardStorageList.RemoveAt(0);
        }
    }
    
    //填充卡牌仓库
    private void FillCardStorage()
    {
        CardStorageList.Clear();
        for (int i = 0; i < MaxCardStorageCount; i++)
        {
            CardStorageList.Add(_cardFactory.RandomCreateCard());
        }
    }
}
