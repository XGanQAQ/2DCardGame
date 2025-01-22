using System;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BaseMinionCardUI : MonoBehaviour
    {
        private MinionCard _minionCard;
        
        private Text _attackText;

        public void InitBaseMinionCardUI(MinionCard minionCard)
        {
            _minionCard = minionCard;
            _attackText = transform.GetChild(0).GetComponent<Text>();
            _attackText.text = _minionCard.ActualBasicAttack.ToString();
            _minionCard.OnCardStateChange += UpdateCardUI;
        }
        
        //更新卡牌UI状态
        public void UpdateCardUI()
        {
            _attackText.text = _minionCard.ActualBasicAttack.ToString();
        }
    }
}