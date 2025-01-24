using UnityEngine;
using System;

namespace GamePlay
{
    public class MinionCard : PlayableCard, ICardPlayableOnBase
    {
        public event Action OnBaseCardEffect;
        public event Action OnCardStateChange; //卡牌状态改变时的回调

        //TODO:搞清楚卡牌攻击可能会变化的原因，利用属性特性重构，可以在属性变化时触发事件OnCardStateChange
        public int BuffAttack { get; set; } = 0;

        public int ActualBasicAttack
        {
            get => Attack + BuffAttack;
        }

        public MinionCard(string name, string description, int playMoney, int attack, int level)
        {
            CardType = PlayableCardType.Minion;
            Name = name;
            Description = description;
            PlayMoney = playMoney;
            Attack = attack;
            Level = level;
        }

        public MinionCard()
        {
        }

        public override void PlayCard()
        {
            base.PlayCard();
           // Debug.Log("Play Minion Card: " + Name);
        }

        public virtual void Upgrade()
        {
            //Debug.Log("Upgrade Minion Card: " + Name);
        }

        //在基地上使用卡牌时，触发的效果
        public virtual void PlayOnBase(Base targetBase, PlayerData playerData)
        {
            playerData.myMoney -= PlayMoney;
            //Debug.Log("Minion Card Play On Base: " + Name);
        }

        //每回合结算，触发卡牌的特殊效果
        public override void CardEffect(Base targetBase,PlayerData playerData)
        {
            Effect.TriggerEffect(targetBase, playerData);
            OnBaseCardEffect?.Invoke();
            //Debug.Log("Minion Card Effect: " + Name);
        }

        //每回合结束，随从卡攻击基地
        public virtual void AttackBaseResolution(Base targetBase)
        {
            AttackBase(targetBase, ActualBasicAttack);
        }

        private void AttackBase(Base targetBase, int attackCount)
        {
            targetBase.CurrentHp -= attackCount;
        }
    }
}