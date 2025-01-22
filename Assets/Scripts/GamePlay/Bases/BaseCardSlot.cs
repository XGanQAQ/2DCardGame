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
        public void InsertCard(ICardPlayableOnBase card)
        {
            if (IsCardSlotEmpty)
            {
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
            _card.CardEffect(_targetBase);
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
    }
}