using System;

namespace GamePlay
{
    public class BaseCardSlot
    {
        public event Action<ICardPlayableOnBase> OnCardInsert;
        private ICardPlayableOnBase _card;
        private Base _targetBase;
        public bool IsCardSlotEmpty = true;
        
        public BaseCardSlot(Base targetBase)
        {
            _targetBase = targetBase;
        }
        public void InsertCard( ICardPlayableOnBase card)
        {
            if (IsCardSlotEmpty)
            {
                IsCardSlotEmpty = false;
                _card = card;
                OnCardInsert?.Invoke(card);
            }
        }
        public void TriggerCardEffect()
        {
            if (_card==null)
            {
                return;
            }
            if (_card is MinionCard minionCard)
            {
                minionCard.CardEffect(_targetBase,GameManager.Instance.PlayerData);
            }
        }
        
        public void MinionAttack()
        {
            if (_card==null)
            {
                return;
            }
            if (_card is MinionCard minionCard)
            {
                minionCard.AttackBaseResolution(_targetBase);
            }
        }

        public Base GetTargetBase()
        {
            return _targetBase;
        }
    }
}