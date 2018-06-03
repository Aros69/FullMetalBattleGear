using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    static public char attackPlayer1 = ' ';
    static public char attackPlayer2 = ' ';

    private GameObject[] players;
    private PlayerHealth Player1Health;
    private PlayerHealth Player2Health;

    static public void setAttackPlayer1(char newAttack)
    {
        attackPlayer1 = newAttack;
    }

    static public void setAttackPlayer2(char newAttack)
    {
        attackPlayer2 = newAttack;
    }

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Player1Health = players[0].GetComponent<PlayerHealth>();
        Player2Health = players[1].GetComponent<PlayerHealth>();

        // Get the two player health info
    }

    static bool isAttacking(char attack)
    {
        if (attack == FightCharTab.LittlePunch || attack == FightCharTab.BigPunch ||
            attack == FightCharTab.LittleKick || attack == FightCharTab.BigKick || attack == FightCharTab.Head ||
            attack == FightCharTab.Laser)
        {
            return true;
        }

        return false;
    }

    static public void getInfoAttack(ref char attackZone, ref int damageDealed, char attack)
    {
        switch (attack)
        {
            case FightCharTab.LittlePunch:
                attackZone = 'a';
                damageDealed = 5;
                break;
            case FightCharTab.BigPunch:
                attackZone = 'a';
                damageDealed = 8;
                break;
            case FightCharTab.LittleKick:
                attackZone = 'l';
                damageDealed = 7;
                break;
            case FightCharTab.BigKick:
                attackZone = 'l';
                damageDealed = 11;
                break;
            case FightCharTab.Head:
                attackZone = 'h';
                damageDealed = 12;
                break;
            case FightCharTab.Laser:
                attackZone = 'c';
                damageDealed = 10;
                break;
            case FightCharTab.GuardPunch:
                attackZone = 'a';
                damageDealed = 0;
                break;
            case FightCharTab.GuardKick:
                attackZone = 'l';
                damageDealed = 0;
                break;
            case FightCharTab.GuardHead:
                attackZone = 'h';
                damageDealed = 0;
                break;
            case FightCharTab.GuardLaser:
                attackZone = 'c';
                damageDealed = 0;
                break;
            default:
                attackZone = ' ';
                damageDealed = 0;
                break;
        }
    }

    void Update()
    {
        if (attackPlayer1 != ' ' && attackPlayer2 != ' ')
        {
            /* string combat = "On lance le combat : p1 joue " + attackPlayer1;
            combat += " p2 joue " + attackPlayer2;
            Debug.Log(combat); */
            char attackZoneP1 = ' ';
            char attackZoneP2 = ' ';
            int damageP1 = 0;
            int damageP2 = 0;
            getInfoAttack(ref attackZoneP1, ref damageP1, attackPlayer1);
            getInfoAttack(ref attackZoneP2, ref damageP2, attackPlayer2);

            if (isAttacking(attackPlayer1))
            {
                if (isAttacking(attackPlayer2))
                {
                    Player1Health.getHit(attackZoneP2, damageP2);
                    Player2Health.getHit(attackZoneP1, damageP1);
                }
                else if (attackZoneP1 != attackZoneP2)
                {
                    Player2Health.getHit(attackZoneP1, damageP1);
                }
                else
                {
                    float reflect = (float) damageP1;
                    reflect = Mathf.Floor(reflect / 2);
                    Player1Health.getHit(attackZoneP1, (int) reflect + 1);
                }
            }
            else if (isAttacking(attackPlayer2) && attackZoneP1 != attackZoneP2)
            {
                Player2Health.getHit(attackZoneP2, damageP2);
            }
            else
            {
                float reflect = (float) damageP1;
                reflect = Mathf.Floor(reflect / 2);
                Player2Health.getHit(attackZoneP2, (int) reflect + 1);
            }

            attackPlayer1 = ' ';
            attackPlayer2 = ' ';
        }
    }
}