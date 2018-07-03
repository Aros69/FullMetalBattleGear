using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public static ActionBar main;

    private Vector2 vect;

    private List<Tiles> playerTiles = new List<Tiles>();

    private int actions = 5;
    private int playerActions = 5;
    private int playerActionCount;

    private void Start()
    {
        main = this;
    }

    public static void ResetCards()
    {
        main.playerTiles.ForEach(n => n.FadeOut());
        main.playerActionCount = 0;
    }

    public static void Reveal(int steps)
    {
        main.playerTiles[steps - 1].image.sprite = main.playerTiles[steps - 1].CurrentCard
            .TypeIs(main.playerTiles[steps - 1].CurrentCard.cardType);
    }

    public void PlayerActionAdd(Card.CardType cardType)
    {
        if (playerActionCount < playerActions)
        {
            playerTiles[playerActionCount].AddCard(cardType);
            playerActionCount++;
        }
    }
}