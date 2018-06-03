using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour {

    public Sprite littlePunch;
    public Sprite bigPunch;
    public Sprite littleKick;
    public Sprite bigKick;
    public Sprite lazer;
    public Sprite guardPunch;
    public Sprite guardKick;
    public Sprite guardLazer;
    public Sprite nothing;

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
        guardLazer,
        nothing
    }

    public CardType cardType;

    public Text MyText { private set; get; }

    // Use this for initialization
    void Start () {
        MyText = GetComponentInChildren<Text>();
        //TypeIs(cardType);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reveal()
    {
       TypeIs(cardType);
    }

    public Sprite TypeIs(CardType cardType)
    {
        Sprite output = null;

        switch (cardType)
        {
            case CardType.littlePunch:
                output = littlePunch;
                break;
            case CardType.bigPunch:
                output = bigPunch;
                break;
            case CardType.littleKick:
                output = littleKick;
                break;
            case CardType.bigKick:
                output = bigKick;
                break;

            case CardType.lazer:
                output = lazer;
                break;
            case CardType.guardPunch:
                output = guardPunch;
                break;
            case CardType.guardKick:
                output = guardKick;
                break;

            case CardType.guardLazer:
                output = guardLazer;
                break;
            case CardType.nothing:
                output = nothing;
                break;
        }
        return output;
    }
}
