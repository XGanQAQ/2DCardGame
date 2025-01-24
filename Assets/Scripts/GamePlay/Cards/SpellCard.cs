using UnityEngine;
using System;

namespace GamePlay
{
    public class SpellCard : PlayableCard, ICardPlayableOnBase
    {
        //TODO:完善法术卡的逻辑
        public event Action OnBaseCardEffect;
        public event Action OnCardStateChange; //卡牌状态改变时的回调

        public SpellCard(string name, string description, int playMoney, int maxTriggerCount)
        {
            CardType = PlayableCardType.Spell;
            Name = name;
            Description = description;
            PlayMoney = playMoney;
        }

        public SpellCard()
        {
        }

        public override void PlayCard()
        {
            base.PlayCard();
            //Debug.Log("Play Spell Card: " + Name);
        }

        public virtual void PlayOnBase(Base targetBase, PlayerData playerData)
        {
            playerData.myMoney -= PlayMoney;
            CardEffect(targetBase, playerData);
            //Debug.Log("Spell Card Play On Base: " + Name);
        }

        public override void CardEffect(Base targetBase, PlayerData playerData)
        {
            Effect.TriggerEffect(targetBase, playerData);
            OnBaseCardEffect?.Invoke();
            Debug.Log("Spell Card Effect: " + Name);
        }
    }
}