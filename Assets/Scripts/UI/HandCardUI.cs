using System;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    //手牌UI
    public class HandCardUI : MonoBehaviour,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    //IPointerDownHandler
    // IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
    {
        public event Action<PlayableCard> OnCardPlayed; 
        public UnityEvent onCardDragBegin;
        public UnityEvent onCardDragEnd;
        //public bool isDragging = false;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Image _image;

        //private Selectable _selectable;
        private PlayableCard _card;
        private Text leftTopText;
        private Text rightTopText;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
            Text[] text = GetComponentsInChildren<Text>();
            leftTopText = text[0];
            rightTopText = text[1];
        }

        //根据卡牌类型初始化卡牌UI
        public void Init(PlayableCard card)
        {
            _card = card;
            if (card is MinionCard minionCard)
            {
                leftTopText.text = minionCard.ActualBasicAttack.ToString();
                rightTopText.text = minionCard.PlayMoney.ToString();
                minionCard.OnCardStateChange += RefreshCardState;
            }
            else if (card is SpellCard spellCard)
            {
                leftTopText.text = spellCard.CurrentTriggerCount.ToString();
                rightTopText.text = spellCard.PlayMoney.ToString();
                spellCard.OnCardStateChange += RefreshCardState;
            }

            else if (card is SpecialCard specialCard)
            {
                leftTopText.text = specialCard.PlayMoney.ToString();
                rightTopText.text = "";
            }
        }

        private void CardBePlayed()
        {
            OnCardPlayed?.Invoke(_card);
            Destroy(gameObject);
        }
        
        private void RefreshCardState()
        {
            if (_card is MinionCard minionCard)
            {
                leftTopText.text = minionCard.ActualBasicAttack.ToString();
                rightTopText.text = minionCard.PlayMoney.ToString();
            }
            else if (_card is SpellCard spellCard)
            {
                leftTopText.text = spellCard.CurrentTriggerCount.ToString();
                rightTopText.text = spellCard.PlayMoney.ToString();
            }
            else if (_card is SpecialCard specialCard)
            {
                leftTopText.text = specialCard.PlayMoney.ToString();
                rightTopText.text = "";
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 设置卡牌半透明
            _canvasGroup.alpha = 0.6f;
            // 禁用卡牌的射线检测，避免卡牌挡住其他元素
            _canvasGroup.blocksRaycasts = false;
            onCardDragBegin?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;

            //首先判断射线是否检测到了物体
            if (go != null)
            {
                // 如果卡牌是可在基地上打出的卡牌，触发效果
                if (_card is ICardPlayableOnBase cardPlayableOnBase)
                {
                    // 如果鼠标释放时在卡槽上，将卡牌放入卡槽
                    if (go.CompareTag("CardSlotUI"))
                    {
                        BaseCardSlotUI cardSlotUI = go.GetComponent<BaseCardSlotUI>();
                        // 如果卡槽为空，插入卡牌
                        if (cardSlotUI.IsEmpty())
                        {
                            cardSlotUI.InsertBaseCard(cardPlayableOnBase);  
                            CardBePlayed();
                        }
                    }
                }
            }
            
            // 恢复卡牌的透明度
            _canvasGroup.alpha = 1f;
            // 启用卡牌的射线检测
            _canvasGroup.blocksRaycasts = true;
            _rectTransform.anchoredPosition = Vector2.zero; // 设置为本地坐标的 (0, 0)
            onCardDragEnd?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }
    }
}