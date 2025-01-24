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
        private Text _leftTopText;
        private Text _rightTopText;
        private Text _desciptionText;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
            Text[] text = GetComponentsInChildren<Text>();
            _leftTopText = text[0];
            _rightTopText = text[1];
            _desciptionText = text[2];
        }

        //根据卡牌类型初始化卡牌UI
        public void Init(PlayableCard card)
        {
            _card = card;
            RefreshCardState();
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
                _leftTopText.text = minionCard.ActualBasicAttack.ToString();
                _rightTopText.text = minionCard.PlayMoney.ToString();
                _desciptionText.text = minionCard.Description;
            }
            else if (_card is SpellCard spellCard)
            {
                _leftTopText.text = " ";
                _rightTopText.text = spellCard.PlayMoney.ToString();
                _desciptionText.text = spellCard.Description;
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
                if (go.CompareTag("CardSlotUI"))
                {
                    BaseCardSlotUI cardSlotUI = go.GetComponent<BaseCardSlotUI>();
                    if (_card is MinionCard minionCard)
                    {
                        // 如果卡槽为空，插入卡牌
                        if (cardSlotUI.IsEmpty())
                        {
                            minionCard.PlayOnBase(cardSlotUI.GetDependentBase(), GameManager.Instance.PlayerData);
                            cardSlotUI.InsertBaseCard(minionCard);
                            CardBePlayed();
                        }
                    }
                    else if (_card is SpellCard spellCard)
                    {
                        spellCard.PlayOnBase(cardSlotUI.GetDependentBase(), GameManager.Instance.PlayerData);
                        CardBePlayed();
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