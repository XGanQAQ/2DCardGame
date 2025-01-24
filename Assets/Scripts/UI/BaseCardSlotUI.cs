using System;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BaseCardSlotUI : MonoBehaviour
    {
        private Selectable _selectable;
        private BaseCardSlot _baseCardSlot;
        private BaseMinionCardUI _baseMinionCardUI;

        private void Start()
        {
            _selectable = GetComponent<Selectable>();
            _baseCardSlot.OnCardInsert += CreateBaseCardUI;
        }

        private void Update()
        {

        }

        public Base GetDependentBase()
        {
           return  _baseCardSlot.GetTargetBase();
        }
        
        public void InitBaseCardSlotUI(BaseCardSlot baseCardSlot)
        {
            _baseCardSlot = baseCardSlot;
            _baseCardSlot.OnCardInsert += TriggerCardInsertAnimation;
        }

        public bool IsEmpty()
        {
            return _baseCardSlot.IsCardSlotEmpty;
        }
        //插入卡牌,用于交互
        public void InsertBaseCard(ICardPlayableOnBase card)
        {
            _baseCardSlot.InsertCard(card);
        }
        private void CreateBaseCardUI(ICardPlayableOnBase card)
        {
            if (card is MinionCard)
            {
                _baseMinionCardUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/BaseMinionCardUI"), transform)
                    .GetComponent<BaseMinionCardUI>();
                _baseMinionCardUI.InitBaseMinionCardUI((MinionCard) card);
            }
            else if (card is SpellCard)
            {
                Debug.Log("Spell Card can't be Inserted to the Slot");
            }
            else
            {
                //TODO:这里以后可以加一个空白的卡牌UI，用来占位
                Debug.LogError("ERROR:Unknown card type");
            }
        }
    
        private void TriggerCardInsertAnimation(ICardPlayableOnBase card)
        {
            //TODO:卡牌插入时的动画
        }
    }
}
