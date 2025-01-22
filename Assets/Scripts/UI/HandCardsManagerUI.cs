using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HandCardsManagerUI : MonoBehaviour
    {
        private HorizontalLayoutGroup _layoutGroup;
        private HandCardsManager _handCardsManager;
        private HandCards _handCards;

        private void Awake()
        {
            
        }

        void Start()
        {
            _handCardsManager = GameObject.FindWithTag("HandCardsManager").GetComponent<HandCardsManager>();
            _handCards = _handCardsManager.MyHandCards;
            _layoutGroup = GetComponent<HorizontalLayoutGroup>();
            _handCardsManager.MyHandCards.OnCardDrew += Refresh;
            _handCardsManager.MyHandCards.OnCardPlayed += Refresh;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
            }
        }

        //刷新手牌显示ui
        public void Refresh()
        {
            //Debug.Log("INFO:Refresh HandCardsManagerUI");
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (PlayableCard card in _handCards.GetCardsList())
            {
                CreateCardUI(card);
            }
        }
        private void CreateCardUI(PlayableCard card)
        {
            var cardUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/HandCardUI"), transform).GetComponentInChildren<HandCardUI>();
            cardUI.Init(card);
            cardUI.OnCardPlayed += _handCards.PlayHandCard;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ReGroup()
        {
            //_layoutGroup.CalculateLayoutInputHorizontal();
            _layoutGroup.SetLayoutHorizontal();
        }
    }
}