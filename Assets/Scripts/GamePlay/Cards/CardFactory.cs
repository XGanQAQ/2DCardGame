using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class CardFactory
    {
        private List<CardData> _cardDataList;
        private string _cardDataPath; //"Assets/Resources/CardConfig"
        
        public CardFactory(string cardDataPath)
        {
            _cardDataList = new List<CardData>();
            _cardDataPath = cardDataPath;
            //LoadCardData();
        }

        //根据卡牌编号创建卡牌
        public PlayableCard CreateCardByNumber(int cardNumber)
        {
            CardData cardData = _cardDataList.Find(card => card.Id == cardNumber);
            if (cardData == null)
            {
                Debug.LogError("CardData not found! CardNumber: " + cardNumber);
                return null;
            }
            return CreateCard(cardData);
        }
        
        //根据卡牌名称创建卡牌
        public PlayableCard CreateCardByName(string cardName)
        {
            CardData cardData = _cardDataList.Find(card => card.Name == cardName);
            if (cardData == null)
            {
                Debug.LogError("CardData not found! CardName: " + cardName);
                return null;
            }
            return CreateCard(cardData);
        }
        
        public PlayableCard RandomCreateCard()
        {
            int randomIndex = Random.Range(0, _cardDataList.Count);
            return CreateCard(_cardDataList[randomIndex]);
        }
        
        //从文件中加载卡牌数据
        public void LoadCardData()
        {
            var cardDataArray = Resources.LoadAll<CardData>(_cardDataPath);
            
            // 从Resources文件夹中加载所有的ConfigData配置文件
            _cardDataList.AddRange(cardDataArray);
        
            // 打印加载的配置文件数
            Debug.Log("INFO:Loaded " + _cardDataList.Count + " config files.");
        }
        
        //根据卡牌数据创建卡牌
        private PlayableCard CreateCard(CardData cardData)
        {
            PlayableCard card = null;
            switch (cardData.CardType)
            {
                case PlayableCardType.Minion:
                    card = new MinionCard();
                    break;
                case PlayableCardType.Spell:
                    card = new SpellCard();
                    break;
            }
            card.Id = cardData.Id;
            card.Name = cardData.Name;
            card.Description = cardData.Description;
            card.CardType = cardData.CardType;
            card.Rarity = cardData.Rarity;
            card.Level = cardData.Level;
            card.PlayMoney = cardData.PlayMoney;
            card.Attack = cardData.Attack;
            card.Effect = CreateEffect(cardData.EffectType, cardData.EffectValue);
            return card;
        }
        
        private IEffect CreateEffect(EffectType effectType, int effectValue)
        {
            IEffect effect = null;
            switch (effectType)
            {
                case EffectType.Attack:
                    effect = new AttackEffect();
                    ((AttackEffect) effect).AttackValue = effectValue;
                    break;
                case EffectType.GetMoney:
                    effect = new GetMoneyEffect();
                    ((GetMoneyEffect) effect).MoneyValue = effectValue;
                    break;
                case EffectType.DrewCard:
                    effect = new DrewCardEffect();
                    ((DrewCardEffect) effect).DrewCardCount = effectValue;
                    break;
            }
            return effect;
        }
        
    }
}