using UnityEngine;
using System;

namespace GamePlay
{
    public class SpellCard : PlayableCard,ICardPlayableOnBase
    {
        //TODO:完善法术卡的逻辑
        public event Action OnCardEffect;
        public event Action OnCardStateChange; //卡牌状态改变时的回调

        //可以在属性变化时触发事件OnCardStateChange
        public int  MaxTriggerCount { get; set; }
        public int  CurrentTriggerCount { get; private set; }

        public SpellCard(string name, string description, int playMoney, int maxTriggerCount)
        {
            CardType = PlayableCardType.Spell;
            Name = name;
            Description = description;
            PlayMoney = playMoney;
            MaxTriggerCount = maxTriggerCount;
        }
        
        /// <summary>
        /// 当卡牌被打出时的效果
        /// </summary>
        public override void Play()
        {
            Debug.Log("Play Spell Card: " + Name);
            TriggerOnCardPlayed();
        }


        public void PlayOnBase(Base targetBase)
        {
            Debug.Log("Spell Card Play On Base: " + Name);
        }
        public  void CardEffect(Base targetBase)
        {
            OnCardEffect?.Invoke();
            Debug.Log("Spell Card Effect: " + Name);
        }
        
        //一堆内部调用的方法
        //包含了基本的法术效果
        //用于子类调用
        #region 法术卡效果内部方法
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