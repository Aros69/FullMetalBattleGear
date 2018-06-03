using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    public Card card;

    public Card CurrentCard { set; get; }

    public RectTransform Tr { private set; get; }

	void Update () {

        Tr = GetComponent<RectTransform>();
    }


    public void AddCard(Card.CardType cardType)
    {
        CurrentCard = Instantiate(card, Tr.position, Quaternion.identity, transform);
        CurrentCard.cardType = cardType;
        RectTransform cardTr = GetComponent<RectTransform>();
        cardTr.anchoredPosition = Tr.anchoredPosition;
    }

}
