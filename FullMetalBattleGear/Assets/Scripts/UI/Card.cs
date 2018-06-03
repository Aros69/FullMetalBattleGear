using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour {

    public enum CardType
    {
        littlePunch,
        bigPunch,
        littleKick,
        bigKick,
        head,
        lazer,
        guardPunch,
        guardKick,
        guardHead,
        guardLazer
    }

    public CardType cardType;

    public Text MyText { private set; get; }

    // Use this for initialization
    void Start () {
        MyText = GetComponentInChildren<Text>();
        TypeIs(cardType);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reveal()
    {
        MyText.text = TypeIs(cardType);
    }

    string TypeIs(CardType cardType)
    {
        string output = "";

        switch (cardType)
        {
            case CardType.littlePunch:
                output = "LP";
                break;
            case CardType.bigPunch:
                output = "BP";
                break;
            case CardType.littleKick:
                output = "LK";
                break;
            case CardType.bigKick:
                output = "BK";
                break;
            case CardType.head:
                output = "H";
                break;
            case CardType.lazer:
                output = "L";
                break;
            case CardType.guardPunch:
                output = "GP";
                break;
            case CardType.guardKick:
                output = "GK";
                break;
            case CardType.guardHead:
                output = "GH";
                break;
            case CardType.guardLazer:
                output = "GL";
                break;
        }
        return output;
    }
}
