using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GamePlay
{
    public class Base
    {
        //TODO:基地类
        public event Action OnBaseStateChange; //基地状态改变时的回调
        public event Action<Base> OnBaseDestroy; //基地被摧毁时的回调
        // public event Action<int> OnGetDamage; //基地受到伤害时的回调
        // public event Action<int> OnMoneyChange;
        public string Name { get; set; } 
        public string Describe { get; set; }
        public int MaxCardSlotsNumber { get; set; }
        public List<BaseCardSlot> CardSlotsList;//基地上的卡牌插槽
        public int MaxHp { get; set; }
        private int _currentHp;
        public virtual int CurrentHp
        {
            get { return _currentHp; }
            set
            {
                _currentHp = value;
                OnBaseStateChange?.Invoke();
            }
        }
        
        private int _money;
        public virtual int Money
        {
            get { return _money; }
            set
            {
                _money = value;
                OnBaseStateChange?.Invoke();
            }
        }

        public Base(int maxHp,int money,int maxCardSlotsNumber)
        {
            MaxHp = maxHp;
            _currentHp = MaxHp;
            _money = money;
            MaxCardSlotsNumber = maxCardSlotsNumber;
            CardSlotsList = new List<BaseCardSlot>();
            for (int i = 0; i < maxCardSlotsNumber; i++)
            {
                CardSlotsList.Add(new BaseCardSlot(this));
            }
        }
        
        //回合结束结算注册给GameManager.OnTurnEnd的回调
        public void TurnEndResolution()
        {
            //读取卡槽中的卡牌
            
            //TODO:基地卡在回合结束的时候的结算
            //扣血结算
            //玩家饰品效果结算
            //基地特殊效果结算
            //基地卡槽上的卡效果结算
            foreach (BaseCardSlot cardSolt in CardSlotsList)
            {
                cardSolt.TriggerCardEffect();
            }
            //基地卡槽上的随从卡的攻击力结算
            //TODO:存在结算不正确的BUG
            foreach (BaseCardSlot cardSolt in CardSlotsList)
            {
                cardSolt.MinionAttack();
            }
        }
        
        /// <summary>
        /// 基地被摧毁检查
        /// </summary>
        public bool BaseDestroyCheck()
        {
            if (CurrentHp<=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void DestroyBase()
        {
            OnBaseDestroy?.Invoke(this);
        }
        
        public void GetBaseReward()
        {
            GameManager.Instance.myMoney += Money;
            GameManager.Instance.defeatBaseCount++;
        }
    }
}