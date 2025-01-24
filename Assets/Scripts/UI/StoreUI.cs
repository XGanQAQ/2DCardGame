using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public event Action OnStoreRefresh;
    public Store store;
    private Transform _GroupUI;
    private List<SellCardUI> _sellCardUiList = new List<SellCardUI>();
    public Button CloseButton;
    public Button RefreshButton;
    private void Start()
    {
        store = GameManager.Instance.Store;
        _GroupUI = transform.GetChild(0);
        OnStoreRefresh += store.RefreshShowCard;
        CloseButton.onClick.AddListener(Hide);
        RefreshButton.onClick.AddListener(Refresh);
    }
    
    //生成销售卡牌UI
    private void CreateSellCardUI()
    {
        //TODO:卡牌售价暂时未卡牌打出价格，具体架构生成算法，待补充
        for (int i = 0; i < store.ShowCardList.Count; i++)
        {
            var card = store.ShowCardList[i];
            SellCardUI cardUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SellCardUI"), _GroupUI)
                .GetComponent<SellCardUI>();
            cardUI.Init(card,card.PlayMoney,i);
            _sellCardUiList.Add(cardUI);
            cardUI.OnCardSold += PlayerBuyCard;
        }
    }
    
    public void PlayerBuyCard(PlayableCard card,PlayerData playerData)
    {
        //TODO:购买卡牌逻辑
        store.BuyCard(card, playerData); //调用商店的购买方法
        RemoveProduct(card);
    }
    
    //移除销售卡牌UI
    private void RemoveProduct(PlayableCard  card)
    { 
        SellCardUI sellCardUI =  _sellCardUiList.Find(x=>x.SellCard==card);
        sellCardUI.DestroySellCardUI();
        _sellCardUiList.Remove(sellCardUI);
    }
    //刷新商店卡牌
    public void Refresh()
    {
        if (GameManager.Instance.PlayerData.myMoney>=store.RefreshPrice)
        {
            GameManager.Instance.PlayerData.myMoney -= store.RefreshPrice;
            OnStoreRefresh?.Invoke();
            foreach (Transform child in _GroupUI)
            {
                Destroy(child.gameObject);
            }
            CreateSellCardUI();
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
