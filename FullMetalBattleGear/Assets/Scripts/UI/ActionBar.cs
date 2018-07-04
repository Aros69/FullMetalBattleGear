using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    

    private Vector2 vect;

    //private List<Tiles> playerTiles = new List<Tiles>(playerActions);
    public Tiles[] playerTiles = new Tiles[playerActions];

    private const int playerActions = 5;
    private int playerActionCount = 0;

    

    public void ResetCards()
    {
        for (int i = 0; i < 5; i++)
        {
            playerTiles[i].FadeOut();
        }
        //main.playerTiles.ForEach(n => n.FadeOut());
        playerActionCount = 0;
    }

    public void Reveal(int steps)
    {
        playerTiles[steps].image.sprite = playerTiles[steps].CurrentCard
            .TypeIs(playerTiles[steps].CurrentCard.cardType);
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