using System;

namespace GamePlay
{
    public enum EffectType
    {
        Attack,
        GetMoney,
        DrewCard
    }
    public interface IEffect
    {
        public void TriggerEffect(Base targetBase,PlayerData playerData)
        {

        }
    }
    
    public class AttackEffect : IEffect
    {
        public int AttackValue;
        public void TriggerEffect(Base targetBase,PlayerData playerData)
        {
            targetBase.CurrentHp -= AttackValue;
            // Do something
        }
    }
    
    public class GetMoneyEffect : IEffect
    {
        public int MoneyValue;
        public void TriggerEffect(Base targetBase,PlayerData playerData)
        {
            playerData.myMoney += MoneyValue;
        }
    }
    
    public class DrewCardEffect : IEffect
    {
        public int DrewCardCount;
        public void TriggerEffect(Base targetBase,PlayerData playerData)
        {
            GameManager.Instance.HandCardsManager.DrewCard(DrewCardCount);
        }
    }
}