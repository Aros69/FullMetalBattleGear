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
    private Animator player1Animator;
    private Animator player2Animator;

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
        if (players[0].name == "Player1")
        {
            Player1Health = players[0].GetComponent<PlayerHealth>();
            Player2Health = players[1].GetComponent<PlayerHealth>();
            player1Animator = players[0].GetComponent<Animator>();
            player2Animator = players[1].GetComponent<Animator>();
        }
        else
        {
            Player1Health = players[1].GetComponent<PlayerHealth>();
            Player2Health = players[0].GetComponent<PlayerHealth>();
            player1Animator = players[1].GetComponent<Animator>();
            player2Animator = players[0].GetComponent<Animator>();
        }
        

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

    void setAnimTrigger(Animator playerAnimator, char attackPlayer, char attackEnnemy)
    {
        switch (attackPlayer)
        {
            case FightCharTab.LittlePunch:
                playerAnimator.SetTrigger("LittlePunch");
                break;
            case FightCharTab.BigPunch:
                playerAnimator.SetTrigger("BigPunch");
                break;
            case FightCharTab.LittleKick:
                playerAnimator.SetTrigger("LittleKick");
                break;
            case FightCharTab.BigKick:
                playerAnimator.SetTrigger("BigKick");
                break;
            case FightCharTab.Head:
                playerAnimator.SetTrigger("Head");
                break;
            case FightCharTab.Laser:
                if (attackEnnemy != FightCharTab.GuardLaser)
                {
                    playerAnimator.SetTrigger("Laser");
                }
                else
                {
                    playerAnimator.SetTrigger("FailLaser");
                }

                break;
            case FightCharTab.GuardPunch:
                playerAnimator.SetTrigger("GuardPunch");
                break;
            case FightCharTab.GuardKick:
                playerAnimator.SetTrigger("GuardKick");
                break;
            case FightCharTab.GuardHead:
                playerAnimator.SetTrigger("GuardHead");
                break;
            case FightCharTab.GuardLaser:
                playerAnimator.SetTrigger("GuardLaser");
                break;
            default:
                playerAnimator.SetTrigger("Fail");
                break;
        }
    }

    void Update()
    {
        if (attackPlayer1 != ' ' && attackPlayer2 != ' ')
        {
            setAnimTrigger(player1Animator, attackPlayer1, attackPlayer2);
            setAnimTrigger(player2Animator, attackPlayer2, attackPlayer1);
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