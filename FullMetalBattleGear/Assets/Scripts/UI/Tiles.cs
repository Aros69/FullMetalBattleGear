using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    public Card card;
    
	void Start () {
		
	}
	
	void Update () {
		
        
	}


    public void AddCard(Card.CardType cardType)
    {
        Card tempCard = Instantiate(card, transform);
        tempCard.TypeIs(cardType);
    }

}
