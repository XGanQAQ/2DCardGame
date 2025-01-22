using UnityEngine;
using System;
namespace GamePlay
{
    public class MinionCard : PlayableCard,ICardPlayableOnBase
    {
        public event Action OnCardEffect;
        public event Action OnCardStateChange; //卡牌状态改变时的回调

        //TODO:搞清楚卡牌攻击可能会变化的原因，利用属性特性重构，可以在属性变化时触发事件OnCardStateChange
        public int BasicAttack { get; set; }
        public int ActualBasicAttack { get; set; }
        public int Level { get; set; }
        
        public MinionCard(string name, string description, int playMoney, int basicAttack, int level)
        {
            CardType = PlayableCardType.Minion;
            Name = name;
            Description = description;
            PlayMoney = playMoney;
            BasicAttack = basicAttack;
            ActualBasicAttack = basicAttack;
            Level = level;
        }

        public override void Play()
        {
            base.Play();
            Debug.Log("Play Minion Card: " + Name);
        }

        public virtual void Upgrade()
        {
            Debug.Log("Upgrade Minion Card: " + Name);
        }

        public virtual void PlayOnBase(Base targetBase)
        {
            Debug.Log("Minion Card Play On Base: " + Name);
        }

        //每回合结算，触发卡牌的特殊效果
        public virtual void CardEffect(Base targetBase)
        {
            OnCardEffect?.Invoke();
            Debug.Log("Minion Card Effect: " + Name);
        }

        //每回合结束，随从卡攻击基地
        public virtual void AttackBaseResolution(Base targetBase)
        {
            AttackBase(targetBase, ActualBasicAttack);
        }
        #region 随从卡效果内部方法
        private void AttackBase(Base targetBase,int attackCount)
        {
            targetBase.CurrentHp -= attackCount;
        }
        private void IncreaseRewards(Base targetBase,int rewardCount)
        {
            targetBase.Money += rewardCount;
        }

        #endregion
    }
}