using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BaseSpellCardUI : MonoBehaviour
    {
        private SpellCard _spellCard;
        
        private Text _timesText;

        public void InitBaseSpellCardUI(SpellCard spellCard)
        {
            _spellCard = spellCard;
            _timesText = transform.GetChild(0).GetComponent<Text>();
            _timesText.text = _spellCard.CurrentTriggerCount.ToString();
            _spellCard.OnCardStateChange += UpdateCardUI;
        }
        
        //更新卡牌UI状态
        public void UpdateCardUI()
        {
            _timesText.text = _spellCard.CurrentTriggerCount.ToString();
        }
    }
}