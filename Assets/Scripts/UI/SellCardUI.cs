using System;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SellCardUI : MonoBehaviour,IPointerDownHandler
{
   public event Action<PlayableCard,PlayerData> OnCardSold;
   private int _myIndexInStore;
   public PlayableCard SellCard;
   private GameObject _cardBack;
   public Text _leftTopText;
   public Text _rightTopText;
   public Text _desciptionText;
   private Image _image;

   
   public Text _priceText;
   private void Start()
   {
      _cardBack = transform.GetChild(0).gameObject;
      _image = _cardBack.GetComponent<Image>();
   }
   
   public void Init(PlayableCard card,int price,int index)
   {
      SellCard= card;
      _myIndexInStore = index;
      _priceText.text = price.ToString();
      RefreshCardState();
   }
   private void RefreshCardState()
   {
      if (SellCard is MinionCard minionCard)
      {
         _leftTopText.text = minionCard.ActualBasicAttack.ToString();
         _rightTopText.text = minionCard.PlayMoney.ToString();
         _desciptionText.text = minionCard.Description;
      }
      else if (SellCard is SpellCard spellCard)
      {
         _leftTopText.text = " ";
         _rightTopText.text = spellCard.PlayMoney.ToString();
         _desciptionText.text = spellCard.Description;
      }
   }
   
   public void DestroySellCardUI()
   {
      Destroy(gameObject);
   }
   
   //点击购买卡牌，触发购买事件
   public void OnPointerDown(PointerEventData eventData)
   {
      BuyThisCard();
   }
   private void BuyThisCard()
   {
      Debug.Log($"INFO:Player Decide to buy {_myIndexInStore} Card");
      OnCardSold?.Invoke(SellCard,GameManager.Instance.PlayerData);
   }
}
