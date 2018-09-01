using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    // The current attack of player 1 done (space if none)
    private char attackPlayer1 = ' ';


    // The current attack of player 2 done (space if none)
    private char attackPlayer2 = ' ';

    // Reference to it self ?!
    public static FightManager main;

    // Reference to Player 1
    private GameObject player1;

    private GameObject player2;

    // Reference to player 1 attack script
    private PlayerAttack player1Attack;

    // Reference to player 2 attack script
    private PlayerAttack player2Attack;

    // Use this for initialization
    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0].name == "Player1")
        {
            player1 = players[0];
            player2 = players[1];
        }
        else
        {
            player1 = players[1];
            player2 = players[0];
        }
        player1Attack = player1.GetComponent<PlayerAttack>();
        player2Attack = player2.GetComponent<PlayerAttack>();

        //TODO clean this shit
        main = this;
    }

    // Return the longest animation of the two player
    public float DureeAnimation()
    {
        float a = 0;
        RuntimeAnimatorController rac = player1.GetComponent<Animator>().runtimeAnimatorController;
        foreach (AnimationClip ac in rac.animationClips)
        {
            if (ac.length > a)
                a = ac.length;
        }

        rac = player2.GetComponent<Animator>().runtimeAnimatorController;
        foreach (AnimationClip ac in rac.animationClips)
        {
            if (ac.length > a)
                a = ac.length;
        }

        return a;
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

    // Function that manage each damage of each attack and zone
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

    // Manager of anim trigger
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
        if (!player1Attack.getIsSelectingAttack() && !player2Attack.getIsSelectingAttack() &&
            !player1Attack.getIsAttacking() && !player2Attack.getIsAttacking())
        {
            if (player1Attack.getRealAttackCount() == player1Attack.baseAttackCount)
            {
                player1Attack.newComboAttack();
                player2Attack.newComboAttack();
            }
            else
            {
                attackPlayer1 = player1Attack.getAttackList(player1Attack.getRealAttackCount());
                attackPlayer2 = player2Attack.getAttackList(player2Attack.getRealAttackCount());
                char attackZoneP1 = ' ';
                char attackZoneP2 = ' ';
                int damageP1 = 0;
                int damageP2 = 0;
                getInfoAttack(ref attackZoneP1, ref damageP1, attackPlayer1);
                getInfoAttack(ref attackZoneP2, ref damageP2, attackPlayer2);
                if (!player1Attack.canDoThatAttack(attackZoneP1))
                {
                    attackPlayer1 = 'n';
                    // TODO changer carte pour non attack
                }

                if (!player2Attack.canDoThatAttack(attackZoneP2))
                {
                    attackPlayer2 = 'n';
                    // TODO changer carte pour non attack
                }

                AnimationController.setAnimTrigger(player1, attackPlayer1, attackPlayer2);
                AnimationController.setAnimTrigger(player2, attackPlayer2, attackPlayer1);
                /*setAnimTrigger(player1.GetComponent<Animator>(), attackPlayer1, attackPlayer2);
                setAnimTrigger(player2.GetComponent<Animator>(), attackPlayer2, attackPlayer1);*/
                player1Attack.actionBar.Reveal(player1Attack.getRealAttackCount());
                player2Attack.actionBar.Reveal(player2Attack.getRealAttackCount());
                player1Attack.setIsAttacking(true);
                player2Attack.setIsAttacking(true);
                if (isAttacking(attackPlayer1))
                {
                    if (isAttacking(attackPlayer2))
                    {
                        player1.GetComponent<PlayerHealth>().getHit(attackZoneP2, damageP2);
                        player2.GetComponent<PlayerHealth>().getHit(attackZoneP1, damageP1);
                    }
                    else if (attackZoneP1 != attackZoneP2)
                    {
                        player2.GetComponent<PlayerHealth>().getHit(attackZoneP1, damageP1);
                    }
                    else
                    {
                        float reflect = (float) damageP1;
                        reflect = Mathf.Floor(reflect / 2);
                        player1.GetComponent<PlayerHealth>().getHit(attackZoneP1, (int) reflect + 1);
                    }
                }
                else if (isAttacking(attackPlayer2) && attackZoneP1 != attackZoneP2)
                {
                    player2.GetComponent<PlayerHealth>().getHit(attackZoneP2, damageP2);
                }
                else
                {
                    float reflect = (float) damageP1;
                    reflect = Mathf.Floor(reflect / 2);
                    player2.GetComponent<PlayerHealth>().getHit(attackZoneP2, (int) reflect + 1);
                }

                attackPlayer1 = ' ';
                attackPlayer2 = ' ';
                player1Attack.setRealAttackCount(player1Attack.getRealAttackCount() + 1);
                player2Attack.setRealAttackCount(player2Attack.getRealAttackCount() + 1);
            }
        }
    }
}
