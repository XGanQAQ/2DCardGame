using UnityEngine;
using UnityEngine.UI;

public class MyPoolUI : MonoBehaviour
{
    private Text _text;
    public HandCardsManager HandCardsManager;

    private CardsPool _cardsPool;
    void Start()
    {
        _text = transform.GetChild(0).GetComponent<Text>();
        _cardsPool = HandCardsManager.DrawnCardsPool;
    }
    void Update()
    {
        _text.text =_cardsPool.Cards.Count.ToString();
    }
}
