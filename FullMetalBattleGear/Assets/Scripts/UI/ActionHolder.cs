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

    public static void Reveal(int steps)
    {
        main.player1Tiles[steps - 1].CurrentCard.Reveal();
        main.player2Tiles[steps - 1].CurrentCard.Reveal();
    }

    public void PlayerOneActionAdd(Card.CardType cardType)
    {
        if (player1ActionCount < playerActions)
        {
            player1Tiles[player1ActionCount].AddCard(cardType);
            player1ActionCount++;
        }
    }

    public void PlayerTwoActionAdd(Card.CardType cardType)
    {
        if (player2ActionCount < playerActions)
        {
            player2Tiles[player2ActionCount].AddCard(cardType);
            player2ActionCount++;
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;

    //    Vector2 startPos = left;
    //    startPos.y += 1f;
    //    Vector2 endPos = left;
    //    endPos.y += 1f;

    //    Gizmos.DrawLine(startPos, endPos);

    //    startPos = left;
    //    startPos.x += 1f;
    //    endPos = left;
    //    endPos.x += 1f;

    //    Gizmos.DrawLine(startPos, endPos);


    //    startPos = right;
    //    startPos.y += 1f;
    //    endPos = right;
    //    endPos.y += 1f;

    //    Gizmos.DrawLine(startPos, endPos);

    //    startPos = left;
    //    startPos.x += 1f;
    //    endPos = left;
    //    endPos.x += 1f;

    //    Gizmos.DrawLine(startPos, endPos);
    //}
}
