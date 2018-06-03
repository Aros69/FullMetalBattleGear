using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHolder : MonoBehaviour {

    public static ActionHolder main;

    public Vector2 left;
    public Vector2 right;

    public List<Tiles> player1Tiles = new List<Tiles>();
    public List<Tiles> player2Tiles = new List<Tiles>();

    public int actions = 5;
    public int playerActions = 5;
    private int player1ActionCount;
    private int player2ActionCount;


    private void Start()
    {
        main = this;
    }

    public static void ResetCards()
    {
        main.player1Tiles.ForEach(n => n.FadeOut());
        main.player2Tiles.ForEach(n => n.FadeOut());

        main.player1ActionCount = 0;
        main.player2ActionCount = 0;
    }

    public static void Reveal(int steps)
    {
        main.player1Tiles[steps - 1].image.sprite = main.player1Tiles[steps - 1].CurrentCard.TypeIs(main.player1Tiles[steps - 1].CurrentCard.cardType);
        main.player2Tiles[steps - 1].image.sprite = main.player2Tiles[steps - 1].CurrentCard.TypeIs(main.player2Tiles[steps - 1].CurrentCard.cardType);
    }

    public void PlayerActionAdd(Card.CardType cardType, int playerId)
    {
        Debug.Log(playerId);
        if (playerId == 0) { 
            if (player1ActionCount < playerActions)
            {
                player1Tiles[player1ActionCount].AddCard(cardType);
                player1ActionCount++;
            }
        }
        else
            {
                if (player2ActionCount < playerActions)
                {
                    player2Tiles[player2ActionCount].AddCard(cardType);
                    player2ActionCount++;
                }
            }
    }
}
