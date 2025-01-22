using GamePlay;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public int Id;
    public string Name;
    public PlayableCardType CardType;
    public CardRarity Rarity;
    public int PlayMoney;
    public int Attack;
    public IEffect Effect;
    public string Description;
}

public enum CardRarity
{
    Common,       // 普通
    Rare,         // 稀有
    Epic,         // 史诗
    Legendary     // 传奇
}

public enum PlayableCardType
{
    Spell,
    Minion,
    Special
}
