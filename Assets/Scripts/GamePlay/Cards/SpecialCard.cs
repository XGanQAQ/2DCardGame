using UnityEngine;
namespace GamePlay
{
    public abstract class SpecialCard : PlayableCard
    {
        public SpecialCard(string name, string description, int playMoney)
        {
            CardType = PlayableCardType.Special;
            Name = name;
            Description = description;
            PlayMoney = playMoney;
        }
        public override void Play()
        {
            Debug.Log("Play Special Card: " + Name);
        }
        
        public virtual void SpecialEffect()
        {
            Debug.Log("Special Effect: " + Name);
        }
    }
}