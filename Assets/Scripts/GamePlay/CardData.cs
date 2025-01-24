using GamePlay;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public PlayableCardType CardType;
    public CardRarity Rarity;
    public int Level;
    public int PlayMoney;
    public int Attack;
    public EffectType EffectType;
    public int EffectValue;
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
}
