using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MyMoneyUI : MonoBehaviour
    {
        private Text _moneyText;

        private void Start()
        {
            _moneyText = GetComponentInChildren<Text>();
        }
        
        private void Update()
        {
            _moneyText.text =GameManager.Instance.PlayerData.myMoney.ToString();
        }
    }
}