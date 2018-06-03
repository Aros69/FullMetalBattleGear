using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour {

    public float cardDisapeareanceDuration = 1f;

    public Card card;
    public Card CurrentCard { set; get; }
    public RectTransform Tr { private set; get; }

    Image cardImage;

    private void Start()
    {
        Tr = GetComponent<RectTransform>();

        CurrentCard = Instantiate(card, Tr.position, Quaternion.identity, transform);
        CurrentCard.cardType = Card.CardType.nothing;
        RectTransform cardTr = GetComponent<RectTransform>();
        cardTr.anchoredPosition = Tr.anchoredPosition;


        cardImage = CurrentCard.GetComponent<Image>();
        cardImage.color = new Color(1, 1, 1, 0);
    }

    void Update () {

        Tr = GetComponent<RectTransform>();
    }


    public void AddCard(Card.CardType cardType)
    {
        CurrentCard.cardType = cardType;
        cardImage.color = new Color(1, 1, 1, 1);
    }

    public void FadeOut()
    {

        CurrentCard.MyText.text = "";
        StartCoroutine(FadeOut(cardDisapeareanceDuration));

    }

    IEnumerator FadeOut(float duration)
    {
      
        float t = 1 - cardImage.color.a;
        float startTime = Time.time;
        float startOffset = duration * t;
        float timePassed = 0;

        while (startTime + duration > Time.time + startOffset)
        {
            timePassed += Time.deltaTime;
            t = 1 - ((Time.time + startOffset) - startTime) / duration;

            cardImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
        cardImage.color = new Color(1, 1, 1, 0);

    }

}
