using Codice.Client.BaseCommands;
using UnityEngine;
using System;

namespace GamePlay
{
    //所有可以打在基地上的卡牌都需要实现这个接口
    public interface ICardPlayableOnBase
    {
        public event Action OnBaseCardEffect; //卡牌效果触发时的回调
        public event Action OnCardStateChange; //卡牌状态改变时的回调
        public PlayableCardType CardType { get; set; } //卡牌类型，用于区分是法术卡还是随从卡还是特殊卡 
         /// 当卡牌打在基地上时的效果
        public void PlayOnBase(Base targetBase,PlayerData playerData);
    }
}

