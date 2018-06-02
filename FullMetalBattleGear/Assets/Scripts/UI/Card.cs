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

    public Text MyText { private set; get; }

    // Use this for initialization
    void Start () {
        MyText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TypeIs(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.littlePunch:
                MyText.text = "LP";
                return;
            case CardType.bigPunch:
                MyText.text = "BP";
                return;
            case CardType.littleKick:
                MyText.text = "LK";
                return;
            case CardType.bigKick:
                MyText.text = "BK";
                return;
            case CardType.head:
                MyText.text = "H";
                return;
            case CardType.lazer:
                MyText.text = "L";
                return;
            case CardType.guardPunch:
                MyText.text = "GP";
                return;
            case CardType.guardKick:
                MyText.text = "GK";
                return;
            case CardType.guardHead:
                MyText.text = "GH";
                return;
            case CardType.guardLazer:
                MyText.text = "GL";
                return;
        }
    }
}
