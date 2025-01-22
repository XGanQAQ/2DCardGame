using System;
using UnityEngine;

namespace GamePlay
{
    public abstract class CardBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    


    public abstract class PlayableCard : CardBase
    {
        public event Action<PlayableCard> OnCardPlayed;
        public PlayableCardType CardType { get; set; }
        public int PlayMoney { get; set; } //打出卡牌所需的金钱
        public int BuyMoney { get; set; } //购买卡牌所需的金钱
        public int SellMoney { get; set; } //出售卡牌所得的金钱
        public virtual void TriggerOnCardPlayed()
        {
            OnCardPlayed?.Invoke(this);
        }
        /// 打出卡牌
        public virtual void Play()
        {
            Debug.Log("Play Card: " + Name);
        }
    }
}