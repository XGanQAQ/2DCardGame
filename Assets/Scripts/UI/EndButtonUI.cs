using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndButtonUI : MonoBehaviour
{
    public Button _button;
    void Start()
    {
        if ((_button = GetComponent<Button>()) != null)
        {
            _button.onClick.AddListener(PlayerEndThisTurn); 
        }
    }
    
    private void PlayerEndThisTurn()
    {
        GameManager.Instance.EndTurn();
        
        GameManager.Instance.StartTurn();
    }
}
