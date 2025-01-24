using System;
using UnityEngine;

namespace GamePlay
{
    public abstract class PlayableCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PlayableCardType CardType { get; set; }
        public CardRarity Rarity { get; set; }
        public int Level { get; set; }
        public int PlayMoney { get; set; } //打出卡牌所需的金钱
        public int Attack { get; set; }
        public IEffect Effect { get; set; }
        public event Action<PlayableCard> OnCardPlayed;
        /// 打出卡牌
        public virtual void PlayCard()
        {
            //Debug.Log("Play Card: " + Name);
            OnCardPlayed?.Invoke(this);
        }
        
        public virtual void CardEffect(Base targetBase,PlayerData playerData)
        {
            Effect.TriggerEffect(targetBase, playerData);
        }
    }
}